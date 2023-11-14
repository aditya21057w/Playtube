//###############################################################
// Author >> Elin Doughouz
// Copyright (c) PlayTube 12/07/2018 All Right Reserved
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// Follow me on facebook >> https://www.facebook.com/Elindoughous
// Doc https://doughouzlight.com/?onepage-docs=playtube-andorid
//=========================================================

using PlayTube.Helpers.Models;
using System.Collections.Generic;

namespace PlayTube
{
    internal static class AppSettings
    {
        /// <summary>
        /// Deep Links To App Content
        /// you should add your website without http in the analytic.xml file >> ../values/analytic.xml .. line 5
        /// <string name="ApplicationUrlWeb">demo.playtubescript.com</string>
        /// </summary>
        public static readonly string TripleDesAppServiceProvider = "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";

        //Main Settings >>>>> 
        //********************************************************* 
        public static readonly string ApplicationName = "PlayTube";
        public static readonly string DatabaseName = "PlayTubeVideos";
        public static string Version = "3.6";

        //Main Colors >>
        //*********************************************************
        public static readonly string MainColor = "#0F64F7";

        public static readonly PlayerTheme PlayerTheme = PlayerTheme.Theme2;

        //Language Settings >> http://www.lingoes.net/en/translator/langcode.htm
        //*********************************************************
        public static bool FlowDirectionRightToLeft = false;
        public static string Lang = ""; //Default language ar_AE

        //Set Language User on site from phone 
        public static readonly bool SetLangUser = false;

        //Notification Settings >>
        //*********************************************************
        public static bool ShowNotification = true;
        public static string OneSignalAppId = "e06a3585-d0ac-44ef-b2df-0c24abc23682";

        //Tv Settings >>
        //*********************************************************
        public static readonly bool LinkWithTv = true;

        //AdMob >> Please add the code ads in the Here and analytic.xml 
        //*********************************************************
        public static readonly ShowAds ShowAds = ShowAds.AllUsers;

        //Three times after entering the ad is displayed
        public static readonly int ShowAdInterstitialCount = 3;
        public static readonly int ShowAdRewardedVideoCount = 3;
        public static readonly int ShowAdNativeCount = 4;
        public static readonly int ShowAdAppOpenCount = 2;

        public static readonly bool ShowAdMobBanner = true;
        public static readonly bool ShowAdMobInterstitial = true;
        public static readonly bool ShowAdMobRewardVideo = true;
        public static readonly bool ShowAdMobNative = true;
        public static readonly bool ShowAdMobAppOpen = true;
        public static readonly bool ShowAdMobRewardedInterstitial = true;

        public static readonly string AdInterstitialKey = "ca-app-pub-5135691635931982/6168068662";
        public static readonly string AdRewardVideoKey = "ca-app-pub-5135691635931982/4663415300";
        public static readonly string AdAdMobNativeKey = "ca-app-pub-5135691635931982/2619721801";
        public static readonly string AdAdMobAppOpenKey = "ca-app-pub-5135691635931982/4967593321";
        public static readonly string AdRewardedInterstitialKey = "ca-app-pub-5135691635931982/1850136085";

        //FaceBook Ads >> Please add the code ad in the Here and analytic.xml 
        //*********************************************************
        public static readonly bool ShowFbBannerAds = false;
        public static readonly bool ShowFbInterstitialAds = false;
        public static readonly bool ShowFbRewardVideoAds = false;
        public static readonly bool ShowFbNativeAds = false;

        //YOUR_PLACEMENT_ID
        public static readonly string AdsFbBannerKey = "250485588986218_554026418632132";
        public static readonly string AdsFbInterstitialKey = "250485588986218_554026125298828";
        public static readonly string AdsFbRewardVideoKey = "250485588986218_554072818627492";
        public static readonly string AdsFbNativeKey = "250485588986218_554706301897477";
         
        //Ads AppLovin >> Please add the code ad in the Here 
        //*********************************************************  
        public static readonly bool ShowAppLovinBannerAds = true;
        public static readonly bool ShowAppLovinInterstitialAds = true;
        public static readonly bool ShowAppLovinRewardAds = true;

        public static readonly string AdsAppLovinBannerId = "f9ebf067458aa1df";
        public static readonly string AdsAppLovinInterstitialId = "bd6fa0d996c6fceb";
        public static readonly string AdsAppLovinRewardedId = "d3269ba46c446f63";
        //********************************************************* 

