using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOCRSitePageGenerator
{
    public static class Static
    {
        public static Dictionary<Guid, DataSet> SnippetExternalSource = new Dictionary<Guid, DataSet>();

        public static Dictionary<string, Type> SnippetFieldListDict = new Dictionary<string, Type>();
        public static Dictionary<string, Type> SourceDataTableInfoTableDict = new Dictionary<string, Type>();

        public static void InitStaticVariants()
        {
            for (byte i = 0; i < ReadOnly.SnippetFieldListKeys.Length; i++)
                SnippetFieldListDict.Add(ReadOnly.SnippetFieldListKeys[i], ReadOnly.SnippetFieldListValues[i]);
            for (byte i = 0; i < ReadOnly.SourceDataTableInfoKeys.Length; i++)
                SourceDataTableInfoTableDict.Add(ReadOnly.SourceDataTableInfoKeys[i], ReadOnly.SourceDataTableInfoValues[i]);
        }
    }
}
