using Tempus.Helpers;

namespace Tempus.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        bool _isBusy;

        /// <summary>
        ///     Private backing field to hold the title
        /// </summary>
        string _title = string.Empty;

        /// <summary>
        ///     Get the azure service instance
        /// </summary>
     
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        /// <summary>
        ///     Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}