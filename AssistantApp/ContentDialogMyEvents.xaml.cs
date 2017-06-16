using AssistantApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
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
    public sealed partial class ContentDialogMyEvents : ContentDialog
    {
        DbHelper dbHelper;
        public ContentDialogMyEvents()
        {
            this.InitializeComponent();
            dbHelper = new DbHelper();
            startDatePicker.MinYear = new DateTimeOffset(DateTime.Today);
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!String.IsNullOrWhiteSpace(titleBox.Text))
            {
                MyEvent entity = new MyEvent()
                {
                    Title = titleBox.Text,
                    Place = placeBox.Text,
                    Description = descriptionBox.Text,
                    StartDateTime = Convert.ToDateTime(startDatePicker.Date.DateTime.ToString().Substring(0, 10) + " " + startTimePicker.Time.ToString().Substring(0, 5)),
                    EndTime = endTimePicker.Time
                };
                dbHelper.AddEvent(entity);
                Frame frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(EventsPage));
            }
            else
            {
                MessageDialog ms = new MessageDialog("Поле 'Тема' не заполнено.", "Внимание!");
                await ms.ShowAsync();
                return;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            titleBox.Text = string.Empty;
            placeBox.Text = string.Empty;
            descriptionBox.Text = string.Empty;
            scvContentDialog.ChangeView(0, 0, 1);
            Hide();
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
            scvContentDialog.ChangeView(0, 50, 1);
        }

        private void descriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            scvContentDialog.ChangeView(0, 0, 1);
        }

        private void checkNotice_Checked(object sender, RoutedEventArgs e)
        {
            if (checkNotice.IsChecked == true)
                tbxNoticeTime.IsEnabled = true;
        }

        private void checkNotice_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkNotice.IsChecked == false)
                tbxNoticeTime.IsEnabled = false;
        }
    }
}
