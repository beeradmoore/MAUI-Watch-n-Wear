using System;

using Foundation;
using WatchConnectivity;
using WatchKit;

namespace Step3.WatchOSExtension
{
    public partial class InterfaceController : WKInterfaceController, IWCSessionDelegate
    {
        int count = 0;

        protected InterfaceController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void Awake(NSObject context)
        {
            base.Awake(context);

            // Configure interface objects here.
            Console.WriteLine("{0} awake with context", this);
        }

        public override void WillActivate()
        {
            // This method is called when the watch view controller is about to be visible to the user.
            Console.WriteLine("{0} will activate", this);

            if (WCSession.IsSupported)
            {
                var session = WCSession.DefaultSession;
                session.Delegate = this;
                session.ActivateSession();
            }
        }

        public override void DidDeactivate()
        {
            // This method is called when the watch view controller is no longer visible to the user.
            Console.WriteLine("{0} did deactivate", this);
        }

        partial void decClicked()
        {
            --count;
            UpdateTextDisplay();
            SyncCountToPhone();
        }

        partial void incClicked()
        {
            ++count;
            UpdateTextDisplay();
            SyncCountToPhone();
        }

        void UpdateTextDisplay()
        {
            displayLabel.SetText(count.ToString());
        }


        void SyncCountToPhone()
        {
            if (WCSession.DefaultSession.Reachable)
            {
                var data = new NSDictionary<NSString, NSObject>(new NSString("count"), new NSNumber(count));
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

