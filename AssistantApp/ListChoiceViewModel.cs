using AssistantApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AssistantApp
{
    class ListChoiceViewModel : DependencyObject
    {
        public ObservableCollection<ItemListWithChoiceViewModel> Values { get; set; }
        public ListChoiceViewModel(List<MyEvent> initial)
        {
            Values = new ObservableCollection<ItemListWithChoiceViewModel>();
            foreach (var n in initial)
                Values.Add(new ItemListWithChoiceViewModel() { Value = n, IsSelected = false });
        }

    }
}
