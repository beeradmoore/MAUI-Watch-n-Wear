using Foundation;
using System;
using UIKit;
using WatchConnectivity;

namespace Step1
{
    public partial class ViewController : UIViewController, IWCSessionDelegate
    {
        int count = 0;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (WCSession.IsSupported)
            {
                var session = WCSession.DefaultSession;
                session.Delegate = this;
                session.ActivateSession();
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void decClicked(NSObject sender)
        {
            --count;
            UpdateTextDisplay();
            SyncCountToWatch();
        }

        partial void incClicked(NSObject sender)
        {
            ++count;
            UpdateTextDisplay();
            SyncCountToWatch();
        }

        void UpdateTextDisplay()
        {
            InvokeOnMainThread(() =>
            {
                displayLabel.Text = count.ToString();
            });
        }

        void SyncCountToWatch()
        {
            if (WCSession.DefaultSession.Reachable)
            {
                var data = new NSDictionary<NSString, NSObject>(new NSString("count"),  new NSNumber(count));
                WCSession.DefaultSession.SendMessage(data, null, null);
            }
        }


        #region IWCSessionDelegate
        [Export("session:didReceiveMessage:")]
        public void DidReceiveMessage(WCSession session, NSDictionary<NSString, NSObject> message)
        {
            var key = new NSString("count");
            if (message.ContainsKey(key))
            {
                var countNumber = message[key] as NSNumber;
                count = countNumber.Int32Value;
                UpdateTextDisplay();
            }
        }
        #endregion
    }
}
