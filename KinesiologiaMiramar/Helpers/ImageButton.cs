using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KinesiologiaMiramar.Helpers
{
    public class ImageButton
    {
        public static ImageSource GetImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageProperty);
        }

        public static void SetImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageProperty, value);
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.RegisterAttached("Image", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));

        public static String GetCaption(DependencyObject obj)
        {
            return (String)obj.GetValue(CaptionProperty);
        }

        public static void SetCaption(DependencyObject obj, String value)
        {
            obj.SetValue(CaptionProperty, value);
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.RegisterAttached("Caption", typeof(String), typeof(ImageButton), new UIPropertyMetadata(null));
    }
}
