using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Examen1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageImagenPopup : ContentPage
    {
        public PageImagenPopup(ImageSource imageSource)
        {
            InitializeComponent();
            imgPopup.Source = imageSource;
            System.Diagnostics.Debug.WriteLine(imageSource.ToString());
        }
        

    }
}