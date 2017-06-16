using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantApp.Data.Cist
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Department : CistEntity
    {
        [JsonProperty("teachers")]
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
