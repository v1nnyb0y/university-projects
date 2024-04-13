using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Lab._5.LINQ.Properties;

namespace Lab._5.LINQ.UI.Converters
{
    /// <summary>
    ///     Class for the converter to the Bitmap from byte[]
    /// </summary>
    public class ImagePathConverter : IValueConverter
    {
        public object Convert
        (
            object      value,
            Type        targetType,
            object      parameter,
            CultureInfo culture
        ) {
            var bytes  = (byte[]) value;
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            if (bytes == null) {
                var ms = new MemoryStream();
                Resources.base_avatar.Save
                    (
                     ms,
                     ImageFormat.Bmp
                    );
                ms.Seek
                    (
                     0,
                     SeekOrigin.Begin
                    );
                bitmap.StreamSource = ms;
            }
            else {
                bitmap.StreamSource = new MemoryStream
                    (
                     bytes
                    );
            }

            bitmap.EndInit();
            return bitmap;
        }

        public object ConvertBack
        (
            object      value,
            Type        targetType,
            object      parameter,
            CultureInfo culture
        ) {
            return null;
        }
    }
}