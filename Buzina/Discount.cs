using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzina
{
    public class Discount
    {
        /// <summary>
        /// Метод для получения скидки для партнера
        /// </summary>
        /// <param name="count">Количество реализованной продукции</param>
        /// <returns></returns>
        public int GetDiscount(int count)
        {
            if (count < 10000)
            {
                return 0;
            }
            else if (count >= 10000 && count < 50000)
            {
                return 5;
            }
            else if (count >= 50000 && count < 300000)
            {
                return 10;
            }
            else
            {
                return 15;
            }
        }
    }
}
