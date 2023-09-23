using Newtonsoft.Json;

namespace Slide
{
    /// <summary>
    /// Base for all Cache so they have same functions
    /// </summary>
    public class SlideBase
    {
        /// <summary>
        /// Set to miliseconds, determines how often we can recache and rewrite to the cache variable
        /// </summary>
        public double RecacheTime { get; set; }
        /// <summary>
        /// Current Value Of Cache
        /// </summary>
        public virtual dynamic CacheValue { get; set; }
        /// <summary>
        /// What the name of the Cache Variable is. This also determines the name of the cache file.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Sees if - required - than force write can be done. MUST BE SPECIFIED
        /// </summary>
        public bool AllowHardOverride { get; set; } = false;
        /// <summary>
        /// Will automatically write cache if set to true, won't if false.
        /// </summary>
        public bool AutoWriteCache { get; set; } = true;
        // Will automatically be set. If it is false then there isn't already a preexisiting cache variable
        internal bool _read = false;
        // Internal variable cache variable
        internal virtual dynamic? _cache { get; set; }
        // The folder which the cache will be written to. I.e C:\temp\Chobby\Cache\Test.json. The chobby folder is the GroupFolder
        internal string GroupFolder = "Slide";
        /// <summary>
        /// How the cache will be written. C = for the current user only, A = for all users. (C stored in \Users\User\ dir, A stored is C:\ dir)
        /// </summary>
        public string CacheStoreType = "C";
        // cacheUpdateTime is cacheCreateTime by default but everytime the cache is modified,
        // the cache timer should be updated
        internal DateTime _cacheUpdateTime { get; set; }

        /// <summary>
        /// Base.
        /// </summary>
        public virtual void SetValues(Cache cache)
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


                    _cache = fileWritter.ReadPreCache(path, cache).CacheValue;
                    AllowHardOverride = fileWritter.ReadPreCache(path, cache).AllowHardOverride;
                    AutoWriteCache = fileWritter.ReadPreCache(path, cache).AutoWriteCache;
                    RecacheTime = fileWritter.ReadPreCache(path, cache).RecacheTime;
                    CacheValue = fileWritter.ReadPreCache(path, cache).CacheValue;
                    //_partOfArray = fileWritter.ReadPreCache(path, this)._partOfArray;
                    _read = fileWritter.ReadPreCache(path, cache)._read;
                    CacheStoreType = fileWritter.ReadPreCache(path, cache).CacheStoreType;
                }
            }
        }

        /// <summary>
        /// Gets the current cache value
        /// </summary>
        public virtual string GetCache()
        {
            RefreshArray();
            return _cache;
        }

        /// <summary>
        /// Base.
        /// </summary>
        internal virtual void RefreshArray()
        {
            // Set the public cache var to private cache var
            CacheValue = _cache;
        }

        /// <summary>
        /// Check if it is possible to recache the cache :)
        /// </summary>
        public virtual bool Recachable()
        {
            bool result = false;

            // Check if any of the auto allows are enabled
            if (RecacheTime == -4 || AllowHardOverride == true)
            {
                result = true;
            }

            // Otherwise we can just check the other conditions
            if (DateTime.Now > _cacheUpdateTime.Add(TimeSpan.FromMilliseconds(RecacheTime)) || _cache == null)
            {
                // If the conditions is met we can return true to recache
                result = true;
            }

            // We can't recache :(
            return result;
        }
    }
}
