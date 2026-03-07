using GoodsManager.UIModels;

namespace GoodsManager.Pages;

// Receives the GoodUIModel via Shell navigation parameters.
[QueryProperty(nameof(CurrentGood), nameof(CurrentGood))]
public partial class GoodDetailsPage : ContentPage
{
    private GoodUIModel _currentGood;

    public GoodUIModel CurrentGood
    {
        get => _currentGood;
        set
        {
            _currentGood = value;
            // allows XAML to access all properties of the selected product.
            BindingContext = CurrentGood;
        }
    }

    public GoodDetailsPage()
    {
        InitializeComponent();
    }
}