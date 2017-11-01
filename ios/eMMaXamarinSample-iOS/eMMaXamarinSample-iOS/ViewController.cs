using System;

using UIKit;

using EMMASDK;

namespace eMMaXamarinSampleiOS
{
	public partial class ViewController : UIViewController
	{

        public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void UIButton3_TouchUpInside(UIButton sender)
		{
			
        }

        partial void UIButton17_TouchUpInside(UIButton sender)
        {
            // Track Test Event
            EMMA.TrackEvent("41e6f9a6446ff0cc11f8f4f5e1bd5908");
        }

        partial void UIButton2209_TouchUpInside(UIButton sender)
        {
            EMMA.InAppMessage(InAppType.Startview, new EMMAInAppRequest());
        }
	}
}

