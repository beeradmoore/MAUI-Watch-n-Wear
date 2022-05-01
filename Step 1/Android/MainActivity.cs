using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Widget;

namespace Step1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        int _value = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var decButton = FindViewById<Button>(Resource.Id.dec_button);
            decButton.Click += (object sender, EventArgs e) =>
            {
                --_value;
                UpdateTextDisplay();
            };

            var incButton = FindViewById<Button>(Resource.Id.inc_button);
            incButton.Click += (object sender, EventArgs e) =>
            {
                ++_value;
                UpdateTextDisplay();
            };
        }


        void UpdateTextDisplay()
        {
            var textViewDisplay = FindViewById<TextView>(Resource.Id.text_view_display);
            textViewDisplay.Text = _value.ToString();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
