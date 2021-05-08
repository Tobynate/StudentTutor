using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentTutor.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SubmitPasswordView.xaml
    /// </summary>
    public partial class SubmitPasswordView : MvxWpfView
    {
        public SubmitPasswordView()
        {
            InitializeComponent();
        }
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword; }
        }
        private void ConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).ConfirmPassword = ((PasswordBox)sender).SecurePassword; }
        }
    }
}
