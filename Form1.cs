/* Created by Bryan Le
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BLeFinal
{
    public partial class Form1 : Form
    {
        private bool fileSaved = false;
        private List<Taxpayer> Taxpayers = new List<Taxpayer>();

        public Form1 ( )
        {
            InitializeComponent();
        }

        private void enterDataButton_Click_1 ( object sender, EventArgs e )
        {
            //enter data button
            groupBox1.Visible = true;
        }

        private void loadDataButton_Click ( object sender, EventArgs e )
        {
            //load data button

            try
            {

                string taxpayerFile;

                OpenFileDialog taxpayerFileChooser = new OpenFileDialog();
                taxpayerFileChooser.Filter = "All text files|*.txt";
                taxpayerFileChooser.ShowDialog();
                taxpayerFile = taxpayerFileChooser.FileName;
                taxpayerFileChooser.Dispose();


                using (StreamReader fileReader = new StreamReader(taxpayerFile))
                {
                    while (fileReader.EndOfStream == false)
                    {
                        string[] properties;
                        string line;
                        string name;
                        int salary;
                        decimal investment;
                        int exemptionCount;
                        bool isMarried;

                        line = fileReader.ReadLine();
                        properties = line.Split(',');

                        name = properties[0];
                        salary = int.Parse(properties[1]);
                        investment = decimal.Parse(properties[2]);
                        isMarried = Boolean.Parse(properties[3]);
                        exemptionCount = int.Parse(properties[4]);

                        taxpayerBindingSource.Add(new Taxpayer(name, salary, investment, isMarried, exemptionCount));


                    }
                }

                dataGridView1.Visible = true;
                resetButton.Visible = true;

            }

            catch
            {
                MessageBox.Show("Are you sure you want to cancel? ");
                loadDataButton.Focus();
            }

            fileSaved = true;
        }

        private void submitButton_Click ( object sender, EventArgs e )
        {
            //submit

            try
            {

                String name;
                decimal salary;
                int exemptionCount;
                bool isMarried;
                decimal investment;

                Taxpayer newTaxpayer;

                name = nameTextBox.Text;
                salary = decimal.Parse(salaryTextBox.Text);
                exemptionCount = (int)numericUpDown1.Value;
                isMarried = checkBox1.Checked;
                investment = decimal.Parse(investmentTextBox.Text);

                newTaxpayer = new Taxpayer(name, salary, investment, isMarried, exemptionCount);

                Taxpayers.Add(newTaxpayer);
                taxpayerBindingSource.Add(newTaxpayer);
                fileSaved = true;

                ClearForm();
                groupBox1.Visible = false;
            }
            catch
            {
                MessageBox.Show("Please enter Data");
            }
        }

        private void summaryButton_Click ( object sender, EventArgs e )
        {
            //summary

            displayTextBox.AppendText(Taxpayer.Summary());
        }

        private void displayButton_Click ( object sender, EventArgs e )
        {
            //display


            if (Taxpayer.TotalCount != 0)
            {
                displayTextBox.AppendText(Environment.NewLine + "Name " + "\t" + "Income" + "\t" + "\t" + "Tax" + "\t" + "\t" + "Marital Status" + Environment.NewLine);

                foreach (Taxpayer txpyr in taxpayerBindingSource)
                {
                    displayTextBox.AppendText(txpyr.Display());
                }
                displayTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                displayTextBox.AppendText("No data is available" + Environment.NewLine);
            }
            
        }

        private void resetButton_Click ( object sender, EventArgs e )
        {
            //reset

            SavingBeforeExit();

            Taxpayer.ResetData();
            Taxpayers.Clear();
            displayTextBox.Clear();
            taxpayerBindingSource.Clear();
            ClearForm();
            groupBox1.Visible = false;
        }

        private void saveDataButton_Click ( object sender, EventArgs e )
        {
            //save data

            if (Taxpayer.TotalCount != 0)
            {
                SaveData();

                fileSaved = false;
            }
            else
            {
                displayTextBox.AppendText("No data is Available" + Environment.NewLine);
            }
        }

        //methods
        private void SavingBeforeExit ( )
        {

            if (Taxpayer.TotalCount != 0)
            {

                if (fileSaved == true)
                {
                    string taxpayerLine, taxpayerFile, myFileName;

                    SaveFileDialog taxpayerFileChooser;
                    StreamWriter fileWriter;
                    FileInfo myFile;

                    taxpayerFileChooser = new SaveFileDialog();
                    taxpayerFileChooser.Filter = "All csv Files|*.csv";

                    if (taxpayerFileChooser.ShowDialog() == DialogResult.OK)
                    {
                        taxpayerFile = taxpayerFileChooser.FileName;

                        myFile = new FileInfo(taxpayerFile);
                        myFileName = myFile.Name;

                        taxpayerFileChooser.Dispose();

                        fileWriter = new StreamWriter(taxpayerFile, true);

                        //saving the data

                        foreach (Taxpayer txpyr in taxpayerBindingSource)
                        {

                            taxpayerLine = txpyr.Name + "," +
                            txpyr.Salary.ToString() + "," +
                            txpyr.Investment.ToString() + "," +
                            txpyr.TotalIncome.ToString() + "," +
                            txpyr.TaxRate + "," + txpyr.Tax;

                            fileWriter.WriteLine(taxpayerLine);

                        }
                        myFile = new FileInfo(taxpayerFile);
                        myFileName = myFile.Name;


                        fileWriter.Close();

                        MessageBox.Show("Data is saved to " + myFileName.ToString());

                        fileWriter.Dispose();
                    }

                }
            }

            else
            {
                {
                    displayTextBox.AppendText("No data Available" + Environment.NewLine);
                }
            }
        }

        private void SaveData ( )
        {
            string taxpayerLine;
            string taxpayerFile;
            string myFileName;

            SaveFileDialog taxpayerFileChooser;
            StreamWriter fileWriter;
            FileInfo myFile;


            taxpayerFileChooser = new SaveFileDialog();
            taxpayerFileChooser.Filter = "CSV file|*.csv";
            taxpayerFileChooser.ShowDialog();
            taxpayerFile = taxpayerFileChooser.FileName;
            taxpayerFileChooser.Dispose();

            fileWriter = new StreamWriter(taxpayerFile, true);

            foreach (Taxpayer txpyr in taxpayerBindingSource)
            {
                taxpayerLine = txpyr.Name + "," +
                    txpyr.Salary.ToString() + "," +
                    txpyr.Investment.ToString() + "," +
                    txpyr.TotalIncome.ToString() + "," +
                    txpyr.TaxRate + "," + txpyr.Tax;

                fileWriter.WriteLine(taxpayerLine);
            }

            myFile = new FileInfo(taxpayerFile);
            myFileName = myFile.Name;

            fileWriter.Close();
            fileWriter.Dispose();  

        }

        private void ClearForm ( )
        {
            salaryTextBox.Clear();
            investmentTextBox.Clear();
            nameTextBox.Clear();
            checkBox1.Checked = false;
            numericUpDown1.Value = 0;
            nameTextBox.Focus();
        }

        private void exitButton_Click ( object sender, EventArgs e )
        {
            Application.Exit();
            SavingBeforeExit();
        }
     



      
    }
}
