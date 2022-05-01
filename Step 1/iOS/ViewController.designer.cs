// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Step1
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UILabel displayLabel { get; set; }

		[Action ("decClicked:")]
		partial void decClicked (Foundation.NSObject sender);

		[Action ("incClicked:")]
		partial void incClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (displayLabel != null) {
				displayLabel.Dispose ();
				displayLabel = null;
			}
		}
	}
}
