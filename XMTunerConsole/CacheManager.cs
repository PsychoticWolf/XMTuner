using System;
using System.Collections.Generic;
using System.IO;

namespace XMTuner
{
    public class CacheManager
    {
        private Boolean useCache = true;
        private List<String[]> cacheFiles = new List<String[]>();
        private Boolean useLocalDatapath = false;
        Log log;

        public CacheManager(Boolean uLDP, Log log) : this(uLDP) { this.log = log; }
        public CacheManager(Boolean uLDP)
        {
            useLocalDatapath = uLDP;
        }

        public Boolean enabled
        {
            get { return useCache; }
        }

        public void purgeCache()
        {
            foreach (String[] file in cacheFiles)
            {
                invalidateFile(file[0]);
            }
        }

        public void addCacheFile(String file, String ident, Double MaxAge)
        {
            if (useCache == false) { return; }

            if (findCacheFile(file) == null)
            {
                String[] item = new String[] { file, MaxAge.ToString(), ident };
                cacheFiles.Add(item);
            }
        }

        public String getFile(String file)
        {
            if (useCache == false) { return null; }
            String path = getDataPath(file);
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader textIn = new StreamReader(fs);
            String data = textIn.ReadToEnd();
            textIn.Close();
            return data;
        }

        private String getDataPath(String file)
        {
            String directory = "";
            if (useLocalDatapath == false)
            {
                directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            }
            if (file.Equals(""))
            {
                return directory;
            }
            else
            {
                if (directory.Equals(""))
                {
                    return file;
                }
                else
                {
                    return directory + "\\" + file;
                }
            }
        }

        private String[] findCacheFile(String filename)
        {
            String[] result = cacheFiles.Find(
                delegate(String[] cacheFile)
                {
                    return cacheFile[0] == filename;
                }
            );
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private Boolean isDataCurrent(String file, Double value)
        {
            String path = getDataPath(file);
            DateTime dt = File.GetLastWriteTime(path);
            DateTime maxage = DateTime.Now;
            maxage = maxage.AddDays(-1);
            if (dt > maxage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean isCached(String file)
        {
            if (useCache == false) { return false; }
            Double value = Convert.ToDouble(findCacheFile(file)[1]);
            return isDataCurrent(file, value);
        }

        public void invalidateFile(String file)
        {
            String path = getDataPath(file);
            try
            {
                File.SetLastWriteTime(path, new DateTime(1985, 1, 1));
            }
            catch (FileNotFoundException) { }
        }

        public Boolean saveFile(String filename, String data)
        {
            if (useCache == false) { return false; }
            String ident = filename;
            String path = getDataPath(filename);
            ident = findCacheFile(filename)[2];

            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter textOut = new StreamWriter(fs);
                textOut.Write(data);
                textOut.Close();
                return true;
            }
            catch (IOException e)
            {
                if (log == null) { return false; }
                log.output("Error encountered saving "+ident+" to cache. (" + e.Message + ")", "error");
                return false;
            }

        }
    }
}
