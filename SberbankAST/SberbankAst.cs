using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace SberbankAST
{
    public class SberbankAst : ATorg
    {
        public SberbankAst(Hit inpHit)
        {
            //internalID = inpHit._id;
            TorgNameStr = inpHit.Source.PurchName;           // Описание закупки "Открытый запрос котировок в электронной форме на право заключение договора на поставку ГСМ и технических жидкостей для автомобильного транспорта"            
            LotNameStr = inpHit.Source.BidNameSt;           // ЛОТ: товар для закупки "Смазочные материалы и технические жидкости для автомобильного транспорта"                        
            LotNameUrl = inpHit.Source.ObjectHrefTerm;
            PriceStart = inpHit.Source.PurchAmount.ToString() + " " + inpHit.Source.PurchCurrency;
            LotNumberStr = inpHit.Source.PurchCodeTerm;
            //
            Organizer = inpHit.Source.OrgName;                        // заказчик
            Active = inpHit.Source.CreateRequestAlowed;            // разрешена ли подача заявок? (0/1)            
            DateAuctionStart = inpHit.Source.EndDate;                        // Дата проведения аукциона	"24.07.2020"	string            
            Status = inpHit.Source.PurchStateName;	                // Текущая стадия "Прием заявок"	string  
            TorgType = inpHit.Source.PurchaseTypeName;                // Тип торгов "Открытый запрос котировок в электронной форме"	string            
            DateAcceptFinish = inpHit.Source.RequestAcceptDate;	            // Дата окончания приема заявок "24.07.2020 00:01"	string
            Section = inpHit.Source.SourceTerm;	                    // Раздел торгов: "Закупки по 223-ФЗ"	string
            //inpHit.Source.CreateRequestHrefTerm;          // ссылка для подачи заявки (требуется регистрация)	
            //inpHit.Source.PublicDate;	                    // Дата публикации "16.07.2020 16:02"	string
            //inpHit.Source.RequestDate;	                    // Дата окончания приема заявок "24.07.2020 00:01"	string
            //inpHit.Source.RequestStartDate;	            // Дата начала приема заявок "16.07.2020 08:00"	string
        }
        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }

        //
        public string TorgNameStr { get; private set; }
        public string Organizer { get; private set; }
        public string Section { get; private set; }
        public long Active { get; private set; }
        public string DateAcceptFinish { get; private set; }
        public string DateAuctionStart { get; private set; }
        public string Status { get; private set; }
        public string TorgType { get; private set; }
                
        public override bool Equals(object obj)
        {
            //throw new NotImplementedException();
            if (!(obj is SberbankAst))
                return false;
            SberbankAst curObj = (SberbankAst)obj;

            if (this.DateAcceptFinish == curObj.DateAcceptFinish &
                this.DateAuctionStart == curObj.DateAuctionStart &
                this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &                                
                this.LotNumberStr == curObj.LotNumberStr &
                this.Organizer == curObj.Organizer &                
                this.PriceStart == curObj.PriceStart &
                this.Status == curObj.Status &
                this.TorgNameStr == curObj.TorgNameStr &
                this.Active == curObj.Active &
                this.Section == curObj.Section &
                this.TorgType == curObj.TorgType)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 661515800;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAuctionStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(internalID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Organizer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<long>.Default.GetHashCode(Active);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Section);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgType);            

            internalID = hashCode.ToString();

            return hashCode;
        }

        public override string ToString(bool html)
        {
            //throw new NotImplementedException();
            string baseUri = "http://bankrupt.centerr.ru";
            string result = "";

            if (html)
                result += String.Format("<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +
                    @"<a href =""{4}"">{5}</a>" + "</td><td>" +
                    @"<a href =""{6}"">{7}</a>" + "</td><td>" +
                    @"{8}" + "</td><td>" +
                    @"<a href =""{9}"">{10}</a>" + "</td><td>" +
                    @"{11}" + "</td><td>" +
                    @"{12}" + "</td><td>" +
                    @"{13}" + "</td><td>" +
                    @"<a href =""{14}"">{15}</a>" + "</td><td>" +
                    //@"{16}"                          + "</td></tr></table>",
                    @"{16}" + "</td></tr>",
                    baseUri + TorgNumber.ItemUri.Replace("\'", "").Replace("class", ""), TorgNumber.ItemString,
                    baseUri + TorgName.ItemUri.Replace("\"", ""), TorgName.ItemString,
                    baseUri + LotNumber.ItemUri.Replace("\"", "").Replace("\'", ""), LotNumber.ItemString,
                    baseUri + LotName.ItemUri.Replace("\"", ""), LotName.ItemString,
                    PriceStart,
                    baseUri + Organizer.ItemUri.Replace("\"", ""), Organizer.ItemString,
                    DateAcceptFinish,
                    DateAuctionStart,
                    Status,
                    baseUri + Winner.ItemUri.Replace("\"", ""), Winner.ItemString,
                    TorgType
                    );
            else
                result += String.Format(
                    @"{1}" + ";" +
                    @"{3}" + ";" +
                    @"{5}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + ";" +
                    @"{10}" + ";" +
                    @"{11}" + ";" +
                    @"{12}" + ";" +
                    @"{13}" + ";" +
                    @"{15}" + ";" +
                    @"{16}" + Environment.NewLine +
                    // строка таблицы с ссылками
                    @"{0}" + ";" +
                    @"{2}" + ";" +
                    @"{4}" + ";" +
                    @"{6}" + ";" +
                    @"" + ";" +
                    @"{9}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"{14}" + ";" +
                    @"" + Environment.NewLine,
                    baseUri + TorgNumber.ItemUri.Replace("\'", "").Replace("class", ""), TorgNumber.ItemString,
                    baseUri + TorgName.ItemUri.Replace("\"", ""), TorgName.ItemString,
                    baseUri + LotNumber.ItemUri.Replace("\"", "").Replace("\'", ""), LotNumber.ItemString,
                    baseUri + LotName.ItemUri.Replace("\"", ""), LotName.ItemString,
                    PriceStart,
                    baseUri + Organizer.ItemUri.Replace("\"", ""), Organizer.ItemString,
                    DateAcceptFinish,
                    DateAuctionStart,
                    Status,
                    baseUri + Winner.ItemUri.Replace("\"", ""), Winner.ItemString,
                    TorgType
                    );

            return result;
        }
    }
}
