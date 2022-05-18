using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support;
using AndroidX.Wear.Activity;
using Android.Gms.Wearable;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Step3.WearOSApp;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity,
        MessageClient.IOnMessageReceivedListener,
        CapabilityClient.IOnCapabilityChangedListener
{
    int count = 0;
    TextView? textView;
    ICollection<INode> nodes = new List<INode>();

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);

        textView = FindViewById<TextView>(Resource.Id.text_view_display);


        var decButton = FindViewById<Button>(Resource.Id.dec_button);
        if (decButton != null)
        {
            decButton.Click += (object? sender, EventArgs e) =>
            {
                --count;
                UpdateTextDisplay();
                SyncCountToPhone();
            };
        }

        var incButton = FindViewById<Button>(Resource.Id.inc_button);
        if (incButton != null)
        { 
            incButton.Click += (object? sender, EventArgs e) =>
            {
                ++count;
                UpdateTextDisplay();
                SyncCountToPhone();
            };
        }

        SetupCapability();
    }


    async Task SetupCapability()
    {
        await WearableClass.GetMessageClient(this).AddListenerAsync(this);

        var capabilityInfo = await WearableClass.GetCapabilityClient(this).GetCapabilityAsync("step3_sync_count", CapabilityClient.FilterReachable);
        nodes = capabilityInfo.Nodes;

        await WearableClass.GetCapabilityClient(this).AddListenerAsync(this, "step3_sync_count");
    }

    void UpdateTextDisplay()
    {
        RunOnUiThread(() =>
        {
            if (textView != null)
            {
                textView.Text = count.ToString();
            }
        });
    }

    async Task SyncCountToPhone()
    {
        var data = BitConverter.GetBytes(count);
        //await UpdateConnectedNodes();
        System.Diagnostics.Debug.WriteLine($"Watch: Attempting to send to {nodes.Count} nodes");
        foreach (var node in nodes)
        {
            System.Diagnostics.Debug.WriteLine($"Watch: Attempting to send to {node.DisplayName}");
            var result = await WearableClass.GetMessageClient(this).SendMessageAsync(node.Id, "/count", data);
            System.Diagnostics.Debug.WriteLine(result);
        }
    }


    /*
    async Task<ICollection<INode>> UpdateConnectedNodes()
    {
        var capabilityInfo = await WearableClass.GetCapabilityClient(this).GetCapabilityAsync("step3_sync_count", CapabilityClient.FilterReachable);
        System.Diagnostics.Debug.WriteLine($"UpdateConnectedNodes: Found {capabilityInfo.Nodes.Count}");
        foreach (var node in capabilityInfo.Nodes)
        {
            System.Diagnostics.Debug.WriteLine($"UpdateConnectedNodes: {node.DisplayName}");
        }

        nodes = await WearableClass.GetNodeClient(this).GetConnectedNodesAsync();
        return nodes;
    }
    */


    #region CapabilityClient.IOnCapabilityChangedListener
    public void OnCapabilityChanged(ICapabilityInfo capabilityInfo)
    {
        System.Diagnostics.Debug.WriteLine("Watch: OnCapabilityChanged");

        nodes = capabilityInfo.Nodes;
    }
    #endregion


    #region MessageClient.IOnMessageReceivedListener
    public void OnMessageReceived(IMessageEvent messageEvent)
    {
        System.Diagnostics.Debug.WriteLine("Watch: OnMessageReceived");
        if (messageEvent.Path == "/count")
        {
            var data = messageEvent.GetData();
            count = BitConverter.ToInt32(data);
            UpdateTextDisplay();
        }
    }
    #endregion
}