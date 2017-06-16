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
    class DepartmentViewModel
    {
        public List<Department> Depts { get; set; }
        public async Task<string> GetJson(string fac_id)
        {
            string url = "http://cist.nure.ua/ias/app/tt/P_API_DEPARTMENTS_JSON?p_id_faculty=" + fac_id;
            UtilRequest util = new UtilRequest();
            string content = await util.GetContentFromUrl(url);
            System.Diagnostics.Debug.WriteLine("======LOOK THIS=====>" + content);
            return content;
        }

        public async Task GetDept(string fac_id)
        {
            string json = await GetJson(fac_id);
            Depts = JsonConvert.DeserializeObject<Faculty>(json).Departments.ToList();
        }
    }
}
