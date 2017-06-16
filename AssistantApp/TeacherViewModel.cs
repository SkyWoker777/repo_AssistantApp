using AssistantApp.Data.Cist;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AssistantApp
{
    class TeacherViewModel
    {
        public List<Teacher> Teachers { get; set; }
        public async Task<string> GetJson(string dept_id)
        {
            string url = "http://cist.nure.ua/ias/app/tt/P_API_TEACHERS_JSON/?p_id_department=" + dept_id;
            UtilRequest util = new UtilRequest();
            string content = await util.GetContentFromUrl(url);
            System.Diagnostics.Debug.WriteLine("======LOOK THIS=====>" + content);
            return content;
        }

        public async Task GetTeachers(string dept_id)
        {
            string json = await GetJson(dept_id);
            Teachers = JsonConvert.DeserializeObject<Department>(json).Teachers.ToList();
        }
    }
}
