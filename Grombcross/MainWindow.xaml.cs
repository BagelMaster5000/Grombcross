using Grombcross.ViewModels;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Grombcross {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        MainViewModel mainViewModel;

        private Duration transitionOverlayFadeInDuration;
        private Duration transitionOverlayFadeOutDuration;

        private Duration transitionBackgroundScaleInDuration;
        private Duration transitionBackgroundScaleOutDuration;

        public MainWindow() {
            Loaded += OnLoaded;

            InitializeComponent();

            TransitionOverlay.Opacity = 0;
            TransitionOverlay.IsHitTestVisible = false;

            Background.RenderTransform = new ScaleTransform() { ScaleX = 1, ScaleY = 1, CenterX = 0.5, CenterY = 0.5 };
            Background.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            mainViewModel = DataContext as MainViewModel;


            transitionOverlayFadeInDuration = new Duration(TimeSpan.FromSeconds(mainViewModel.TransitionInDuration));
            transitionOverlayFadeOutDuration = new Duration(TimeSpan.FromSeconds(mainViewModel.TransitionOutDuration));

            transitionBackgroundScaleInDuration = new Duration(TimeSpan.FromSeconds(mainViewModel.TransitionInDuration));
            transitionBackgroundScaleOutDuration = new Duration(TimeSpan.FromSeconds(mainViewModel.TransitionOutDuration));

            mainViewModel.OnTransitionIn += TransitionInAnimation;
            mainViewModel.OnTransitionOut += TransitionOutAnimation;
            mainViewModel.OnTransitionFinished += TransitionFinished;
        }

        private void TransitionInAnimation() {
            DoubleAnimation opacityAnimation = new DoubleAnimation(1, transitionOverlayFadeInDuration);
            opacityAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            TransitionOverlay.BeginAnimation(OpacityProperty, opacityAnimation);
            TransitionOverlay.IsHitTestVisible = true;

            DoubleAnimation backgroundScaleAnimation = new DoubleAnimation(1.5, transitionBackgroundScaleInDuration);
            backgroundScaleAnimation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Background.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, backgroundScaleAnimation);
            Background.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, backgroundScaleAnimation);
        }

        private void TransitionOutAnimation() {
            DoubleAnimation opacityAnimation = new DoubleAnimation(0, transitionOverlayFadeOutDuration);
            //opacityAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            TransitionOverlay.BeginAnimation(OpacityProperty, opacityAnimation);

            DoubleAnimation backgroundScaleAnimation = new DoubleAnimation(1, transitionBackgroundScaleOutDuration);
            backgroundScaleAnimation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Background.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, backgroundScaleAnimation);
            Background.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, backgroundScaleAnimation);
        }

        private void TransitionFinished() {
            TransitionOverlay.IsHitTestVisible = false;
        }
    }
}
