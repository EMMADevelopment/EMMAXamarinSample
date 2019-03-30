using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Runtime;
using IO.Emma.Android;
using IO.Emma.Android.Model;

namespace AndroidExample
{
    [Activity(Label = "EMMA Xamarin", MainLauncher = true, Icon = "@mipmap/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            EMMA.Instance.StartTrackingLocation();

            Button sendTokenButton = FindViewById<Button>(Resource.Id.send_token);

            sendTokenButton.Click += delegate {

                EMMAEventRequest eventRequest = 
                    new EMMAEventRequest("41e6f9a6446ff0cc11f8f4f5e1bd590");
                EMMA.Instance.TrackEvent(eventRequest);
            };

            Button checkStartViewButton = FindViewById<Button>(Resource.Id.check_startview); 
            checkStartViewButton.Click += delegate {

                EMMAInAppRequest inAppRequest = 
                    new EMMAInAppRequest(EMMACampaign.Type.Startview);
                EMMA.Instance.GetInAppMessage(inAppRequest);
            };

            //When notification is opened this method checks if notification has
            //rich push data
            EMMA.Instance.CheckForRichPushUrl();
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            //When notification is opened this method checks if notification has
            //rich push data. Only when app does not pass through the method onCreate.
            EMMA.Instance.OnNewNotification(intent, true);
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
            EMMA.Instance.OnRequestPermissionsResult(requestCode, permissions, (int[])(object)grantResults);
		}
	}
}
