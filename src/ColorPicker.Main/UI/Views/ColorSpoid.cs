﻿using ColorPicker.Converter;
using ColorPicker.LayoutSupport.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ColorPicker.Main.UI.Views
{
	public class ColorSpoid : Explorer
	{
		#region DefaultStyleKey

		static ColorSpoid()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSpoid), new FrameworkPropertyMetadata(typeof(ColorSpoid)));
		}
		#endregion

		#region Constructor

		public ColorSpoid()
		{
			IsFixedSize = true;
			ResizeMode = System.Windows.ResizeMode.NoResize;
			Width = 400;
			Height = 360;

			Loaded += (s, e) => Topmost = true;
		}
		#endregion
	}
}