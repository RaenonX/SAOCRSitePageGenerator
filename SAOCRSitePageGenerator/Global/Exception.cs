using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOCRSitePageGenerator
{
    [Serializable]
    public class IllegalParameterException : Exception
    {
        public StringProcessCMDType CMDType { get; set; }
        public IllegalParameterException() { }
        public IllegalParameterException(StringProcessCMDType CMDType) {
            this.CMDType = CMDType;
        }
        public IllegalParameterException(string message) : base(message) { }
        public IllegalParameterException(string message, StringProcessCMDType CMDType) : base(message) {
            this.CMDType = CMDType;
        }
        protected IllegalParameterException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    
    [Serializable]
    public class DataColumnNotExistException : Exception
    {
        public DataColumnNotExistException() { }
        public DataColumnNotExistException(string message) : base(message) { }
        public DataColumnNotExistException(string message, Exception inner) : base(message, inner) { }
        protected DataColumnNotExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
