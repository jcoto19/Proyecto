using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageResultados : ContentPage
    {
        public PageResultados()
        {
            InitializeComponent();
        }

        private void ListaContactos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            ListaContactos.ItemsSource= await App.BDContactos.GetListContactos();
           
        }
    }
}