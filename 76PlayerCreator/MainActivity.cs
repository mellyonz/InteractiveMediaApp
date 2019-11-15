using System;
using System.Drawing;
using System.IO;
using Android;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PerksDefualt;
using SpecialDefualt;

namespace _76PlayerCreator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private Special[] special;
        private Perks[] perks;
        private VideoView videoView;
        public float strength;
        public float lifegiver;

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
                    var layout_main = FindViewById<LinearLayout>(Resource.Id.add_layout_special);

                    CardView.LayoutParams layout_card_par = new CardView.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                    layout_card_par.SetMargins(30, 30, 30, 30);
                    LinearLayout layout_card = new LinearLayout(this)
                    {
                        Orientation = Android.Widget.Orientation.Vertical,
                        LayoutParameters = layout_card_par
                    };
                    CardView.LayoutParams card_par = new CardView.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                    card_par.SetMargins(30, 30, 30, 30);
                    CardView card = new CardView(this)
                    {
                        LayoutParameters = card_par,
                    };
                    card.SetCardBackgroundColor(Android.Graphics.Color.Black);

                    var special_name = new TextView(this) { Text = special[c_s].Info[0].ToString() };
                    special_name.SetTextColor(Android.Graphics.Color.LimeGreen);
                    special_name.SetTextSize(ComplexUnitType.Px, 100);

                    var special_description = new TextView(this) { Text = special[c_s].ToString() };
                    special_description.SetTextColor(Android.Graphics.Color.LimeGreen);

                    LinearLayout.LayoutParams par_layout_buttons = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent)
                    {
                        Gravity = GravityFlags.Right
                    };
                    LinearLayout layout_buttons = new LinearLayout(this)
                    {
                        Orientation = Android.Widget.Orientation.Horizontal,
                        LayoutParameters = par_layout_buttons
                    };

                    var button1 = new ImageButton(this)
                    {
                        ContentDescription = c_s.ToString()
                    };
                    button1.SetImageResource(Resource.Drawable.arrow_left);
                    button1.SetBackgroundColor(Android.Graphics.Color.LimeGreen);

                    var button2 = new ImageButton(this)
                    {
                    };
                    button2.SetImageResource(Resource.Drawable.arrow_right);
                    button2.SetBackgroundColor(Android.Graphics.Color.LimeGreen);

                    var editText1 = new EditText(this) { Text = special[c_s].Data[0].ToString() };
                    editText1.SetTextColor(Android.Graphics.Color.LimeGreen);

                    layout_card.AddView(special_name);
                    layout_card.AddView(special_description);
                    layout_buttons.AddView(button1);
                    layout_buttons.AddView(editText1);
                    layout_buttons.AddView(button2);
                    layout_card.AddView(layout_buttons);
                    card.AddView(layout_card);
                    layout_main.AddView(card);
                    

                    button1.Click += (o, e) => {
                        var current = Int32.Parse(button1.ContentDescription);
                        if (2 <= Int32.Parse(editText1.Text))
                        {
                            editText1.Text = (Int32.Parse(editText1.Text) - 1).ToString();
                            special[current].assignedPoints -= 1;
                        }
                    };
                    button2.Click += (o, e) => {
                        var current = Int32.Parse(button1.ContentDescription.ToString());
                        if (Int32.Parse(editText1.Text) <= 14)
                        {
                            editText1.Text = (Int32.Parse(editText1.Text) + 1).ToString();
                            special[current].assignedPoints += 1;
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

                    var layout_main = FindViewById<LinearLayout>(Resource.Id.add_layout_special);

                    CardView.LayoutParams layout_card_par = new CardView.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                    layout_card_par.SetMargins(30, 30, 30, 30);
                    LinearLayout layout_card = new LinearLayout(this)
                    {
                        Orientation = Android.Widget.Orientation.Vertical,
                        LayoutParameters = layout_card_par
                    };
                    CardView.LayoutParams card_par = new CardView.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                    card_par.SetMargins(50, 0, 60, 60);
                    CardView card = new CardView(this)
                    {
                        LayoutParameters = card_par
                    };
                    card.SetCardBackgroundColor(Android.Graphics.Color.Black);

                    var perk_name = new TextView(this) { Text = perks[c_p].Info[0][0].ToString() };
                    perk_name.SetTextColor(Android.Graphics.Color.LimeGreen);
                    perk_name.SetTextSize(ComplexUnitType.Px, 100);

                    var perk_description = new TextView(this) { Text = perks[c_p].Info[1][0].ToString() };
                    perk_description.SetTextColor(Android.Graphics.Color.LimeGreen);

                    LinearLayout.LayoutParams par_layout_buttons = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent)
                    {
                        Gravity = GravityFlags.Right
                    };
                    LinearLayout layout_buttons = new LinearLayout(this)
                    {
                        Orientation = Android.Widget.Orientation.Horizontal,
                        LayoutParameters = par_layout_buttons
                    };

                    var button1 = new ImageButton(this)
                    {
                        ContentDescription = perks[c_p].Data[1].ToString() + c_p.ToString()
                    };
                    button1.SetImageResource(Resource.Drawable.arrow_left);
                    button1.SetBackgroundColor(Android.Graphics.Color.LimeGreen);

                    var button2 = new ImageButton(this)
                    {
                        ContentDescription = perks[c_p].Data[1].ToString() + c_p.ToString()
                    };
                    button2.SetImageResource(Resource.Drawable.arrow_right);
                    button2.SetBackgroundColor(Android.Graphics.Color.LimeGreen);

                    var editText1 = new EditText(this) { Text = perks[c_p].Data[1].ToString() };
                    editText1.SetTextColor(Android.Graphics.Color.LimeGreen);

                    var layoutParams = FindViewById<LinearLayout>(Resource.Id.add_layout_perks);

                    layout_card.AddView(perk_name);
                    layout_card.AddView(perk_description);
                    layout_buttons.AddView(button1);
                    layout_buttons.AddView(editText1);
                    layout_buttons.AddView(button2);
                    layout_card.AddView(layout_buttons);
                    card.AddView(layout_card);
                    layoutParams.AddView(card);
                    var maxCardLevel = perks[c_p].Data[2];
                    var description = "";
                    foreach (int desc in perks[c_p].Info[1].ToString())
                        description += desc;


                    button1.Click += (o, e) => {
                        //count name and description
                        var count = Int32.Parse(button1.ContentDescription[0].ToString());
                        var current = Int32.Parse(button1.ContentDescription[1].ToString());
                        if (1 <= Int32.Parse(editText1.Text))
                        {
                            editText1.Text = (Int32.Parse(editText1.Text) - 1).ToString();
                            count -= 1;
                            try
                            {
                                perk_name.Text = perks[current].Info[0][count].ToString();
                                perk_description.Text = perks[current].Info[1][count].ToString();
                                perks[current].assignedLevel -= 1;
                            }
                            catch
                            {
                                perk_name.Text = perks[current].Info[0][0].ToString();
                                perk_description.Text = perks[current].Info[1][count].ToString();
                                perks[current].assignedLevel -= 1;
                            }
                            button1.ContentDescription = count.ToString() + current.ToString();
                        }
                    };
                    button2.Click += (o, e) => {
                        //count name and description
                        var count = Int32.Parse(button1.ContentDescription[0].ToString());
                        var current = Int32.Parse(button1.ContentDescription[1].ToString());
                        if (Int32.Parse(editText1.Text) <= maxCardLevel - 1)
                        {
                            editText1.Text = (Int32.Parse(editText1.Text) + 1).ToString();
                            try
                            {
                                perk_name.Text = perks[current].Info[0][count].ToString();
                                perk_description.Text = perks[current].Info[1][count].ToString();
                                perks[current].assignedLevel += 1;
                            }
                            catch
                            {
                                perk_name.Text = perks[current].Info[0][0].ToString();
                                perk_description.Text = perks[current].Info[1][count].ToString();
                                perks[current].assignedLevel += 1;
                            }
                            count += 1;
                            button1.ContentDescription = count.ToString() + current.ToString();
                        }
                    };
                    c_p += 1;
                }
            }




            videoView = FindViewById<VideoView>(Resource.Id.videoView1);
            var uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.fallout_76_video);
            videoView.SetVideoURI(uri);
            videoView.Visibility = ViewStates.Visible;

            MediaController media_controller = new Android.Widget.MediaController(this);
            //videoView.SetOnPreparedListener(new VideoLoop());
            media_controller.SetMediaPlayer(videoView);

            videoView.SetMediaController(media_controller);
            videoView.RequestFocus();

            var play_button = FindViewById<Button>(Resource.Id.play_button);
            var pause_button = FindViewById<Button>(Resource.Id.pause_button);
            var mute_button = FindViewById<Button>(Resource.Id.mute_button);
            var volume_seekBar = FindViewById<SeekBar>(Resource.Id.seek_bar);

            MediaPlayer player = MediaPlayer.Create(this, Resource.Raw.fallout_76_music);

            play_button.Click += (o, e) =>
            {
                player.Start();
            };
            pause_button.Click += (o, e) =>
            {
                player.Pause();
            };
            mute_button.Click += (o, e) =>
            {
                player.SetVolume(0f,0f);
            };
            volume_seekBar.ProgressChanged += (o, e) =>
            {
                float vol = (float)(Math.Log(volume_seekBar.Progress) / Math.Log(50));
                player.SetVolume(vol, vol);
            };

            var showPopupMenu = FindViewById<Button>(Resource.Id.popupButton);

            showPopupMenu.Click += (o, e) => {

                RelativeLayout layout_status = FindViewById<RelativeLayout>(Resource.Id.content_status);
                RelativeLayout layout_splash = FindViewById<RelativeLayout>(Resource.Id.content_splash);
                RelativeLayout layout_navigation = FindViewById<RelativeLayout>(Resource.Id.content_main);
                layout_splash.Visibility = ViewStates.Gone;
                layout_status.Visibility = ViewStates.Visible;
                layout_navigation.Visibility = ViewStates.Visible;

            };

            /*var image_splash = FindViewById<ImageView>(Resource.Id.splash_image);
            var Image = FindViewById<Image>(Resource.Raw.Fallout_76);
            image_splash(Image);*/
            Update_status();
        }
        public void Update_status()
        {
            var health_value = FindViewById<TextView>(Resource.Id.status_health_value);
            health_value.Text = perks[2].assignedLevel.ToString();
            health_value.SetTextColor(Android.Graphics.Color.LimeGreen);

            var carryweight_value = FindViewById<TextView>(Resource.Id.status_carryweight_value);
            carryweight_value.Text = special[0].assignedPoints.ToString();
            carryweight_value.SetTextColor(Android.Graphics.Color.LimeGreen);
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

        /*public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }*/

        public class VideoLoop : Java.Lang.Object, Android.Media.MediaPlayer.IOnPreparedListener
        {
            public void OnPrepared(MediaPlayer mp)
            { mp.SetVolume(0f, 0f); }
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            RelativeLayout layout_status = FindViewById<RelativeLayout>(Resource.Id.content_status);
            RelativeLayout layout_special = FindViewById<RelativeLayout>(Resource.Id.content_special);
            RelativeLayout layout_perks = FindViewById<RelativeLayout>(Resource.Id.content_perks);
            RelativeLayout layout_navigation = FindViewById<RelativeLayout>(Resource.Id.content_main);
            RelativeLayout layout_tutorial = FindViewById<RelativeLayout>(Resource.Id.content_tutorial);
            RelativeLayout layout_settings = FindViewById<RelativeLayout>(Resource.Id.content_settings);
            RelativeLayout layout_splash = FindViewById<RelativeLayout>(Resource.Id.content_splash);
            

            int id = item.ItemId;
            if (id == Resource.Id.content_main)
            {
                layout_status.Visibility = ViewStates.Visible;
                layout_navigation.Visibility = ViewStates.Visible;
                layout_tutorial.Visibility = ViewStates.Gone;
                layout_settings.Visibility = ViewStates.Gone;
                layout_splash.Visibility = ViewStates.Gone;
                Update_status();
                videoView.Pause();
            }
            else if (id == Resource.Id.content_tutorial)
            {
                layout_perks.Visibility = ViewStates.Gone;
                layout_special.Visibility = ViewStates.Gone;
                layout_status.Visibility = ViewStates.Gone;
                layout_navigation.Visibility = ViewStates.Gone;
                layout_tutorial.Visibility = ViewStates.Visible;
                layout_settings.Visibility = ViewStates.Gone;
                layout_splash.Visibility = ViewStates.Gone;
                videoView.Start();
            }
            else if (id == Resource.Id.content_settings)
            {
                layout_perks.Visibility = ViewStates.Gone;
                layout_special.Visibility = ViewStates.Gone;
                layout_status.Visibility = ViewStates.Gone;
                layout_navigation.Visibility = ViewStates.Gone;
                layout_tutorial.Visibility = ViewStates.Gone;
                layout_settings.Visibility = ViewStates.Visible;
                layout_splash.Visibility = ViewStates.Gone;
                videoView.Pause();
            }
            else if (id == Resource.Id.content_splash)
            {
                layout_perks.Visibility = ViewStates.Gone;
                layout_special.Visibility = ViewStates.Gone;
                layout_status.Visibility = ViewStates.Gone;
                layout_navigation.Visibility = ViewStates.Gone;
                layout_tutorial.Visibility = ViewStates.Gone;
                layout_settings.Visibility = ViewStates.Gone;
                layout_splash.Visibility = ViewStates.Visible;
                videoView.Pause();
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
                    Update_status();
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

