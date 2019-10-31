using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    public interface IObject
    {
        /// <summary>
        /// Внутренний идентификатор записи
        /// </summary>
        string internalID { get; }
        /// <summary>
        /// Строка названия лота
        /// </summary>
        string LotNameStr { get; }
        /// <summary>
        /// URL названия лота
        /// </summary>
        string LotNameUrl { get; }
        /// <summary>
        /// Стартовая цена
        /// </summary>
        string PriceStart { get; }
        /// <summary>
        /// Строка номера лота
        /// </summary>
        string LotNumberStr { get; }
        /// <summary>
        /// Строковое представление записи
        /// </summary>
        /// <param name="html">Формат вывода HTML</param>
        /// <returns>Строка для печати</returns>
        string ToString(bool html);
    }
}
