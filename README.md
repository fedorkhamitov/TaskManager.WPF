# Task Manager WPF

Простое CRUD-приложение на **Windows Presentation Foundation (WPF)** с использованием **MVVM-архитектуры**.

## 🎯 Особенности

- **MVVM Pattern** — чистая архитектура с разделением Model, View, ViewModel
- **Data Binding** — двусторонняя привязка данных между UI и ViewModel
- **INotifyPropertyChanged** — автоматическое обновление UI при изменении данных
- **ObservableCollection** — реактивная коллекция для списка задач
- **CRUD операции** — добавление, редактирование, удаление, переключение статуса
- **Фильтрация и поиск** — фильтрация по статусу, поиск по названию и описанию
- **Command Pattern** — RelayCommand для привязки действий кнопок

## 📋 Функциональность

- ✅ Добавление новых задач
- ✅ Редактирование названия задачи
- ✅ Удаление задач с подтверждением
- ✅ Переключение статуса (Активна ↔ Завершена)
- ✅ Фильтрация по статусу (Все / Активные / Завершённые)
- ✅ Поиск по названию и описанию задачи в реальном времени
- ✅ Отображение информации о приоритете и дате создания

## 🛠️ Технологии

- **Framework**: .NET Framework 4.7.2
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Architecture**: MVVM (Model-View-ViewModel)
- **Language**: C#

## 📁 Структура проекта

TaskManager/
├── Models/
│ └── TaskItem.cs # Модель данных для задачи
├── ViewModels/
│ ├── BaseViewModel.cs # Базовый класс ViewModel
│ └── MainViewModel.cs # ViewModel главного окна
├── Views/
│ └── (будущие дополнительные окна)
├── Commands/
│ └── RelayCommand.cs # Реализация ICommand
├── Services/
│ └── (будущие сервисы)
├── MainWindow.xaml # UI главного окна
├── MainWindow.xaml.cs # Code-behind
└── App.xaml # Конфигурация приложения

## 🚀 Как запустить

1. Клонируй репозиторий:
git clone https://github.com/ТВ_ЮЗЕРНЕЙМ/TaskManager.WPF.git
cd TaskManager

2. Откройте проект в Visual Studio 2022 (Community Edition подходит)

3. Восстанови NuGet пакеты (должно произойти автоматически)

4. Нажми **F5** для запуска приложения

## 📚 Изученные концепции

В этом проекте продемонстрированы ключевые концепции WPF и MVVM:

- **MVVM Pattern** — разделение ответственности между моделью, представлением и логикой
- **Data Binding** — связь между UI элементами и свойствами ViewModel
- **INotifyPropertyChanged** — уведомление об изменении свойств
- **ObservableCollection** — реактивная коллекция с уведомлениями
- **Command Pattern** — обработка действий пользователя
- **Dependency Injection** — (базовое) внедрение зависимостей через конструктор

## 💡 Возможные улучшения

- [ ] Интеграция с базой данных (SQL Server, SQLite)
- [ ] Сохранение состояния приложения между запусками
- [ ] Импорт/экспорт задач в CSV/JSON
- [ ] Приоритет задач с цветовой кодировкой
- [ ] Поддержка категорий/тегов
- [ ] Синхронизация между несколькими устройствами
- [ ] Unit-тесты для ViewModel

## 👨‍💻 Автор

Фёдор Хамитов / Fedor Khamitov

- GitHub: [@fedorkhamitov](https://github.com/fedorkhamitov)
- Email: fedor.khamitov@gmail.com
