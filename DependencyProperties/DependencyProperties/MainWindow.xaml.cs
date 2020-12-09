﻿namespace DependencyProperties
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (_, __) => MainWindowLoaded();
        }

        private void MainWindowLoaded()
        {
            DataContext = new MainWindowViewModel();
        }
    }
}