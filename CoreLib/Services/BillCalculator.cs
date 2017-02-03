using RestaurantBilling.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBilling.Core.Services
{
    public class BillCalculator : IBillCalculator
    {
        public double TipAmount(double subTotal, int gratuity)
        {
            return subTotal * ((double)gratuity) / 100.0;
        }

        public double BillTotal(double subTotal, int gratuity)
        {
            return subTotal + TipAmount(subTotal, gratuity);
        }
        public double Gratuity(double subTotal, double tip)
        {
            // Ensure we don't divide by zero.
            return tip == 0 ? 0 : subTotal / tip;
        }
    }
}
