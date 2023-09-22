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
    public class CacheArray : SlideBase
    {
        public CacheArray(string defaultName, List<Cache> DefaultValue)
        {
            FileWritter fileWritter = new FileWritter();

            CacheArray b = fileWritter.ReadPreCache(@"C:\Users\jacob\Temp\Grandma\Cache\Plant.json", this);

            Console.WriteLine(CacheValue);
        }

        public IEnumerable<Cache> GetCacheList()
        {
            return _cache.AsReadOnly();
        }

        public List<Cache> getArray()
        {
            RefreshArray();
            return _cache;
        }

        public void Add(Cache d)
        {
            if(AllowHardOverride)
            {
                d.AllowHardOverride = true;
            }
            _cache.Add(d);
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
            _cache[loc].Store(val);
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
            foreach(Cache cache in _cache)
            {
                if(cache.Name == name)
                {
                    _cache[loop].Store(val);
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
            _cache[loc].Name = val;
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
            foreach (Cache cache in _cache)
            {
                if (cache.Name == name)
                {
                    _cache[loop].Name = val;
                    return;
                }
                loop++;
            }
        }
    }
}
