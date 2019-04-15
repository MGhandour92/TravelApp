using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Model;
using Xamarin.Forms;

namespace TravelApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            logoImage.Source = ImageSource.FromResource("TravelApp.Assets.Images.plane.png",
                assembly);
        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {
            bool userLogin = User.Login(emailTB.Text, passwordTB.Text);

            if (userLogin)
                Navigation.PushAsync(new HomePage());
            else
                DisplayAlert("Error", "Email or Password is wrong", "Ok");
        }

        private void RegisterUserBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
