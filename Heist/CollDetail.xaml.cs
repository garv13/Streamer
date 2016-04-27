using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class CollDetail : Page
    {
        public CollDetail()
        {
            this.InitializeComponent();
        }
        private IMobileServiceTable<Chapter> Table = App.MobileService.GetTable<Chapter>();
        private MobileServiceCollection<Chapter, Chapter> items;
        int Price=0;
        private IMobileServiceTable<Book> Table2 = App.MobileService.GetTable<Book>();
        private MobileServiceCollection<Book, Book> items2;
        Collections rec;
        List<string> book;
        List<string> bookName;
        List<string> Chap;
        List<string> ChapName;

        List<CollView> CollList;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            CollList = new List<CollView>();
             book = new List<string>();
            Chap = new List<string>();

             bookName = new List<string>();
             ChapName = new List<string>();
            LoadingBar.IsIndeterminate = true;
            LoadingBar.Visibility = Visibility.Visible;
            rec = new Collections();
            rec = e.Parameter as Collections;
            Title.Text = rec.Name;
            Author.Text = rec.CreatedBy;
            FullCost.Text = "Full Collection Price: ";
            string[] lis = rec.books.Split(',');
            for(int i=0;i<lis.Length;i++)
            {
                string[] temp = lis[i].Split('.');
                book.Insert(i, temp[0]);
                Chap.Insert(i, temp[1]);
            }
            foreach (string lol in book)
            {
                items2 = await Table2.Where(Book
                                   => Book.Id == lol ).ToCollectionAsync();
                bookName.Insert(bookName.Count, items2[0].Title);
            }
            foreach (string lol in Chap)
            {
                items = await Table.Where(Chapter
                                   => Chapter.Id == lol).ToCollectionAsync();
                ChapName.Insert(ChapName.Count, "Chapter no. " + (items[0].sno+1).ToString());
                Price += items[0].price;
            }
            FullCost.Text = Price.ToString();
            for (int i = 0; i < ChapName.Count; i++)
            {
                CollView temp = new CollView();
                temp.Book = bookName[i];
                temp.Chapter = ChapName[i];
                CollList.Insert(CollList.Count, temp);
            }
            StoreListView.ItemsSource = CollList;
            LoadingBar.Visibility = Visibility.Collapsed;
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
            Frame.Navigate(typeof(Purchased));
        }

        private void MenuButton4_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Store));
        }

        private void MenuButton7_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyCollection));
        }

        private void MenuButton5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private async void MenuButton6_Click(object sender, RoutedEventArgs e)
        {
            await (new MessageDialog("You are successfully loged out :):)")).ShowAsync();
            Frame.Navigate(typeof(Login));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
