/*
 * XMTuner: Copyright (C) 2009-2012 Chris Crews and Curtis M. Kularski.
 * 
 * This file is part of XMTuner.

 * XMTuner is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * XMTuner is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with XMTuner.  If not, see <http://www.gnu.org/licenses/>.
 */

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
        List<int> favorites = new List<int>();
        //List<int> presets = new List<int>(); //OrderedDictionary?
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
            int i = 0;
            String newline = "";
            String[] parts = new String[2];
            String header = textIn.ReadLine();
            while (textIn.Peek() != -1)
            {

                newline = textIn.ReadLine();
                if (newline.Contains(","))
                {
                    parts = newline.Split(',');
                    favorites.Add(Convert.ToInt32(parts[0]));
                }
                i++;
            }
            textIn.Close();
        }

        private Boolean save()
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);
            String value;
            textOut.WriteLine("XMTuner Favorite Channels");
            foreach (int channel in favorites.AsReadOnly())
            {
                value = "";//newConfig.Get(configKey);
                textOut.WriteLine(channel + "," + value);
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

            favorites.Add(channel);
            save();
            return true;
        }

        public Boolean removeFavoriteChannel(int channel)
        {
            if (exists(channel) == false) { return false; }
            Boolean r = favorites.Remove(channel);
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
                delegate(int chan)
                {
                    return chan == channel;
                }
            ))
            {
                return true;
            }
            return false;
        }
    }
}
