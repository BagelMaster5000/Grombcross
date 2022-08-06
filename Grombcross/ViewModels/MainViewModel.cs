using Grombcross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Grombcross.ViewModels {
    public class MainViewModel : ViewModelBase {
        public ViewModelBase CurrentViewModel { get; set; }

        // Transitioning variables
        private Timer _transitionInTimer;
        public Action OnTransitionIn;
        public float TransitionInDuration = 0.35f;

        private Timer _transitionOutTimer;
        public Action OnTransitionOut;
        public float TransitionOutDuration = 0.6f;

        public Action OnTransitionFinished;
        private ViewModelBase queuedViewModel = null;

        #region Setup
        public MainViewModel() {
            InitializeTransitionTimer();

            ShowPuzzleSelectView();
        }

        private void InitializeTransitionTimer() {
            _transitionInTimer = new Timer(TransitionInDuration * 1000);
            _transitionInTimer.Elapsed += (object? sender, ElapsedEventArgs e) => {
                Application.Current.Dispatcher.Invoke(() => {
                    StartTransitioningOut();
                    ShowQueuedViewModel();
                });
            };
            _transitionInTimer.AutoReset = false;

            _transitionOutTimer = new Timer(TransitionOutDuration * 1000);
            _transitionOutTimer.Elapsed += (object? sender, ElapsedEventArgs e) => {
                Application.Current.Dispatcher.Invoke(() => {
                    FinishedTransitioning();
                });
            };
            _transitionOutTimer.AutoReset = false;
        }
        #endregion


        #region Transition
        private void StartTransitioningIn() {
            _transitionInTimer.Enabled = true;
            _transitionInTimer.Start();

            OnTransitionIn?.Invoke();
        }
        private void StartTransitioningOut() {
            _transitionOutTimer.Enabled = true;
            _transitionOutTimer.Start();

            OnTransitionOut?.Invoke();
        }
        private void FinishedTransitioning() {
            OnTransitionFinished?.Invoke();
        }

        private bool ShowOrQueueViewModel(ViewModelBase setViewModel) {
            if (CurrentViewModel == null) {
                CurrentViewModel = setViewModel;
                return false;
            }
            else {
                QueueViewModel(setViewModel);
                return true;
            }
        }
        private void ShowQueuedViewModel() {
            if (queuedViewModel == null) {
                throw new Exception("No queued view model for after transition");
            }

            CurrentViewModel = queuedViewModel;
            queuedViewModel = null;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        private void QueueViewModel(ViewModelBase setQueuedViewModel) {
            queuedViewModel = setQueuedViewModel;
        }
        #endregion

        #region Showing ViewModels
        public bool ShowPuzzleSelectView() {
            PuzzleSelectViewModel selectViewModel = new PuzzleSelectViewModel(ShowCreditsView, ShowPuzzleGameView);
            bool queuedViewModel = ShowOrQueueViewModel(selectViewModel);

            if (queuedViewModel) {
                StartTransitioningIn();
            }

            return true;
        }

        public bool ShowPuzzleGameView(int puzzleIndex) {
            PuzzleGameModel gameModel = new PuzzleGameModel(puzzleIndex);
            PuzzleGameViewModel gameViewModel = new PuzzleGameViewModel(gameModel, ShowPuzzleSelectView);
            bool queuedViewModel = ShowOrQueueViewModel(gameViewModel);

            if (queuedViewModel) {
                StartTransitioningIn();
            }

            return true;
        }

        public bool ShowCreditsView() {
            CreditsViewModel creditsViewModel = new CreditsViewModel(ShowPuzzleSelectView);
            bool queuedViewModel = ShowOrQueueViewModel(creditsViewModel);

            if (queuedViewModel) {
                StartTransitioningIn();
            }

            return true;
        }
        #endregion
    }
}
