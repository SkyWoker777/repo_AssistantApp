using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssistantApp.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CistEvent
    {
        [JsonProperty("subject_id")]
        public string Subject_id { get; set;}

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("auditory")]
        public string Auditory { get; set; }

        //[JsonProperty("groups")]
        //public IEnumerable<Group> Groups { get; set; }

        //[JsonProperty("teachers")]
        //public IEnumerable<Teacher> Teachers { get; set; }
    }
}
