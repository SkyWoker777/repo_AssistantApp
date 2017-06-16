using AssistantApp.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AssistantApp
{
    public class DbHelper
    { 
        private EventRepository context;

        public DbHelper()
        {
            context = new EventRepository();
        }

        public void DeleteChosenEvents(IList<object> list)
        {
            foreach (ItemListWithChoiceViewModel item in list)
            {
                context.Delete(item.Value.ID);
            }
        }

        public void DeleteEvent(int id)
        {
            context.Delete(id);
        }

        public MyEvent GetMyEvent(int id)
        {
            return context.GetEvent(id);
        }

        public ObservableCollection<MyEvent> GetAllEvents()
        {
            ObservableCollection<MyEvent> events = new ObservableCollection<MyEvent>(context.GetEvents());
            return events;
        }

        public void SaveChanges(MyEvent entity)
        {
            context.Update(entity);
        }

        public void AddEvent(MyEvent entity)
        {
            context.Insert(entity);
        }

        public void AddServiceEvent(MyEvent entity)
        {
            context.InsertEventFromService(entity);
        }

        public void SaveChangesFromService(MyEvent entity)
        {
            context.UpdateEventFromService(entity);
        }

        public bool CheckExistsInDb(string serviceId)
        {
            return context.CheckExistsServiceEventsInDb(serviceId);
        }
    }
}
