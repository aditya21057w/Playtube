using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using AndroidX.Lifecycle;
using Firebase;
using IO.Agora.Rtc2;
using Java.Lang;
using PlayTube.Activities;
using PlayTube.Activities.Live.Rtc;
using PlayTube.Activities.Live.Stats;
using PlayTube.Activities.Live.Utils;
using PlayTube.Activities.SettingsPreferences;
using PlayTube.Helpers.Ads;
using PlayTube.Helpers.Utils;
using PlayTube.SQLite;
using PlayTubeClient;
using Plugin.CurrentActivity;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Android.Net;
using Console = System.Console;
using Constants = PlayTube.Activities.Live.Page.Constants;
using Exception = System.Exception;

namespace PlayTube
{
    //You can specify additional application information in this attribute
    [Application(UsesCleartextTraffic = true)]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        private static MainApplication Instance;
        public Activity Activity;
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {

        }

        public override void OnCreate()
        {
            try
            {
                base.OnCreate();
                //A great place to initialize Xamarin.Insights and Dependency Services!
                RegisterActivityLifecycleCallbacks(this);
                Instance = this;

                //Bypass Web Errors 
                //======================================
                if (AppSettings.TurnSecurityProtocolType3072On)
                {
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    var client = new HttpClient(new AndroidMessageHandler());
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13 | SecurityProtocolType.SystemDefault;
                    Console.WriteLine(client);
                }

                //If you are Getting this error >>> System.Net.WebException: Error: TrustFailure /// then Set it to true
                if (AppSettings.TurnTrustFailureOnWebException)
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                //JsonConvert.DefaultSettings = () => UserDetails.JsonSettings;

                InitializePlayTube.Initialize(AppSettings.TripleDesAppServiceProvider, PackageName, AppSettings.TurnSecurityProtocolType3072On, AppSettings.SetApisReportMode);

                var sqLiteDatabase = new SqLiteDatabase();
                sqLiteDatabase.Connect();
                sqLiteDatabase.Get_data_Login();

                FirstRunExcite();
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void FirstRunExcite()
        {
            try
            {
                //Init Settings
                MainSettings.Init();

                Methods.AppLifecycleObserver appLifecycleObserver = new Methods.AppLifecycleObserver();
                ProcessLifecycleOwner.Get().Lifecycle.AddObserver(appLifecycleObserver);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public void SecondRunExcite(Activity activity)
        {
            try
            {
                AdsGoogle.InitializeAdsGoogle.Initialize(activity);

                InitializeFacebook.Initialize(activity);

                AdsAppLovin.Initialize(activity);

                ClassMapper.SetMappers();

                //App restarted after crash
                AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentOnUnhandledExceptionRaiser;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
                //TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

                AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
                FirebaseApp.InitializeApp(this);

                //CrossCurrentActivity.Current.Init(this);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void AndroidEnvironmentOnUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            try
            {
                Intent intent = new Intent(Activity, typeof(SplashScreenActivity));
                intent.AddCategory(Intent.CategoryHome);
                intent.PutExtra("crash", true);
                intent.SetAction(Intent.ActionMain);
                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask | ActivityFlags.ClearTask);

                PendingIntent pendingIntent = PendingIntent.GetActivity(GetInstance().BaseContext, 0, intent, Build.VERSION.SdkInt >= BuildVersionCodes.M ? PendingIntentFlags.OneShot | PendingIntentFlags.Immutable : PendingIntentFlags.OneShot);
                AlarmManager mgr = (AlarmManager)GetInstance()?.BaseContext?.GetSystemService(AlarmService);
                mgr?.Set(AlarmType.Rtc, JavaSystem.CurrentTimeMillis() + 100, pendingIntent);

                Activity.Finish();
                JavaSystem.Exit(2);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                //var message = e.Exception.Message;
                var stackTrace = e.Exception.StackTrace;

                Methods.DisplayReportResult(Activity, stackTrace);
                Console.WriteLine(e.Exception);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                //var message = e;
                Methods.DisplayReportResult(Activity, e);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public static MainApplication GetInstance()
        {
            return Instance;
        }

        #region Agora

        private RtcEngine MRtcEngine;
        private readonly EngineConfig MGlobalConfig = new EngineConfig();
        private readonly AgoraEventHandler MHandler = new AgoraEventHandler();
        private readonly StatsManager MStatsManager = new StatsManager();

        public void InitConfig()
        {
            try
            {
                ISharedPreferences pref = PrefManager.GetPreferences(ApplicationContext);
                MGlobalConfig.SetVideoDimenIndex(pref.GetInt(Constants.PrefResolutionIdx, Constants.DefaultProfileIdx));

                bool showStats = pref.GetBoolean(Constants.PrefEnableStats, false);
                MGlobalConfig.SetIfShowVideoStats(showStats);
                MStatsManager.EnableStats(showStats);

                MGlobalConfig.SetMirrorLocalIndex(pref.GetInt(Constants.PrefMirrorLocal, 0));
                MGlobalConfig.SetMirrorRemoteIndex(pref.GetInt(Constants.PrefMirrorRemote, 0));
                MGlobalConfig.SetMirrorEncodeIndex(pref.GetInt(Constants.PrefMirrorEncode, 0));
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void InitRtcEngine()
        {
            try
            {
                MRtcEngine = IO.Agora.Rtc2.RtcEngine.Create(ApplicationContext, AppSettings.AppIdAgoraLive, MHandler);
                // Sets the channel profile of the Agora RtcEngine.
                // The Agora RtcEngine differentiates channel profiles and applies different optimization algorithms accordingly. For example, it prioritizes smoothness and low latency for a video call, and prioritizes video quality for a video broadcast.
                MRtcEngine.SetChannelProfile(IO.Agora.Rtc2.Constants.ChannelProfileLiveBroadcasting);
                MRtcEngine.EnableVideo();
                MRtcEngine.SetLogFile(FileUtil.InitializeLogFile(this));
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public EngineConfig EngineConfig()
        {
            return MGlobalConfig;
        }

        public RtcEngine RtcEngine()
        {
            return MRtcEngine;
        }

        public StatsManager StatsManager()
        {
            return MStatsManager;
        }

        public void RegisterEventHandler(IEventHandler handler)
        {
            MHandler.AddHandler(handler);
        }

        public void RemoveEventHandler(IEventHandler handler)
        {
            MHandler.RemoveHandler(handler);
        }

        #endregion

        public override void OnTerminate() // on stop
        {
            try
            {
                base.OnTerminate();
                UnregisterActivityLifecycleCallbacks(this);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            try
            {
                Activity = activity;
                CrossCurrentActivity.Current.Activity = activity;
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public void OnActivityDestroyed(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPaused(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityResumed(Activity activity)
        {
            try
            {
                Activity = activity;
                CrossCurrentActivity.Current.Activity = activity;
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            Activity = activity;
        }

        public void OnActivityStarted(Activity activity)
        {
            try
            {
                Activity = activity;
                CrossCurrentActivity.Current.Activity = activity;
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public void OnActivityStopped(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPostCreated(Activity activity, Bundle savedInstanceState)
        {
            Activity = activity;
        }

        public void OnActivityPostDestroyed(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPostPaused(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPostResumed(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPostSaveInstanceState(Activity activity, Bundle outState)
        {
            Activity = activity;
        }

        public void OnActivityPostStarted(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPostStopped(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPreCreated(Activity activity, Bundle savedInstanceState)
        {
            Activity = activity;
        }

        public void OnActivityPreDestroyed(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPrePaused(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPreResumed(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPreSaveInstanceState(Activity activity, Bundle outState)
        {
            Activity = activity;
        }

        public void OnActivityPreStarted(Activity activity)
        {
            Activity = activity;
        }

        public void OnActivityPreStopped(Activity activity)
        {
            Activity = activity;
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
    }
}