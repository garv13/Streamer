using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class Store : Page
    {
        private IMobileServiceTable<Book> Table = App.MobileService.GetTable<Book>();
        private MobileServiceCollection<Book, Book> items;
        private List<string> BookNames;
        private List<StoreListing> StoreList;
        public Store()
        {
            this.InitializeComponent();
            Loaded += Store_Loaded;
        }

        private async void Store_Loaded(object sender, RoutedEventArgs e)
        {
            Box.Visibility = Visibility.Collapsed;
            SearchButton.Visibility = Visibility.Collapsed;
            StoreListView.Visibility = Visibility.Collapsed;
            BookNames = new List<string>();
            StoreList = new List<StoreListing>();
            LoadingBar.IsIndeterminate = true;
            StoreListing temp;
            try
            {
                items = await Table.Where(Book
                        => Book.IsReady == true).ToCollectionAsync();
                foreach(Book lol in items)
                {
                    temp = new StoreListing();
                    BookNames.Add(lol.Title);
                    temp.Title = lol.Title;
                    temp.Author = lol.Author;
                    temp.Image = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(lol.ImageUri2));
                    temp.Id = lol.Id;
                    StoreList.Add(temp);
                }
                Box.AutoCompleteSource = BookNames;
                StoreListView.ItemsSource = StoreList;
                Box.Visibility = Visibility.Visible;
                StoreListView.Visibility = Visibility.Visible;
                SearchButton.Visibility = Visibility.Visible;
                LoadingBar.Visibility = Visibility.Collapsed;
                
            }
            catch (Exception ex)
            {
                //TODO: Add some thing here
            }
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void MenuButton2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Downloads));
        }

        private void MenuButton3_Click(object sender, RoutedEventArgs e)
        {
            //upgrade option 
        }

        private async void MenuButton4_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Store));
        }


        private void MenuButton5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }


        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StoreListView.Visibility = Visibility.Collapsed;
            SearchButton.Visibility = Visibility.Collapsed;
            LoadingBar.Visibility = Visibility.Visible;
            StoreList = new List<StoreListing>();
            LoadingBar.IsIndeterminate = true;
            StoreListing temp;
            try
            {
                items = await Table.Where(Book
                        => Book.Title.Contains(Box.Text)).ToCollectionAsync();
                foreach (Book lol in items)
                {
                    temp = new StoreListing();
                    temp.Id = lol.Id;
                    temp.Title = lol.Title;
                    temp.Author = lol.Author;
                    temp.Image = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(lol.ImageUri2));
                    StoreList.Add(temp);
                }
                
                StoreListView.ItemsSource = StoreList;
                Box.Visibility = Visibility.Visible;
                StoreListView.Visibility = Visibility.Visible;
                SearchButton.Visibility = Visibility.Visible;
                LoadingBar.Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                //TODO: Add some thing here
            }
        }

        
        private void StoreListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            StoreListing sent = e.ClickedItem as StoreListing;
            Frame.Navigate(typeof(StoreDetail), sent);
        }
    }
}
