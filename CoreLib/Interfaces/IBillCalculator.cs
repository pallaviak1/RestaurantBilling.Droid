using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBilling.Core.Interfaces
{
    public interface IBillCalculator
    {
        double TipAmount(double subTotal, int gratuity);
        double BillTotal(double subTotal, int gratuity);
        double Gratuity(double subTotal, double tip); 
    }
}
