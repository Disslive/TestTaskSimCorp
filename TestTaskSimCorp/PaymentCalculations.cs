using System;

namespace TestTaskSimCorp
{
    class PaymentCalculations
    {
        public double CalculatePaymentAmount(double invSumm, double intRate, int paymentNumbers)
        {
            double paymentAmount = invSumm * ((intRate * (Math.Pow(1 + intRate, paymentNumbers))) / (Math.Pow(1 + intRate, paymentNumbers) - 1));
            return Math.Round(paymentAmount, 2);
        }

        public double CalculateInterestAmount(double summ, double intRate)
        {
            return Math.Round(intRate * summ, 2);
        }

        public int CalculatePaymentNumbers(int years)
        {
            return years*12;
        }

        public double CalculatePaymentGap(DateTime endDate, DateTime calculationDate, int paymentNumbers)
        {
            double paymentGap = (endDate - calculationDate).TotalDays;
            paymentGap = Math.Round(paymentGap/paymentNumbers, 1);
            return paymentGap;
        }

        public double CalculateMonthlyRate(double intRate)
        {
            double monthRate = intRate / (12 * 100);
            return monthRate;
        }

    }
}
