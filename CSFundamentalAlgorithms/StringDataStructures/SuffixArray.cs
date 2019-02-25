﻿/* 
 * Copyright (c) 2019 (PiJei) 
 * 
 * This file is part of CSFundamentalAlgorithms project.
 *
 * CSFundamentalAlgorithms is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * CSFundamentalAlgorithms is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with CSFundamentalAlgorithms.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System;

// TODO: Add a linear-time implementation of suffix array. 

namespace CSFundamentalAlgorithms.StringDataStructures
{
    /// <summary>
    /// Implements SuffixArray data structure. A suffix array of an string is an array of integers that contains the starting index of all suffixes of the string in alphabetically sorted order. 
    /// For example for string 'data' , where suffixes are : 'data', 'ata', 'ta', 'a' the suffix array is [3, 1, 0, 2]
    /// </summary>
    [DataStructure("SuffixArray")]
    public class SuffixArray
    {
        public List<int> Build(string text)
        {
            List<int> suffixArray = new List<int>(); // length is text.Length

            List<StringSuffix> suffixes = new List<StringSuffix>();
            for (int i = 0; i < text.Length; i++)
            {
                char firstChar = text[i];
                char secondChar = (i + 1) < text.Length ? text[i + 1] : '\0';
                suffixes.Add(new StringSuffix(i, firstChar, secondChar));
            }

            SortSuffixes(suffixes, 0, suffixes.Count);

            return suffixArray;
        }

        /// <summary>
        /// Implementing a sort using mergeSort.
        /// TODO: MergeSort shall be modified to be generic, and operational on all types. 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="lowIndex"></param>
        /// <param name="highIndex"></param>
        public static void SortSuffixes(List<StringSuffix> array, int lowIndex, int highIndex)
        {
            if (lowIndex <= highIndex)
            {
                int middleIndex = (lowIndex + highIndex) / 2;
                SortSuffixes(array, lowIndex, middleIndex);
                SortSuffixes(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }
        }

        public static void Merge(List<StringSuffix> array, int lowIndex, int middleIndex, int highIndex)
        {
            List<StringSuffix> coppyArray = new List<StringSuffix>(array);

            int leftLowIndex = lowIndex;
            int leftHighIndex = middleIndex;

            int rightLowIndex = middleIndex + 1;
            int rightHighIndex = highIndex;

            int arraylowIndex = lowIndex;

            int leftHalfCounter = leftLowIndex;
            int rightHalfCounter = rightLowIndex;

            while(leftHalfCounter <= leftHighIndex && rightHalfCounter <= rightHighIndex)
            {
                if(coppyArray[leftHalfCounter] <= coppyArray[rightHalfCounter])
                {
                    array[arraylowIndex] = coppyArray[leftHalfCounter];
                    leftHalfCounter++;
                }
                else
                {
                    array[arraylowIndex] = coppyArray[rightHalfCounter];
                    rightHalfCounter++;
                }
                arraylowIndex++;
            }

            while(leftHalfCounter <= leftHighIndex)
            {
                array[arraylowIndex] = coppyArray[leftHalfCounter];
                leftHalfCounter++;
                arraylowIndex++;
            }
            while(rightHalfCounter <= rightHighIndex)
            {
                array[arraylowIndex] = coppyArray[rightHalfCounter];
                rightHalfCounter++;
                arraylowIndex++;
            }
        }
    }
}
