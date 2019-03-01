﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Method635")]
[assembly: ExportEffect(typeof(SketchPoC.iOS.TouchEffect), "TouchEffect")]
namespace SketchPoC.iOS
{
    public class TouchEffect : PlatformEffect
    {
        UIView view;
        TouchRecognizer touchRecognizer;

        protected override void OnAttached()
        {
            // Get the iOS UIView corresponding to the Element that the effect is attached to
            view = Control ?? Container;

            // Uncomment this line if the UIView does not have touch enabled by default
            //view.UserInteractionEnabled = true;

            // Get access to the TouchEffect class in the .NET Standard library

            Sketching.TouchEffect.TouchEffect effect = (SketchPoC.Sketching.TouchEffect.TouchEffect)Element.Effects.FirstOrDefault(e => e is SketchPoC.Sketching.TouchEffect.TouchEffect);

            if (effect != null && view != null)
            {
                // Create a TouchRecognizer for this UIView
                touchRecognizer = new TouchRecognizer(Element, view, effect);
                view.AddGestureRecognizer(touchRecognizer);
            }
        }

        protected override void OnDetached()
        {
            if (touchRecognizer != null)
            {
                // Clean up the TouchRecognizer object
                touchRecognizer.Detach();

                // Remove the TouchRecognizer from the UIView
                view.RemoveGestureRecognizer(touchRecognizer);
            }
        }
    }
}