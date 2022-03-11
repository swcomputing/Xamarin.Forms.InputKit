﻿using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using InputKit.Shared.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InputKit.Handlers.IconView
{
    public partial class IconViewHandler : ViewHandler<IIconView, ImageView>
    {
        protected override ImageView CreateNativeView()
        {
            return new ImageView(Context);
        }

        public static void MapSource(IIconViewHandler handler, IIconView view)
        {
            UpdateBitmap(handler, view);
        }

        public static void MapFillColor(IIconViewHandler handler, IIconView view)
        {
            UpdateBitmap(handler, view);
        }

        // TODO: Remove after following issue closed https://github.com/dotnet/maui/issues/3410
        public static void MapIsVisible(IIconViewHandler handler, IIconView view)
        {
            ViewHandler.MapVisibility(handler, view);
        }

        public static void UpdateBitmap(IIconViewHandler handler, IIconView view)
        {
            if (view.Source == null)
                return;

            Drawable d = default;

            if (view.Source is StreamImageSource streamImageSource)
            {
                var cTokenSource = new CancellationTokenSource(30000);
                var stream = streamImageSource.Stream(cTokenSource.Token).Result;
                d = Drawable.CreateFromStream(stream, "inputkit_check");
            }
            else if (view.Source is FileImageSource fileImageSource)
            {
                d = handler.MauiContext.Context.GetDrawable(fileImageSource.File);
            }
            else
            {
                d = handler.MauiContext.Context.GetDrawable(view.Source.ToString());
            }

            if (d == null) 
                return;

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                d.SetTint(view.FillColor.ToAndroid());
            else
                d.SetColorFilter(new LightingColorFilter(Colors.Black.ToAndroid(), view.FillColor.ToAndroid()));

            d.Alpha = view.FillColor.ToAndroid().A;
            handler.TypedNativeView.SetImageDrawable(d);
        }
    }
}
