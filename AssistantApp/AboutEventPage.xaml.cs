using AssistantApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AssistantApp
{
    public sealed partial class AboutEventPage : Page
    {
        DbHelper dbHelper;
        private int ItemId { get; set; }
        public AboutEventPage()
        {
            this.InitializeComponent();
            dbHelper = new DbHelper();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ItemId = (int)e.Parameter;
                MyEvent ev = dbHelper.GetMyEvent(ItemId);
                if (ev != null)
                {
                    tbDate.Text = ev.StartDateTime.ToString("dddd, dd MMMM");
                    tbTitle.Text = ev.Title;
                    tbPlace.Text = ev.Place;
                    if (String.IsNullOrWhiteSpace(tbPlace.Text))
                    {
                        tbPlace.Text = "Нет подробностей";
                    }
                    tbDescription.Text = ev.Description;
                    if (String.IsNullOrWhiteSpace(tbDescription.Text))
                    {
                        tbDescription.Text = "Нет подробностей";
                    }
                    tbStartTime.Text = ev.StartDateTime.ToString("HH:mm");
                    tbEndTime.Text = ev.EndTimeText;
                }
            }
        }

        private void btnAppBarDelete_Click(object sender, RoutedEventArgs e)
        {
            dbHelper.DeleteEvent(ItemId);
            if (Frame.CanGoBack)
                Frame.GoBack();
            else Frame.Navigate(typeof(EventsPage));
        }

        private void btnAppBarEdit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditEventsPage), ItemId);
        }

        private void GoToSyncPage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
