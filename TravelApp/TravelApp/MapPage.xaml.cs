using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using TravelApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged += Locator_PositionChanged;

            await locator.StartListeningAsync(TimeSpan.Zero, 100);

            var position = await locator.GetPositionAsync();

            var center = new Position(position.Latitude, position.Longitude);
            var span = new MapSpan(center, 2, 2);

            MyMap.MoveToRegion(span);

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Create or Replace the table
                //To overcome exceptions when user first open this page before inserting
                conn.CreateTable<Post>();

                //Select Records from the table
                var postList = conn.Table<Post>().ToList();

                DisplayInMap(postList);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;

            await locator.StopListeningAsync();
        }

        private void DisplayInMap(List<Post> postList)
        {
            //Create pin for every post
            foreach (var post in postList)
            {
                try
                {
                    var position = new Position(post.Latitude, post.Longtitude);

                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    MyMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre)
                {
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Position(e.Position.Latitude, e.Position.Longitude);
            var span = new MapSpan(center, 2, 2);

            MyMap.MoveToRegion(span);
        }
    }
}