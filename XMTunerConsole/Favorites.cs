using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace XMTuner
{
    class Favorites
    {
        String datapath = configMan.datapath;
        String favoritesFile = "favorites.txt";
        List<FavoriteChannel> favorites = new List<FavoriteChannel>();
        String path;

        public Favorites()
        {
            path = datapath+favoritesFile;
            open();
        }

        private void open() {
            FileStream fs;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException)
            {
                return;
            }
            StreamReader textIn = new StreamReader(fs);
            String newline = "";
            String[] parts = new String[2];
            String header = textIn.ReadLine();
            while (textIn.Peek() != -1)
            {

                newline = textIn.ReadLine();
                if (newline.Contains(","))
                {
                    parts = newline.Split(',');
                    int num = Convert.ToInt32(parts[0]);
                    if (parts[1].Equals("") == true) { parts[1] = "0"; } //Support 0.6 format favorites files with no presets
                    int preset = Convert.ToInt32(parts[1]);
                    if (preset > 0)
                    {
                        favorites.Add(new FavoriteChannel(num, preset));
                    }
                    else
                    {
                        favorites.Add(new FavoriteChannel(num));
                    }
                }
            }
            textIn.Close();
        }

        private Boolean save()
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);
            textOut.WriteLine("XMTuner Favorite Channels");
            foreach (FavoriteChannel channel in favorites.AsReadOnly())
            {
                textOut.WriteLine(channel.ToString());
            }

            textOut.Close();
            return true;
        }

        public Boolean addFavoriteChannel(int channel)
        {
            if (exists(channel))
            {
                return false;
            }

            favorites.Add(new FavoriteChannel(channel));
            save();
            return true;
        }

        public Boolean removeFavoriteChannel(int channel)
        {
            if (exists(channel) == false) { return false; }
            Boolean r = favorites.Remove(Find(channel));
            save();
            return r;
        }

        public Boolean isFavorite(int channel)
        {
            return exists(channel);
        }

        private Boolean exists(int channel)
        {
            if (favorites.Exists(
                delegate(FavoriteChannel chan)
                {
                    return chan.num == channel;
                }
            ))
            {
                return true;
            }
            return false;
        }

        protected FavoriteChannel Find(int channum)
        {
            FavoriteChannel result = favorites.Find(
            delegate(FavoriteChannel chan)
            {
                return chan.num == channum;
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

        public Boolean addPreset(int num, int preset)
        {
            FavoriteChannel channel = Find(num);
            if (channel == null) { return false; }
            channel.addPreset(preset);
            if (save() == false)
            {
                return false;
            }
            return true;
        }

        public Boolean hasPreset(int num)
        {
            FavoriteChannel channel = Find(num);
            if (channel == null || channel.preset == 0) { return false; }
            return true;
        }

        public Int32 getPreset(int num)
        {
            FavoriteChannel channel = Find(num);
            if (channel == null) { return 0; }
            return channel.preset;
        }
        public String getPreset(String num)
        {
            String preset = "";
            int pn = getPreset(Convert.ToInt32(num));
            if (pn > 0)
            {
                preset = pn.ToString();
            }
            return preset;
        }

        public Boolean removePreset(int num)
        {
            FavoriteChannel channel = Find(num);
            if (channel == null) { return false; }
            channel.removePreset();
            if (save() == false)
            {
                return false;
            }
            return true;
        }

        public Int32 findPreset(int presetnum)
        {
            FavoriteChannel result = favorites.Find(
                delegate(FavoriteChannel chan)
                {
                    return chan.preset == presetnum;
                }
            );
            if (result != null)
            {
                return result.num;
            }
            else
            {
                return 0;
            }
        }
    }

    class FavoriteChannel
    {
        public Int32 num;
        public Int32 preset;

        public FavoriteChannel(Int32 num, Int32 preset) : this(num) { this.preset = preset; }
        public FavoriteChannel(Int32 num)
        {
            this.num = num;
        }

        public void addPreset(Int32 preset)
        {
            this.preset = preset;
        }

        public void removePreset()
        {
            this.preset = 0;
        }

        public override String ToString()
        {
            String theString =  num + "," + preset;
            return theString;
        }

    }
}
