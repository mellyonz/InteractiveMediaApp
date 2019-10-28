using System;
using System.Collections;
using System.Collections.Generic;
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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PerksDefualt;
using SpecialDefualt;

namespace _76PlayerCreator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        Special[] special;
        Perks[] perks;
        VideoView videoView;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);

            navigation.SetOnNavigationItemSelectedListener(this);

            AssetManager assets = this.Assets;
            using (StreamReader sr = new StreamReader(assets.Open("special.json")))
            {
                var json = sr.ReadToEnd();
                var rootobject_special = JsonConvert.DeserializeObject<SpecialDefualt.Rootobject>(json);
                special = rootobject_special.special;
            }

            using (StreamReader sr = new StreamReader(assets.Open("perks.json")))
            {
                var json = sr.ReadToEnd();
                var rootobject_perks = JsonConvert.DeserializeObject<PerksDefualt.Rootobject>(json);
                perks = rootobject_perks.perks;
            }

            using (var writer = new System.IO.StringWriter())
            {
                //count_special
                int c_s = 0;
                foreach (var c in special)
                {
                    var textSimilarArtist = new TextView(this) { Text = special[c_s].Info[0].ToString() };
                    CardView card = new CardView(this);
                    LinearLayout layout_card = new LinearLayout(this);
                    layout_card.Orientation = Android.Widget.Orientation.Vertical;
                    LinearLayout layout_buttons = new LinearLayout(this);
                    layout_buttons.Orientation = Android.Widget.Orientation.Horizontal;
                    var button1 = new Button(this) { Text = "Lower" };
                    var button2 = new Button(this) { Text = "Raise" };
                    var editText1 = new EditText(this) { Text = special[c_s].Data[0].ToString() };
                    var layoutParams = FindViewById<LinearLayout>(Resource.Id.add_layout_special);

                    layout_card.AddView(textSimilarArtist);
                    layout_buttons.AddView(button1);
                    layout_buttons.AddView(button2);
                    layout_card.AddView(layout_buttons);
                    layout_card.AddView(editText1);
                    card.AddView(layout_card);
                    layoutParams.AddView(card);

                    button1.Click += (o, e) => {
                        if (2 <= Int32.Parse(editText1.Text))
                        {
                            editText1.Text = (Int32.Parse(editText1.Text) - 1).ToString();
                        }
                    };
                    button2.Click += (o, e) => {
                        if (Int32.Parse(editText1.Text) <= 14)
                        {
                            editText1.Text = (Int32.Parse(editText1.Text) + 1).ToString();
                        }
                    };
                    c_s += 1;
                }
            }

            using (var writer = new System.IO.StringWriter())
            {
                //count_perks
                int c_p = 0;
                foreach (var c in perks)
                {
                    var textSimilarArtist = new TextView(this) { Text = perks[c_p].Info[0][0].ToString() };
                    CardView card = new CardView(this);
                    LinearLayout layout_card = new LinearLayout(this);
                    layout_card.Orientation = Android.Widget.Orientation.Vertical;
                    LinearLayout layout_buttons = new LinearLayout(this);
                    layout_buttons.Orientation = Android.Widget.Orientation.Horizontal;
                    var button1 = new Button(this) { Text = "Lower"};
                    var button2 = new Button(this) { Text = "Raise" };
                    var editText1 = new EditText(this) { Text = perks[c_p].Data[0].ToString() };
                    var layoutParams = FindViewById<LinearLayout>(Resource.Id.add_layout_perks);

                    layout_card.AddView(textSimilarArtist);
                    layout_buttons.AddView(button1);
                    layout_buttons.AddView(button2);
                    layout_card.AddView(layout_buttons);
                    layout_card.AddView(editText1);
                    card.AddView(layout_card);
                    layoutParams.AddView(card);

                    c_p += 1;
                }
            }

            videoView = FindViewById<VideoView>(Resource.Id.videoView1);
            var uri = Android.Net.Uri.Parse("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4");
            MediaController media_controller = new Android.Widget.MediaController(this);
            videoView.SetVideoURI(uri);
            media_controller.SetMediaPlayer(videoView);
            videoView.SetMediaController(media_controller);
            videoView.RequestFocus();
            videoView.Visibility = ViewStates.Visible;

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



        public bool OnNavigationItemSelected(IMenuItem item)
        {
            RelativeLayout layout_status = FindViewById<RelativeLayout>(Resource.Id.content_status);
            RelativeLayout layout_special = FindViewById<RelativeLayout>(Resource.Id.content_special);
            RelativeLayout layout_perks = FindViewById<RelativeLayout>(Resource.Id.content_perks);
            RelativeLayout layout_navigation = FindViewById<RelativeLayout>(Resource.Id.content_main);
            RelativeLayout layout_tutorial = FindViewById<RelativeLayout>(Resource.Id.content_tutorial);
            
            int id = item.ItemId;
            if (id == Resource.Id.nav_camera)
            {
                layout_status.Visibility = ViewStates.Visible;
                layout_navigation.Visibility = ViewStates.Visible;
                layout_tutorial.Visibility = ViewStates.Gone;

                videoView.Start();
            }
            else if (id == Resource.Id.nav_gallery)
            {
                layout_perks.Visibility = ViewStates.Gone;
                layout_special.Visibility = ViewStates.Gone;
                layout_status.Visibility = ViewStates.Gone;
                layout_navigation.Visibility = ViewStates.Gone;
                layout_tutorial.Visibility = ViewStates.Visible;
            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);

            //special[0].ToString()
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    layout_perks.Visibility = ViewStates.Gone;
                    layout_special.Visibility = ViewStates.Gone;
                    layout_status.Visibility = ViewStates.Visible;
                    return true;
                case Resource.Id.navigation_dashboard:
                    layout_perks.Visibility = ViewStates.Gone;
                    layout_special.Visibility = ViewStates.Visible;
                    layout_status.Visibility = ViewStates.Gone;
                    return true;
                case Resource.Id.navigation_notifications:
                    layout_perks.Visibility = ViewStates.Visible;
                    layout_special.Visibility = ViewStates.Gone;
                    layout_status.Visibility = ViewStates.Gone;
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

