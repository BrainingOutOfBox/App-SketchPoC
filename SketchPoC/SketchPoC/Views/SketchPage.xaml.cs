﻿using SketchPoC.BLServices;
using SketchPoC.Sketching;
using SketchPoC.Sketching.TouchEffect;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace SketchPoC.Views
{
    public partial class SketchPage : ContentPage
    {
        public SketchPage(ISaveService saveService)
        {
            InitializeComponent();
            _saveService = saveService;
        }
        Dictionary<long, FingerPaintPolyline> inProgressPolylines = new Dictionary<long, FingerPaintPolyline>();
        List<FingerPaintPolyline> completedPolylines = new List<FingerPaintPolyline>();

        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };
        private readonly ISaveService _saveService;

        void OnClearButtonClicked(object sender, EventArgs args)
        {
            completedPolylines.Clear();

            // Informs the canvas that it needs to redraw itself
            canvasView.InvalidateSurface();
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!inProgressPolylines.ContainsKey(args.Id))
                    {
                        Color strokeColor = (Color)typeof(Color).GetRuntimeField(colorPicker.Items[colorPicker.SelectedIndex]).GetValue(null);
                        float strokeWidth = ConvertToPixel(new float[] { 1, 2, 5, 10, 20 }[widthPicker.SelectedIndex]);

                        FingerPaintPolyline polyline = new FingerPaintPolyline
                        {
                            StrokeColor = strokeColor,
                            StrokeWidth = strokeWidth
                        };
                        polyline.Path.MoveTo(ConvertToPixel(args.Location));

                        inProgressPolylines.Add(args.Id, polyline);

                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Moved:
                    if (inProgressPolylines.ContainsKey(args.Id))
                    {
                        FingerPaintPolyline polyline = inProgressPolylines[args.Id];
                        polyline.Path.LineTo(ConvertToPixel(args.Location));
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                    if (inProgressPolylines.ContainsKey(args.Id))
                    {
                        completedPolylines.Add(inProgressPolylines[args.Id]);
                        inProgressPolylines.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Cancelled:
                    if (inProgressPolylines.ContainsKey(args.Id))
                    {
                        inProgressPolylines.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            foreach (FingerPaintPolyline polyline in completedPolylines)
            {
                paint.Color = polyline.StrokeColor.ToSKColor();
                paint.StrokeWidth = polyline.StrokeWidth;
                canvas.DrawPath(polyline.Path, paint);
            }

            foreach (FingerPaintPolyline polyline in inProgressPolylines.Values)
            {
                paint.Color = polyline.StrokeColor.ToSKColor();
                paint.StrokeWidth = polyline.StrokeWidth;
                canvas.DrawPath(polyline.Path, paint);
            }
        }

        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
        }

        float ConvertToPixel(float fl)
        {
            return (float)(canvasView.CanvasSize.Width * fl / canvasView.Width);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            var info = new SKImageInfo((int)canvasView.CanvasSize.Width, (int)canvasView.CanvasSize.Height);
            var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear();

            foreach (FingerPaintPolyline polyline in completedPolylines)
                canvas.DrawPath(polyline.Path, paint);

            foreach (FingerPaintPolyline polyline in inProgressPolylines.Values)
                canvas.DrawPath(polyline.Path, paint);

            canvas.Flush();

            var snap = surface.Snapshot();

            using (var data = snap.Encode(SKEncodedImageFormat.Png, 80))
            {
                _saveService.Save(data.AsStream());
            }
        }
    }
}
