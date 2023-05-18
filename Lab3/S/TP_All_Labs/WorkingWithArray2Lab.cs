using System;
using System.Collections.Generic;
using System.Text;

namespace TP_All_Labs
{
    class WorkingWithArray2Lab
    {
        private int[] array;

        public WorkingWithArray2Lab(int[] arrayToWorkWith) {
            array = arrayToWorkWith;
        }

        public void deleteSameDigits (int index, bool[] IsNeedToCopy)
        {
            if (array[index] < 10 && array[index] > -10)
            {
                return;
            }
            else if (checkForSameDigits(array[index]))
            {
                return;
            }
            else
            {
                IsNeedToCopy[index] = true;
            }
        }

        private bool checkForSameDigits(int integer)
        {
            if(integer < 0)
            {
                integer *= -1;
            }
            string integerInString = integer.ToString();
            bool[] massOfDigits = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            for(int i = 0; i != integerInString.Length; i++)
            {
                int tmp = (int)(integerInString[i] - 48);
                if (massOfDigits[tmp] == true)
                {
                    return true;
                } 
                else
                {
                    massOfDigits[tmp] = true;
                }
            }
            return false;
        }

        public void enterKBeforeAllIntsWith1(int index, int[] answerArray, int k)
        {
            answerArray[index] = array[index];
            if (checkFor1InInt(array[index]))
            {
                if (array[index] < 0)
                {
                    string tmp = k.ToString();
                    tmp += (array[index] * -1);
                    int temp = -1;
                    temp *= Convert.ToInt32(tmp);
                    answerArray[index] = temp;
                }
                else
                {
                    string tmp = k.ToString();
                    tmp += array[index];
                    int temp = Convert.ToInt32(tmp);
                    answerArray[index] = temp;
                }
            }
            else
            {
                answerArray[index] = array[index];
            }
        }

        private bool checkFor1InInt(int integer)
        {
            string integerInString = integer.ToString();
            for (int i = 0; i != integerInString.Length; i++)
            {
                if (integerInString[i] == '1')
                {
                    return true;
                }
            }
            return false;
        }

        public void change3ElementsPlaces(int index, int[] answerArray)
        {
            if (index < 3) { answerArray[index] = array[answerArray.Length - 3 + index]; return; }
            if (index > answerArray.Length - 4) { answerArray[index] = array[index - (answerArray.Length - 3)]; return; }
            answerArray[index] = array[index];
        }
    }
}
