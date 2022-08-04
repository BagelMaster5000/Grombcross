using Grombcross.ViewModels;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Grombcross {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        MainViewModel mainViewModel;
        private Duration transitionInDuration;
        private Duration transitionOutDuration;

        public MainWindow() {
            Loaded += OnLoaded;

            InitializeComponent();

            TransitionOverlay.Opacity = 0;
            TransitionOverlay.IsHitTestVisible = false;
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            mainViewModel = DataContext as MainViewModel;


            transitionInDuration = new Duration(TimeSpan.FromSeconds(mainViewModel.TransitionInDuration));
            transitionOutDuration = new Duration(TimeSpan.FromSeconds(mainViewModel.TransitionOutDuration));

            mainViewModel.OnTransitionIn += TransitionInAnimation;
            mainViewModel.OnTransitionOut += TransitionOutAnimation;
            mainViewModel.OnTransitionFinished += TransitionFinished;
        }

        private void TransitionInAnimation() {
            DoubleAnimation opacityAnimation = new DoubleAnimation(1, transitionInDuration);
            TransitionOverlay.BeginAnimation(OpacityProperty, opacityAnimation);
            TransitionOverlay.IsHitTestVisible = true;
        }

        private void TransitionOutAnimation() {
            DoubleAnimation opacityAnimation = new DoubleAnimation(0, transitionOutDuration);
            TransitionOverlay.BeginAnimation(OpacityProperty, opacityAnimation);
        }

        private void TransitionFinished() {
            TransitionOverlay.IsHitTestVisible = false;
        }
    }
}
