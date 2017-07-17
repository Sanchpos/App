using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Android.Content;
using VKontakte;

namespace KatadZe.Droid
{
	[Activity (Label = "KatadZe", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            FacebookSdk.SdkInitialize(ApplicationContext);
            ImageCircleRenderer.Init();
			LoadApplication (new KatadZe.App ());
		}

        protected override void OnResume()
        {
            base.OnResume();
            AppEventsLogger.ActivateApp(Application);
        }

           protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            bool vkResult;
            var task = VKSdk.OnActivityResultAsync(requestCode, resultCode, data, out vkResult);

            if (!vkResult)
            {
                base.OnActivityResult(requestCode, resultCode, data);
                AndroidFacebookService.Instance.OnActivityResult(requestCode, (int)resultCode, data);
                return;
            }

            try
            {
                var token = await task;
                AndroidVkService.Instance.SetUserToken(token);
            }
            catch (Exception e)
            {
                var vkException = e as VKException;
                if (vkException == null || vkException.Error.ErrorCode != VKontakte.API.VKError.VkCanceled)
                    AndroidVkService.Instance.SetErrorResult(e.Message);
                else
                    AndroidVkService.Instance.SetCanceledResult();
            }
        }
    }
}

