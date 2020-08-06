using CenterRu;
using HtmlParser;
using System;

namespace UTender
{
    [Serializable]
    public class UTender : Centerr
    {
        public UTender(Tag inpTag) : base(inpTag){}

        override protected string baseUrl { get { return "http://www.utender.ru/"; } }
    }
}
