using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInicio : ContentPage
    {
        public PageInicio()
        {
            InitializeComponent();
            
        }        
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {

        }

        private void AgregarContacto_Clicked(object sender, EventArgs e)
        {
            var pageContacto = new Views.PageIngreso();
            Navigation.PushAsync(pageContacto);
        }

        private void ToolbarItem_DatosGuardados(object sender, EventArgs e)
        {
            var pageContacto = new Views.PageResultados();
            Navigation.PushAsync(pageContacto);
        }

        private void SitiosGuardado_Clicked(object sender, EventArgs e)
        {
            var pageSitios = new Views.PageSitios();
            Navigation.PushAsync(pageSitios);
        }

        private void VerSitios_Clicked(object sender, EventArgs e)
        {
            var pageVerSitios = new Views.PageResultadoSitios();
            Navigation.PushAsync(pageVerSitios);
            
        }
    }
}