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
    public partial class RegisterPage : ContentPage
    {
        User user;
        public RegisterPage()
        {
            InitializeComponent();

            user = new User();
            containerSL.BindingContext = user;
        }

        private void RegBtn_Clicked(object sender, EventArgs e)
        {
            if (passwordTB.Text == confirmPasswordTB.Text)
            {
                User user = new User
                {
                    Email = emailTB.Text,
                    Password = passwordTB.Text
                };

                int rowsInserted = User.Insert(user);

                if (rowsInserted > 0)
                    DisplayAlert("Success", "A new User " + user.Email + " is Registered", "Ok");
                else
                    DisplayAlert("Failure", "User is failed to be registered", "Ok");
            }
            else
            {
                DisplayAlert("Error", "Passwords do not match", "Ok");
            }
        }
    }
}