﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ImageSort.WPF.Converters
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    class PathToBitmapImageConverter : IValueConverter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The app should not crash just because some exception happened")]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (value is string path)
            {
                try
                {
                    var bitmapImage = new BitmapImage();

                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = new Uri(path);
                    if (LoadWidth != null && LoadWidth.HasValue) bitmapImage.DecodePixelWidth = LoadWidth.Value;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public int? LoadWidth { get; set; } = null;
    }
}
