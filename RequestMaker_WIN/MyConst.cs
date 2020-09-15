using ASVorgRU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestMaker_WIN
{
    public static class MyConst
    {
        public const string ASVorgRU = "АСВ сайт";                         //  0
        public const string B2B = "B2B центр";                        //  1
        public const string CenterRu = "Центр Реализации";                 //  2
        public const string ETP_GPB = "ЭТП ГПБ";                          //  3
        public const string RosElTorg = "РосЭлТорг";                        //  4
        public const string RTSTender = "РТС-тендер";                       //  5
        public const string SberbankAST = "Сбербанк-АСТ";                     //  6
        public const string TekTorg = "ТЭК-Торг *";                       //  7
        public const string TorgiASV = "Торги АСВ";                        //  8
        public const string UTender = "Ю-Тендер";                         //  9
        public const string ZakupkiGov = "ГосЗакупки";                       //  10
        //public const string LotOnline      =   "Лот-Онлайн";                       //  11
        public const string LotOnlineArrested = "Лот-Онлайн Арест";                       //  12
        public const string LotOnlineConfiscate = "Лот-Онлайн Конфискат";                   //  13
        public const string LotOnlineFish = "Лот-Онлайн Рыба";                        //  14
        public const string LotOnlineLease = "Лот-Онлайн Лизинг";                      //  15
        public const string LotOnlinePrivatization = "Лот-Онлайн Приватизация";                //  16
        public const string LotOnlineRad = "Лот-Онлайн РАД";                         //  17
        public const string LotOnlineTrade = "Лот-Онлайн Торги";                    //  18
        public const string LotOnlineZalog = "Лот-Онлайн Залог";                       //  19

        public const string LotOnlineGz = "РАД Закупки";                    //  20
        public const string LotOnlineSales = "РАД Банкротство";                //  21
        public const string LotOnlineTender = "РАД Тендер **";                     //  22

        public static Dictionary<string, string> ETPs = new Dictionary<string, string>()
        {
            { "ASVorgRU","АСВ сайт" },
            { "B2B", "B2B центр" },                        //  1
            { "CenterRu", "Центр Реализации" },                 //  2
            { "ETP_GPB", "ЭТП ГПБ" },                          //  3
            { "RosElTorg", "РосЭлТорг" },                        //  4
            { "RTSTender", "РТС-тендер" },                       //  5
            { "SberbankAST", "Сбербанк-АСТ" },                     //  6
            { "TekTorg", "ТЭК-Торг *" },                       //  7
            { "TorgiASV", "Торги АСВ" },                        //  8
            { "UTender", "Ю-Тендер" },                         //  9
            { "ZakupkiGov", "ГосЗакупки" },                       //  10
            //{ "LotOnline      =   "Лот-Онлайн" },                       //  11
            { "LotOnlineArrested", "Лот-Онлайн Арест" },                       //  12
            { "LotOnlineConfiscate", "Лот-Онлайн Конфискат" },                   //  13
            { "LotOnlineFish", "Лот-Онлайн Рыба" },                        //  14
            { "LotOnlineLease", "Лот-Онлайн Лизинг" },                      //  15
            { "LotOnlinePrivatization", "Лот-Онлайн Приватизация" },                //  16
            { "LotOnlineRad", "Лот-Онлайн РАД" },                         //  17
            { "LotOnlineTrade", "Лот-Онлайн Торги" },                    //  18
            { "LotOnlineZalog", "Лот-Онлайн Залог" },                       //  19
    
            { "LotOnlineGz", "РАД Закупки" },                    //  20
            { "LotOnlineSales", "РАД Банкротство" },                //  21
            { "LotOnlineTender", "РАД Тендер **" },                     //  22
        };

        public static Dictionary<string, ETPStruct> ETP = new Dictionary<string, ETPStruct>()
        {
            { "АСВ сайт",                   new ETPStruct("ASVOrg", typeof(ASVorgRequest), typeof(ASVorgResponse))},
            { "B2B центр",                  new ETPStruct("B2B", typeof(B2B.B2BRequest), typeof(B2B.B2BResponse))},
            { "Центр Реализации",           new ETPStruct("CenterRu", typeof(CenterRu.CenterrRequest), typeof(CenterRu.CenterrResponse))},
            { "ЭТП ГПБ",                    new ETPStruct("ETP_GPB", typeof(ETP_GPB.GPBRequest), typeof(ETP_GPB.GPBResponse))},
            { "РосЭлТорг",                  new ETPStruct("RosElTorg", typeof(RosElTorg.RosElTorgRequest), typeof(RosElTorg.RosElTorgResponse))},
            { "РТС-тендер",                 new ETPStruct("RTSTender", typeof(RTSTender.RTSTenderRequest), typeof(RTSTender.RTSTenderResponse))},
            { "Сбербанк-АСТ",               new ETPStruct("SberbankAST", typeof(SberbankAST.SberbankAstRequest), typeof(SberbankAST.SberbankAstResponse))},
            { "ТЭК-Торг *",                 new ETPStruct("TekTorg", typeof(TekTorg.TekTorgRequest), typeof(TekTorg.TekTorgResponse))},
            { "Торги АСВ",                  new ETPStruct("TorgiASV", typeof(TorgiASV.TorgASVRequest), typeof(TorgiASV.TorgASVResponse))},
            { "Ю-Тендер",                   new ETPStruct("UTender", typeof(UTender.UTenderRequest), typeof(UTender.UTenderResponse))},
            { "ГосЗакупки",                 new ETPStruct("ZakupkiGov", typeof(ZakupkiGov.ZakupkiGovRequest), typeof(ZakupkiGov.ZakupkiGovResponse))},

            { "Лот-Онлайн Арест",           new ETPStruct("LotOnlineArrested", typeof(LotOnline.ArrestedLotOnlineRequest), typeof(LotOnline.LotOnlineResponse))},
            { "Лот-Онлайн Конфискат",       new ETPStruct("LotOnlineConfiscate", typeof(LotOnline.ConfiscateLotOnlineRequest), typeof(LotOnline.LotOnlineResponse))},
            { "Лот-Онлайн Рыба",            new ETPStruct("LotOnlineFish", typeof(LotOnline.FishLotOnlineRequest), typeof(LotOnline.LotOnlineResponse))},
            { "Лот-Онлайн Лизинг",          new ETPStruct("LotOnlineLease", typeof(LotOnline.LeaseLotOnlineRequest), typeof(LotOnline.LotOnlineResponse))},
            { "Лот-Онлайн Приватизация",    new ETPStruct("LotOnlinePrivatization", typeof(LotOnline.PrivatizationLotOnlineRequest), typeof(LotOnline.LotOnlineResponse))},
            { "Лот-Онлайн РАД",             new ETPStruct("LotOnlineRad", typeof(LotOnline.RadLotOnlineRequest), typeof(LotOnline.LotOnlineResponse))},
            { "Лот-Онлайн Торги",           new ETPStruct("LotOnlineTrade", typeof(LotOnline.TradeLotOnlineRequest), typeof(LotOnline.LotOnlineResponse))},
            { "Лот-Онлайн Залог",           new ETPStruct("LotOnlineZalog", typeof(LotOnline.ZalogLotOnlineRequest), typeof(LotOnline.LotOnlineResponse))},

            { "РАД Закупки",                new ETPStruct("LotOnlineGz", typeof(LotOnline.Gz.LotOnlineGzRequest), typeof(LotOnline.Gz.LotOnlineGzResponse))},
            { "РАД Банкротство",            new ETPStruct("LotOnlineSales", typeof(LotOnline.Sales.LotOnlineSalesRequest), typeof(LotOnline.Sales.LotOnlineSalesResponse))},
            { "РАД Тендер **",              new ETPStruct("LotOnlineTender", typeof(LotOnline.Tender.LotOnlineTenderRequest), typeof(LotOnline.Tender.LotOnlineTenderResponse))},
        };

        


    }
}
