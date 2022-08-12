using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrnekData3
{
    
    public class Root
    {
        public List<JsonData> data { get; set; }
        public List<JsonDataParameter> dataParameter { get; set; }
    }
    public class JsonData
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthday { get; set; }
        public string typeId { get; set; }
        public string gender { get; set; }
        public string displayName { get; set; }
        //public string avatar { get; set; }
    }
    public class JsonDataParameter
    {
        public string id { get; set; }
        public string displayName { get; set; } 
    }

}
