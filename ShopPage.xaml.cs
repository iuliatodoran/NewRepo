using Plugin.LocalNotification;
using TodoranIuliaLab7.Models;

namespace TodoranIuliaLab7;

public partial class ShopPage : ContentPage
{
	public ShopPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object seder, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Address;
        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions { Name = "Magazinul meu preferat" };
        var shoplocation = locations?.FirstOrDefault();

        var myLocation = await Geolocation.GetLocationAsync();
        /* var myLocation = new Location(46.7731796289, 23.6213886738);
//pentru Windows Machine */

        var distance = myLocation.CalculateDistance(shoplocation, DistanceUnits.Kilometers);

        if (distance < 5)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }

        await Map.OpenAsync(shoplocation, options);
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {shop.ShopName}?", "Yes", "No");

        if (confirm)
        {
            await App.Database.DeleteShopAsync(shop);
            await Navigation.PopAsync();
        }
    }

}