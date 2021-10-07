using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

namespace DirectoryCacherConsoleApp;
public class DirectoryCacher
{
    IMemoryCache m_memoryCache;
    public DirectoryCacher(IMemoryCache memoryCache)
    {
        m_memoryCache = memoryCache;
    }

    public string GetListCache(string path)
    {
        string msg = string.Empty;

        var dirInfo = new DirectoryInfo(path);
        if (dirInfo.Exists)
        {
            List<string> cacheList = new List<string>();
            m_memoryCache.TryGetValue(path, out cacheList);

            if (cacheList == null)
            {
                BuildCache(path);
                m_memoryCache.TryGetValue(path, out cacheList);
                msg = "Reading directory from drive.";
            }
            else
            {
                msg = "Reading directory from cache.";
            }
        }
        else
        {
            throw new DirectoryNotFoundException();
        }

        return msg;
    }

    private void BuildCache(string path)
    {
        // Get a list of files from the given path
        var extensions = new string[] { ".png", ".jpg", ".gif" };
        var dirInfo = new DirectoryInfo(path);

        List<System.IO.FileInfo> fileInfoList = dirInfo.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower())).ToList();

        //
        // Check if directory is empty or not
        //
        if (fileInfoList.Count() != 0)
        {
            // Put all file names in our list
            List<string> fileInfo2string = fileInfoList.Select(f => f.FullName.ToString()).ToList();

            // Set cache options.
            var memCacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(120));

            // Cache the list
            m_memoryCache.Set(path, fileInfo2string, memCacheEntryOptions);

        }
        else
        {
            throw new FileNotFoundException();
        };

    }
}