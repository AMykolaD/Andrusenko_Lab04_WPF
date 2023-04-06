using Andrusenko_Lab04_WPF.ViewModels;
using System.Collections.ObjectModel;
using System;
using System.Windows;

namespace Andrusenko_Lab04_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new PersonTableViewModel();
        }
    }
}
