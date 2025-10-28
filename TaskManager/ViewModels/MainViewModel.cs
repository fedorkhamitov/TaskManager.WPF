using DevExpress.Data.NetCompatibility.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Commands;
using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// ViewModel для главного окна приложения
    /// Содержит логику работы со списком задач
    /// </summary>
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TaskItem> _tasks;
        private ObservableCollection<TaskItem> _filteredTasks;
        private TaskItem _selectedTask;
        private string _searchText;
        private TaskStatus? _filterStatus;
        private int _nextId = 1;

        /// <summary>
        /// Полный список всех задач
        /// </summary>
        public ObservableCollection<TaskItem> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        /// <summary>
        /// Отфильтрованный список задач для отображения
        /// </summary>
        public ObservableCollection<TaskItem> FilteredTasks
        {
            get => _filteredTasks;
            set => SetProperty(ref _filteredTasks, value);
        }

        /// <summary>
        /// Выбранная в таблице задача
        /// </summary>
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set => SetProperty(ref _selectedTask, value);
        }

        /// <summary>
        /// Текст для поиска задач
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    ApplyFilter();
                }
            }
        }

        /// <summary>
        /// Фильтр по статусу задачи
        /// </summary>
        public TaskStatus? FilterStatus
        {
            get => _filterStatus;
            set
            {
                if (SetProperty(ref _filterStatus, value))
                {
                    ApplyFilter(); // Применяем фильтр при изменении статуса
                }
            }
        }

        // Команды для кнопок
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ToggleStatusCommand { get; }
        public ICommand ShowAllCommand { get; }
        public ICommand ShowActiveCommand { get; }
        public ICommand ShowCompletedCommand { get; }


        /// <summary>
        /// Конструктор ViewModel
        /// Инициализирует коллекции и команды
        /// </summary>
        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            FilteredTasks = new ObservableCollection<TaskItem>();

            // Инициализация команд
            AddTaskCommand = new RelayCommand(AddTask, null);
            EditTaskCommand = new RelayCommand(EditTask, CanEditOrDelete);
            DeleteTaskCommand = new RelayCommand(DeleteTask, CanEditOrDelete);
            ToggleStatusCommand = new RelayCommand(ToggleStatus, CanEditOrDelete);
            ShowAllCommand = new RelayCommand(_ => { FilterStatus = null; }, null);
            ShowActiveCommand = new RelayCommand(_ => { FilterStatus = TaskStatus.Active; }, null);
            ShowCompletedCommand = new RelayCommand(_ => { FilterStatus = TaskStatus.Completed; }, null);

            // Добавляем тестовые данные
            LoadSampleData();
        }

        /// <summary>
        /// Добавляет тестовые задачи для демонстрации
        /// </summary>
        private void LoadSampleData()
        {
            AddTaskInternal("Изучить WPF", "Основы WPF и XAML", TaskPriority.High, TaskStatus.Active);
            AddTaskInternal("Разобраться с DevExpress", "Установить и попробовать компоненты", TaskPriority.High, TaskStatus.Active);
            AddTaskInternal("Создать тестовый проект", "Простое CRUD-приложение", TaskPriority.Medium, TaskStatus.Active);
            AddTaskInternal("Подготовиться к собеседованию", "Повторить SOLID, DRY, KISS", TaskPriority.High, TaskStatus.Completed);
        }

        /// <summary>
        /// Внутренний метод для добавления задачи
        /// </summary>
        private void AddTaskInternal(string title, string description, TaskPriority priority, TaskStatus status)
        {
            var task = new TaskItem
            {
                Id = _nextId++,
                Title = title,
                Description = description,
                Priority = priority,
                Status = status,
                CreatedDate = DateTime.Now
            };

            Tasks.Add(task);
            ApplyFilter();
        }

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        private void AddTask(object parameter)
        {
            // Простой диалог для ввода (в реальном проекте — отдельное окно)
            var title = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите название задачи:",
                "Новая задача",
                "Новая задача");

            if (string.IsNullOrWhiteSpace(title))
                return;

            var description = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите описание задачи:",
                "Описание",
                "");

            AddTaskInternal(title, description, TaskPriority.Medium, TaskStatus.Active);
        }

        /// <summary>
        /// Редактирование выбранной задачи
        /// </summary>
        private void EditTask(object parameter)
        {
            if (SelectedTask == null)
                return;

            var newTitle = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите новое название:",
                "Редактирование",
                SelectedTask.Title);

            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                SelectedTask.Title = newTitle;
            }
        }

        /// <summary>
        /// Удаление выбранной задачи
        /// </summary>
        private void DeleteTask(object parameter)
        {
            if (SelectedTask == null)
                return;

            var result = MessageBox.Show(
                $"Удалить задачу '{SelectedTask.Title}'?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Tasks.Remove(SelectedTask);
                ApplyFilter();
            }
        }

        /// <summary>
        /// Переключение статуса задачи (Активна ↔ Завершена)
        /// </summary>
        private void ToggleStatus(object parameter)
        {
            if (SelectedTask == null)
                return;

            SelectedTask.Status = SelectedTask.Status == TaskStatus.Active
                ? TaskStatus.Completed
                : TaskStatus.Active;

            ApplyFilter();
        }

        /// <summary>
        /// Проверка, можно ли редактировать/удалять (есть ли выбранная задача)
        /// </summary>
        private bool CanEditOrDelete(object parameter)
        {
            return SelectedTask != null;
        }

        /// <summary>
        /// Применение фильтров к списку задач
        /// </summary>
        private void ApplyFilter()
        {
            FilteredTasks.Clear();

            var filtered = Tasks.AsEnumerable();

            // Фильтр по статусу
            if (FilterStatus.HasValue)
            {
                filtered = filtered.Where(t => t.Status == FilterStatus.Value);
            }

            // Фильтр по тексту поиска
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(t =>
                    t.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            foreach (var task in filtered)
            {
                FilteredTasks.Add(task);
            }
        }
    }
}
