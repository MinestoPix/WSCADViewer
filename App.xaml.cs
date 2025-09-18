using System.Windows;

namespace WSCADViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Domain.ShapeRegistration.RegisterAll();

            //var mainWindow = new Views.MainWindow();
            //mainWindow.Show();
        }
    }
}
