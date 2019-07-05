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

        private void txtNumbers_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string textBoxValue = txtNumbers.Text;

                //this will check if textbox is empty 
                if (string.IsNullOrEmpty(txtNumbers.Text))
                {
                    txtWord.Clear();
                }
                else
                {
                    //اذا مو نلل و يحتوي على كسور ف راح يحولة الى كلمة 
                    txtWord.Text = InternationalNumericFormatter.FormatWithCulture("L", Convert.ToDouble(txtNumbers.Text), null, new CultureInfo("ar"));
                }

                //هنا راح نمسح القديمة ونسوي كلمتين قبل الفاصلة وبعد الفاصلة
                if (txtNumbers.Text.Contains("."))
                {
                    int index = textBoxValue.IndexOf('.');

                    //قبل الفاصلة
                    int resultBeforePoint = Convert.ToInt32(textBoxValue.Substring(0, index));
                    //بعد الفاصلة
                    int resultAfterPoint = Convert.ToInt32(textBoxValue.Substring(index + 1));

                    //الكلمة قبل الفاصلة
                    string wordBeforePoint = InternationalNumericFormatter.FormatWithCulture("L", resultBeforePoint, null, new CultureInfo("ar"));
                    //الكلمة بعد الفاصلة
                    string wordAfterPoint = InternationalNumericFormatter.FormatWithCulture("L", resultAfterPoint, null, new CultureInfo("ar"));

                    //طول الرقم بعد الفاصلة لأحتساب  الاعداد العشرية
                    int lengthAfterPoint = Convert.ToString(resultAfterPoint).Length;

                    switch (lengthAfterPoint)
                    {
                        case 3://بالمئة
                            txtWord.Text = wordBeforePoint + " و " + wordAfterPoint + " بالألف ";
                            break;
                        case 4:
                            txtWord.Text = wordBeforePoint + " و " + wordAfterPoint + " بالعشرة الالف ";
                            break;
                        case 5:
                            txtWord.Text = wordBeforePoint + " و " + wordAfterPoint + " بالمئة الالف ";
                            break;
                        case 6:
                        case 7://بالمليون
                        case 8:
                        case 9:
                            txtWord.Text = wordBeforePoint + " و " + wordAfterPoint + " بالمليون ";
                            break;
                    }


                }
            }
            catch { }
        }


    }

}


