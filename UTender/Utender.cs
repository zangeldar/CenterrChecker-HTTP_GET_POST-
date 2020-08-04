using CenterRu;
using MyHTMLParser;
using System;

namespace UTender
{
    public class UTender : Centerr
    {
        public UTender(Tag inpTag) : base(inpTag){}

        override protected string baseUrl { get { return "http://bankrupt.centerr.ru"; } }
    }
}
