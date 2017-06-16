using AssistantApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace AssistantApp
{
    public sealed partial class EventsPage : Page
    {
        DbHelper dbHelper;
        public EventsPage()
        {
            this.InitializeComponent();
            dbHelper = new DbHelper();
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    //lstBox1.ItemsSource = dbHelper.GetAllEvents();
        //    //if (lstBox1.Items.Count != 0)
        //    //{
        //    //    infolstblock.Visibility = Visibility.Collapsed;
        //    //    sad_bgImage.Visibility = Visibility.Collapsed;
        //    //    btnAppBarChoose.IsEnabled = true;
        //    //}
        //}

        private void spListBoxItem_Holding(object sender, HoldingRoutedEventArgs e)
        {
            OpenMenuFlyoutAction contextMenu = new OpenMenuFlyoutAction();
            contextMenu.Execute(sender, e.PointerDeviceType);
        }

        private async void MenuFlyoutItem_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var listItem = (e.OriginalSource as MenuFlyoutItem).DataContext as MyEvent;
                var itemId = listItem.ID;
                Frame.Navigate(typeof(EditEventsPage), itemId);
            }
            catch(NullReferenceException)
            {
                await new MessageDialog("Сначала выберите мероприятие.").ShowAsync();
            }
        }

        private async void MenuFlyoutItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var listItem = (e.OriginalSource as MenuFlyoutItem).DataContext as MyEvent;
                int itemId = listItem.ID;
                dbHelper.DeleteEvent(itemId);
                if (Views.SelectedItem == AllEventsView)
                {
                    lstBox1.ItemsSource = dbHelper.GetAllEvents();
                    if (lstBox1.Items.Count == 0)
                    {
                        infolstblock.Visibility = Visibility.Visible;
                        sad_bgImage.Visibility = Visibility.Visible;
                        btnAppBarChoose.IsEnabled = false;
                    }
                }
                else if (Views.SelectedItem == TodayView)
                {
                    lstBox2.ItemsSource = dbHelper.GetAllEvents().Where(d => d.StartDateTime.Date == DateTime.Today);
                    if (lstBox2.Items.Count == 0)
                    {
                        infolstblock2.Visibility = Visibility.Visible;
                        btnAppBarChoose.IsEnabled = false;
                    }
                }
                else if (Views.SelectedItem == TomorowView)
                {
                    lstBox3.ItemsSource = dbHelper.GetAllEvents().Where(d => d.StartDateTime.Date == DateTime.Today.AddDays(1));
                    if (lstBox3.Items.Count == 0)
                    {
                        infolstblock3.Visibility = Visibility.Visible;
                        btnAppBarChoose.IsEnabled = false;
                    }
                }
            }
            catch (NullReferenceException)
            {
                await new MessageDialog("Сначала выберите мероприятие.").ShowAsync();
            }
        }

        private async void btnAppBarAdd_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogMyEvents contentdialog = new ContentDialogMyEvents();
            await contentdialog.ShowAsync();
        }

        private void Details_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int itemId = 0;
            if (Views.SelectedItem == AllEventsView)
            {
                itemId = ((MyEvent)lstBox1.SelectedItem).ID;
            }
            else if(Views.SelectedItem == TodayView)
            {
                itemId = ((MyEvent)lstBox2.SelectedItem).ID;
            }
            else if (Views.SelectedItem == TomorowView)
            {
                itemId = ((MyEvent)lstBox3.SelectedItem).ID;
            }
            Frame.Navigate(typeof(AboutEventPage), itemId);
        }

        private void GoToSyncPage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(MainPage));
        }

        private void btnAppBarChoose_Click(object sender, RoutedEventArgs e)
        {
            if (Views.SelectedItem == AllEventsView)
            {
                Frame.Navigate(typeof(CheckListView), lstBox1.Items);
            }
            else if(Views.SelectedItem == TodayView)
            {
                Frame.Navigate(typeof(CheckListView), lstBox2.Items);
            }
            else if (Views.SelectedItem == TomorowView)
            {
                Frame.Navigate(typeof(CheckListView), lstBox3.Items);
            }
        }

        private void btnAppBarFind_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchEventPage),lstBox1.Items);
        }

        private void Views_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Views.SelectedItem == TodayView)
            {
                lstBox2.ItemsSource = dbHelper.GetAllEvents().Where(d => d.StartDateTime.Date == DateTime.Today);
                if (lstBox2.Items.Count != 0)
                {
                    infolstblock2.Visibility = Visibility.Collapsed;
                    btnAppBarChoose.IsEnabled = true;
                }
                else btnAppBarChoose.IsEnabled = false;
            }
            else if(Views.SelectedItem == AllEventsView)
            {
                lstBox1.ItemsSource = dbHelper.GetAllEvents();
                if (lstBox1.Items.Count != 0)
                {
                    infolstblock.Visibility = Visibility.Collapsed;
                    sad_bgImage.Visibility = Visibility.Collapsed;
                    btnAppBarChoose.IsEnabled = true;
                }
            }
            else if(Views.SelectedItem == TomorowView)
            {
                lstBox3.ItemsSource = dbHelper.GetAllEvents().Where(d => d.StartDateTime.Date == DateTime.Today.AddDays(1));
                if (lstBox3.Items.Count != 0)
                {
                    infolstblock3.Visibility = Visibility.Collapsed;
                }
                else btnAppBarChoose.IsEnabled = false;
            }
        }


    }
}
