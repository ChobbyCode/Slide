using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Slide
{
    internal class FileWritter
    {
        [SupportedOSPlatform("windows")]
        internal void WriteFile(string loc, string text)
        {

            FileStream? file = null;
            FileInfo? fileInfo = null;

            if(!File.Exists(loc))
            {
                try
                {
                    File.Create(loc).Close();
                }catch (Exception ex)
                {
                    throw new Exception($"Failed to create cache file. \n\n {ex}");
                }
            }

            try
            {
                string fileOpen = File.ReadAllText(loc);

                fileOpen = text;

                File.WriteAllText(loc, fileOpen);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to write file '{loc}'");
            }
        }

        /// <summary>
        /// Gets the current user. Windows Only!
        /// </summary>
        [SupportedOSPlatform("windows")]
        public string GetCurrentUser()
        {

            string path = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            int EndLoc = path.IndexOf(@"\");

            if (EndLoc == -1)
            {
                // Throw Error if no '\' is found
                throw new Exception($"Failed to path path. Could not parse 'CurrentUser' from path.. '{path}'");
            }

            // Remove all the stuff up to the back slash
            path = path.Remove(0, EndLoc + 1);

            // return the new value
            return path;
        }

        [SupportedOSPlatform("windows")]
        internal void CreateSubDomains(string loc)
        {

            string[] subdomains = loc.Split(@"\");

            string fullDomain = "";
            int loop = 0;
            foreach(string domain in subdomains)
            {
                loop++;
                if(loop == subdomains.Length)
                {
                    return;
                }

                fullDomain = fullDomain + domain + @"\";

                if(domain != "C:" && domain != "Users" && domain != GetCurrentUser())
                {
                    if(!Directory.Exists(fullDomain))
                    {
                        Directory.CreateDirectory(fullDomain);
                    }
                }
            }
        }
    }
}
