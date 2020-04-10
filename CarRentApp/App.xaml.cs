using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using Unity;

namespace CarRentApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private UnityContainer container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            container = new UnityContainer();
            container.RegisterType<IDbContext, CarRentDbContext>();
            container.RegisterType<ISearchCarService, SearchCarService>();
            container.RegisterType<IRentCarService, RentCarService>();
            container.RegisterType<IDataGenerationService, DataGenerationService>();

            var context = container.Resolve<IDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
                container.Resolve<IDataGenerationService>().SeedDatabaseAsync().ContinueWith(_ => Dispatcher.Invoke(ShowMainWindow));
            }
            else
            {
                ShowMainWindow();
            }
        }

        private void ShowMainWindow() => container.Resolve<MainWindow>().Show();
    }
}
