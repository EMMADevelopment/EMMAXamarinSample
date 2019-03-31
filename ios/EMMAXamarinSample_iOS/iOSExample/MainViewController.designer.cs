// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit; 

namespace iOSExample
{
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CheckStartviewButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton EventButton { get; set; }

        [Action ("CheckStartviewButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CheckStartviewButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("EventButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void EventButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (CheckStartviewButton != null) {
                CheckStartviewButton.Dispose ();
                CheckStartviewButton = null;
            }

            if (EventButton != null) {
                EventButton.Dispose ();
                EventButton = null;
            }
        }
    }
}