        //Social Logins >>
        //If you want login with facebook or google you should change id key in the analytic.xml file or AndroidManifest.xml
        //Facebook >> ../values/analytic.xml  
        //Google >> ../Properties/AndroidManifest.xml .. line 27
        //*********************************************************
        public static readonly bool EnableSmartLockForPasswords = false;

        public static readonly bool ShowFacebookLogin = true;
        public static readonly bool ShowGoogleLogin = true;
        public static readonly bool ShowWoWonderLogin = true;

        public static readonly string AppNameWoWonder = "WoWonder";
        public static readonly string WoWonderDomainUri = "https://demo.wowonder.com";
        public static readonly string WoWonderAppKey = "131c471c8b4edf662dd0ebf7adf3c3d7365838b9";

        public static readonly string ClientId = "404363570731-j48d139m31tgaq2tj0gamg8ah430botj.apps.googleusercontent.com";

        //First Page
        //*********************************************************
        public static readonly bool ShowSkipButton = true;

        public static readonly bool ShowRegisterButton = true;
        public static readonly bool EnablePhoneNumber = false;

        //Set Theme Full Screen App
        //*********************************************************
        public static readonly bool EnableFullScreenApp = false;
        public static bool EnablePictureToPictureMode = true; //>> Not Working >> Next update 

        //Data Channal Users >> About
        //*********************************************************
        public static readonly bool ShowEmailAccount = true;
        public static readonly bool ShowActivities = true;

        //Tab >> 
        //*********************************************************
        public static readonly bool ShowArticle = true;
        public static readonly bool ShowMovies = true;
        public static readonly bool ShowShorts = true;
        public static readonly bool ShowChannelPopular = true;

        //how in search 
        public static readonly List<string> LastKeyWordList = new List<string>() { "Music", "Party", "Nature", "Snow", "Entertainment", "Holidays", "Covid19", "Comedy", "Politics", "Suspense" };

        //Offline Watched Videos >>  
        //*********************************************************
        public static readonly bool AllowOfflineDownload = true;
        public static readonly bool AllowDownloadProUser = true;
        public static readonly bool AllowWatchLater = true;
        public static readonly bool AllowRecentlyWatched = true;
        public static readonly bool AllowPlayLists = true;
        public static readonly bool AllowLiked = true;
        public static readonly bool AllowShared = true;
        public static readonly bool AllowPaid = true;

        //Import && Upload Videos >>  
        //*********************************************************
        public static bool ShowButtonImport { get; set; } = true;
        public static bool ShowButtonUpload { get; set; } = true;

        //Last_Messages Page >>
        ///********************************************************* 
        public static readonly bool RunSoundControl = true;
        public static readonly int RefreshChatActivitiesSeconds = 6000; // 6 Seconds
        public static readonly int MessageRequestSpeed = 3000; // 3 Seconds

        public static readonly int ShowButtonSkip = 10; // 6 Seconds 

        //Set Theme App >> Color - Tab
        //*********************************************************
        public static TabTheme SetTabDarkTheme = TabTheme.Light;

        public static readonly bool SetYoutubeTypeBadgeIcon = true;
        public static readonly bool SetVimeoTypeBadgeIcon = true;
        public static readonly bool SetDailyMotionTypeBadgeIcon = true;
        public static readonly bool SetTwichTypeBadgeIcon = true;
        public static readonly bool SetOkTypeBadgeIcon = true;
        public static readonly bool SetFacebookTypeBadgeIcon = true;

        //Bypass Web Errors 
        ///*********************************************************
        public static readonly bool TurnTrustFailureOnWebException = true;
        public static readonly bool TurnSecurityProtocolType3072On = true;

        //Error Report Mode
        //*********************************************************
        public static readonly bool SetApisReportMode = false;

        public static readonly int AvatarSize = 60;
        public static readonly int ImageSize = 400;

        //Home Page 
        //*********************************************************
        public static readonly bool ShowStockVideo = true;

        public static readonly int CountVideosTop = 10;
        public static readonly int CountVideosLatest = 10;
        public static readonly int CountVideosFav = 10;
        public static readonly int CountVideosLive = 13;
        public static readonly int CountVideosStock = 10;

        /// <summary>
        /// if Radius you can select how much Radius in the parameter #CardPlayerViewRadius
        /// </summary>
        public static readonly CardPlayerView CardPlayerView = CardPlayerView.Square;
        public static readonly float CardPlayerViewRadius = 10F;

