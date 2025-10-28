using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// Базовый класс для всех ViewModel
    /// Реализует INotifyPropertyChanged для автоматического обновления UI
    /// </summary>
    internal class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие для уведомления об изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод для вызова события PropertyChanged
        /// Автоматически определяет имя свойства благодаря CallerMemberName
        /// </summary>
        /// <param name="propertyName">Имя изменённого свойства</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Устанавливает новое значение свойства и вызывает OnPropertyChanged, если значение изменилось
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="field">Поле для изменения (ref)</param>
        /// <param name="value">Новое значение</param>
        /// <param name="propertyName">Имя свойства (автоматически)</param>
        /// <returns>true, если значение изменилось</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
