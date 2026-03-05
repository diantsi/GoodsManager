using GoodsManager.Common;
using GoodsManager.Common.Enums;
using GoodsManager.UIModels;

namespace GoodsManager.Pages;

public partial class GoodCreatePage : ContentPage
{
    public GoodCreatePage()
    {
        InitializeComponent();
        var categories = EnumExtensions.GetValueWithNames<Category>();
        pCategory.ItemsSource = categories;
        pCategory.ItemDisplayBinding = new Binding("DisplayName");
    }

    private async void CreateClicked(object sender, EventArgs e)
    {
        //Ensures essential fields are not empty before proceeding
        if (string.IsNullOrWhiteSpace(eTitle.Text))
        {
            await DisplayAlert("Помилка!", "Назва товару не може бути порожньою", "OK");
            return;
        }

        if (pCategory.SelectedItem == null)
        {
            await DisplayAlert("Помилка!", "Категорія має бути обрана", "OK");
            return;
        }

        var good = new GoodUIModel(Guid.Empty);

        good.Title = eTitle.Text;
        good.Category = ((EnumWithName<Category>)pCategory.SelectedItem).Value;

        // Converting string input from Entry fields to numeric types.
        if (int.TryParse(eQuantity.Text, out int qty)) good.Quantity = qty;
        if (decimal.TryParse(ePrice.Text, out decimal price)) good.UnitPrice = price;

        good.Description = eDescription.Text ?? "";

        good.SaveChangesToDBModel();

        await DisplayAlert("Товар створено!", $"Товар {good.Title} успішно додано.", "OK");
    }

    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}