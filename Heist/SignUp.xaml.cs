using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Heist
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadingBar.Visibility = Visibility.Visible;
            LoadingBar.IsIndeterminate = true;
            User a = new User();
            int i = 1;

            if (!(Name.Text.All(char.IsLetter) && Name.Text.Length != 0))
            {
                i = 0;
            }

            if (!(Mobile.Text.All(char.IsDigit) && Mobile.Text.Length == 10))
            {
                i = 0;
            }

            if (!Regex.Match(Email.Text, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$").Success)
                i = 0;


            if (!(UserName.Text.All(char.IsLetterOrDigit) && UserName.Text.Length != 0))
            {
                i = 0;
            }

            if (Password.Password.Length < 8)
            {
                MessageDialog msgbox = new MessageDialog("Password length must be 8");
                await msgbox.ShowAsync();
            }

            if (i == 0)
            {
                MessageDialog msgbox = new MessageDialog("Enter Correct Information");
                await msgbox.ShowAsync();
            }

            else
            {
                a.name = Name.Text;
                a.phone = Mobile.Text;
                a.email = Email.Text;
                a.password = Password.Password;
                a.username = UserName.Text;
                a.wallet = 0;
                a.purchases = "";
                await App.MobileService.GetTable<User>().InsertAsync(a);

                MessageDialog msgbox = new MessageDialog("Register Successful");
                await msgbox.ShowAsync();
                LoadingBar.Visibility = Visibility.Collapsed;
                Frame.Navigate(typeof(Login));
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
