using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;
using Plugin.Permissions;
using Plugin.CurrentActivity;

namespace TravelApp.Droid
{
    [Activity(Label = "TravelApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //Enable maps initialization
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            //Name and set the path for the DB
            string dbName = "travel_db.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbName);

            //Load app with database location
            LoadApplication(new App(fullPath));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}