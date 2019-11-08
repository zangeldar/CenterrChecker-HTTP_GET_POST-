using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParserNew
{
    public class Parser
    {
        public Parser()
        {

        }

        static public Tag ParseHTML(string innerHTML)
        {
            Tag curTag = null;

            while (curTag == null)
            {

            }
            return curTag;
        }

        static public void MyParse(string rawHtml)
        {
            List<Tag> docHTML = new List<Tag>();

            bool tagStart = false;
            bool tagEnd = false;
            bool tagSelf = false;
            bool tagOpen = false;
            string curTag = "";
            string tagContent = "";
            char prevCh = ' ';

            List<string> tags = new List<string>();
            List<string> cont = new List<string>();

            foreach (char chItem in rawHtml)
            {
                // проверка флагов предыдущего этапа                
                if (tagEnd)
                {
                    tagStart = false;
                    tagEnd = false;
                    //addTag(curTag);
                    tags.Add(curTag);
                    curTag = "";
                    tagOpen = true;
                }
                if (tagSelf)
                {
                    tagOpen = false;
                }

                // текущее выполнение
                if (tagOpen)
                {
                    tagContent += chItem;
                }
                else if (tagContent != "")
                {
                    cont.Add(tagContent);
                    tagContent = "";
                }

                if (tagStart)
                {                    
                    curTag += chItem;
                }                 

                // расстановка флагов для следующего этапа
                if (chItem == '<')          // если < значит начало тега
                {
                    tagOpen = false;
                    tagStart = true;
                }
                    
                else if (chItem == '/')     // иначе если / значит
                {                               //
                    if (prevCh == '<')          // если пред < значит закрытие тега
                        tagOpen = false;
                }                    
                else if (chItem == '>')     // иначе если > значит
                {                               //
                    if (prevCh == '/')          // если пред / тогда самотег
                        tagSelf = true;
                    tagEnd = true;              // значит тег закончен.
                }
                
                



                prevCh = chItem;
            }

        }
    }
}
