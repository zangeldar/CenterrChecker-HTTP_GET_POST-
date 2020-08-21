using HtmlParser;
using IAuction;
using System;

namespace RTSTender
{
    public class RTSTender : ATorg
    {
        public RTSTender(Tag inpTag) { }

        public override string internalID { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override string LotNameStr { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override string LotNameUrl { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override string PriceStart { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override string LotNumberStr { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString(bool html)
        {
            throw new NotImplementedException();
        }
    }
}
