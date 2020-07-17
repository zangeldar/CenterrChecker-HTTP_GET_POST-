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
            DateAcceptStart = inpHit.Source.RequestStartDate;	            // Дата окончания приема заявок "24.07.2020 00:01"	string
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
        public string DateAcceptStart { get; private set; }
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
            string baseUri = "https://sberbank-ast.ru";
            string result = "";

            if (html)
                result += String.Format("<tr><td>" +                    
                    @"{0}" + "</td><td>" +                          // LotNumberStr
                    @"<a href =""{2}"">{1}</a>" + "</td><td>" +                          // TorgNameStr
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +     // LotNameUrl     LotNameStr
                    @"{4}" + "</td><td>" +                          // PriceStart
                    @"{5}" + "</td><td>" +                          // Organizer
                    @"{6}<br>-<br>{7}" + "</td><td>" +              // DataAcceptStart + DataAcceptFinish
                    @"{8}" + "</td><td>" +                          // DateAuctionStart
                    @"{9}" + "</td><td>" +                          // Status
                    @"{10}" + "</td><td>" +                         // TorgType                    
                    @"{11}" + "</td></tr>",                         // Section
                    //@"{11}"                          + "</td></tr></table>",
                    LotNumberStr,
                    TorgNameStr,
                    LotNameUrl, LotNameStr,
                    PriceStart,
                    Organizer,
                    DateAcceptStart, DateAcceptFinish,
                    DateAuctionStart,
                    Status,
                    TorgType,
                    Section                    
                    );
            else
                result += String.Format(
                    @"{0}" + ";" +
                    @"{1}" + ";" +
                    @"{3}" + ";" +
                    @"{4}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{8}" + ";" +
                    @"{9}" + ";" +
                    @"{10}" + ";" +
                    @"{11}" + Environment.NewLine +
                    // строка таблицы с ссылками
                    @"" + ";" +
                    @"{2}" + ";" +
                    @"{2}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"{7}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + Environment.NewLine,
                    LotNumberStr,
                    TorgNameStr,
                    LotNameUrl, LotNameStr,
                    PriceStart,
                    Organizer,
                    DateAcceptStart, DateAcceptFinish,
                    DateAuctionStart,
                    Status,
                    TorgType,
                    Section
                    );

            return result;
        }
    }
}
