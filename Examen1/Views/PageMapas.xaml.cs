using Examen1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Examen1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMapas : ContentPage
    {
        public PageMapas()
        {
            InitializeComponent();
        }

        public PageMapas(double latitud, double longitud) : this()
        {
            var posicion = new Position(latitud, longitud);
            mapa.MoveToRegion(MapSpan.FromCenterAndRadius(posicion, Distance.FromKilometers(1)));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    var posicion = new Position(location.Latitude, location.Longitude);
                    mapa.MoveToRegion(MapSpan.FromCenterAndRadius(posicion, Distance.FromKilometers(1)));
                }
            }
            catch(Exception ex)
            { 
            }
        }
    }
}