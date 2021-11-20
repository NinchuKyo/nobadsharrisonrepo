using FroggoBase;
using FroggoBase.Views;
using RandoThing.ViewModels;
using RandoThing.Views;
using System;

namespace RandoThing
{
    /// <summary>
    /// Entry-point for the randomizer app program.
    /// </summary>
    public static class RandoThing
    {
        /// <summary>
        /// Main entry-point into the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Exit code.</returns>
        [STAThread]
        public static int Main(string[] args)
        {
            FroggoApp app = new RandoThingApp();

            FroggoMainWindow mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };

            app.MainWindow = mainWindow;

            return app.Run(mainWindow);
        }
    }
}