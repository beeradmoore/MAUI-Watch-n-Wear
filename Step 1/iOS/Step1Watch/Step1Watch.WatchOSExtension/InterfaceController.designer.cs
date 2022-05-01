// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Step1Watch.WatchOSExtension
{
	[Register ("InterfaceController")]
	partial class InterfaceController
	{
		[Outlet]
		WatchKit.WKInterfaceLabel displayLabel { get; set; }

		[Action ("decClicked")]
		partial void decClicked ();

		[Action ("incClicked")]
		partial void incClicked ();
		
		void ReleaseDesignerOutlets ()
		{
			if (displayLabel != null) {
				displayLabel.Dispose ();
				displayLabel = null;
			}
		}
	}
}
