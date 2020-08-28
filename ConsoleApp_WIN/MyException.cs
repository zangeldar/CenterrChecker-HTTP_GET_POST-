using IAuction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WIN
{
    [Serializable]
    public class MyException : Exception
    {        
        public MyException(IResponse myResp) { SrcResponse = myResp; }
        public MyException(IResponse myResp, string message) : base(message) { SrcResponse = myResp; }
        public MyException(IResponse myResp, string message, Exception inner) : base(message, inner) { SrcResponse = myResp; }
        protected MyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public IResponse SrcResponse { get; private set; }

        public bool SaveToFile(string fileName = "temp.err", bool overwrite = false)
        {
            return SFileIO.SaveMyObject(this, fileName, overwrite);
        }

        static public MyException LoadFromFile(string fileName = "temp.err")
        {
            Object myObj = SFileIO.LoadMyObject(fileName);
            if (myObj is MyException)
                return (MyException)myObj;
            return null;
        }
    }
}
