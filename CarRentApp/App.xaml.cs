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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();
            container.RegisterType<IDbContext, CarRentDbContext>();
            container.RegisterType<ICarService, CarService>();
            container.RegisterType<IDataGenerationService, DataGenerationService>();

            var context = container.Resolve<CarRentDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
                container.Resolve<IDataGenerationService>().SeedDatabase();
            }
            
            container.Resolve<MainWindow>().Show();
        }
    }
}
