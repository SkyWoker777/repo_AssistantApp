using AssistantApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.System;
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
    public sealed partial class EditEventsPage : Page
    {
        DbHelper dbHelper;
        private int ItemId { get; set; }
        public EditEventsPage()
        {
            this.InitializeComponent();
            dbHelper = new DbHelper();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ItemId = (int)e.Parameter;
                MyEvent entity = dbHelper.GetMyEvent(ItemId);
                if (entity != null)
                {
                    titleBox.Text = entity.Title;
                    placeBox.Text = entity.Place;
                    descriptionBox.Text = entity.Description;
                    startDatePicker.Date = entity.StartDateTime.Date;
                    startTimePicker.Time = entity.StartDateTime.TimeOfDay;
                    endTimePicker.Time = entity.EndTime;
                }
            }
        }

        private async void btnAppBarSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(titleBox.Text))
            {
                MyEvent entity = new MyEvent
                {
                    ID = ItemId,
                    Title = titleBox.Text,
                    Place = placeBox.Text,
                    Description = descriptionBox.Text,
                    StartDateTime = Convert.ToDateTime(startDatePicker.Date.DateTime.ToString().Substring(0, 10) + " " + startTimePicker.Time.ToString().Substring(0, 5)),
                    EndTime = endTimePicker.Time
                };
                dbHelper.SaveChanges(entity);
            }
            else
            {
                MessageDialog ms = new MessageDialog("* Не все поля заполнены.", "Внимание!");
                await ms.ShowAsync();
            }
            if (Frame.CanGoBack)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(EventsPage));
        }

        private void btnAppBarCancel_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(EventsPage));
        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Focus(FocusState.Programmatic);
            }
        }
        private void descriptionBox_GotFocus(object sender, RoutedEventArgs e)
        {
            scvEditPage.ChangeView(0, 50, 1);
        }
        private void descriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            scvEditPage.ChangeView(0, 0, 1);
        }

    }
}
