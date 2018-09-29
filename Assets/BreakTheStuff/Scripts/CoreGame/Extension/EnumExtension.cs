using BreakTheStuff.CoreGame.Models;
using System.Collections.Generic;
using UnityEngine;

namespace BreakTheStuff.CoreGame.Extension
{
    public static class EnumExtension
    {
        public static int[] GenerateEnum(int[] numArr, int lenght, int whence, int where)
        {
            for (int i = lenght - 1; i > 1; i--)
            {
                int j = Random.Range(whence, where - 1);
                int temp = numArr[i];
                numArr[i] = numArr[j];
                numArr[j] = temp;
            }

            return numArr;
        }

        public static List<Slab> ToList(this Slab[,] array)
        {
            List<Slab> result = new List<Slab>();
            int x = array.GetLength(0);
            int y = array.GetLength(1);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    result.Add(array[i, j]);
                }
            }

            return result;
        }

    }
}
