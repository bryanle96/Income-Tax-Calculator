/* Created by Bryan Le 04.19.17
 * user enters income and marital status
 * outputs tax
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BLeHW3._5
{
    public partial class Form1 : Form
    {
        private decimal taxDec;

        public Form1 ( )
        {
            InitializeComponent();
        }

        private void submitButton_Click ( object sender, EventArgs e )
        {

            //Submit button
            //Declarations
            decimal incomeDec;
            decimal incomeTaxDec;
            bool maritalBool;
            
            //Inputs
            incomeDec = decimal.Parse(textBox1.Text);
            maritalBool = checkBox1.Checked;

            //Computations

            if (maritalBool == true)
            {
                if (incomeDec > 150000m)
                {
                    taxDec = 0.3m;
                }
                else
                {
                    taxDec = 0.25m;
                }
            }
            else
            {
                if (incomeDec > 100000m)
                {
                    taxDec = 0.2m;
                }
                else
                {
                    taxDec = 0.15m;
                }

            }

            incomeTaxDec = incomeDec * taxDec;

            
            //Outputs
            MessageBox.Show("Hello your income tax is " + incomeTaxDec.ToString("C"));


        }

        private void clearButton_Click ( object sender, EventArgs e )
        {

            //Clear button
            textBox1.Clear();
            checkBox1.Checked = false;
            textBox1.Focus();


        }

        private void button1_Click ( object sender, EventArgs e )
        {
            Application.Exit();


        }
    }
}
