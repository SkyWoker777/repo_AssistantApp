using AssistantApp.Data.Cist;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AssistantApp
{
    class FacultyViewModel : DependencyObject
    {
        public List<Faculty> Faculties { get; set; }

        public FacultyViewModel()
        {
        }

        public async Task<string> GetJson()
        {
            string url = "http://cist.nure.ua/ias/app/tt/P_API_FACULTIES_JSON";
            UtilRequest util = new UtilRequest();
            string content = await util.GetContentFromUrl(url);
            System.Diagnostics.Debug.WriteLine("======LOOK THIS=====>" + content);
            return content;
        }

        public async Task<List<Faculty>> GetFaculties()
        {
            string json = await GetJson();
            Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(json);
            University university = JsonConvert.DeserializeObject<University>(obj["university"].ToString());
            Faculties = new List<Faculty>();
            foreach(var item in university.Faculties)
            {
                Faculties.Add(item);
            }
            return Faculties;
        }
    }
}
