using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOCRSitePageGenerator
{
    public static class Static
    {
        public static Dictionary<string, Type> SnippetFieldListDict = new Dictionary<string, Type>();

        public static void InitStaticVariants()
        {
            for (byte i = 0; i < ReadOnly.SnippetFieldListKeys.Length; i++)
                SnippetFieldListDict.Add(ReadOnly.SnippetFieldListKeys[i], ReadOnly.SnippetFieldListValues[i]);
        }
    }
}
