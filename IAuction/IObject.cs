using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    public interface IObject
    {
        string internalID { get; }
        string LotNameStr { get; }
        string LotNameUrl { get; }
        string PriceStart { get; }
        string LotNumberStr { get; }
        string ToString(bool html);
    }
}
