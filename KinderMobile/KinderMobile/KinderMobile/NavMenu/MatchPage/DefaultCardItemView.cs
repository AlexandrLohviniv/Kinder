﻿using System;
using FFImageLoading.Forms;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KinderMobile.NavMenu.MatchPage
{
	public class DefaultCardItemView : AbsoluteLayout
	{
		public DefaultCardItemView()
		{
			var tapGesture = new TapGestureRecognizer();
			tapGesture.Tapped += (s, e) => Application.Current.MainPage.DisplayAlert("Tap!", null, "Ok");
			GestureRecognizers.Add(tapGesture);

			var frame = new Frame
			{
				Padding = 0,
				HasShadow = false,
				CornerRadius = 10,
				IsClippedToBounds = true
			};
			frame.SetBinding(BackgroundColorProperty, "Color");
			Children.Add(frame, new Rectangle(.5, .5, 300, 300), AbsoluteLayoutFlags.PositionProportional);

			var image = new CachedImage
			{
				Aspect = Aspect.AspectFill
			};

			image.SetBinding(CachedImage.SourceProperty, "Source");

			frame.Content = image;
		}
	}
}
