using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public static class Algorithm
    {
        static void Merge(List<User> array, int lowIndex, int middleIndex, int highIndex)
        {
            int left = lowIndex;
            int right = middleIndex + 1;
            var tempArray = new User[highIndex - lowIndex + 1];
            long index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (int.Parse(array[left].RfgRating) <= int.Parse(array[right].RfgRating))
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        static void MergeSort(List<User> array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }
        }

        static void HeapSort(List<User> arr)
        {
            var n = arr.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(arr, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                var temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                heapify(arr, i, 0);
            }
        }


        public static List<User> KMP(string needle, List<User> list)
        {
            var res = new List<User>();
            var lps = prefix(needle);
            foreach (var t in list)
            {
                var haystack = t.Username;
                var j = 0;
                for (var i = 0; i < haystack.Length;)
                {
                    if (needle[j] == haystack[i])
                    {
                        j++;
                        i++;
                    }

                    if (j == needle.Length)
                    {
                        res.Add(t);
                        break;
                    }

                    if (i >= haystack.Length || needle[j] == haystack[i]) continue;
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i += 1;
                }
            }

            return res;
        }

        static int[] prefix(string pat)
        {
            var len = 0;
            var pref = new int[pat.Length];
            pref[0] = 0;
            var i = 1;
            while (i < pat.Length)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    pref[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = pref[len - 1];
                    }
                    else
                    {
                        pref[i] = 0;
                        i++;
                    }
                }
            }

            return pref;
        }


        static void heapify(List<User> arr, int n, int i)
        {
            var largest = i;
            var l = 2 * i + 1;
            var r = 2 * i + 2;

            if (l < n && int.Parse(arr[l].RfgRating) > int.Parse(arr[largest].RfgRating))
                largest = l;

            if (r < n && int.Parse(arr[r].RfgRating) > int.Parse(arr[largest].RfgRating))
                largest = r;

            if (largest != i)
            {
                var swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                heapify(arr, n, largest);
            }
        }

        public static void SortUsers(List<User> arr)
        {
            Random rnd = new Random();
            if (rnd.Next() % 2 == 0)
                HeapSort(arr);
            else
                MergeSort(arr, 0, arr.Count - 1);
        }
    }
}