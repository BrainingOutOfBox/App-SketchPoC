﻿using SkiaSharp;
using Xamarin.Forms;

namespace SketchPoC.Sketching
{
    class FingerPaintPolyline
    {
        public FingerPaintPolyline()
        {
            Path = new SKPath();
        }

        public SKPath Path { set; get; }

        public Color StrokeColor { set; get; }

        public float StrokeWidth { set; get; }
    }
}
