using System;
using System.Windows;
using System.Windows.Controls;

namespace ToursApp
{
    public partial class MainWindow : Window
    {
        private string userRole;

        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;
            userRole = "Гость"; 
            MainFrame.Navigate(new Страница_2(userRole));
        }

        public MainWindow(string role)
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;
            userRole = role;
            MainFrame.Navigate(new Страница_2(userRole));
        }

        public string GetUserRole()
        {
            return userRole;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.Content is Страница_2)
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnBack.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;
            }
        }
    }
}