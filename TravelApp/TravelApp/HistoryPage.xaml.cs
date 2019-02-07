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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage() => InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Read Database
            //Create Connection
            //Using statement because it implments IDisposable, and to avoid forgetting to close connection
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Create or Replace the table
                //To overcome exceptions when user first open this page before inserting
                conn.CreateTable<Post>();

                //Select Records from the table
                var postList = conn.Table<Post>().ToList();

                //Bind the Listview with the returned list
                postLV.ItemsSource = postList;
            }//No need to close connection, as we use Using statment
        }
    }
}