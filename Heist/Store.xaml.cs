using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Store : Page
    {
        private IMobileServiceTable<Events> Table = App.MobileService.GetTable<Events>();
        private MobileServiceCollection<Events, Events> items;
        public Store()
        {
            this.InitializeComponent();
            Loaded += Store_Loaded;
        }

        private void Store_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
