using BreakTheStuff.Game.Models;
using System.Collections.Generic;
using UnityEngine;

namespace BreakTheStuff.Game.Extension
{
    public static class EnumExtension
    {
        public static int[] GenerateEnum(this int[] numArr, int whence, int where)
        {
            for (int i = numArr.Length - 1; i > 0; i--)
            {
                int j = Random.Range(whence, where);
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
