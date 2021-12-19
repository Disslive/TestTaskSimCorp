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

        public int CalculatePaymentNumbers(DateTime agreementDate, DateTime calculationDate)
        {
            int paymentNums = 0;
            paymentNums = calculationDate.Month - agreementDate.Month;
            paymentNums += (calculationDate.Year - agreementDate.Year) * 12;
            return paymentNums;
        }



        public double CalculateMonthlyRate(double intRate)
        {
            double monthRate = intRate / (12 * 100);
            return monthRate;
        }

    }
}
