using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace AssistantApp.Data
{
    public class EventRepository
    {
        public SQLiteConnection con;
        static string dbname = "assistantdb.db";
        public EventRepository()
        {
            con = new SQLiteConnection(dbname);
        }

        public static async Task CopyDatabase()
        {
            bool isDatabaseExisting = false;

            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(dbname);
                isDatabaseExisting = true;
            }
            catch
            {
                isDatabaseExisting = false;
            }

            if (!isDatabaseExisting)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync("assistantdb.db");
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
            }
        }

        public void Insert(MyEvent entity)
        {
            using (var statement = con.Prepare(
                "INSERT INTO MyEvents(title, place, description, startDateTime, endDateTime) VALUES (?,?,?,?,?)"))
            {
                statement.Bind(1, entity.Title);
                statement.Bind(2, entity.Place);
                statement.Bind(3, entity.Description);
                statement.Bind(4, entity.StartDateTime.Ticks);
                statement.Bind(5, entity.EndTime.Ticks);
                statement.Step();
            }
        }

        public void InsertEventFromService(MyEvent entity)
        {
            using (var statement = con.Prepare(
                "INSERT INTO MyEvents(title, place, description, startDateTime, endDateTime, service_id) VALUES (?,?,?,?,?,?)"))
            {
                statement.Bind(1, entity.Title);
                statement.Bind(2, entity.Place);
                statement.Bind(3, entity.Description);
                statement.Bind(4, entity.StartDateTime.Ticks);
                statement.Bind(5, entity.EndTime.Ticks);
                statement.Bind(6, entity.Service_ID);
                statement.Step();
            }
        }
        public void Update(MyEvent entity)
        {
            using (var statement = con.Prepare(
                "UPDATE MyEvents SET title=?, place=?, description=?, startDateTime=?, endDateTime=? WHERE id=?"))
            {
                statement.Bind(1, entity.Title);
                statement.Bind(2, entity.Place);
                statement.Bind(3, entity.Description);
                statement.Bind(4, entity.StartDateTime.Ticks);
                statement.Bind(5, entity.EndTime.Ticks);
                statement.Bind(6, entity.ID);
                statement.Step();
            }
        }
        public void UpdateEventFromService(MyEvent entity)
        {
            using (var statement = con.Prepare(
                "UPDATE MyEvents SET title=?, place=?, description=?, startDateTime=?, endDateTime=? WHERE service_id=?"))
            {
                statement.Bind(1, entity.Title);
                statement.Bind(2, entity.Place);
                statement.Bind(3, entity.Description);
                statement.Bind(4, entity.StartDateTime.Ticks);
                statement.Bind(5, entity.EndTime.Ticks);
                statement.Bind(6, entity.Service_ID);
                statement.Step();
            }
        }
        public void Delete(int id)
        {
            using (var statement = con.Prepare("DELETE FROM MyEvents WHERE id=?"))
            {
                statement.Bind(1, id);
                statement.Step();
            }
        }

        public List<MyEvent> GetEvents()
        {
            List<MyEvent> events = new List<MyEvent>();
 
            using (var statement = con.Prepare("SELECT * FROM MyEvents ORDER BY startDateTime"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    MyEvent entity = new MyEvent();
                    entity.ID = Convert.ToInt32(statement[0]);
                    entity.Title = statement.GetText(1);
                    entity.Place = statement.GetText(2);
                    entity.Description = statement.GetText(3);
                    entity.StartDateTime = new DateTime(statement.GetInteger(4));
                    entity.EndTime = new TimeSpan(statement.GetInteger(5));
                    if (statement[6] != null)
                    {
                        entity.Service_ID = statement.GetText(6);
                    }
                    entity.Service_ID = "";
                    events.Add(entity);
                }
            }
            return events;
        }

        public MyEvent GetEvent(int id)
        {
            MyEvent entity = null;

            using (var statement = con.Prepare("SELECT * FROM MyEvents WHERE id=?"))
            {
                statement.Bind(1, id);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    entity = new MyEvent();
                    entity.ID = Convert.ToInt32(statement[0]);
                    entity.Title = statement.GetText(1);
                    entity.Place = statement.GetText(2);
                    entity.Description = statement.GetText(3);
                    entity.StartDateTime = new DateTime(statement.GetInteger(4));
                    entity.EndTime = new TimeSpan(statement.GetInteger(5));
                    if (statement[6] != null)
                        entity.Service_ID = statement.GetText(6);
                    entity.Service_ID = "";
                }
            }

            return entity;
        }

        public bool CheckExistsServiceEventsInDb(string service_id)
        {
            using (var statement = con.Prepare("SELECT EXISTS(SELECT * FROM MyEvents WHERE service_id=?)"))
            {
                statement.Bind(1, service_id);
                statement.Step();
                if(statement.GetInteger(0) == 1)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
