using System;
using UIKit;
using EMMASDK;

namespace iOSExample
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController(IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

        partial void EventButton_TouchUpInside(UIButton sender)
        {
            EMMAEventRequest eventRequest = 
                new EMMAEventRequest("41e6f9a6446ff0cc11f8f4f5e1bd5908");
            EMMA.TrackEvent(eventRequest);
        }

        partial void CheckStartviewButton_TouchUpInside(UIButton sender)
        {
            EMMAInAppRequest inAppRequest = 
                new EMMAInAppRequest(InAppType.Startview);
            EMMA.InAppMessage(inAppRequest);
        }
    }
}

