﻿using System;
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
    public class Cache
    {
        public string Name {  get; set; }

        public bool AllowHardOverride { get; set; } = false;

        public bool AutoWriteCache { get; set; } = true;

        // The cache variable
        internal dynamic? _cache { get; set; }
        // This variable can be set by the user but underlines how often a cache can be cached
        // -4 means there is no limit and can always be cached
        public double RecacheTime { get; set; }

        public dynamic CacheValue { get; set; }

        internal bool _partOfArray { get; } = false;

        internal bool _read = false;

        internal string GroupFolder = "Slide/";

        /// <summary>
        /// THIS METHOD IS NOT FULLY FINISHED!
        /// 
        /// All Users or Current User use (A/C) for this
        /// </summary>
        public string CacheStoreType = "C";


        // cacheUpdateTime is cacheCreateTime by default but everytime the cache is modified,
        // the cache timer should be updated
        internal DateTime _cacheUpdateTime { get; set; }


        public Cache(string defaultName, dynamic defaultValue, string group)
        {
            // Sets default variables. Cache is not set by default so it can be null,
            // and cache create time can be set
            RecacheTime = 0;
            _cacheUpdateTime = DateTime.Now;
            CacheValue = "";

            GroupFolder = group + "/";

            Name = defaultName;

            _cache = defaultValue;

            RefreshArray();

            SetValues();

            if (AutoWriteCache)
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

            SetValues();

            if (AutoWriteCache)
            {
                this.WriteCache();
            }
        }

        public Cache(string defaultName) {
            RecacheTime = 0;
            _cacheUpdateTime = DateTime.Now;
            CacheValue = "";
            Name = defaultName; 

            _cache = "";

            RefreshArray();

            SetValues();

            if (AutoWriteCache)
            {
                this.WriteCache();
            }
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

        public virtual void SetValues()
        {

            FileWritter fileWritter = new FileWritter();

            if (File.Exists($@"C:\Users\jacob\Temp\Slide\Cache\{Name}.json"))
            {
                _read = true;
            }

            if (_read)
            {
                if (OperatingSystem.IsWindows())
                {
                    string path = $@"C:\Users\jacob\Temp\Slide\Cache\{Name}.json";

                    _cache = fileWritter.ReadPreCache(path, this).CacheValue;
                    AllowHardOverride = fileWritter.ReadPreCache(path, this).AllowHardOverride;
                    AutoWriteCache = fileWritter.ReadPreCache(path, this).AutoWriteCache;
                    RecacheTime = fileWritter.ReadPreCache(path, this).RecacheTime;
                    CacheValue = fileWritter.ReadPreCache(path, this).CacheValue;
                    //_partOfArray = fileWritter.ReadPreCache(path, this)._partOfArray;
                    _read = fileWritter.ReadPreCache(path, this)._read;
                    CacheStoreType = fileWritter.ReadPreCache(path, this).CacheStoreType;
                }
            }
        }


        public virtual bool Recachable()
        {
            bool result = false;

            if (RecacheTime == -4 || AllowHardOverride == true)
            {
                // Return true if -4 as that means we can always recache
                result = true;
            }

            if (DateTime.Now > _cacheUpdateTime.Add(TimeSpan.FromMilliseconds(RecacheTime)) || _cache == null)
            {
                // If the conditions is met we can return true to recache
                result = true;
            }

            // We can't recache :(
            return result;
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

        public virtual string GetCache()
        {
            RefreshArray();
            return _cache;
        }

        public virtual string ToJSON()
        {
            RefreshArray();
            return this.ConvertToJSON();
        }        

        internal void RefreshArray()
        {
            CacheValue = _cache;
        }

        

        
    }
}
