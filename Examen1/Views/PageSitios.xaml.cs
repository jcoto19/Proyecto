using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageSitios : ContentPage
	{
		public PageSitios ()
		{
			InitializeComponent ();
		}

        private async void btnGuardarSitio_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSitio.Text))
            {
                await DisplayAlert("Aviso", "Debe agregar un nombre", "Ok");
                txtSitio.Focus();
            }

            else
                if (string.IsNullOrEmpty(txtNota.Text))
            {
                await DisplayAlert("Aviso", "Debe agregar una nota", "Ok");
                txtNota.Focus();
            }
            else
            {
                var sitio = new Models.SitiosVisitadoscs
                {
                    Sitio = txtSitio.Text, 
                    Pais = cbPais.SelectedItem.ToString(),
                    Nota = txtNota.Text,

                };
                if (await App.BDSitios.SaveContacto(sitio) >0)
                    await DisplayAlert("Aviso", "Sitio Guardado", "Ok");
                else
                    await DisplayAlert("Aviso", "Error", "Ok");

                //var page = new Views.PageResultados();
                //page.BindingContext = sitio;
                //await Navigation.PushAsync(page);
            }

        }

        private async void btnCamara_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Capturar foto"
                });

                imgFoto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async void btnGaleria_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Seleccionar foto"
                });

                imgFoto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private void VerSitios_Clicked(object sender, EventArgs e)
        {
            var pageSitios = new Views.PageSitios();
            Navigation.PushAsync(pageSitios);

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    txtLatitud.Text = Convert.ToString(location.Latitude);
                    txtLongitud.Text= Convert.ToString(location.Longitude);

                    Console.WriteLine($"Latitud: {location.Latitude}, Longitud: {location.Longitude}");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void VerSitio_Clicked(object sender, EventArgs e)
        {
            var pageMapas = new Views.PageMapas();
            Navigation.PushAsync(pageMapas);

        }
    }
}