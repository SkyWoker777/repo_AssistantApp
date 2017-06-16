using AssistantApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class CheckListView : Page
    {
        DbHelper dbHelper;
        ListChoiceViewModel listchoice;
        public CheckListView()
        {
            this.InitializeComponent();
            dbHelper = new DbHelper();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                List<MyEvent> initial = new List<MyEvent>();
                foreach (MyEvent item in e.Parameter as IList<object>)
                {
                    initial.Add(item);
                }
                listchoice = new ListChoiceViewModel(initial);
                lstBox.ItemsSource = listchoice.Values;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedItem = this.lstBox.ContainerFromItem((sender as CheckBox).DataContext) as ListBoxItem;
            if (checkedItem != null)
            {
                checkedItem.IsSelected = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedItem = this.lstBox.ContainerFromItem((sender as CheckBox).DataContext) as ListBoxItem;
            if (checkedItem != null)
            {
                checkedItem.IsSelected = false;
            }
        }

        private void btnAppBarDelete_Click(object sender, RoutedEventArgs e)
        {
            dbHelper.DeleteChosenEvents(lstBox.SelectedItems);

            if (Frame.CanGoBack)
                Frame.GoBack();
            else Frame.Navigate(typeof(EventsPage));
        }

        private void btnAppBarBack_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
            else Frame.Navigate(typeof(EventsPage));
        }
    }
}
