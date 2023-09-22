using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.IO;

namespace Slide
{
    public class Cache : SlideBase
    {
        internal bool _partOfArray { get; } = false;


        public Cache(string defaultName, dynamic defaultValue, string group, string storeType)
        {
            // Sets default variables. Cache is not set by default so it can be null,
            // and cache create time can be set
            RecacheTime = 0;
            _cacheUpdateTime = DateTime.Now;
            CacheValue = "";

            GroupFolder = group;

            Name = defaultName;

            CacheStoreType = storeType;

            _cache = defaultValue;

            RefreshArray();

            SetValues(this);
        }

        public Cache(string defaultName, dynamic defaultValue) {
            // Sets default variables. Cache is not set by default so it can be null,
            // and cache create time can be set
            RecacheTime = 0;
            _cacheUpdateTime = DateTime.Now;
            CacheValue = "";

            Name = defaultName;

            _cache = defaultValue;

            RefreshArray();

            SetValues(this);
        }

        public Cache(string defaultName) {
            RecacheTime = 0;
            _cacheUpdateTime = DateTime.Now;
            CacheValue = "";
            Name = defaultName; 

            _cache = "";

            RefreshArray();

            SetValues(this);
        }

        public Cache()
        {
            RecacheTime = 0;
            _cacheUpdateTime = DateTime.Now;
            CacheValue = "";

            _cache = "";

            RefreshArray();

            if (AutoWriteCache)
            {
                this.WriteCache();
            }
        }

        public void Store(string data)
        {
            if(Recachable())
            {
                // Stores the new cache
                _cache = data;
                // Updates the cache timer
                _cacheUpdateTime = DateTime.Now;
                RefreshArray();
            }

            if (AutoWriteCache)
            {
                this.WriteCache();
            }
        }
    }
}
