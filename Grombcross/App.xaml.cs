using Grombcross.Models.Systems;
using Grombcross.ViewModels;
using System.Windows;

namespace Grombcross {

    public partial class App : Application {
        public App() { }

        protected override void OnStartup(StartupEventArgs e) {
            AudioSystem.InitializeMediaPlayers();
            PuzzleGenerationSystem.GeneratePuzzles();
            SaveSystem.LoadGame();

            MainWindow = new MainWindow() {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
