using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //lol();
        }

        StorageFolder openBook = null;

        async void lol()
        {
            await load();
        }
        async Task retreive(string name)
        {
            StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(name, CreationCollisionOption.OpenIfExists);
            if (folder != null)
            {
                openBook = folder;
                int count = 0;
                List<GridClass> lg = new List<GridClass>();
                GridClass gd = new GridClass();
                IReadOnlyList<StorageFile> sf = await folder.GetFilesAsync();

                foreach (StorageFile s in sf)
                {
                    count++;
                    gd = new GridClass();
                    gd.title = "Chapter No:" + count.ToString();
                    gd.authName = null;
                    lg.Add(gd);
                }
               event1.Visibility = Visibility.Collapsed;
                event2.ItemsSource = lg;
                event2.Visibility = Visibility.Visible;
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
            try
            {
                await Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
            }
            catch (Exception es)
            {
                await (new MessageDialog("Can't review now please try again later")).ShowAsync();
            }
        }

        private void MenuButton5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }
        
       
        private async Task load()
        {
            List<GridClass> lg = new List<GridClass>();
            GridClass gd = new GridClass();
            StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("AllBooks", CreationCollisionOption.OpenIfExists);
            IReadOnlyList<StorageFolder> sf = await folder.GetFoldersAsync();

            //foreach(StorageFolder s in sf)
            // {
            //      gd = new GridClass();
            //      gd.title = s.Name;
            //      gd.authName = "gr";
            //      lg.Add(gd);
            // }
            for (int i = 0; i < 10; i++)
            {
                gd = new GridClass();
                gd.title = "efkbj";
                gd.authName = "gr";
                lg.Add(gd);
            }
            if (lg.Count != 0)
            {
                event1.ItemsSource = lg;
                //Books.Visibility = Visibility.Visible;
            }
            else
                await (new MessageDialog("Nothing Purchased")).ShowAsync();
        }

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
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

            if (str != null)
                await retreive(t2.Text);
            else
                await printPdf(t2.Text);
        }

        private async Task printPdf(string text)
        {
            StorageFile file = await openBook.GetFileAsync(text);
            var l = await file.OpenAsync(FileAccessMode.Read);
            Stream str = l.AsStreamForRead();
            byte[] buffer = new byte[str.Length];
            str.Read(buffer, 0, buffer.Length);

           // Loads the PDF document.
             PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);
            pdfViewer.LoadDocument(ldoc);
            //Chapters.Visibility = Visibility.Collapsed;
            PdfGrid.Visibility = Visibility.Visible;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            lol();
        }
    }
}
