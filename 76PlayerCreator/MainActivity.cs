using System;
using System.IO;
using System.Reflection;
using Android;
using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SpecialDefualt;

namespace _76PlayerCreator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage1;
        TextView textMessage2;
        TextView textMessage3;
        Special[] special;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            textMessage1 = FindViewById<TextView>(Resource.Id.message1);
            textMessage2 = FindViewById<TextView>(Resource.Id.message2);
            textMessage3 = FindViewById<TextView>(Resource.Id.message3);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);

            navigation.SetOnNavigationItemSelectedListener(this);

            AssetManager assets = this.Assets;
            using (StreamReader sr = new StreamReader(assets.Open("special.json")))
            {
                var json = sr.ReadToEnd();
                var rootobject = JsonConvert.DeserializeObject<Rootobject>(json);
                special = rootobject.special;
            }

            Button button = FindViewById<Button>(Resource.Id.button1);
            EditText editText1 = FindViewById<EditText>(Resource.Id.editText1);

            button.Click += (o, e) => {
                editText1.Text += 1;
            };

        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            Android.Views.View view = (Android.Views.View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.nav_camera)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_gallery)
            {

            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);

            RelativeLayout layout_status = FindViewById<RelativeLayout>(Resource.Id.content_status);
            RelativeLayout layout_special = FindViewById<RelativeLayout>(Resource.Id.content_special);
            RelativeLayout layout_perks = FindViewById<RelativeLayout>(Resource.Id.content_perks);
            //special[0].ToString()
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    layout_perks.Visibility = ViewStates.Gone;
                    layout_special.Visibility = ViewStates.Gone;
                    layout_status.Visibility = ViewStates.Visible;
                    using (var writer = new System.IO.StringWriter())
                    {
                        textMessage1.Text = "";
                    }
                    return true;
                case Resource.Id.navigation_dashboard:
                    layout_perks.Visibility = ViewStates.Gone;
                    layout_special.Visibility = ViewStates.Visible;
                    layout_status.Visibility = ViewStates.Gone;
                    using (var writer = new System.IO.StringWriter())
                    {
                        textMessage2.Text = special[0].ToString();
                        textMessage3.Text = special[1].ToString();
                    }
                    return true;
                case Resource.Id.navigation_notifications:
                    layout_perks.Visibility = ViewStates.Visible;
                    layout_special.Visibility = ViewStates.Gone;
                    layout_status.Visibility = ViewStates.Gone;
                    using (var writer = new System.IO.StringWriter())
                    {
                        
                    }
                    return true;
            }
            return false;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

