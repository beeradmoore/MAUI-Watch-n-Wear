using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Wearable;
using Step2.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Step2.Droid.Services.Watch))]

namespace Step2.Droid.Services
{
    public class Watch : Java.Lang.Object, IWatch,
        MessageClient.IOnMessageReceivedListener,
        CapabilityClient.IOnCapabilityChangedListener
    {
        public Action<int> ValueUpdated { get; set; }

        ICollection<INode> nodes;

        public void SendValue(int value)
        {
            var data = BitConverter.GetBytes(value);
            //await UpdateConnectedNodes();
            System.Diagnostics.Debug.WriteLine($"Phone: Attempting to send to {nodes.Count} nodes");
            var messageClient = WearableClass.GetMessageClient(Xamarin.Essentials.Platform.CurrentActivity);
            foreach (var node in nodes)
            {
                System.Diagnostics.Debug.WriteLine($"Phone: Attempting to send to {node.DisplayName}");
                // This is also gross becuase we are not awaiting the any result properly.
                messageClient.SendMessageAsync(node.Id, "/count", data).ContinueWith((result) =>
                {
                    System.Diagnostics.Debug.WriteLine(result.Result);
                });
            }
        }

        public void Activate()
        {
            // This is gross because we are not awaiting any result.
            var messageClient = WearableClass.GetMessageClient(Xamarin.Essentials.Platform.CurrentActivity);
            messageClient.AddListenerAsync(this);

            var capabilityClient = WearableClass.GetCapabilityClient(Xamarin.Essentials.Platform.CurrentActivity);
            var capabilityClientGetCapabilityTask = capabilityClient.GetCapabilityAsync("step2_sync_count", CapabilityClient.FilterReachable);
            capabilityClientGetCapabilityTask.ContinueWith((capabilityInfo) =>
            {
                nodes = capabilityInfo.Result.Nodes;
            });

            capabilityClient.AddListenerAsync(this, "step2_sync_count");
        }

        public void Deactivate()
        {
            var messageClient = WearableClass.GetMessageClient(Xamarin.Essentials.Platform.CurrentActivity);
            messageClient.RemoveListener(this);

            var capabilityClient = WearableClass.GetCapabilityClient(Xamarin.Essentials.Platform.CurrentActivity);
            capabilityClient.RemoveListener(this, "step2_sync_count");
        }


        #region CapabilityClient.IOnCapabilityChangedListener
        public void OnCapabilityChanged(ICapabilityInfo capabilityInfo)
        {
            System.Diagnostics.Debug.WriteLine("Phone: OnCapabilityChanged");
            nodes = capabilityInfo.Nodes;
        }
        #endregion

        #region MessageClient.IOnMessageReceivedListener
        public void OnMessageReceived(IMessageEvent messageEvent)
        {
            System.Diagnostics.Debug.WriteLine("Phone: OnMessageReceived");
            if (messageEvent.Path == "/count")
            {
                var data = messageEvent.GetData();
                var count = BitConverter.ToInt32(data);
                ValueUpdated?.Invoke(count);
            }
        }
        #endregion
    }
}
