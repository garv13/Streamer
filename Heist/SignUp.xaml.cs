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
            User a = new User();
            int i = 1;

            if (!Regex.Match(Name.Text, "^[A-Z][a-zA-Z]*$").Success)
                i = 0;

            if (!Regex.Match(Mobile.Text, @"^[1-9]\d{2}-[1-9]\d{2}-\d{4}$").Success)
                i = 0;
            if (!Regex.Match(Email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$").Success)
                i = 0;

            if (!Regex.Match(UserName.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$").Success)
                i = 0;

            if (!Regex.Match(Password.Password, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$").Success)
                i = 0;

            if (i == 0)
            {
                MessageDialog msgbox = new MessageDialog("Password or username incorrect");
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
                //add object a in cloud
            }
        }
    }
}
