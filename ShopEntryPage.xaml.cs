using TodoranIuliaLab7.Models;

namespace TodoranIuliaLab7;

public partial class ShopEntryPage : ContentPage
{
	public ShopEntryPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		ShopListView.ItemsSource = await App.Database.GetShopsAsync();
	}
	async void OnShopAddedClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShopPage
		{
			BindingContext = new Shop()
		});
    }
    async void OnListViewItemSelected(object sender,
		SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ShopPage
            {
                BindingContext = e.SelectedItem as Shop
            });
        }
    }

}