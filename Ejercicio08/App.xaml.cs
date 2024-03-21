using Ejercicio08.Services;
using System;
using Ejercicio08.View;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio08
{
    public partial class App : Application
    {
        static TareasDataBaseService tareasDatabase;

        public static TareasDataBaseService TareasDataBase 
        {
            get
            {
                if (tareasDatabase == null)
                {
                    tareasDatabase = new TareasDataBaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamarinAppDb.db3"));
                }
                return tareasDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new TareasView();
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
