﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class CollectionSort : Page
    {
        public CollectionSort()
        {
            this.InitializeComponent();       
        }
        string testlol = "";
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await folder.GetFileAsync("sample.txt");
            testlol = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            List<CollSort> myList = new List<CollSort>();
            foreach (MeriCollection d in App.mc)
            {
                CollSort c = new CollSort();
                c.BookId = d.BookId;
                c.BookName = d.BookName;
                c.ChapterId = d.ChapterId;
                c.ChapterNo = d.ChapterNo;
                c.UserName = d.UserName;
                c.sel = false;
                myList.Add(c);
            }
            View.ItemsSource = myList;
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

        private async void MenuButton6_Click(object sender, RoutedEventArgs e)
        {
            await (new MessageDialog("You are successfully loged out :):)")).ShowAsync();
            Frame.Navigate(typeof(Login));
        }
        private void MenuButton7_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyCollection));
        }

        private async void NextBar_Click(object sender, RoutedEventArgs e)
        {
            LoadingBar.Visibility = Visibility.Visible;
            
            LoadingBar.IsActive = true;
            string sn = "";
            MeriCollection l = new MeriCollection();
            l.BookName = CollName.Text;
            l.UserName = testlol;
            sn = JsonConvert.SerializeObject(l);
            try
            {
                StorageFolder mainFol = await ApplicationData.Current.LocalFolder.CreateFolderAsync(testlol + "My Collections", CreationCollisionOption.OpenIfExists);
                if (mainFol != null)
                {
                    StorageFolder folder = await mainFol.CreateFolderAsync(CollName.Text, CreationCollisionOption.OpenIfExists);
                    if (folder != null)
                    {
                        Uri url = new Uri("https://www.ebookstreamer.me/downloads");
                        HttpClient httpClient = new HttpClient();
                        var myClientHandler = new HttpClientHandler();


                        foreach (MeriCollection d in App.mc)
                        {
                            HttpResponseMessage httpResponse = new HttpResponseMessage();
                            var content = new FormUrlEncodedContent(new[]
                             {
                                  new KeyValuePair<string, string>("id", d.ChapterId)
                             });
                            httpResponse = await httpClient.PostAsync(url, content);
                            httpResponse.EnsureSuccessStatusCode();
                            Stream str = await httpResponse.Content.ReadAsStreamAsync();
                            byte[] pd = new byte[str.Length];
                            str.Read(pd, 0, pd.Length);

                            StorageFile file = await folder.CreateFileAsync(d.ChapterNo + ".txt", CreationCollisionOption.ReplaceExisting);
                            using (var fileStream = await file.OpenStreamForWriteAsync())
                            {
                                str.Seek(0, SeekOrigin.Begin);
                                await str.CopyToAsync(fileStream);
                            }
                        }
                        StorageFile useFile =
                      await folder.CreateFileAsync("UserName.txt", CreationCollisionOption.ReplaceExisting);
                        await Windows.Storage.FileIO.WriteTextAsync(useFile, sn);
                    }
                }
                LoadingBar.Visibility = Visibility.Collapsed;
                await (new MessageDialog("Your collection was made!!")).ShowAsync();
                Frame.Navigate(typeof(MyCollection));
            }
            catch(Exception)
            {

            }
        }

        private async void BackBar_Click(object sender, RoutedEventArgs e)
        {
            LoadingBar.Visibility = Visibility.Collapsed;
            await (new MessageDialog("Your collection was not made")).ShowAsync();
            Frame.Navigate(typeof(MyCollection));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
