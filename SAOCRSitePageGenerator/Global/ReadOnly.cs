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
        public static readonly string SnippetsListPath = SnippetsPath + "/Snippets";




        public static readonly string SnippetStructSnippet = "Snippet";
        public static readonly string SnippetStructSnippetHead = "Snippet Head";
        public static readonly string SnippetStructSnippetLoop = "Snippet Loop";
        public static readonly string SnippetStructSnippetEnd = "Snippet End";
        public static readonly string SnippetStructFieldList = "FieldList";
        public static readonly string SnippetStructConfig = "Config";



        public static readonly string SnippetName = "Snippet Name";
        public static readonly string SnippetGUID = "Snippet GUID";
        public static readonly string SnippetCreatedDate = "Created Date";
        public static readonly string SnippetLastUsed = "Last Used";
        public static readonly string SnippetRemark = "Remark";
        public static readonly string SnippetDestination = "Output Destination";
        public static readonly string SnippetOutputExtension = "Output Extension";
        public static readonly string SnippetFieldBracketL = "Field Bracket L";
        public static readonly string SnippetFieldBracketR = "Field Bracket R";
        public static readonly string SnippetIsRepeatedSnippet = "Repeated Snippet";




        public static readonly string SnippetFieldIndex = "Field Index";
        public static readonly string SnippetFieldName = "Field Name";
        public static readonly string SnippetFieldManualValue = "Value";
        public static readonly string SnippetFieldAutoFill = "Auto Fill";
        public static readonly string SnippetFieldReadOnly = "ReadOnly";
        public static readonly string SnippetFieldDataSet = "Data Set";
        public static readonly string SnippetFieldDataTable = "Data Table";
        public static readonly string SnippetFieldDataForLoopUse = "Data For Loop";
        public static readonly string SnippetFieldDataQuery = "Data Query";
        public static readonly string SnippetFieldDataQueryReturnColumnName = "Data Column Return Name";
        public static readonly string SnippetFieldForInternalUse = "Internal";
        public static readonly string SnippetFieldProcessCMD = "String Process";

        public static readonly string[] SnippetsListKeys = { SnippetName, SnippetCreatedDate, SnippetLastUsed, SnippetRemark, SnippetGUID };

        public static readonly string[] SnippetStructSingle = { SnippetStructSnippet, SnippetStructFieldList, SnippetStructConfig };
        public static readonly string[] SnippetStructMulti = { SnippetStructSnippetHead, SnippetStructSnippetLoop, SnippetStructSnippetEnd, SnippetStructFieldList, SnippetStructConfig };
        public static readonly string[] SnippetConfigKeys = { SnippetName, SnippetDestination, SnippetOutputExtension, SnippetFieldBracketL, SnippetFieldBracketR, SnippetCreatedDate, SnippetLastUsed, SnippetRemark, SnippetIsRepeatedSnippet };
        public static readonly string[] SnippetFieldListKeys = { SnippetFieldIndex, SnippetFieldName, SnippetFieldManualValue, SnippetFieldAutoFill, SnippetFieldReadOnly, SnippetFieldDataSet, SnippetFieldDataTable, SnippetFieldDataForLoopUse, SnippetFieldDataQuery, SnippetFieldDataQueryReturnColumnName, SnippetFieldForInternalUse, SnippetFieldProcessCMD };
        public static readonly Type[] SnippetFieldListValues = { typeof(int), typeof(string), typeof(string), typeof(bool), typeof(bool), typeof(string), typeof(string), typeof(bool), typeof(string), typeof(string), typeof(bool), typeof(string) };
        public static readonly Dictionary<string, Type> SnippetFieldListDict = Static.SnippetFieldListDict;




        public static readonly string FieldInQueryIdentifyCharL = "F<";
        public static readonly string FieldInQueryIdentifyCharR = ">";




        public static readonly string SourcePath = Application.StartupPath + "/Source";
        public static readonly string SourceDataSetDict = SourcePath + "/Sources";
        public static readonly string SourceMemoExtension = ".memo";

        public static readonly string SourceDataTableKey = "DataTable Key";
        public static readonly string SourceDataTableName = "DataTable Name";
        public static readonly string SourceDataTableList = "DataTable List";
        public static readonly string SourceDataTableRemark = "DataTable Remark";
        public static readonly string SourceDataTableGUID = "DataTable GUID";
        public static readonly string SourceDataTableInfoTableName = "DataTable Information Table";
        public static readonly string SourceDataTableColumnCount = "Column Count";
        public static readonly string SourceDataTableRowCount = "Row Count";
        public static readonly string SourceDataSetGUID = "DataSet GUID";
        public static readonly string SourceDataSetName = "DataSet Name";
        public static readonly string SourceDataSetRemark = "DataSet Remark";
        public static readonly string SourceDataSetLoaded = "DataSet Loaded";
        public static readonly string SourceDataSetDefaultName = "Default DataSet";

        public static readonly string[] SourceDataTableInfoKeys = { SourceDataTableName, SourceDataTableRemark, SourceDataTableGUID };
        public static readonly Type[] SourceDataTableInfoValues = { typeof(string), typeof(string), typeof(string) };
        public static readonly Dictionary<string, Type> SourceDataTableInfoTableDict = Static.SourceDataTableInfoTableDict;


        
        public static readonly string FieldProcessReplace = "RPL";
        public static readonly string FieldProcessSubstring = "SUB";
        public static readonly string FieldProcessRemove = "RMV";
        public static readonly string FieldProcessSplitAndGetAt = "SPL";
        public static readonly string FieldProcessInsert = "INS";
        public static readonly string FieldProcessToUpper = "TUP";
        public static readonly string FieldProcessToLower = "TDN";

        public static readonly string[] FieldProcessCommands = { FieldProcessReplace, FieldProcessSubstring, FieldProcessRemove, FieldProcessSplitAndGetAt, FieldProcessInsert, FieldProcessToUpper, FieldProcessToLower };
    }
}
