using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookShop.Admin.Converter
{
    public class TextBoxMaskBehavior
    {
        public static MaskType GetMask(DependencyObject obj)
        {
            return (MaskType)obj.GetValue(MaskProperty);
        }

        public static void SetMask(DependencyObject obj, MaskType value)
        {
            obj.SetValue(MaskProperty, value);
        }

        public static readonly DependencyProperty MaskProperty =
           DependencyProperty.RegisterAttached(
           "Mask",
           typeof(MaskType),
           typeof(TextBoxMaskBehavior),
           new FrameworkPropertyMetadata(MaskChangedCallback)
           );

        private static void MaskChangedCallback(DependencyObject d,
                            DependencyPropertyChangedEventArgs e)
        {
            TextBox textbox = d as TextBox;
            textbox.PreviewTextInput += new TextCompositionEventHandler(NumberValidationTextBox);
        }
        private static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
