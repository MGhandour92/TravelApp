using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);

            venueLV.ItemsSource = venues;
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            bool emptyExp = string.IsNullOrEmpty(experienceTB.Text);

            if (!emptyExp)
            {
                try
                {
                    var selectedVenue = venueLV.SelectedItem as Venue;
                    var selectedCateg = selectedVenue.categories.FirstOrDefault();

                    //Set props in model object
                    Post post = new Post()
                    {
                        Experience = experienceTB.Text,
                        CategoryId = selectedCateg.id,
                        CategoryName = selectedCateg.name,
                        Address = selectedVenue.location.address,

                        Distance = selectedVenue.location.distance,
                        Longtitude = selectedVenue.location.lng,
                        Latitude = selectedVenue.location.lat,
                        VenueName = selectedVenue.name,
                        UserId = App.currentUser.Id
                    };

                    int rowsInserted = Post.Insert(post);

                    if (rowsInserted > 0)
                        DisplayAlert("Success", "Experince successfully added", "Ok");
                    else
                        DisplayAlert("Failure", "Experince failed to be added", "Ok");
                }
                catch (NullReferenceException nre)
                {
                    DisplayAlert("Failure", "Experince failed to be added", "Ok");
                }
                catch (Exception ex)
                {
                    DisplayAlert("Failure", "Experince failed to be added", "Ok");
                }
            }
            else
                DisplayAlert("Failure", "Please enter a valid experience", "Ok");
        }
    }
}