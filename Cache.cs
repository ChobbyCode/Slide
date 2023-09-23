using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Slide
{
    public class Cache : SlideBase
    {
        internal bool _partOfArray { get; set; } = false;

        public Cache(string defaultName, dynamic defaultValue, string group, string storeType/*, bool poa = false*/)
        {
            // Sets default variables. Cache is not set by default so it can be null,
            // and cache create time can be set
            RecacheTime = 0;
            _cacheUpdateTime = DateTime.Now;
            CacheValue = "";

            GroupFolder = group;

            //_partOfArray = poa;

            Name = defaultName;

            CacheStoreType = storeType;

            _cache = defaultValue;

            RefreshArray();

            SetValues(this);

            if (!_partOfArray)
            {
                this.WriteCache();
            }
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

            if (!_partOfArray)
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

        public void SetValues(Cache cache)
        {
            FileWritter fileWritter = new FileWritter();

            if (File.Exists($@"C:\Users\jacob\Temp\{GroupFolder}\Cache\{Name}.json") && CacheStoreType == "C")
            {
                _read = true;
            }
            if (File.Exists($@"C:\Temp\{GroupFolder}\Cache\{Name}.json") && CacheStoreType == "A")
            {
                _read = true;
            }

            if (_read)
            {
                if (OperatingSystem.IsWindows())
                {
                    string path = "";

                    if (CacheStoreType == "C")
                    {
                        path = $@"C:\Users\jacob\Temp\{GroupFolder}\Cache\{Name}.json";
                    }
                    else if (CacheStoreType == "A")
                    {
                        path = $@"C:\Temp\{GroupFolder}\Cache\{Name}.json";
                    }

                    string json = File.ReadAllText(path);

                    CacheDes tp = JsonConvert.DeserializeObject<CacheDes>(json);

                    _cache = tp.CacheValue;
                    CacheValue = tp.CacheValue;
                    Name = tp.Name;
                    RecacheTime = tp.RecacheTime;
                    AllowHardOverride = tp.AllowHardOverride;
                    AutoWriteCache = tp.AutoWriteCache;
                    CacheStoreType = tp.CacheStoreType;
                }
            }
        }
    }
}
