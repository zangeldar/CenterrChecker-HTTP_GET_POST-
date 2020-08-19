//using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    [Serializable]
    public abstract class ATorg : IObject
    {
        //
        // Часть интерфейса
        //
        public Exception LastError { get; protected set; }
        public abstract string internalID { get; protected set; }
        public abstract string LotNameStr { get; protected set; }
        public abstract string LotNameUrl { get; protected set; }
        public abstract string PriceStart { get; protected set; }
        public abstract string LotNumberStr { get; protected set; }
        public abstract string ToString(bool html);

        /////////////////
        ///Часть абстрактного класса
        ///
        abstract public override bool Equals(object obj);
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
