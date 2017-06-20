using Android.App;
using Android.Widget;
using Android.OS;

using eMMaSDK;

namespace eMMaXamarinSample_Android
{
	[Activity(Label = "EMMAXamarinSample-Android", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
            base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { 

				button.Text = string.Format("{0} clicks!", count++);

				eMMa.TrackEvent("41e6f9a6446ff0cc11f8f4f5e1bd5908");
			};

            eMMa.CheckForRichPushUrl(this);
		}
	}
}


