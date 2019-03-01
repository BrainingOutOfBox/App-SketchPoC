using Prism.Commands;
using Prism.Mvvm;
using SketchPoC.Sketching;
using SketchPoC.Sketching.TouchEffect;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SketchPoC.ViewModels
{
	public class SketchPageViewModel : BindableBase
	{
        readonly Dictionary<long, FingerPaintPolyline> inProgressPolylines = new Dictionary<long, FingerPaintPolyline>();
        readonly List<FingerPaintPolyline> completedPolylines = new List<FingerPaintPolyline>();
        readonly SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };
        public SketchPageViewModel()
        {
            //PaintSurfaceCommand = new DelegateCommand<SKSurface>(PaintSurface);
            //TouchEffectAction += OnAction;
        }
        //public event TouchActionEventHandler TouchEffectAction;
        //public void OnAction(object sender, TouchActionEventArgs args)
        //{

        //}
        //private void PaintSurface(SKSurface surface)
        //{
        //    var canvas = surface.Canvas;
        //    canvas.Clear();

        //    foreach (FingerPaintPolyline polyline in completedPolylines)
        //    {
        //        paint.Color = polyline.StrokeColor.ToSKColor();
        //        paint.StrokeWidth = polyline.StrokeWidth;
        //        canvas.DrawPath(polyline.Path, paint);
        //    }

        //    foreach (FingerPaintPolyline polyline in inProgressPolylines.Values)
        //    {
        //        paint.Color = polyline.StrokeColor.ToSKColor();
        //        paint.StrokeWidth = polyline.StrokeWidth;
        //        canvas.DrawPath(polyline.Path, paint);
        //    }
        //}

        //public DelegateCommand<SKSurface> PaintSurfaceCommand { get; }
    }
}
