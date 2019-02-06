using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {
            bool emptyEmail = string.IsNullOrEmpty(emailTB.Text);
            bool emptyPassword = string.IsNullOrEmpty(passwordTB.Text);

            if (emptyEmail || emptyPassword)
            { }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
