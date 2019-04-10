using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Logic;
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

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);

            venueLV.ItemsSource = venues;
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
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
                    VenueName = selectedVenue.name
                };

                //Create SQLite connection
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    //Create Table using the connection
                    conn.CreateTable<Post>();

                    //insert the model object (record)
                    int rowsInserted = conn.Insert(post);

                    if (rowsInserted > 0)
                        DisplayAlert("Success", "Experince successfully added", "Ok");
                    else
                        DisplayAlert("Failure", "Experince failed to be added", "Ok");
                }
            }
            catch (NullReferenceException nre)
            {

            }
            catch (Exception ex)
            {

            }            
        }
    }
}