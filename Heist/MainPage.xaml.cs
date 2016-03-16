using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using System.Net.Http;

namespace Heist
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
           // getdata();
        }


        async private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            var test = sender as Button;
            var test2 = test.Parent as Grid;
            var chapName = test2.Children[2] as TextBlock;
            var bokName = test2.Children[0] as TextBlock;

            StorageFolder mainFol = await ApplicationData.Current.LocalFolder.CreateFolderAsync("My Books", CreationCollisionOption.OpenIfExists);
            if (mainFol != null)
            {
                StorageFolder folder = await mainFol.CreateFolderAsync(bokName.Text, CreationCollisionOption.OpenIfExists);
                if (folder != null)
                {
                    StorageFile file = await folder.CreateFileAsync(chapName.Text.ElementAt<char>((chapName.Text.Length) - 1).ToString() + ".txt", CreationCollisionOption.OpenIfExists);
                    var l = await file.OpenAsync(FileAccessMode.Read);
                    Stream str = l.AsStreamForRead();
                    byte[] buffer = new byte[str.Length];
                    str.Read(buffer, 0, buffer.Length);

                    //Loads the PDF document.
                    PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);
                    pdfViewer.LoadDocument(ldoc);
                    PurchasedListView.Visibility = Visibility.Collapsed;
                    lol.Visibility = Visibility.Visible;
                }
            }
        }

        //private async void button_Click(object sender, RoutedEventArgs e)
        //{
        //    Uri url = new Uri("https://www.ebookstreamer.me/downloads");
        //    HttpClient httpClient = new HttpClient();
        //    var myClientHandler = new HttpClientHandler();
        //    myClientHandler.ClientCertificateOptions = ClientCertificateOption.Automatic;
        //    HttpResponseMessage httpResponse = new HttpResponseMessage();
        //    var content = new FormUrlEncodedContent(new[]
        //     {
        //        new KeyValuePair<string, string>("id", "F013D07A-CA56-47FE-A783-24E944AF1ED6")
        //    });
        //    httpResponse = await httpClient.PostAsync(url, content);
        //    httpResponse.EnsureSuccessStatusCode();
        //    Stream str = await httpResponse.Content.ReadAsStreamAsync();

        //    byte[] pd = new byte[str.Length];
        //    str.Read(pd, 0, pd.Length);

        //    PdfLoadedDocument ldoc = new PdfLoadedDocument(pd);
        //    pdfViewer.LoadDocument(ldoc);
        //    lol.Visibility = Visibility.Visible;

        //    StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("book name", CreationCollisionOption.OpenIfExists);
        //    if (folder != null)
        //    {
        //        StorageFile file = await folder.CreateFileAsync("Chapter no.txt", CreationCollisionOption.OpenIfExists);

        //        using (var fileStream = await file.OpenStreamForWriteAsync())
        //        {
        //            str.Seek(0, SeekOrigin.Begin);
        //            await str.CopyToAsync(fileStream);
        //        }
        //    }

        //}


        //public async void getdata()
        //{
        //    LoadingBar.IsIndeterminate = true;
        //    List<BookView> bl = new List<BookView>();
        //    try
        //    {
        //        StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("My Books", CreationCollisionOption.OpenIfExists);
        //        if (folder != null)
        //        {
        //            BookView b = new BookView();
        //            IReadOnlyList<StorageFolder> bookName = await folder.GetFoldersAsync(Windows.Storage.Search.CommonFolderQuery.DefaultQuery);
        //            foreach (StorageFolder s in bookName)
        //            {

        //                IReadOnlyList<StorageFile> chapterName = await s.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
        //                foreach (StorageFile f in chapterName)
        //                {
        //                    b = new BookView();
        //                    b.bookName = s.Name;
        //                    b.chapterName = "Chapter" + f.DisplayName;
        //                    bl.Add(b);
        //                }
        //            }
        //        }
        //        LoadingBar.Visibility = Visibility.Collapsed;
        //        if (bl.Count != 0)
        //        {
                    
        //            PurchasedListView.ItemsSource = bl;
        //        }
        //        else
        //            await (new MessageDialog("Nothing Purchased")).ShowAsync();
                
        //    }

        //    catch (Exception)
        //    {
        //        LoadingBar.Visibility = Visibility.Collapsed;
        //        await (new MessageDialog("Can't get data now please try again later Or go to downloaded section")).ShowAsync();
        //    }
        //}


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

    }
}
 