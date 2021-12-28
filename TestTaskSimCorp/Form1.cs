using System;
using System.Data;
using System.Windows.Forms;

namespace TestTaskSimCorp
{
    public partial class Form1 : Form
    {

        double intRate, invSumm, paymentAmount, principalAmount, interestAmount, totalPayments, totalInterest, paymentGap;
        DateTime agreementDate, paymentDate, calculationDate, endDate;
        int years, paymentNumbers;
        string message;
        string[] columnnames = { "PaymentDate", "PaymentAmount", "PrincipalAmount", "InterestAmount" };
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PaymentCalculations pc = new PaymentCalculations();
            DataTable table = new DataTable("Result");
            for (int i = 0; i < columnnames.Length; i++)
            {
                table = CreateColumn(table, "System.String", columnnames[i]);
            }


            if (Double.TryParse(textBox1.Text, out intRate) && Double.TryParse(textBox2.Text, out invSumm) && int.TryParse(textBox3.Text, out years))
            {
                agreementDate = dateTimePicker1.Value;
                calculationDate = dateTimePicker2.Value;
                endDate = agreementDate.AddYears(years);
                totalPayments = 0;
                totalInterest = 0;
                if (endDate > agreementDate && calculationDate > agreementDate && calculationDate < endDate && calculationDate >= agreementDate.AddMonths(1).AddDays(-1))
                {
                    intRate = pc.CalculateMonthlyRate(intRate);
                    paymentNumbers = pc.CalculatePaymentNumbers(years);
                    paymentAmount = pc.CalculatePaymentAmount(invSumm, intRate, paymentNumbers);
                    paymentDate = calculationDate;
                    totalInterest = invSumm;
                    paymentGap = pc.CalculatePaymentGap(endDate, calculationDate, paymentNumbers);
                    while (invSumm != 0)
                    {
                        interestAmount = pc.CalculateInterestAmount(invSumm, intRate);
                        if (invSumm > paymentAmount)
                        {
                            principalAmount = Math.Round(paymentAmount - interestAmount, 2);
                            invSumm = Math.Round(invSumm - principalAmount, 2);
                            paymentDate = paymentDate.AddDays(paymentGap);
                        }
                        else
                        {
                            principalAmount = invSumm;
                            invSumm = 0;
                            paymentAmount = Math.Round(principalAmount + interestAmount, 2);
                            paymentDate = endDate;
                        }
                        totalPayments += paymentAmount;
                        table = CreateRow(paymentDate, paymentAmount, principalAmount, interestAmount, table, columnnames);
                    }
                    totalPayments = Math.Round(totalPayments, 2);
                    totalInterest = Math.Round(totalPayments - totalInterest, 2);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = table;
                    label7.Text = $"Total payments: {totalPayments} \nTotal Interest: {totalInterest}";
                    label7.Visible = true;
                }
                else
                {
                    message = "Calculation date must be in range of Agreement date and End date(Agreement date + Investment duration).";
                    MessageOutput(message);
                }
            }
            else
            {
                message = "Wrong data in some fields.";
                MessageOutput(message);
            }

        }

        private DataTable CreateRow(DateTime paymentDate, double paymentAmount, double principalAmount, double interestAmount, DataTable table, string[] columnnames)
        {
            DataRow row = table.NewRow();
            row[columnnames[0]] = Convert.ToString(paymentDate.ToShortDateString());
            row[columnnames[1]] = Convert.ToString(paymentAmount);
            row[columnnames[2]] = Convert.ToString(principalAmount);
            row[columnnames[3]] = Convert.ToString(interestAmount);
            table.Rows.Add(row);
            return table;
        }

        private DataTable CreateColumn(DataTable table, string dataType, string columnname)
        {
            DataColumn column = new DataColumn();
            column.DataType = Type.GetType(dataType);
            column.ColumnName = columnname;
            column.AutoIncrement = false;
            column.Caption = columnname;
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);
            return table;
        }
        private void MessageOutput(string message) => MessageBox.Show(message, "Error", MessageBoxButtons.OK);
    }
}
