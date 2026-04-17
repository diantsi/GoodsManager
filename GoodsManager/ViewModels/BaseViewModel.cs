using CommunityToolkit.Mvvm.ComponentModel;

namespace GoodsManager.ViewModels
{
    /// <summary>
    /// Base ViewModel providing common properties for busy state tracking.
    /// </summary>
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy = false;

        public bool IsNotBusy => !IsBusy;
    }
}
