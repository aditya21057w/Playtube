using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.Core.OS;
using Com.Google.Android.Exoplayer2.UI;
using PlayTube.Activities.Base;
using PlayTube.Activities.Models;
using PlayTube.Helpers.Utils;
using PlayTube.MediaPlayers.Exo;
using System;

namespace PlayTube.Activities.Videos
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.Keyboard | ConfigChanges.Orientation | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenLayout | ConfigChanges.ScreenSize | ConfigChanges.SmallestScreenSize | ConfigChanges.UiMode | ConfigChanges.Locale)]
    public class FullScreenVideoActivity : AppCompatActivity
    {
        public ExoController ExoController;
        public VideoDataWithEventsLoader VideoDataWithEventsLoader;
        private StyledPlayerView PlayerViewFullScreen;
        public static FullScreenVideoActivity Instance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                Methods.App.FullScreenApp(this, true);

                var type = Intent?.GetStringExtra("type") ?? "";
                if (type == "RequestedOrientation")
                {
                    //ScreenOrientation.Portrait >>  Make to run your application only in portrait mode
                    //ScreenOrientation.Landscape >> Make to run your application only in LANDSCAPE mode 
                    RequestedOrientation = ScreenOrientation.Landscape;
                }

                SetContentView(Resource.Layout.FullScreenDialogLayout);
                InitBackPressed();
                Instance = this;

                PlayerViewFullScreen = FindViewById<StyledPlayerView>(Resource.Id.player_view2);

                VideoDataWithEventsLoader = VideoDataWithEventsLoader.GetInstance();
                ExoController = VideoDataWithEventsLoader.ExoController;

                ExoController.SetFullScreenPlayerView(PlayerViewFullScreen);
                ExoController.SetPlayerControl(true, true);
                ExoController.PlayFullScreen();

                VideoDataWithEventsLoader.SetDownloadIcon();
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void InitBackPressed()
        {
            try
            {
                if (BuildCompat.IsAtLeastT && Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
                {
                    OnBackInvokedDispatcher.RegisterOnBackInvokedCallback(0, new BackCallAppBase2(this, "FullScreenVideoActivity"));
                }
                else
                {
                    OnBackPressedDispatcher.AddCallback(new BackCallAppBase1(this, "FullScreenVideoActivity", true));
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public override void OnTrimMemory(TrimMemory level)
        {
            try
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                base.OnTrimMemory(level);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public override void OnLowMemory()
        {
            try
            {
                GC.Collect(GC.MaxGeneration);
                base.OnLowMemory();
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public void BackPressed()
        {
            VideoDataWithEventsLoader?.InitFullscreenDialog("", "Close");
            Finish();
        }

        //public override void OnConfigurationChanged(Configuration newConfig)
        //{
        //    try
        //    {
        //        if (newConfig.Orientation == Orientation.Landscape)
        //        {
        //        }
        //        else if (newConfig.Orientation == Orientation.Portrait)
        //        {
        //            //VideoDataWithEventsLoader?.InitFullscreenDialog("","Close");
        //        }
        //        base.OnConfigurationChanged(newConfig);
        //    }
        //    catch (Exception e)
        //    {
        //        Methods.DisplayReportResultTrack(e);
        //    }
        //}

        protected override void OnDestroy()
        {
            Instance = null;
            base.OnDestroy();
        }
    }
}