﻿using Grombcross.Models;
using Grombcross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grombcross.Views {
    /// <summary>
    /// Interaction logic for PuzzleGameView.xaml
    /// </summary>
    public partial class PuzzleGameView : UserControl {
        private PuzzleGameViewModel _dataContext;

        public PuzzleGameView() {
            Loaded += SetInstanceVariables;

            InitializeComponent();
        }

        private void LeftClickBlock(object sender, EventArgs e) {
            Button button = sender as Button;
            Block block = (Block)button.DataContext;
            _dataContext.LeftClickBlock(block);
        }

        private void RightClickBlock(object sender, EventArgs e) {

        }

        private void SetInstanceVariables(object sender, RoutedEventArgs routedEventArgs) {
            _dataContext = DataContext as PuzzleGameViewModel;
        }
    }
}
