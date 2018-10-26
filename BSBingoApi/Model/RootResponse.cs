using System;
using Newtonsoft.Json;

namespace BSBingoApi.Model
{
    public class RootResponse : Resource
    {
        public Link Info { get; set; }
        public Link Words { get; set; }
        public Link Languages { get; set; }
        public Link Categories { get; set; }

        public Link Users { get; set; }

        public Form Token { get; set; }

        //public string GetEtag()
        //{
        //    var serialized = JsonConvert.SerializeObject(this);
        //    return Md5Hash.ForString(serialized);
        //}
    }
}
