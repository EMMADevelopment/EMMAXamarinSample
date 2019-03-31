using Android.App;
using System;
using Android.Runtime;
using EMMASDK;
using EMMASDK.Model;

namespace AndroidExample
{
    [Application]
    public class EMMAApplication : Application
    {
        public EMMAApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            // do any initialisation you want here (for example initialising properties)
        }


        public override void OnCreate()
        {
            base.OnCreate();

            EMMA.Configuration configuration = new EMMA.Configuration.Builder(this)
                .SetQueueTime(25)
                .SetDebugActive(true)
                .Build();

            EMMA.Instance.StartSession(configuration);

            EMMAPushOptions pushOptions = new EMMAPushOptions.Builder(
            Java.Lang.Class.FromType(typeof(MainActivity)),
                Resource.Drawable.push_icon
            ).Build();

            EMMA.Instance.StartPushSystem(pushOptions);

        }
    }
}