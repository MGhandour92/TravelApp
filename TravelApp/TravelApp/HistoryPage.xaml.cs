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

            //Bind the Listview with the returned list
            var posts = Post.GetPosts();
            postLV.ItemsSource = posts;
        }
    }
}