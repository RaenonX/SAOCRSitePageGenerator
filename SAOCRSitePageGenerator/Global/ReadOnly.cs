using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAOCRSitePageGenerator
{
    public static class ReadOnly
    {
        public static readonly string SnippetsPath = Application.StartupPath + "/Snippets";

        public static readonly string SnippetsList = "Snippets";

        public static readonly string SnippetStructSnippet = "Snippet";
        public static readonly string SnippetStructSnippetHead = "Snippet Head";
        public static readonly string SnippetStructSnippetLoop = "Snippet Loop";
        public static readonly string SnippetStructSnippetEnd = "Snippet End";
        public static readonly string SnippetStructFieldList = "FieldList";
        public static readonly string SnippetStructConfig = "Config";

        public static readonly string SnippetName = "Snippet Name";
        public static readonly string SnippetFolderKey = "Folder Key";
        public static readonly string SnippetCreatedDate = "Created Date";
        public static readonly string SnippetLastUsed = "Last Used";
        public static readonly string SnippetNotes = "Notes";
        public static readonly string SnippetDestination = "Output Destination";
        public static readonly string SnippetOutputExtension = "Output Extension";
        public static readonly string SnippetFieldBracketL = "Field Bracket L";
        public static readonly string SnippetFieldBracketR = "Field Bracket R";
        public static readonly string SnippetIsRepeatedSnippet = "Repeated Snippet";

        public static readonly string SnippetFieldName = "Field Name";
        public static readonly string SnippetFieldDataSet = "Data Set";
        public static readonly string SnippetFieldDataSource = "Data Source";
        public static readonly string SnippetFieldDataColumnName = "Data Column Name";
        public static readonly string SnippetFieldDataSelect = "Data Select";
        public static readonly string SnippetFieldAutoFill = "Auto Fill";
        public static readonly string SnippetFieldReadOnly = "ReadOnly";

        public static readonly string[] SnippetsListKeys = { SnippetName, SnippetCreatedDate, SnippetLastUsed, SnippetNotes, SnippetFolderKey };

        public static readonly string[] SnippetStructSingle = { SnippetStructSnippet, SnippetStructFieldList, SnippetStructConfig };
        public static readonly string[] SnippetStructMulti = { SnippetStructSnippetHead, SnippetStructSnippetLoop, SnippetStructSnippetEnd, SnippetStructFieldList, SnippetStructConfig };
        public static readonly string[] SnippetConfigKeys = { SnippetName, SnippetDestination, SnippetOutputExtension, SnippetFieldBracketL, SnippetFieldBracketR, SnippetCreatedDate, SnippetLastUsed, SnippetNotes, SnippetIsRepeatedSnippet };
        public static readonly string[] SnippetFieldListKeys = { SnippetFieldName, SnippetFieldAutoFill, SnippetFieldReadOnly, SnippetFieldDataSet, SnippetFieldDataSource, SnippetFieldDataColumnName, SnippetFieldDataSelect };
        public static readonly Type[] SnippetFieldListValues = { typeof(string), typeof(bool), typeof(bool), typeof(string), typeof(string), typeof(string), typeof(string) };
        public static readonly Dictionary<string, Type> SnippetFieldListDict = Static.SnippetFieldListDict;


        public static readonly string SourcePath = Application.StartupPath + "/Source";
        public static readonly string SourceDataSetPath = SourcePath + "/DataSet";
        public static readonly string SourceDataTablePath = SourcePath + "/DataTable";

        public static readonly string[] SourceDataFolders = { SourceDataSetPath, SourceDataTablePath };
    }
}
