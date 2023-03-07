using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Examen1
{
    public partial class App : Application
    {
        static Controllers.ContactosControllers bdContactos;

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
        
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.PageInicio());
                
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
