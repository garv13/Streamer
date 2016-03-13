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
    public sealed partial class Purchaseddetail : Page
    {
        private IMobileServiceTable<User> Table2 = App.MobileService.GetTable<User>();
        private MobileServiceCollection<User, User> items2;
        private PurchasedView rec;
        private IMobileServiceTable<Chapter> Table = App.MobileService.GetTable<Chapter>();
        private MobileServiceCollection<Chapter, Chapter> items;
        string testlol;
        private List<ChapterView> list;
        public Purchaseddetail()
        {
            this.InitializeComponent();
            Loaded += Purchaseddetail_Loaded;
            rec = new PurchasedView();
            rec.sel.Title = "ajdgivfvw";
        }

        private void Purchaseddetail_Loaded(object sender, RoutedEventArgs e)
        {
            while (rec.sel.Title == "ajdgivfvw")
            { }

            Cover.Source = rec.sel.Image;
            Title.Text = rec.sel.Title;
            Author.Text = rec.sel.Author;
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            rec = e.Parameter as PurchasedView;
            List<string> chaps = new List<string>();
            string[] lol = rec.purchases.Split(',');
            for (int i = 0; i < lol.Length; i++)
            {
                string test3 = lol[i];
                string[] test4 = test3.Split('.');
                if (test4[0] == rec.sel.Id)
                {
                    if (test4[1] != "full")
                        chaps.Add(test4[1]);
                    else {
                        items = await Table.Where(Chapter
            => Chapter.bookid == rec.sel.Id).ToCollectionAsync();
                        foreach(Chapter lol2 in items)
                        {
                            chaps.Add(lol2.Id);

                        }
                    }
                }
            }

            list = new List<ChapterView>();
            ChapterView temp;
            try
            {
                items = await Table.Where(Chapter
                            => chaps.Contains(Chapter.Id)).ToCollectionAsync();
             

                foreach (Chapter lol2 in items)
                {
                    temp = new ChapterView();
                    temp.Id = lol2.Id;
                    temp.Title = "Chapter No: " + (lol2.sno + 1).ToString();
                    temp.Price = "Price: " + lol2.price.ToString();
                    list.Add(temp);
                }
                StoreListView.ItemsSource = list;

            }
            catch (Exception ex)
            {
                //TODO Something here
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            var test = sender as Button;
            var test2 = test.Parent as Grid;
            var test3 = test2.Children[2] as TextBlock;
            //test3.text has id
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

        private void MenuButton4_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Store));
        }


        private void MenuButton5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }
    }
}
