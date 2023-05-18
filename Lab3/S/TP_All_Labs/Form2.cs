using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TP_All_Labs
{
    public partial class SecondLabForm : Form
    {
        MainFrame mainFrame1;
        private int numOfColumns;

        public SecondLabForm(MainFrame mainFrame)
        {
            InitializeComponent();
            numOfColumns = 1;
            this.dataGridView1.ColumnCount = numOfColumns;
            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[0].HeaderCell.Value = ("Исходный:");
            this.dataGridView1.Rows[1].HeaderCell.Value = ("Конечный:");
            mainFrame1 = mainFrame;
        }

        private void buttomRandomCompletion_Click(object sender, EventArgs e)
        {
            if (textBoxFillingFrequency.Text.Length == 0)
            {
                string stringToMessageBox = "Error. You didn't enter the frequency";
                MessageBox.Show(stringToMessageBox);
                string stringToLog = "In ";
                stringToLog += DateTime.Now.ToString();
                stringToLog += " in SecondLabForm in method \"buttomRandomCompletion_Click\" - ArgumentOutOfRangeException";
                mainFrame1.setExeptionInLog(stringToLog);
                return;
            }
            char charfrequency = Convert.ToChar(textBoxFillingFrequency.Text);
            if (!CheckToIntOnly(charfrequency))
            {
                MessageBox.Show("You didn't enter integer. Please enter integer");
                string stringToLog = "In ";
                stringToLog += DateTime.Now.ToString();
                stringToLog += " in SecondLabForm in method \"buttomRandomCompletion_Click\" - ArgumentOutOfRangeException";
                mainFrame1.setExeptionInLog(stringToLog);
            } 
            else {
                int frequency = Convert.ToInt16(textBoxFillingFrequency.Text);
                for (int i = 0; i < numOfColumns;)
                {
                    Random rnd = new Random();
                    int rand = rnd.Next(-40, 30);
                    dataGridView1[i, 0].Value = rand;
                    i += frequency;
                }
            }
        }

        private bool CheckToIntOnly(char integer)
        {
            if (integer < 49 || integer > 57)                    
            {
                return false;
            }
            return true;
        }

        private void buttonDeleteEqualElements_Click(object sender, EventArgs e)
        {   

            if(checkForArgumentsOutOfRangeException("buttonDeleteEqualElements_Click"))
            {
                return;
            }

            for (int i = 0; i != numOfColumns; i++)
            {
                dataGridView1[i, 1].Value = null;
            }

            int[] array = new int[numOfColumns];
            for(int i = 0; i != numOfColumns; i++)
            {
                array[i] = Convert.ToInt16(dataGridView1[i, 0].Value);
            }

            textBoxInfoOfWorkingTime.Text += "Method delete elements with same digits.";
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
            WorkingWithArray2Lab WWA2L = new WorkingWithArray2Lab(array);
            bool[] IsNeedToCopy = new bool[numOfColumns];
            for(int i = 0; i != numOfColumns; i++)
            {
                IsNeedToCopy[i] = false;
            }

            // Работа одного потока
            System.Diagnostics.Stopwatch sw1 = new System.Diagnostics.Stopwatch();
            sw1.Start();

            for(int i = 0; i != numOfColumns*10000; i++)
            {
                WWA2L.deleteSameDigits(i % numOfColumns, IsNeedToCopy);
            }
            for (int i = 0; i < numOfColumns; i++)
            {
                if (IsNeedToCopy[i] == true)
                {
                    dataGridView1[i, 1].Value = dataGridView1[i, 0].Value;
                }
            }

            sw1.Stop();
            textBoxInfoOfWorkingTime.Text += "Time of 1 thread working: " + (sw1.Elapsed).ToString();
            textBoxInfoOfWorkingTime.Text += Environment.NewLine; 
            
            //Очищаем массив need to copy чтобы работать с потоками
            for (int i = 0; i != numOfColumns; i++)
            {
                IsNeedToCopy[i] = false;
            }

            // Работа нескольких потоков
            System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
            sw2.Start();

            var offsets = Enumerable.Range(0, numOfColumns);
            Parallel.ForEach(offsets, offset =>
            {
                if (offset < numOfColumns * 10000)
                {
                    WWA2L.deleteSameDigits(offset % numOfColumns, IsNeedToCopy);
                }
            });

            for (int i = 0; i < numOfColumns; i++)
            {
                if (IsNeedToCopy[i] == true)
                {
                    dataGridView1[i, 1].Value = dataGridView1[i, 0].Value;
                }
            }

            sw2.Stop();
            textBoxInfoOfWorkingTime.Text += "Time of number of threads working: " + (sw2.Elapsed).ToString();
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
        }

        private void buttonEnterdigitK_Click(object sender, EventArgs e)
        {
            if (checkForArgumentsOutOfRangeException("buttonEnterdigitK_Click"))
            {
                return;
            }

            for(int i = 0; i != numOfColumns; i++)
            {
                dataGridView1[i, 1].Value = null;
            }

            int[] array = new int[numOfColumns];
            for (int i = 0; i != numOfColumns; i++)
            {
                array[i] = Convert.ToInt16(dataGridView1[i, 0].Value);
            }

            WorkingWithArray2Lab WWA2L = new WorkingWithArray2Lab(array);

            // Считываем k у пользователя
            DialogForSetKIn2Lab dfs2l = new DialogForSetKIn2Lab();
            dfs2l.ShowDialog();
            string whatToSet = dfs2l.getK();
            if(whatToSet.Length == 0)
            {
                string stringToMessageBox = "Your didn't enter k";
                MessageBox.Show(stringToMessageBox);
                string stringToLog = "In ";
                stringToLog += DateTime.Now.ToString();
                stringToLog += " in SecondLabForm in method \"buttonEnterdigitK_Click\" - ArgumentOutOfRangeException";
                mainFrame1.setExeptionInLog(stringToLog);
                return;
            }
            int k;
            if (checkToIntOnly(whatToSet))
            {
                k = Convert.ToInt32(whatToSet);
            }
            else
            {
                string stringToMessageBox = "Your data is not integer at all";
                MessageBox.Show(stringToMessageBox);
                string stringToLog = "In ";
                stringToLog += DateTime.Now.ToString();
                stringToLog += " in SecondLabForm in method \"buttonEnterdigitK_Click\" - ArgumentOutOfRangeException";
                mainFrame1.setExeptionInLog(stringToLog);
                return;
            }

            textBoxInfoOfWorkingTime.Text += "Method Enter K before elements with 1.";
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
            int[] answerArray = new int[numOfColumns];
            
            // Работа с одним потоком
            System.Diagnostics.Stopwatch sw1 = new System.Diagnostics.Stopwatch();
            sw1.Start();

            for (int i = 0; i != numOfColumns * 10000; i++)
            {
                WWA2L.enterKBeforeAllIntsWith1(i % numOfColumns, answerArray, k);
            }
            for (int i = 0; i < numOfColumns; i++)
            {
                dataGridView1[i, 1].Value = answerArray[i];
            }

            sw1.Stop();
            textBoxInfoOfWorkingTime.Text += "Time of 1 thread working: " + (sw1.Elapsed).ToString();
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
            

            // Для чистоты сравнения очищаем массив-ответ
            for (int i = 0; i != numOfColumns; i++)
            {
                answerArray[i] = 0;
            }
            
            // Работа с несколькими потоками
            System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
            sw2.Start();

            var offsets = Enumerable.Range(0, numOfColumns);
            Parallel.ForEach(offsets, offset =>
            {
                if (offset < numOfColumns * 10000)
                {
                    WWA2L.enterKBeforeAllIntsWith1(offset % numOfColumns, answerArray, k);
                }
            });

            for (int i = 0; i < numOfColumns; i++)
            {
                dataGridView1[i, 1].Value = answerArray[i];
            }

            sw2.Stop();
            textBoxInfoOfWorkingTime.Text += "Time of number of threads working: " + (sw2.Elapsed).ToString();
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
        }

        private void buttonRearrangeElements_Click(object sender, EventArgs e)
        {
            if (checkForArgumentsOutOfRangeException("buttonRearrangeElements_Click"))
            {
                return;
            }
            if (numOfColumns < 6)
            {
                string stringToMessageBox = "Array is less then 6";
                MessageBox.Show(stringToMessageBox);
                string stringToLog = "In ";
                stringToLog += DateTime.Now.ToString();
                stringToLog += " in SecondLabForm in method \"buttonRearrangeElements_Click\" - ArgumentOutOfRangeException";
                mainFrame1.setExeptionInLog(stringToLog);
                return;
            }

            for (int i = 0; i != numOfColumns; i++)
            {
                dataGridView1[i, 1].Value = null;
            }

            int[] array = new int[numOfColumns];
            for (int i = 0; i != numOfColumns; i++)
            {
                array[i] = Convert.ToInt16(dataGridView1[i, 0].Value);
            }

            textBoxInfoOfWorkingTime.Text += "Method Rearrange first 3 elements and last 3 elements.";
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
            WorkingWithArray2Lab WWA2L = new WorkingWithArray2Lab(array);

            int[] answerArray = new int[numOfColumns];
            
            // Работа с одним потоком
            System.Diagnostics.Stopwatch sw1 = new System.Diagnostics.Stopwatch();
            sw1.Start();

            for (int i = 0; i != numOfColumns * 10000; i++)
            {
                WWA2L.change3ElementsPlaces(i % numOfColumns, answerArray);
            }

            for (int i = 0; i < numOfColumns; i++)
            {
                dataGridView1[i, 1].Value = answerArray[i];
            }

            sw1.Stop();
            textBoxInfoOfWorkingTime.Text += "Time of 1 thread working: " + (sw1.Elapsed).ToString();
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
            

            // Для чистоты сравнения очищаем массив-ответ
            for (int i = 0; i != numOfColumns; i++)
            {
                answerArray[i] = 0;
            } 

            // Работа с несколькими потоками
            System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
            sw2.Start();

            var offsets = Enumerable.Range(0, numOfColumns);
            Parallel.ForEach(offsets, offset =>
            {
                if (offset < numOfColumns * 10000)
                {
                    WWA2L.change3ElementsPlaces(offset % numOfColumns, answerArray);
                }
            });

            for (int i = 0; i < numOfColumns; i++)
            {
                dataGridView1[i, 1].Value = answerArray[i];
            }

            sw2.Stop();
            textBoxInfoOfWorkingTime.Text += "Time of number of threads working: " + (sw2.Elapsed).ToString();
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
            textBoxInfoOfWorkingTime.Text += Environment.NewLine;
        }

        private bool checkForArgumentsOutOfRangeException (string method)
        {
            for (int i = 0; i != numOfColumns; i++)
            {   
                if (dataGridView1[i, 0].Value == null)
                {
                    string stringToMessageBox = "Array is not filled";
                    MessageBox.Show(stringToMessageBox);
                    string stringToLog = "In ";
                    stringToLog += DateTime.Now.ToString();
                    stringToLog += " in SecondLabForm in method \"" + method + "\" - ArgumentOutOfRangeException";
                    mainFrame1.setExeptionInLog(stringToLog);
                    return true;
                }
                if (!checkToIntOnlyAndFrom40To30(dataGridView1[i, 0].Value.ToString()))
                {
                    string stringToMessageBox = "Element with " + i.ToString() + " index is not int or not in range between -40 and 30";
                    MessageBox.Show(stringToMessageBox);
                    string stringToLog = "In ";
                    stringToLog += DateTime.Now.ToString();
                    stringToLog += " in SecondLabForm in method \"" + method + "\" - ArgumentOutOfRangeException";
                    mainFrame1.setExeptionInLog(stringToLog);
                    return true;
                }
            }
            return false;
        }

        private bool checkToIntOnlyAndFrom40To30(string integer)
        {

            for (int i = 0; i != integer.Length; i++)
            {
                if (integer[i] < 48 || integer[i] > 57)                    
                {
                    if (integer[i] != '-')
                    {
                        return false;
                    }
                }
            }

            int CheckForInRange = Int32.Parse(integer);
            if (CheckForInRange > 30 || CheckForInRange < -40)
            {
                return false;
            }

            return true;
        }

        private bool checkToIntOnly(string integer)
        {
            for (int i = 0; i != integer.Length; i++)
            {
                if (integer[i] < 48 || integer[i] > 57)                    
                {
                    if (integer[i] != '-')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void buttonSetArraySize_Click(object sender, EventArgs e)
        {
            if (textBoxColumnsNumber.Text.Length == 0) 
            {
                string stringToMessageBox = "Error. You didn't enter the size of array";
                MessageBox.Show(stringToMessageBox);
                string stringToLog = "In ";
                stringToLog += DateTime.Now.ToString();
                stringToLog += " in SecondLabForm in method \"buttonSetArraySize_Click\" - ArgumentOutOfRangeException";
                mainFrame1.setExeptionInLog(stringToLog);
                return;
            }
            if (!checkToIntOnly(textBoxColumnsNumber.Text))
            {
                MessageBox.Show("You didn't enter integer. Please enter integer");
                string stringToLog = "In ";
                stringToLog += DateTime.Now.ToString();
                stringToLog += " in SecondLabForm in method \"buttonSetArraySize_Click\" - ArgumentOutOfRangeException";
                mainFrame1.setExeptionInLog(stringToLog);
            }
            int frequency = Convert.ToInt16(textBoxColumnsNumber.Text);
            if(frequency > 500)
            {
                MessageBox.Show("Your integer is too big");
                string stringToLog = "In ";
                stringToLog += DateTime.Now.ToString();
                stringToLog += " in SecondLabForm in method \"buttonSetArraySize_Click\" - ArgumentOutOfRangeException";
                mainFrame1.setExeptionInLog(stringToLog);
            }
            else
            {
                numOfColumns = frequency;
                this.dataGridView1.ColumnCount = numOfColumns;
                for (int i = 0; i != numOfColumns; i++)
                {
                    this.dataGridView1.Columns[i].HeaderText = i.ToString();
                }
            }
        }
    }  
}
