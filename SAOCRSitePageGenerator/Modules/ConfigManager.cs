using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAOCRSitePageGenerator
{
    public class ConfigManager : IDisposable
    {
        string mConfigPath;
        Dictionary<string, string> mConfigs = new Dictionary<string, string>();
        StreamReader SR;

        public ConfigManager(string ConfigPath)
        {
            mConfigPath = ConfigPath;

            if (!File.Exists(mConfigPath))
                using (FileStream FS = File.Create(mConfigPath)) { }

            SR = new StreamReader(mConfigPath);

            lock (SR)
            {
                while (!SR.EndOfStream)
                {
                    string Raw = SR.ReadLine();
                    string[] Data = Raw.Split('=');
                    if (Data.Length == 2) mConfigs.Add(Data[0], Data[1]);
                }
                SR.Close();
            }
        }

        public string GetConfig(string Key)
        {
            string ReturnVal = string.Empty;
            try
            {
                mConfigs.TryGetValue(Key, out ReturnVal);
            }
            catch (Exception) { }
            return ReturnVal;
        }

        public Dictionary<string, string> GetConfigAll()
        {
            return mConfigs;
        }

        public void SetConfig(string Key, string Data)
        {
            if (mConfigs.ContainsKey(Key))
                mConfigs[Key] = Data;
            else
                mConfigs.Add(Key, Data);
        }

        public void RemoveConfig(string Key)
        {
            mConfigs.Remove(Key);
        }

        public void SaveAsync()
        {
            int SaveTryCounter = 0;
            
            try
            {
                SaveTryCounter++;
                Thread T = new Thread(new ThreadStart(new MethodInvoker(delegate ()
                {
                    StreamWriter SW = new StreamWriter(mConfigPath);
                    lock (SW)
                    {
                        SW.AutoFlush = true;
                        SW.NewLine = "\n";

                        foreach (KeyValuePair<string, string> Data in mConfigs) SW.WriteLine(Data.Key + "=" + Data.Value);
                        SW.Close();
                        SW.Dispose();
                    }
                })));
                T.Start();
            }
            catch (IOException)
            {
                if (SaveTryCounter > 10)
                {
                    MessageBox.Show("Config file saving failed.\n\nPath: " + mConfigPath);
                }
                SaveAsync();
            }
        }

        public DataTable GetDataTable(string TableName = "Configs")
        {
            string[] Names = { "Key", "Value" };

            DataTable DT = new DataTable(TableName);

            foreach (string Name in Names)
            {
                DataColumn DC = new DataColumn(Name, typeof(string));
                DC.AllowDBNull = true;
                DT.Columns.Add(DC);
            }

            foreach (KeyValuePair<string, string> Data in mConfigs)
            {
                try
                {
                    DataRow DR;
                    DR = DT.NewRow();
                    DR.SetField(Names[0], Data.Key);
                    DR.SetField(Names[1], Data.Value);
                    DT.Rows.Add(DR);
                }
                catch (Exception) { }
            }
            return DT;
        }

        public void Dispose()
        {
            SR.Dispose();
        }
    }
}
