using Android.App;
using Android.Widget;
using Android.OS;

using EMMASDK;
using EMMASDK.Model;
using Android.Content.PM;
using Android.Runtime;

namespace eMMaXamarinSample_Android
{
    [Activity(Label = "eMMaXamarinSample-Android", MainLauncher = true, Icon = "@mipmap/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EMMA.Instance.StartTrackingLocation();

            // Get our button from the layout resource,
            // and attach an event to it
            Button sendTokenButton = FindViewById<Button>(Resource.Id.send_token);

            sendTokenButton.Click += delegate {

                EMMA.Instance.TrackEvent("41e6f9a6446ff0cc11f8f4f5e1bd5908");
            };

            Button checkStartViewButton = FindViewById<Button>(Resource.Id.check_startview); 
            checkStartViewButton.Click += delegate {
                EMMA.Instance.GetInAppMessage(EMMACampaign.Type.Startview, null);
            };

            EMMA.Instance.CheckForRichPushUrl();
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            EMMA.Instance.OnNewNotification(intent, true);
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
            EMMA.Instance.OnRequestPermissionsResult(requestCode, permissions, (int[])(object)grantResults);
		}
	}
}
