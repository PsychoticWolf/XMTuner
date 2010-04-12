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
        String network;

        private String versionPrefix = "::XMTUNER-VERSION-START::";
        private String versionSuffix = "::XMTUNER-VERSION-END::";

        public CacheManager(Log log, String network) : this() { this.log = log; this.network = network; }
        public CacheManager(Log log) : this() { this.log = log; }
        public CacheManager()
        {
            useLocalDatapath = new configMan().useLocalDatapath;
        }

        public Boolean enabled
        {
            get { return useCache; }
        }

        private String version
        {
            get { return versionPrefix + configMan.version + "|" + network + versionSuffix; }
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

        public String getFile(String file) { return getFile(file, true); }
        private String getFile(String file, Boolean rmVersion)
        {
            if (useCache == false) { return null; }
            String path = getDataPath(file);
            if (isInvalidatedFileP(path))
            {
                return null;
            }
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader textIn = new StreamReader(fs);
            String data = textIn.ReadToEnd();
            textIn.Close();
            if (rmVersion)
            {
                return removeVersion(data);
            }
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
            if (isVersionCurrent(file))
            {

                Double value = Convert.ToDouble(findCacheFile(file)[1]);
                return isDataCurrent(file, value);
            }
            return false;
        }

        public bool isInvalidatedFile(String file)
        {
            return isInvalidatedFileP(getDataPath(file));
        }
        private bool isInvalidatedFileP(String path)
        {
            if (File.GetLastWriteTime(path) == new DateTime(1985, 1, 1))
            {
                return true;
            }
            return false;
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
            data = prependVersion(data);

            String ident = filename;
            String path = getDataPath(filename);
            if (findCacheFile(filename) != null)
            {
                ident = findCacheFile(filename)[2];
            }

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

        private String prependVersion(String data)
        {
            return version+data;
        }

        private String removeVersion(String data)
        {
            return data.Replace(version, "");
        }

        private Boolean isVersionCurrent(String file)
        {
            String data = getFile(file, false);
            if (data == null) { return false; }

            int start = data.IndexOf(versionPrefix)+versionPrefix.Length;
            int length = data.IndexOf(versionSuffix);
            if (length == -1) {
                invalidateFile(file);
                return false;
            }
            String[] temp = data.Substring(start, length-start).Split('|');

            String dVersion = temp[0];
            String dNetwork = temp[1];

            if (dVersion != configMan.version || dNetwork != network)
            {
                invalidateFile(file);
                return false;
            }
            return true;
        }

        public static String getFileS(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader textIn = new StreamReader(fs);
            String data = textIn.ReadToEnd();
            textIn.Close();

            return data;
        }
    }
}
