using HTTP_GET_POST;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TorgiASV
{
    [Serializable]
    public class ASVResponse
    {
        public ASVRequest MyRequest { get; private set; }
        public List<ASV> ListResponse { get; private set; }

        public ASVResponse(ASVRequest myReq)
        {
            this.MyRequest = myReq;
            FillListResponse();
        }

        public ASVResponse(ASVRequest myReq, List<ASV> listResp)
        {
            this.MyRequest = myReq;
            this.ListResponse = listResp;
        }

        private void FillListResponse()
        {
            string myWorkAnswer = MyRequest.GetResponse;

            myHTMLParser myParser = new myHTMLParser();
            List<HTTP_GET_POST.Tag> myList = myParser.getTags(myWorkAnswer, "ul");
            List<HTTP_GET_POST.Tag> resList = new List<HTTP_GET_POST.Tag>();

            bool found = false;     // вычленяем из всех списков на странице только нужный
            foreach (HTTP_GET_POST.Tag item in myList)
            {
                foreach (HTTP_GET_POST.tagAttribute atItem in item.Attributes)
                {
                    if (atItem.Name == "class" & atItem.Value == "\"component-list")
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    resList.Add(item);
                    break;
                }
            }

            foreach (HTTP_GET_POST.Tag item in resList[0].InnerTags)    // заполняем результаты по списку
            {
                ListResponse.Add(new ASV(item.InnerTags));
            }
        }

        static bool SaveMyASVResponseObject(ASVResponse curObj, string fileName = "lastresponse.tasv")
        {
            bool result = false;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream output = File.OpenWrite(fileName))
                {
                    bf.Serialize(output, curObj);
                }
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                //throw;
            }

            return result;
        }

        static ASVResponse LoadMyASVResponseObject(string fileName = "lastresponse.tasv")
        {
            ASVResponse result = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (ASVResponse)bf.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }
    }
}
