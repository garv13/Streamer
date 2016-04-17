﻿using Microsoft.WindowsAzure.MobileServices;
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
using System.Text;
using Newtonsoft.Json;
using Syncfusion.Pdf;
using Windows.Media.SpeechSynthesis;

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
        //private IMobileServiceTable<Book> Table2 = App.MobileService.GetTable<Book>();
        //private MobileServiceCollection<Book, Book> items2;
        public BitmapImage Im { get; set; }
        string testlol;
        BookData ob = new BookData();
        StorageFolder openBook = null;

        async void lol()
        {
            LoadingBar.IsActive = true;
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await folder.GetFileAsync("sample.txt");
            testlol = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            await load();
            LoadingBar.Visibility = Visibility.Collapsed;

        }
        async Task retreive(string name)
        {
    
            try
            {
                StorageFolder mainFol = await ApplicationData.Current.LocalFolder.CreateFolderAsync(testlol + "My Books", CreationCollisionOption.OpenIfExists);

                if (mainFol != null)
                {
                    ob = new BookData();
                    StorageFolder folder = await mainFol.CreateFolderAsync(name, CreationCollisionOption.OpenIfExists);
                    openBook = folder;

                    StorageFile sampleFile = await folder.GetFileAsync("UserName.txt");
                    var t = await sampleFile.OpenAsync(FileAccessMode.Read);
                    Stream na = t.AsStreamForRead();
                    using (var streamReader = new StreamReader(na, Encoding.UTF8))
                    {
                        string line;
                        line = streamReader.ReadToEnd();
                        ob = JsonConvert.DeserializeObject<BookData>(line);
                    }      
                    IReadOnlyList<StorageFile> sf = await folder.GetFilesAsync();
                    StorageFile imgFile = await folder.GetFileAsync("image.jpeg");
                    Im = new BitmapImage(new Uri(imgFile.Path));
                    List<GridClass> lg = new List<GridClass>();
                    GridClass gd = new GridClass();


                    foreach (StorageFile s in sf)
                    {
                         
                        if (s.Name.CompareTo("UserName.txt") == 0)
                            break;
                        if (s.Name.CompareTo("image.jpeg") == 0)
                            break;
                        gd = new GridClass();
                        gd.title = "Chapter No:" + s.DisplayName;
                        gd.Image = Im;
                        gd.authName = "";
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


        private void MenuButton6_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }

        private async Task load()
        {
            try
            {
                List<GridClass> lg = new List<GridClass>();
                GridClass gd = new GridClass();
                StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(testlol + "My Books", CreationCollisionOption.OpenIfExists);
                IReadOnlyList<StorageFolder> sf = await folder.GetFoldersAsync();
                gd = new GridClass();
                foreach (StorageFolder s in sf)
                {
                    ob = new BookData();
                    StorageFile sampleFile = await s.GetFileAsync("UserName.txt");
                    var t = await sampleFile.OpenAsync(FileAccessMode.Read);
                    Stream na = t.AsStreamForRead();
                    using (var streamReader = new StreamReader(na, Encoding.UTF8))
                    {
                        string line;
                        line = streamReader.ReadToEnd();
                        ob = JsonConvert.DeserializeObject<BookData>(line);
                    }

                    IReadOnlyList<StorageFile> fi = await s.GetFilesAsync();
                    StorageFile imgFile = await s.GetFileAsync("image.jpeg");
                    Im = new BitmapImage(new Uri(imgFile.Path));
                    gd = new GridClass();
                    gd.title = ob.Title;
                    gd.Image = Im;
                    gd.authName = ob.Author;
                    lg.Add(gd);
                }

                if (lg.Count != 0)
                {
                    event1.ItemsSource = lg;

                }
                else
                {
                    LoadingBar.Visibility = Visibility.Collapsed;
                    ErrorBox.Text = "No Books Downloaded";
                    ErrorBox.Visibility = Visibility.Visible;
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
            LoadingBar.IsActive = true;
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

        string loc = null;

        private async Task printPdf(string text)
        {
            loc = text;
            try
            {
              if((ob.userName.CompareTo(testlol) != 0))
                 {
                    await (new MessageDialog("Maybe this Pdf doesn't belong to you.If it does then download it again plzzz :):)")).ShowAsync();
                    Frame.Navigate(typeof(Downloads));
                 }
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
                Appbar.Visibility = Visibility.Visible;
               
            }
            catch(Exception)
            {
                await (new MessageDialog("Can't open Pdf")).ShowAsync();
            }
        }

        async Task tts(string text)
        {
            try
            {
                if ((ob.userName.CompareTo(testlol) != 0))
                {
                    await (new MessageDialog("Maybe this Pdf doesn't belong to you.If it does then download it again plzzz :):)")).ShowAsync();
                    Frame.Navigate(typeof(Downloads));
                }
                StorageFile file = await openBook.GetFileAsync(text);
                var l = await file.OpenAsync(FileAccessMode.Read);
                Stream str = l.AsStreamForRead();
                byte[] buffer = new byte[str.Length];
                str.Read(buffer, 0, buffer.Length);

                // Loads the PDF document.
                PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);
                // Loading Page collections
                PdfLoadedPageCollection loadedPages = ldoc.Pages;

                string s = "";

                // Extract text from PDF document pages
                foreach (PdfLoadedPage lpage in loadedPages)
                {
                    s += lpage.ExtractText();
                }

                s = s.Replace("\r", "");
                s = s.Replace("\n", "");
                SpeechSynthesizer synt = new SpeechSynthesizer();
                SpeechSynthesisStream syntStream = await synt.SynthesizeTextToStreamAsync(s);
                mediaElement.DefaultPlaybackRate = 0.85;
                mediaElement.SetSource(syntStream, syntStream.ContentType);
                mediaElement.Play();

            }
            catch(Exception)
            {
                LoadingBarPdf.Visibility = Visibility.Collapsed;
                await (new MessageDialog("Can't read Pdf")).ShowAsync();
            }
        }

        private async void PlayPdf_Click(object sender, RoutedEventArgs e)
        {
            LoadingBarPdf.IsActive = true;
            LoadingBarPdf.Visibility = Visibility.Visible;
            await tts(loc);
            LoadingBarPdf.Visibility = Visibility.Collapsed;
        }
    }
}
