using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    public abstract class ATorg : IObject
    {
        /// <summary>
        /// Часть интерфейса
        /// </summary>
        public abstract string internalID { get; }
        public abstract string LotNameStr { get; }
        public abstract string LotNameUrl { get; }
        public abstract string PriceStart { get; }
        public abstract string LotNumberStr { get; }

        public abstract string ToString(bool html);

        /////////////////
        ///Часть абстрактного класса
        ///
        public ATorg(List<StringUri> itemsList)
        {

        }

        /////////////////////
        ///Часть реализации интерфейса
        ///


    }
}