        public static readonly bool ShowGoLive = true;
        public static readonly string AppIdAgoraLive = "9471c47b589c4a35abf3f7338ef18629";

        public static readonly ShareSystem ShareSystem = ShareSystem.WebsiteUrl;

        //Settings 
        //*********************************************************
        public static readonly bool ShowEditPassword = true;
        public static readonly bool ShowMonetization = true; //(Withdrawals)
        public static readonly bool ShowVerification = true;
        public static readonly bool ShowBlockedUsers = true;
        public static readonly bool ShowPoints = true;
        public static readonly bool ShowSettingsTwoFactor = true;
        public static readonly bool ShowSettingsManageSessions = true;

        public static readonly bool ShowSettingsRateApp = true;
        public static readonly int ShowRateAppCount = 5;

        public static readonly bool ShowSettingsUpdateManagerApp = false;

        public static readonly bool ShowGoPro = true;

        public static readonly bool ShowClearHistory = true;
        public static readonly bool ShowClearCache = true;

        public static readonly bool ShowHelp = true;
        public static readonly bool ShowTermsOfUse = true;
        public static readonly bool ShowAbout = true;
        public static readonly bool ShowDeleteAccount = true;

        //*********************************************************
        /// <summary>
        /// Currency
        /// CurrencyStatic = true : get currency from app not api 
        /// CurrencyStatic = false : get currency from api (default)
        /// </summary>
        public static readonly bool CurrencyStatic = false;
        public static readonly string CurrencyIconStatic = "$";
        public static readonly string CurrencyCodeStatic = "USD";

        //********************************************************* 
        public static readonly bool RentVideosSystem = true;

        //*********************************************************  
        public static readonly bool DonateVideosSystem = true;

        //*********************************************************  
        /// <summary>
        /// Paypal and google pay using Braintree Gateway https://www.braintreepayments.com/
        /// 
        /// Add info keys in Payment Methods : https://prnt.sc/1z5bffc & https://prnt.sc/1z5b0yj
        /// To find your merchant ID :  https://prnt.sc/1z59dy8
        ///
        /// Tokenization Keys : https://prnt.sc/1z59smv
        /// </summary>
        public static readonly bool ShowPaypal = true;
        public static readonly string MerchantAccountId = "test";

        public static readonly string SandboxTokenizationKey = "sandbox_kt2f6mdh_hf4c******";
        public static readonly string ProductionTokenizationKey = "production_t2wns2y2_dfy45******";

        public static readonly bool ShowCreditCard = true;
        public static readonly bool ShowBankTransfer = true;

        /// <summary>
        /// if you want this feature enabled go to Properties -> AndroidManefist.xml and remove comments from below code
        /// <uses-permission android:name="com.android.vending.BILLING" />
        /// </summary>
        public static readonly bool ShowInAppBilling = false;

        //*********************************************************   
        public static readonly bool ShowCashFree = true;

        /// <summary>
        /// Currencies : http://prntscr.com/u600ok
        /// </summary>
        public static readonly string CashFreeCurrency = "INR";

        /// <summary>
        /// Currencies : https://razorpay.com/accept-international-payments
        /// </summary>
        public static readonly string RazorPayCurrency = "INR";

        /// <summary>
        /// If you want RazorPay you should change id key in the analytic.xml file
        /// razorpay_api_Key >> .. line 18 
        /// </summary>
        public static readonly bool ShowRazorPay = true;

        public static readonly bool ShowPayStack = true;
        public static readonly bool ShowPaySera = true;
        public static readonly bool ShowSecurionPay = false;  //>> To the next update of the API it will work
        public static readonly bool ShowAuthorizeNet = true;
        public static readonly bool ShowIyziPay = true;
        public static readonly bool ShowAamarPay = false; //>> To the next update of the API it will work

        /// <summary>
        /// FlutterWave get Api Keys From https://app.flutterwave.com/dashboard/settings/apis/live
        /// </summary>
        public static readonly bool ShowFlutterWave = true;
        public static readonly string FlutterWaveCurrency = "NGN";
        public static readonly string FlutterWavePublicKey = "FLWPUBK_TEST-9c877b3110438191127e631c8*****"; 
        public static readonly string FlutterWaveEncryptionKey = "FLWSECK_TEST298f1****";

        //*********************************************************  

        public static readonly bool ShowVideoWithDynamicHeight = true;

        //********************************************************* 
        public static readonly bool ShowTextWithSpace = true;

    }
}