using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slide
{
    /// <summary>
    /// Collective cache array. 
    /// </summary>
    public class CacheArray
    {
        // Settings Variables
        public string Name { get; set; }

        public bool AllowHardOverride { get; set; } = false;

        // Create and set value of cache array
        private List<Cache> _cacheList { get; set; }

        public List<Cache> ArrayValue { get; set; } = new List<Cache>();

        public CacheArray(List<Cache> DefaultValue)
        {
            RefreshArray();

            // Make array not null
            _cacheList = DefaultValue;

            RefreshArray();
        }

        public IEnumerable<Cache> GetCacheList()
        {
            return _cacheList.AsReadOnly();
        }

        public List<Cache> getArray()
        {
            RefreshArray();
            return _cacheList;
        }

        public void Add(Cache d)
        {
            if(AllowHardOverride)
            {
                d.AllowHardOverride = true;
            }
            _cacheList.Add(d);
            RefreshArray();
        }

        /// <summary>
        /// Sets a cache based on the position in the array
        /// </summary>
        /// <param name="loc">The position it is in the array</param>
        /// <param name="val">What the cache will be set to</param>
        public void SetFromPos(int loc, string val)
        {
            // Sets based of position
            _cacheList[loc].Store(val);
        }

        /// <summary>
        /// Sets the contents of a peice of Cache based on the name passed in
        /// </summary>
        /// <param name="name">The name of the cache</param>
        /// <param name="val">What the cache will be set to</param>
        public void SetFromName(string name, string val)
        {
            // Loops thru array to find the correct one
            int loop = 0;
            foreach(Cache cache in _cacheList)
            {
                if(cache.Name == name)
                {
                    _cacheList[loop].Store(val);
                    return;
                }
                loop++;
            }
        }

        /// <summary>
        /// Renames the name of a cache from the position in the array
        /// </summary>
        /// <param name="loc">The location where the element is in the array</param>
        /// <param name="val">What the cache will be renamed to</param>
        public void SetNameFromPos (int loc, string val)
        {
            // Sets based of position
            _cacheList[loc].Name = val;
        }

        /// <summary>
        /// Renames the name of a cache from the name of a cache
        /// </summary>
        /// <param name="name">Theh name of the cache which will be renamed</param>
        /// <param name="val">What the cache will be renamed to</param>
        public void SetNameFromName (string name, string val)
        {
            // Loops thru array to find the correct one
            int loop = 0;
            foreach (Cache cache in _cacheList)
            {
                if (cache.Name == name)
                {
                    _cacheList[loop].Name = val;
                    return;
                }
                loop++;
            }
        }

        private void RefreshArray()
        {
            if (AllowHardOverride == true)
            {
                foreach (Cache cache in _cacheList)
                {
                    cache.AllowHardOverride = true;
                }
            }

            ArrayValue = _cacheList;
        }
    }
}
