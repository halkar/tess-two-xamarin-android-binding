using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Googlecode.Leptonica.Android;
using Com.Googlecode.Tesseract.Android;

namespace Test.Test
{
    [Activity(Label = "Test.Test", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;  

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
            {
                TessBaseAPI api = new TessBaseAPI(new MyProcessNotifier());
                bool result = api.Init("/mnt/sdcard/tesseract-ocr/", "eng");
                ///storage/emulated/0/DCIM/Camera/IMG_20150530_124916.jpg
                //ExifInterface exif = new ExifInterface("/storage/emulated/0/DCIM/Camera/IMG_20150530_124916.jpg");
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InSampleSize = 4;

                Bitmap bitmap = BitmapFactory.DecodeFile("/storage/emulated/0/DCIM/Camera/IMG_20150530_124916.jpg", options);
                api.SetImage(bitmap);
                String recognizedText = api.UTF8Text;
                api.End();
            };
            
        }
    }

    public class MyProcessNotifier : Java.Lang.Object, TessBaseAPI.IProgressNotifier
    {
        public void OnProgressValues(TessBaseAPI.ProgressValues p0)
        {
            
        }
    }
}

