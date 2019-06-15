using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.International.Formatters;
using System.Globalization;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        //belwo code that must be inside the button

        //string isNegative = "";
        //try
        //{
        //    Console.WriteLine("Enter a Number to convert to currency");
        //    string number = textBox1.Text;
        //    number = Convert.ToDouble(number).ToString();

        //    if (number.Contains("-"))
        //    {
        //        isNegative = "سالب ";
        //        number = number.Substring(1, number.Length - 1);
        //    }
        //    if (number == "0")
        //    {
        //        textBox2.Text = "صفر";
        //    }
        //    else
        //    {
        //        textBox2.Text = ConvertToWords(number);
        //        //MessageBox.Show( isNegative + ConvertToWords(number));
        //    }
        //    Console.ReadKey();
        //}
        //catch (Exception ex)
        //{

        //    Console.WriteLine(ex.Message);
        //}

        private static String ConvertToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = "فقط";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "و";// just to separate whole numbers from points/cents  
                        endStr = "Paisa " + endStr;//Cents  
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }
        private static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX
                bool isDone = false;//test if already translated
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping
                    String place = "";//digit grouping name:hundres,thousand,etc...

                    switch (numDigits)
                    {
                        case 1://ones' range

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range
                            pos = (numDigits % 3) + 1;
                            place = " مائة ";
                            break;
                        case 4://thousands' range
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " ألف ";
                            break;
                        case 7://millions' range
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " مليون ";
                            break;
                        case 10://Billions's range
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " مليار ";
                            break;
                        //add extra case options for anything above Billion...
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                //word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros
                        //if (beginsZero) word = " and " + word.Trim();
                    }
                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }
        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "عشرة";
                    break;
                case 11:
                    name = "أحد عشر";
                    break;
                case 12:
                    name = "اثنا عشر";
                    break;
                case 13:
                    name = "ثلاثة عشر";
                    break;
                case 14:
                    name = "اربعة عشر";
                    break;
                case 15:
                    name = "خمسة عشر";
                    break;
                case 16:
                    name = "ستة عشر";
                    break;
                case 17:
                    name = "سبعة عشر";
                    break;
                case 18:
                    name = "ثمانية عشر";
                    break;
                case 19:
                    name = "تسعة عشر";
                    break;
                case 20:
                    name = "عشرون";
                    break;
                case 30:
                    name = "ثلاثون";
                    break;
                case 40:
                    name = "اربعون";
                    break;
                case 50:
                    name = "خمسون";
                    break;
                case 60:
                    name = "ستون";
                    break;
                case 70:
                    name = "سبعون";
                    break;
                case 80:
                    name = "ثمانون";
                    break;
                case 90:
                    name = "تسعون";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = ones(Number.Substring(1)) + " و " + tens(Number.Substring(0, 1) + "0");
                    }
                    break;
            }
            return name;
        }
        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "واحد";
                    break;
                case 2:
                    name = "اثنان";
                    break;
                case 3:
                    name = "ثلاثة";
                    break;
                case 4:
                    name = "اربعة";
                    break;
                case 5:
                    name = "خمسة";
                    break;
                case 6:
                    name = "ستة";
                    break;
                case 7:
                    name = "سبعة";
                    break;
                case 8:
                    name = "ثمانية";
                    break;
                case 9:
                    name = "تسعة";
                    break;
            }
            return name;
        }
        private static String ConvertDecimals(String number)
        {
            String cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "صفر";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
        }

        private void txtNumbers_TextChanged(object sender, EventArgs e)
        {
            try
            {


                Double Value;
                string textBoxValue = txtNumbers.Text;
                //int decimalPointPostion;

                //if (Double.TryParse(txtNumbers.Text, out Value))
                //{

                //    if (Value <= 9999999999999999999)
                //    {

                //without decimal point
                txtWord.Text = InternationalNumericFormatter.FormatWithCulture("L", Convert.ToDouble(txtNumbers.Text), null, new CultureInfo("ar"));

                //هنا راح نمسح القديمة ونسوي كلمتين قبل الفاصلة وبعد الفاصلة
                if (txtNumbers.Text.Contains("."))
                {

                    int index = textBoxValue.IndexOf('.');



                    string resultBeforePoint = textBoxValue.Substring(0, index);
                    string resultAfterPoint = textBoxValue.Substring(index + 1);

                    string wordBeforePoint = InternationalNumericFormatter.FormatWithCulture("L", Convert.ToInt32(resultBeforePoint), null, new CultureInfo("ar"));
                    string wordAfterPoint = InternationalNumericFormatter.FormatWithCulture("L", Convert.ToInt32(resultAfterPoint) , null, new CultureInfo("ar"));

                    int lengthAfterPoint = resultAfterPoint.Length;

                    //txtWord.Text = "";

                    // MessageBox.Show("result: " + length);

                    //if (length > 2)
                    //        {
                    //string wordToNumberAfterDecimal = "";
                   

                    switch (lengthAfterPoint)
                    {
                        //case 2://بالعشرة
                        //    break;
                        case 3://بلمئة
                            txtWord.Text = wordBeforePoint + " و " +  wordAfterPoint + " من المئة ";
                            break;
                        case 4://بالألف
                        case 5:
                        case 6:
                            txtWord.Text = wordBeforePoint + " و " + wordAfterPoint + " من الألف ";
                            break;
                        case 7:
                        case 8:
                        case 9:
                            txtWord.Text = wordBeforePoint + " و " + wordAfterPoint + " من المليون ";
                            break;
                    }
                    //        }
                }
                //}
                //else
                //{
                //    txtWord.Text = "The number is out of range";
                //}

                //}

                //else
                //{
                //    label1.Text = "";
                //}


            }
            catch { }
        }


    }

}


