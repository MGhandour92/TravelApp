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

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            //Set props in model object
            Post post = new Post()
            {
                Experience = experienceTB.Text
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
    }
}