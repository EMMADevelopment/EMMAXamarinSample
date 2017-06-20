using Android.App;
using eMMaSDK;
using System;
using Android.Runtime;

namespace eMMaXamarinSample_Android
{
    [Application]
    public class EMMAApplication: Application
    {
		public EMMAApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle,transfer)
        {
			// do any initialisation you want here (for example initialising properties)
		}


		public override void OnCreate()
		{
			base.OnCreate();

            eMMa.StartEMMASession(this);
            eMMa.SetDebuggerOutput(true);
			eMMa.StartPushSystem(this, this.Class, 0, true);

		}
    }
}
