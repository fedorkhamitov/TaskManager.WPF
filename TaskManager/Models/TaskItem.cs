using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.Models
{
    /// <summary>
    /// Модель данных для задачи
    /// Реализует INotifyPropertyChanged для привязки данных в WPF
    /// </summary>
    internal class TaskItem : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private string _description;
        private TaskStatus _status;
        private TaskPriority _priority;
        private DateTime _createdDate;

        /// <summary>
        /// Уникальный идентификатор задачи
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Название задачи
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Статус задачи (Активна, Завершена)
        /// </summary>
        public TaskStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Приоритет задачи (Низкий, Средний, Высокий)
        /// </summary>
        public TaskPriority Priority
        {
            get => _priority;
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public DateTime CreatedDate
        {
            get => _createdDate;
            set
            {
                if (_createdDate != value)
                {
                    _createdDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Событие для уведомления об изменении свойства
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Статусы задачи
    /// </summary>
    public enum TaskStatus
    {
        Active = 0,
        Completed = 1
    }

    /// <summary>
    /// Приоритеты задачи
    /// </summary>
    public enum TaskPriority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
}
