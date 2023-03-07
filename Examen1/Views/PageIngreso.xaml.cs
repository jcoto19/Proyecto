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
    public partial class PageIngreso : ContentPage
    {
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
                if (string.IsNullOrEmpty(txtApellidos.Text))
                {
                    await DisplayAlert("Aviso", "Debe agregar un apellido", "Ok");
                    txtApellidos.Focus();   
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
                Apellidos = txtApellidos.Text,
                Edad = int.Parse(txtEdad.Text),
                Pais = cbPais.SelectedItem.ToString(),
                Nota = txtNota.Text,
                latitud = double.Parse(txtLatitud.Text),
                longitud = double.Parse(txtLongitud.Text),
                
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
                    txtLatitud.Text = Convert.ToString(location.Latitude);
                    txtLongitud.Text= Convert.ToString(location.Longitude);

                    Console.WriteLine($"Latitud: {location.Latitude}, Longitud: {location.Longitude}");
                }
            }
            catch (Exception ex) 
            { 
            }
        }
    }
}