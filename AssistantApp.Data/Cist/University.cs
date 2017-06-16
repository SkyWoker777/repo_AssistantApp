using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantApp.Data.Cist
{
    [JsonObject(MemberSerialization.OptIn)]
    public class University
    {
        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName ="faculties")]
        public List<Faculty> Faculties { get; set; }
    }
}
