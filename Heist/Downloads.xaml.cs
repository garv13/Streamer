using Microsoft.WindowsAzure.MobileServices;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Heist
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Downloads : Page
    {
        public Downloads()
        {
            this.InitializeComponent();
            lol();
        }
        private IMobileServiceTable<Book> Table2 = App.MobileService.GetTable<Book>();
        private MobileServiceCollection<Book, Book> items2;
        public BitmapImage Im { get; set; }

        StorageFolder openBook = null;

        async void lol()
        {
            LoadingBar.IsIndeterminate = true;
            await load();
            LoadingBar.Visibility = Visibility.Collapsed;
        }
        async Task retreive(string name)
        {
            try
            {
                StorageFolder mainFol = await ApplicationData.Current.LocalFolder.CreateFolderAsync("My Books", CreationCollisionOption.OpenIfExists);

                if (mainFol != null)
                {
                    StorageFolder folder = await mainFol.CreateFolderAsync(name, CreationCollisionOption.OpenIfExists);
                    openBook = folder;
                    List<GridClass> lg = new List<GridClass>();
                    GridClass gd = new GridClass();
                    IReadOnlyList<StorageFile> sf = await folder.GetFilesAsync();

                    items2 = await Table2.Where(Book
                                => Book.Title == folder.Name).ToCollectionAsync();
                    foreach (Book lol in items2)
                        Im = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(lol.ImageUri2));

                    foreach (StorageFile s in sf)
                    {
                        gd = new GridClass();
                        gd.title = "Chapter No:" + s.DisplayName;
                        gd.Image = Im;
                        gd.authName = null;
                        lg.Add(gd);
                    }
                    event1.Visibility = Visibility.Collapsed;
                    event2.ItemsSource = lg;
                    event2.Visibility = Visibility.Visible;
                }
            }
            catch(Exception)
            {
                await (new MessageDialog("ahhmm Something not so good Happened :(:(")).ShowAsync();
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
            Frame.Navigate(typeof(Purchased));
        }

        private void MenuButton4_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Store));
        }


        private void MenuButton5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }


        private async Task load()
        {
            try
            {
                List<GridClass> lg = new List<GridClass>();
                GridClass gd = new GridClass();
                StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("My Books", CreationCollisionOption.OpenIfExists);
                IReadOnlyList<StorageFolder> sf = await folder.GetFoldersAsync();

                foreach (StorageFolder s in sf)
                {
                    gd = new GridClass();
                    items2 = await Table2.Where(Book
                                 => Book.Title == s.Name).ToCollectionAsync();
                    foreach (Book lol in items2)
                    {
                        gd.Image = new BitmapImage(new Uri(lol.ImageUri2));
                        gd.title = s.Name;
                        gd.authName = lol.Author;
                        lg.Add(gd);
                    }
                }

                if (lg.Count != 0)
                {
                    event1.ItemsSource = lg;

                }
                else
                {
                    LoadingBar.Visibility = Visibility.Collapsed;
                    await (new MessageDialog("Nothing Purchased")).ShowAsync();
                }
            }
            catch(Exception)
            {
                LoadingBar.Visibility = Visibility.Collapsed;
                await (new MessageDialog("Oops Something Bad Happened :(:(")).ShowAsync();
            }
        }

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            LoadingBar.IsIndeterminate = true;
            LoadingBar.Visibility = Visibility.Visible;
            Grid g = new Grid();
            g = sender as Grid;
            FrameworkElement auth = null;
            FrameworkElement titl = null;
            foreach (FrameworkElement child in g.Children)
            {
                if ((Grid.GetRow(child) == 0) && (Grid.GetColumn(child) == 1))
                {
                    titl = child;
                }


                if ((Grid.GetRow(child) == 1) && (Grid.GetColumn(child) == 1))
                {
                    auth = child;
                }
            }
            TextBlock t = auth as TextBlock;
            TextBlock t2 = titl as TextBlock;
            
            string str = t.Text;

            if (str != "")
                await retreive(t2.Text);
            else
                await printPdf(t2.Text.ElementAt<char>(t2.Text.Length-1).ToString()+".txt");
            LoadingBar.Visibility = Visibility.Collapsed;
        }

        private async Task printPdf(string text)
        {
            try
            {
                StorageFile file = await openBook.GetFileAsync(text);
                var l = await file.OpenAsync(FileAccessMode.Read);
                Stream str = l.AsStreamForRead();
                byte[] buffer = new byte[str.Length];
                str.Read(buffer, 0, buffer.Length);

                // Loads the PDF document.
                PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);
                TitlBox.Text = "Chapter " + text.ElementAt<char>(0).ToString();
                pdfViewer.LoadDocument(ldoc);
                event2.Visibility = Visibility.Collapsed;
                PdfGrid.Visibility = Visibility.Visible;
            }
            catch(Exception)
            {
                await (new MessageDialog("Can't open Pdf")).ShowAsync();
            }
        }
    }
}
