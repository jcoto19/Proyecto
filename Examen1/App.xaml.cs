using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Examen1
{
    public partial class App : Application
    {
        static Controllers.ContactosControllers bdContactos;
        static Controllers.SitiosControllers bdsitios;
        public static string DBPath { get; set; }

        public static Controllers.ContactosControllers BDContactos
        {
            get
            {
                if (bdContactos == null)
                {
                    var dbpath  = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    var dbname = "Contactos.db3";
                    bdContactos = new Controllers.ContactosControllers(Path.Combine(dbpath, dbname));
                }
                return bdContactos;
            }
        }

        public static Controllers.SitiosControllers BDSitios
        {
            get
            {
                if (bdsitios == null)
                {
                    var dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    var dbname = "Sitios.db3";
                    bdsitios = new Controllers.SitiosControllers(Path.Combine(dbpath, dbname));
                }
                return bdsitios;
            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.PageInicio());
            DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contactos.db");

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
