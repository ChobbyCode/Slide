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

        public CacheHttp(string defaultValue, string defaultPullURL) : base(defaultValue)
        {
            _cache = defaultValue;
            PullURL = defaultPullURL;

            RefreshArray();
        }

        public CacheHttp(string defaultValue) : base(defaultValue)
        {
            _cache = defaultValue;
            PullURL = "";

            RefreshArray();
        }

        public CacheHttp() : base()
        {
            _cache = "";
            PullURL = "";

            RefreshArray();
        }

        public string GetPullURL()
        {
            return this.PullURL;
        }
    }
}
