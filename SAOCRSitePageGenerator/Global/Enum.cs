using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Flags]
public enum StringProcessCMDType
{
    Replace,
    Substring,
    Remove,
    SplitAndGetAt,
    Insert,
    ToUpper,
    ToLower,
    Unknown
}
