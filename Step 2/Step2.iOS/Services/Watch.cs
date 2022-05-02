using System;
using Foundation;
using Step2.Services;
using WatchConnectivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(Step2.iOS.Services.Watch))]

namespace Step2.iOS.Services
{
    public class Watch : NSObject, IWatch, IWCSessionDelegate
    {
        public Action<int> ValueUpdated { get; set; }

        public void SendValue(int value)
        {
            if (WCSession.DefaultSession.Reachable)
            {
                var data = new NSDictionary<NSString, NSObject>(new NSString("count"), new NSNumber(value));
                WCSession.DefaultSession.SendMessage(data, null, null);
            }
        }

        public void Activate()
        {
            if (WCSession.IsSupported)
            {
                var session = WCSession.DefaultSession;

                if (session.ActivationState == WCSessionActivationState.NotActivated || session.ActivationState == WCSessionActivationState.Inactive)
                {
                    session.Delegate = this;
                    session.ActivateSession();
                }
            }
        }

        public void Deactivate()
        {
            if (WCSession.IsSupported)
            {
                var session = WCSession.DefaultSession;

                if (session.ActivationState == WCSessionActivationState.Activated)
                {
                    session.Delegate = null;
                }
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
                var count = countNumber.Int32Value;
                ValueUpdated?.Invoke(count);
            }
        }
        #endregion
    }
}
