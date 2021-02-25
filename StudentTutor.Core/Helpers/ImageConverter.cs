using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace StudentTutor.Core.Helpers
{
    public class ImageConverter
    {
        public static byte[] ConvertImgToByte(Image image)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                image.Save(ms, image.RawFormat);
                byte[] output = ms.ToArray();

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static BitmapSource ConvertByteToImg(byte[] imageBytes)
        {
            try
            {
                MemoryStream ms = new MemoryStream(imageBytes);
                Image output = Image.FromStream(ms, true);

                return GetImageStream(output);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);
        private static BitmapSource GetImageStream(Image myImage)
        {
            try
            {
                var bitmap = new Bitmap(myImage);
                IntPtr bmpPt = bitmap.GetHbitmap();
                BitmapSource bitmapSource =
                 System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                       bmpPt,
                       IntPtr.Zero,
                       Int32Rect.Empty,
                       BitmapSizeOptions.FromEmptyOptions());

                //freeze bitmapSource and clear memory to avoid memory leaks
                bitmapSource.Freeze();
                DeleteObject(bmpPt);

                return bitmapSource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
