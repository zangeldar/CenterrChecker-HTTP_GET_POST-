using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestMaker_WIN
{
    public struct ETPStruct
    {
        public string Name { get; private set; }
        public Type RequestType { get; private set; }
        public Type ResponseType { get; private set; }        

        public ETPStruct(string name, Type reqType, Type respType)
        {
            Name = name;
            RequestType = reqType;
            ResponseType = respType;
        }
    }
}
