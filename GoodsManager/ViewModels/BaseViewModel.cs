using CommunityToolkit.Mvvm.ComponentModel;

namespace GoodsManager.ViewModels
{
    /// <summary>
    /// Base ViewModel providing common properties for busy state tracking.
    /// Follows the instructor's architecture from LecturerManager.
    /// </summary>
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy = false;

        public bool IsNotBusy => !IsBusy;
    }
}
