using CenterRu;
using HtmlParser;
using IAuction;
using System;

namespace UTender
{
    [Serializable]
    public class UTender : Centerr
    {
        public UTender(Tag inpTag, IRequest myReq) : base(inpTag, myReq){}

        //override protected string baseUrl { get { return "http://www.utender.ru/"; } }
    }
}
