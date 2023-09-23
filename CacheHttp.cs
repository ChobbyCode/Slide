using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slide.Web
{
    public class CacheHttp : Cache
    {
        public string PullURL { get; set; }

        public CacheHttp(string defaultName, string defaultValue, string defaultPullURL) : base(defaultName, defaultValue)
        {
            _cache = defaultValue;
            Name = defaultName;
            PullURL = defaultPullURL;

            RefreshArray();
        }

        public CacheHttp(string defaultName, string defaultValue) : base(defaultName, defaultValue)
        {
            _cache = defaultValue;
            Name= defaultName;
            PullURL = "";

            RefreshArray();
        }

        public string GetPullURL()
        {
            return this.PullURL;
        }
    }
}
