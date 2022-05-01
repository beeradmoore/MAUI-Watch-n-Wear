using Foundation;
using System;
using UIKit;

namespace Step1
{
    public partial class ViewController : UIViewController
    {
        int _value = 0;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void decClicked(NSObject sender)
        {
            --_value;
            UpdateTextDisplay();
        }

        partial void incClicked(NSObject sender)
        {
            ++_value;
            UpdateTextDisplay();
        }

        void UpdateTextDisplay()
        {
            displayLabel.Text = _value.ToString();
        }
    }
}
