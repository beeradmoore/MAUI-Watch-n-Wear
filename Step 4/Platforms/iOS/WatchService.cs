using System;
using Foundation;
using WatchConnectivity;

namespace Step4.Services
{
	public partial class WatchService : NSObject, IWCSessionDelegate
    {
        public partial void SendValue(int value)
        {
            if (WCSession.DefaultSession.Reachable)
            {
                var data = new NSDictionary<NSString, NSObject>(new NSString("count"), new NSNumber(value));
                WCSession.DefaultSession.SendMessage(data, null, null);
            }
        }

        public partial void Activate()
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

        public partial void Deactivate()
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

        public void ActivationDidComplete(WCSession session, WCSessionActivationState activationState, NSError error)
        {
            System.Diagnostics.Debug.WriteLine("ActivationDidComplete");
        }

        public void DidBecomeInactive(WCSession session)
        {
            System.Diagnostics.Debug.WriteLine("DidBecomeInactive");
        }

        public void DidDeactivate(WCSession session)
        {
            System.Diagnostics.Debug.WriteLine("DidDeactivate");
        }
        #endregion
    }
}

