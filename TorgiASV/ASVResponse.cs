using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TorgiASV
{
    [Serializable]
    public class ASVResponse : IResponse
    {
        public IRequest MyRequest { get; private set; }
        public IEnumerable<IObject> ListResponse { get; private set; }

        public IEnumerable<IObject> NewRecords => throw new NotImplementedException();

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
            List<ASV> curList = new List<ASV>();

            myHTMLParser myParser = new myHTMLParser();
            List<Tag> myList = myParser.getTags(myWorkAnswer, "ul");
            List<Tag> resList = new List<Tag>();

            bool found = false;     // вычленяем из всех списков на странице только нужный
            foreach (Tag item in myList)
            {
                foreach (tagAttribute atItem in item.Attributes)
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

            foreach (Tag item in resList[0].InnerTags)    // заполняем результаты по списку
            {
                curList.Add(new ASV(item.InnerTags));
            }
            this.ListResponse = curList;
        }

        public bool SaveToXml(string fileName = "lastresponse.tasv")
        {
            return SaveMyASVResponseObject(this, fileName);
        }

        public IResponse LoadFromXml(string fileName = "lastresponse.tasv")
        {
            return LoadMyASVResponseObject(fileName);
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

        public bool HaveNewRecords(IResponse checkResponse)
        {
            throw new NotImplementedException();
        }

        public string NewRecordsOutput(bool html)
        {
            throw new NotImplementedException();
        }
    }
}
