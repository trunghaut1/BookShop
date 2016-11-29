using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.IO;
using DevExpress.Xpf.Editors.Internal;
using DevExpress.Xpf.Editors;
using Microsoft.Win32;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Core;

namespace BookShop.Admin.Converter
{
    public class MyImageEdit : ImageEdit
    {
        public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register("ImagePath", typeof(string), typeof(MyImageEdit), null);
        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        protected override void LoadCore()
        {
            if (Image == null)  
                return;

            ImageSource image = LoadImage();

            if (image != null)
                EditStrategy.SetImage(image);
        }
        ImageSource LoadImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = EditorLocalizer.GetString(EditorStringId.ImageEdit_OpenFileFilter);
            if (dlg.ShowDialog() == true)
            {
                using (Stream stream = dlg.OpenFile())
                {
                    if (stream is FileStream)
                        ImagePath = ((FileStream)stream).Name;
                    MemoryStream ms = new MemoryStream(stream.GetDataFromStream());
                    return ImageHelper.CreateImageFromStream(ms);
                }
            }
            return null;
        }
    }
}
