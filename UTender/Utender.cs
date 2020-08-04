using CenterRu;
using MyHTMLParser;
using System;

namespace UTender
{
    public class UTender : Centerr
    {
        public UTender(Tag inpTag) : base(inpTag)
        {

        }

        private string baseUrl = "http://bankrupt.centerr.ru";
    }
}
