using Newtonsoft.Json;

namespace Slide
{
    public static class CacheExtensions
    {
        /// <summary>
        /// Converts a Cache to JSON
        /// </summary>
        public static string ConvertToJSON(this Cache data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            return json;
        }

        /// <summary>
        /// Converts a CacheArray To JSON
        /// </summary>
        public static string ConvertToJSON(this CacheArray data)
        {
            foreach (Cache cache in data.GetCacheList())
            {
                cache.RefreshArray();
            }

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            return json;
        }

       /// <summary>
       /// Used for writting Cache
       /// </summary>
        public static string WriteCache(this Cache cache)
        {
            if (OperatingSystem.IsWindows())
            {
                FileWritter fileWritter = new FileWritter();

                string CacheFolder = $@"C:\Users\{fileWritter.GetCurrentUser()}\Temp\{cache.GroupFolder}\Cache\";
                string FileLocation = CacheFolder + $@"{cache.Name}.json";

                fileWritter.CreateSubDomains(CacheFolder);
                fileWritter.WriteFile(FileLocation, cache.ConvertToJSON());

                return $"Wrote '{cache.GetType()}' to '{CacheFolder}'";
            }

            throw new Exception($"An instance of 'Slide.CacheConstructor' tried to call a function that is not supported on your Operating System: Write your own WriteFile function if you want to use WriteCache.");
        }
    

        /// <summary>
        /// Used for writting CacheArray
        /// </summary>
        public static string WriteCache(this CacheArray cache)
        {
            if (OperatingSystem.IsWindows())
            {
                FileWritter fileWritter = new FileWritter();

                string CacheFolder = $@"C:\Users\{fileWritter.GetCurrentUser()}\Temp\Slide\Cache\";
                string FileLocation = CacheFolder + $@"{cache.Name}.json";

                fileWritter.CreateSubDomains(CacheFolder);
                fileWritter.WriteFile(FileLocation, cache.ConvertToJSON());

                return $"Wrote '{cache.GetType()}' to '{CacheFolder}'";
            }

            throw new Exception($"An instance of 'Slide.CacheConstructor' tried to call a function that is not supported on your Operating System: Write your own WriteFile function if you want to use WriteCache.");
        }

        /// <summary>
        /// Converts a string Cache into a double Cache
        /// </summary>
        public static Cache ConvertToCacheDouble(this Cache cache)
        {
            try
            {
                Cache convert = new Cache(cache.Name, Convert.ToDouble(cache._cache))
                {
                    RecacheTime = cache.RecacheTime,
                    AllowHardOverride = cache.AllowHardOverride,
                    Name = cache.Name,
                };

                return convert;
            }
            catch
            {
                throw new InvalidCastException($"Cannot convert 'Slide.Cache' to 'Slide.CacheDouble' : as string is not a number. \n\n _cache: '{cache._cache}");
            }
        }

        /// <summary>
        /// Converts the type of Cache from Double to String and returns a new Cache
        /// </summary>
        public static Cache ConvertToCacheString(this Cache cache)
        {
            try
            {
                Cache convert = new Cache(cache.Name, cache._cache.ToString())
                {
                    RecacheTime = cache.RecacheTime,
                    AllowHardOverride = cache.AllowHardOverride,
                    Name = cache.Name,
                };

                return convert;
            }
            catch
            {
                throw new InvalidCastException($"Cannot convert 'Slide.CacheDouble' to 'Slide.Cache' : as Double is not a string. \n\n _cache: '{cache._cache}");
            }
        }
    }
}
