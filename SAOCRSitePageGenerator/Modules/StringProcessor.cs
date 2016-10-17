using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOCRSitePageGenerator
{
    public class StringProcessor
    {
        public string unHandledString { get; set; }
        public string HandledString { get; private set; }

        #region Initialize
        public StringProcessor(string unHandledString)
        {
            HandledString = this.unHandledString = unHandledString;
        }

        public StringProcessor()
        {

        }
        #endregion

        public string Process(string CMDSingleLine)
        {
            return Process(CMDSingleLine, false);
        }

        public string Process(string CMDSingleLine, bool useHandledString)
        {
            if (string.IsNullOrEmpty(unHandledString)) { return string.Empty; }
            List<string> ParsedCommand = new List<string>(CMDSingleLine.Replace("\r", "").Split(new char[] { ' ' }, 3));

            while (ParsedCommand.Count < 3) { ParsedCommand.Add(string.Empty); }
            
            StringProcessCMDType CMDType = StringToEnumTranfer(ParsedCommand[0]);
            string Param1 = ParsedCommand[1];
            string Param2 = ParsedCommand[2];

            switch (CMDType)
            {
                case StringProcessCMDType.Replace:
                    if (string.IsNullOrEmpty(Param1) || string.IsNullOrEmpty(Param2))
                    {
                        throw new ArgumentException("Illegal Parameter when using REPLACE string processing command. Received command: " + CMDSingleLine);
                    }
                    else
                    {
                        HandledString = (useHandledString ? HandledString : unHandledString).Replace(Param1, Param2);
                        return HandledString;
                    }
                case StringProcessCMDType.Substring:
                    if (string.IsNullOrEmpty(Param1))
                    {
                        throw new ArgumentException("Illegal Parameter when using SUBSTRING string processing command. Received command: " + CMDSingleLine);
                    }
                    else
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(Param2))
                            {
                                HandledString = (useHandledString ? HandledString : unHandledString).Substring(Convert.ToInt32(Param1));
                                return HandledString;
                            }
                            else
                            {
                                HandledString = (useHandledString ? HandledString : unHandledString).Substring(Convert.ToInt32(Param1), Convert.ToInt32(Param2));
                                return HandledString;
                            }
                        }
                        catch (FormatException)
                        {
                            throw new ArgumentException("Illegal Parameter format sent in when using SUBSTRING string processing command. Parameters must be all integer. Received command: " + CMDSingleLine);
                        }
                    }
                case StringProcessCMDType.Remove:
                    if (string.IsNullOrEmpty(Param1))
                    {
                        throw new ArgumentException("Illegal Parameter when using REMOVE string processing command. Received command: " + CMDSingleLine);
                    }
                    else
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(Param2))
                            {
                                HandledString = (useHandledString ? HandledString : unHandledString).Remove(Convert.ToInt32(Param1));
                                return HandledString;
                            } else
                            {
                                HandledString = (useHandledString ? HandledString : unHandledString).Remove(Convert.ToInt32(Param1), Convert.ToInt32(Param2));
                                return HandledString;
                            }
                        }
                        catch (FormatException)
                        {
                            throw new ArgumentException("Illegal Parameter format sent in when using REMOVE string processing command. Parameters must be all integer. Received command: " + CMDSingleLine);
                        }
                    }
                case StringProcessCMDType.SplitAndGetAt:
                    if (string.IsNullOrEmpty(Param1) || string.IsNullOrEmpty(Param2))
                    {
                        throw new ArgumentException("Illegal Parameter when using SPLIT string processing command. All Parameters must contain a value. Received command: " + CMDSingleLine);
                    }
                    else
                    {
                        try
                        {
                            if (Param1.Length > 1)
                            {
                                throw new ArgumentException("Illegal Parameter 1 when using SPLIT string processing command. Parameter 1 is the separator to split and it force to only have 1 character. Received command: " + CMDSingleLine);
                            }
                            try
                            {
                                HandledString = (useHandledString ? HandledString : unHandledString).Split(Param1[0])[Convert.ToInt32(Param2)];
                            }
                            catch (IndexOutOfRangeException)
                            {
                                string[] Splitted = (useHandledString ? HandledString : unHandledString).Split(Param1[0]);
                                HandledString = Splitted[Splitted.Length - 1];
                            }
                            return HandledString;
                        }
                        catch (FormatException)
                        {
                            throw new ArgumentException("Illegal Parameter 2 when using SPLIT string processing command. Parameter 2 is the element index you want to get after split, so it should be integer. Received command: " + CMDSingleLine);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            string[] ReturnValue = (useHandledString ? HandledString : unHandledString).Split(Param1[0]);
                            HandledString = ReturnValue[ReturnValue.Length];
                            return HandledString;
                        }
                    }
                case StringProcessCMDType.Insert:
                    if (string.IsNullOrEmpty(Param1) || string.IsNullOrEmpty(Param2))
                    {
                        throw new ArgumentException("Illegal Parameter when using INSERT string processing command. All Parameters must contain a value. Received command: " + CMDSingleLine);
                    }
                    else
                    {
                        try
                        {
                            HandledString = (useHandledString ? HandledString : unHandledString).Insert(Convert.ToInt32(Param1), Param2);
                            return HandledString;
                        }
                        catch (FormatException)
                        {
                            throw new ArgumentException("Illegal Parameter 1 when using INSERT string processing command. Parameter 1 is the position that you want to insert the specified string(Param2). Received command: " + CMDSingleLine);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return unHandledString;
                        }
                    }
                case StringProcessCMDType.ToUpper:
                    HandledString = (useHandledString ? HandledString : unHandledString).ToUpper();
                    return HandledString;
                case StringProcessCMDType.ToLower:
                    HandledString = (useHandledString ? HandledString : unHandledString).ToLower();
                    return HandledString;
                case StringProcessCMDType.Unknown:
                default:
                    throw new ArgumentException("Illegal string process command. Received command: " + CMDSingleLine);
            }
        }

        public string GetStringProcessCMD(StringProcessCMDType CMDType, string Param1, string Param2)
        {
            string CMDString = "";
            bool isIllegalCMD = false;

            switch (CMDType)
            {
                case StringProcessCMDType.Replace:
                    CMDString += ReadOnly.FieldProcessReplace;
                    if (string.IsNullOrEmpty(Param1) || string.IsNullOrEmpty(Param2)) { isIllegalCMD = true; }
                    break;
                case StringProcessCMDType.Substring:
                    CMDString += ReadOnly.FieldProcessSubstring;
                    if (string.IsNullOrEmpty(Param1) || string.IsNullOrEmpty(Param2)) { isIllegalCMD = true; }
                    break;
                case StringProcessCMDType.Remove:
                    CMDString += ReadOnly.FieldProcessRemove;
                    if (string.IsNullOrEmpty(Param1)) { isIllegalCMD = true; }
                    break;
                case StringProcessCMDType.SplitAndGetAt:
                    CMDString += ReadOnly.FieldProcessSplitAndGetAt;
                    if (string.IsNullOrEmpty(Param1) || string.IsNullOrEmpty(Param2)) { isIllegalCMD = true; }
                    break;
                case StringProcessCMDType.Insert:
                    CMDString += ReadOnly.FieldProcessInsert;
                    if (string.IsNullOrEmpty(Param1) || string.IsNullOrEmpty(Param2)) { isIllegalCMD = true; }
                    break;
                case StringProcessCMDType.ToUpper:
                    CMDString += ReadOnly.FieldProcessToUpper;
                    break;
                case StringProcessCMDType.ToLower:
                    CMDString += ReadOnly.FieldProcessToLower;
                    break;
                case StringProcessCMDType.Unknown:
                    break;
                default:
                    throw new ArgumentException("Illegal string process command type.");
            }
            CMDString += " " + Param1 + " " + Param2;

            if (isIllegalCMD)
            {
                throw new IllegalParameterException("Illegal string process parameter. Received command: " + CMDString, CMDType);
            }

            return CMDString;
        }

        #region Private Methods
        private StringProcessCMDType StringToEnumTranfer(string CMDTypeSection)
        {
            if (CMDTypeSection == ReadOnly.FieldProcessReplace)
                return StringProcessCMDType.Replace;
            else if (CMDTypeSection == ReadOnly.FieldProcessSubstring)
                return StringProcessCMDType.Substring;
            else if (CMDTypeSection == ReadOnly.FieldProcessRemove)
                return StringProcessCMDType.Remove;
            else if (CMDTypeSection == ReadOnly.FieldProcessSplitAndGetAt)
                return StringProcessCMDType.SplitAndGetAt;
            else if (CMDTypeSection == ReadOnly.FieldProcessInsert)
                return StringProcessCMDType.Insert;
            else if (CMDTypeSection == ReadOnly.FieldProcessToUpper)
                return StringProcessCMDType.ToUpper;
            else if (CMDTypeSection == ReadOnly.FieldProcessToLower)
                return StringProcessCMDType.ToLower;
            else
                return StringProcessCMDType.Unknown;
        }
        #endregion
    }
}
