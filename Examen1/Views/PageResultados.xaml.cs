using Examen1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using System.Diagnostics.Contracts;
using Xamarin.Forms.PlatformConfiguration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Plugin.Messaging;
using Examen1.Controllers;


namespace Examen1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageResultados : ContentPage
    {
        private ContactosControllers contactosControllers;
        private Contactos _contactoSeleccionado;
        public PageResultados()
        {
            InitializeComponent();
            contactosControllers = new ContactosControllers(App.DBPath);
        }
        
        
        
        private void ListaContactos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            Editar.IsEnabled = true;
            Borrar.IsEnabled = true;
            var contacto = e.Item as Models.Contactos;

            VerImagen.IsEnabled = true;
            //if (contacto.Foto != null)
            //{
                //await DisplayAlert("Confirmar selección", "Foto cargada", "Sí", "No");
                imgFoto.Source = ImageSource.FromFile(contacto.Foto);
            //}
            //else
            //{
            //  await DisplayAlert("Confirmar selección", "No hay foto disponible", "Sí", "No");
            //}
            
                _contactoSeleccionado = e.Item as Contactos;
            


            var answer = await DisplayAlert("Confirmar selección", $"¿Desea llamar a {contacto.Nombre}?", "Sí", "No");

            if (answer)
            {
                var number = contacto.Telefono;
                if (!string.IsNullOrEmpty(number))
                {
                    try
                    {
                        var phoneCallUri = new Uri($"tel:{number}");
                        await Launcher.OpenAsync(phoneCallUri);
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "Aceptar");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "El número de teléfono está vacío.", "Aceptar");
                }
            }
        }
              
        

        protected async override void OnAppearing()
        {
            base.OnAppearing();        

            ListaContactos.ItemsSource = await App.BDContactos.GetListContactos();
        }
                
        private async void Llamar_Clicked(object sender, EventArgs e)
        {
            Contactos contacto = (Contactos)ListaContactos.SelectedItem;
            string phoneNumber = contacto.Telefono;

            // Verificar si se ha otorgado el permiso para hacer una llamada telefónica
            if (await Permissions.CheckStatusAsync<Permissions.Phone>() != PermissionStatus.Granted)
            {
                // Si no se ha otorgado, solicitar el permiso
                await Permissions.RequestAsync<Permissions.Phone>();
            }

            // Intentar hacer la llamada
            try
            {
                PhoneDialer.Open(phoneNumber);
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que pueda ocurrir al hacer la llamada
            }

        }

        private async void VerImagen_Clicked(object sender, EventArgs e)
        {
            if (_contactoSeleccionado != null)
            {
                var pagePopup = new Views.PageImagenPopup(ImageSource.FromFile(_contactoSeleccionado.Foto));
                await Navigation.PushAsync(pagePopup);
            }
        }

        private async void Borrar_Clicked(object sender, EventArgs e)
        {
            if (_contactoSeleccionado == null)
            {
                await DisplayAlert("Advertencia", "No hay elemento seleccionado", "Aceptar");
                return;
            }

            var respuesta = await DisplayAlert("Confirmar Eliminar", "¿Está seguro que desea eliminar este contacto?", "Sí", "No");

            if (respuesta)
            {
                await contactosControllers.DeleteContacto(_contactoSeleccionado);
                await DisplayAlert("Eliminado", "El contacto ha sido eliminado correctamente", "Aceptar");

                // Actualizar la lista de contactos
                OnAppearing();
            }

        }

        private async void ActualizarListaContactos()
        {
            var contactos = await contactosControllers.GetContactosAsync();
            ListaContactos.ItemsSource = contactos;
        }

    }


}

