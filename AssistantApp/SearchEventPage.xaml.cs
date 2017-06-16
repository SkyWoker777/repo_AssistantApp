using AssistantApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace AssistantApp
{
    public sealed partial class SearchEventPage : Page
    {
        List<MyEvent> initial { get; set; }
        public SearchEventPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter != null)
            {
                initial = new List<MyEvent>();
                foreach (MyEvent item in e.Parameter as IList<object>)
                {
                    initial.Add(item);
                }
                lstview1.ItemsSource = initial.OrderBy(k => k.Title);
            }
            tbxSearch.Focus(FocusState.Programmatic);
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbxSearch.Text))
            {
                lstview1.ItemsSource = initial.Where(v => v.Title.ToLower().StartsWith(tbxSearch.Text.ToLower()));
            }
            else
            {
                lstview1.ItemsSource = initial.OrderBy(k => k.Title);
            }
        }

        private void lstview1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int itemId = 0;
            itemId = ((MyEvent)lstview1.SelectedItem).ID;
            Frame.Navigate(typeof(AboutEventPage), itemId);
        }
    }
}
