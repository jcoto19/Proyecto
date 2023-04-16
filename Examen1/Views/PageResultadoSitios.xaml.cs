using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageResultadoSitios : ContentPage
	{
		public PageResultadoSitios ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ListaSitios.ItemsSource = await App.BDSitios.GetListSitios();

        }

        private void ListaSitios_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Editar.IsEnabled= true;
            Borrar.IsEnabled= true;
            var sitio = e.Item as Models.SitiosVisitadoscs;
            var answer = await DisplayAlert("Confirmar selección", $"¿Desea seleccionar el sitio {sitio.Sitio}?", "Sí", "No");

            if (answer)
            {
                // Aquí puedes hacer lo que quieras si el usuario selecciona "Sí", por ejemplo, navegar a otra página
                var pagemapa = new Views.PageMapas(sitio.latitud, sitio.longitud);
                await Navigation.PushAsync(pagemapa);
            }
            else
            {
                // El usuario seleccionó "No", por lo que no hacemos nada
                // Pero si quisieras hacer algo en este caso, puedes agregarlo aquí
            }
        }
    }
}