﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Slide
{
    public class CacheArray : SlideBase
    {
        internal List<Cache> _cache { get; set; } = new List<Cache>();

        public CacheArray(string defaultName) {
            Name = defaultName;

            SetValues(this);

            if (AutoWriteCache)
            {
                this.WriteCache();
            }
        }
    
        public void Add(dynamic value)
        {
            Cache cache = new Cache($"Element_{Name}_{_cache.Count()}", value, "poa", "poa"/*, true*/)
            {
                _partOfArray = true,
            };

            _cache.Add(cache);
            if(AutoWriteCache)
            {
                this.WriteCache();
            }
        }

        public virtual void RefreshArray()
        {
            foreach(Cache c in _cache)
            {
                c.RefreshArray();
            }
            
            CacheValue = _cache;
        }

        /// <summary>
        /// Load Cache from file. Slide automatically calls this, so there is no need to call it.
        /// </summary>
        public virtual void SetValues(CacheArray cache)
        {
            if (File.Exists($@"C:\Users\jacob\Temp\{GroupFolder}\Cache\{Name}.json"))
            {
                _read = true;
            }
            if (File.Exists($@"C:\Temp\{GroupFolder}\Cache\{Name}.json"))
            {
                _read = true;
            }

            if (_read)
            {
                if (OperatingSystem.IsWindows())
                {
                    string path = $@"C:\Users\jacob\Temp\{GroupFolder}\Cache\{Name}.json";

                    // Read File
                    string json = File.ReadAllText($@"C:\Users\jacob\Temp\{GroupFolder}\Cache\{Name}.json");

                    CacheArrayDes des = JsonConvert.DeserializeObject<CacheArrayDes>(json);

                    _cache = des.CacheValue;
                    AllowHardOverride = des.AllowHardOverride;
                    AutoWriteCache = des.AutoWriteCache;
                    RecacheTime = des.RecacheTime;
                    CacheValue = des.CacheValue;
                    //_partOfArray = fileWritter.ReadPreCache(path, this)._partOfArray;
                    _read = des._read;
                    CacheStoreType = des.CacheStoreType;
                }
            }
        }
    }
}
