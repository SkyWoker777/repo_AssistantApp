using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantApp.Data.Cist
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Faculty : CistEntity
    {
        [JsonProperty("departments")]
        public IEnumerable<Department> Departments { get; set; }
    }
}
