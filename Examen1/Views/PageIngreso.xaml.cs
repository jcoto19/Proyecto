using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Examen1.Models;
using Plugin.Media.Abstractions;

namespace Examen1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageIngreso : ContentPage
    {

        private MediaFile photo;

        private string ImageToBase64()
        {
            if (photo != null && imgFoto.Source != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] bytefoto = memory.ToArray();
                    string fotoBase64 = Convert.ToBase64String(bytefoto);
                    return fotoBase64;
                }
            
            }
            return null;
        }

        public PageIngreso()
        {
            InitializeComponent(); 
            
        }        
        private async void btnGuardarContacto_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                await DisplayAlert("Aviso", "Debe agregar un nombre", "Ok");
                txtNombres.Focus();
            }

            else
                if (string.IsNullOrEmpty(txtNota.Text))
            {
                await DisplayAlert("Aviso", "Debe agregar una nota", "Ok");
                txtNota.Focus();
            }
            else
            {
                var contacto = new Models.Contactos
                {
                    Nombre = txtNombres.Text,
                    Telefono = txtTelefono.Text,
                    Foto = ImageToBase64(),
                    Edad = int.Parse(txtEdad.Text),
                    Pais = cbPais.SelectedItem.ToString(),
                    Nota = txtNota.Text,

            };
                

                if (await App.BDContactos.SaveContacto(contacto) > 0)
                    await DisplayAlert("Aviso", "Contacto Guardado", "Ok");
                else
                    await DisplayAlert("Aviso", "Error", "Ok");

                var page = new Views.PageResultados();
                page.BindingContext = contacto;
                await Navigation.PushAsync(page);
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
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Galería no soportada", "No es posible acceder a la galería en este dispositivo.", "Aceptar");
                    return;
                }

                var mediaOptions = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                photo = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (photo != null)
                {
                    imgFoto.Source = ImageSource.FromStream(() => photo.GetStream());
                    ImageToBase64();
                    await DisplayAlert("Mensaje", "Foto Cargada", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }



        private void cargarGPS_Clicked(object sender, EventArgs e)
        {
            var pagemapa = new Views.PageMapas();
            Navigation.PushAsync(pagemapa);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var location= await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    //txtLatitud.Text = Convert.ToString(location.Latitude);
                    //txtLongitud.Text= Convert.ToString(location.Longitude);

                    Console.WriteLine($"Latitud: {location.Latitude}, Longitud: {location.Longitude}");
                }
            }
            catch (Exception ex) 
            { 
            }
            
            
        }
    }
}