using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class StoreDetail : Page
    {
        private IMobileServiceTable<User> Table2 = App.MobileService.GetTable<User>();
        private MobileServiceCollection<User, User> items2;
        private StoreListing rec;
        private IMobileServiceTable<Chapter> Table = App.MobileService.GetTable<Chapter>();
        private MobileServiceCollection<Chapter, Chapter> items;
        string testlol;
        private List<ChapterView> list;
        public StoreDetail()
        {
            this.InitializeComponent();
            Loaded += StoreDetail_Loaded;
            rec = new StoreListing();
            rec.Title = "ajdgivfvw";
           
        }

        private void StoreDetail_Loaded(object sender, RoutedEventArgs e)
        {

            while (rec.Title == "ajdgivfvw")
            { }
           
            Cover.Source = rec.Image;
            Title.Text = rec.Title;
            Author.Text = rec.Author;
            FullCost.Text = "Full Book Price: "+rec.Price;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await folder.GetFileAsync("sample.txt");

            testlol = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            rec = e.Parameter as StoreListing;
       
            list = new List<ChapterView>();
            ChapterView temp;
            try
            {
                items = await Table.Where(Chapter
                            => Chapter.bookid == rec.Id).ToCollectionAsync();
               
                foreach (Chapter lol in items)
                {
                    temp = new ChapterView();
                    temp.Id = lol.Id;
                    temp.Title = "Chapter No: " + (lol.sno+1).ToString();
                    temp.Price = "Price: "+ lol.price.ToString();
                    list.Add(temp);
                }
                StoreListView.ItemsSource = list;
                
            }
            catch (Exception ex)
            {
                //TODO Something here
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

        private  void MenuButton4_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Store));
        }


        private void MenuButton5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO Download full book and add entry to user purchases
            items2 = await Table2.Where(User
                            => User.username == testlol).ToCollectionAsync();
            User a = items2[0];
            if (!a.purchases.Contains(rec.Id + ".full"))
            {
                a.purchases += rec.Id + ".full";
                await Table2.UpdateAsync(a);
                Windows.UI.Popups.MessageDialog mess = new Windows.UI.Popups.MessageDialog("Purchase successfull! Download the file from My purchase section");
            }
            else
            {
                Windows.UI.Popups.MessageDialog mess = new Windows.UI.Popups.MessageDialog("You have already purchased this!");
            }

        }

        private async void Buy_Click(object sender, RoutedEventArgs e)
        {
            //take rec.id to send in post with header id

            var test = sender as Button;
            var test2 = test.Parent as Grid;
            var test3 = test2.Children[3] as TextBlock;
            items2 = await Table2.Where(User
                           => User.username == testlol).ToCollectionAsync();
            User a = items2[0];
            if (!a.purchases.Contains(rec.Id+"."+test3.Text )&& !a.purchases.Contains(rec.Id + ".full"))
            {
                a.purchases += rec.Id + "." + test3.Text+",";
                await Table2.UpdateAsync(a);
                Windows.UI.Popups.MessageDialog mess = new Windows.UI.Popups.MessageDialog("Purchase successfull! Download the file from My purchase section");
            }
            else
            {
                Windows.UI.Popups.MessageDialog mess = new Windows.UI.Popups.MessageDialog("You have already purchased this!");
            }

        }
    }
}
