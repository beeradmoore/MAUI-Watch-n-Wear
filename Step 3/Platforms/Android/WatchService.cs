using System;
using Android.Gms.Wearable;

namespace Step3.Services
{
	public partial class WatchService : Java.Lang.Object,
		MessageClient.IOnMessageReceivedListener,
		CapabilityClient.IOnCapabilityChangedListener
	{

        ICollection<INode> nodes;

        public partial void SendValue(int value)
        {
            var data = BitConverter.GetBytes(value);
            //await UpdateConnectedNodes();
            System.Diagnostics.Debug.WriteLine($"Phone: Attempting to send to {nodes.Count} nodes");
            var messageClient = WearableClass.GetMessageClient(MainApplication.Context);
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

        public partial void Activate()
        {
            // This is gross because we are not awaiting any result.
            var messageClient = WearableClass.GetMessageClient(MainApplication.Context);
            messageClient.AddListenerAsync(this);

            var capabilityClient = WearableClass.GetCapabilityClient(MainApplication.Context);
            var capabilityClientGetCapabilityTask = capabilityClient.GetCapabilityAsync("step3_sync_count", CapabilityClient.FilterReachable);
            capabilityClientGetCapabilityTask.ContinueWith((capabilityInfo) =>
            {
                nodes = capabilityInfo.Result.Nodes;
            });

            capabilityClient.AddListenerAsync(this, "step3_sync_count");
        }

        public partial void Deactivate()
        {
            var messageClient = WearableClass.GetMessageClient(MainApplication.Context);
            messageClient.RemoveListener(this);

            var capabilityClient = WearableClass.GetCapabilityClient(MainApplication.Context);
            capabilityClient.RemoveListener(this, "step3_sync_count");
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

