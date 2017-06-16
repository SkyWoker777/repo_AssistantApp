using AssistantApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AssistantApp
{
    class ItemListWithChoiceViewModel : DependencyObject
    {
        public MyEvent Value
        {
            get { return (MyEvent)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register(
                "Value", typeof(MyEvent), typeof(ItemListWithChoiceViewModel), new PropertyMetadata(null));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                     "IsSelected", typeof(bool), typeof(ItemListWithChoiceViewModel), new PropertyMetadata(null));
    }
}
