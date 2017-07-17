using System;

using Android.App;
using Android.Runtime;
using VKontakte;

namespace KatadZe.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            VKSdk.Initialize(this).WithPayments();
        }
    }
}