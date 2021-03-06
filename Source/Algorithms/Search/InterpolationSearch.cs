﻿#region copyright
/* 
 * Copyright (c) 2019 (PiJei) 
 * 
 * This file is part of AlgorithmsAndDataStructures project.
 *
 * AlgorithmsAndDataStructures is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * AlgorithmsAndDataStructures is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with AlgorithmsAndDataStructures.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion
using System;
using System.Collections.Generic;
using AlgorithmsAndDataStructures.Decoration;

namespace AlgorithmsAndDataStructures.Algorithms.Search
{
    /// <summary>
    /// Implements Interpolation search algorithm for finding a specific value in a sorted list.
    /// </summary>
    public class InterpolationSearch
    {
        /// <summary>
        /// Searches in a sorted list of any comparable type, where values have a uniform distribution. Interpolation search is an improvement over binary search, and has a very similar implementation, the only main difference is where (which index in the array) the search starts at.
        /// The search is named interpolation, as it always has two main poles that it moves back and forth between them, these poles are the start index and the end index of the list. 
        /// Notice that only works if the given list is sorted. 
        /// </summary>
        /// <param name="sortedList">A sorted list of any comparable type that are also uniformly distributed. </param>
        /// <param name="key">The value that is being searched for. </param>
        /// <param name="startIndex">The lowest (left-most) index of the list - inclusive. </param>
        /// <param name="endIndex">The highest (right-most) index of the list - inclusive. </param>
        /// <returns>The index of the <paramref name="key"/> in the list, and -1 if it does not exist in the list. </returns>
        [Algorithm(AlgorithmType.Search, "InterpolationSearch", Assumptions = "List is sorted with an ascending order, and elements are driven from a uniform distribution.")]
        [SpaceComplexity("O(1)", InPlace = true)]
        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "O(n)")]
        [TimeComplexity(Case.Average, "O(Log(Log(n)))")]
        public static int Search<T>(List<T> sortedList, T key, int startIndex, int endIndex) where T : IComparable<T>
        {
            if (startIndex > endIndex)
            {
                return -1;
            }

            /* If key is NOT in the range, terminate search. Since the input list is sorted this early check is feasible. */
            if (key.CompareTo(sortedList[startIndex]) < 0 || key.CompareTo(sortedList[endIndex]) > 0)
            {
                return -1;
            }

            int searchStartIndex = GetStartIndex(sortedList, key, startIndex, endIndex);
            if (!(searchStartIndex >= startIndex && searchStartIndex <= endIndex))
            {
                return -1;
            }

            T searchStartValue = sortedList[searchStartIndex];

            if (key.CompareTo(searchStartValue) == 0)
            {
                return searchStartIndex;
            }

            if (key.CompareTo(searchStartValue) < 0)
            {
                return Search(sortedList, key, startIndex, searchStartIndex - 1);
            }

            if (key.CompareTo(searchStartValue) > 0)
            {
                return Search(sortedList, key, searchStartIndex + 1, endIndex);
            }

            return -1;
        }

        /// <summary>
        /// Computes an index to start the search from, dependent on the value of <paramref name="key"/>. 
        /// This formula is such that if the <paramref name="key"/> is closer to the value in the <paramref name="startIndex"/>, the search start point will be chosen closer to the <paramref name="startIndex"/>, and if the <paramref name="key"/> is closer to the value at <paramref name="endIndex"/>, the search start point will be chosen closer to the <paramref name="endIndex"/>.
        /// </summary>
        /// <param name="sortedList">A sorted list of any comparable type that are also uniformly distributed. </param>
        /// <param name="key">The value that is being searched for. </param>
        /// <param name="startIndex">The lowest (left-most) index of the list - inclusive. </param>
        /// <param name="endIndex">The highest (right-most) index of the list - inclusive. </param>
        /// <returns>The index in the list at which to start the search. </returns>
        public static int GetStartIndex<T>(List<T> sortedList, T key, int startIndex, int endIndex) where T : IComparable<T>
        {
            double distanceFromStartIndex = ((dynamic)key - (dynamic)sortedList[startIndex]) / (double)((dynamic)sortedList[endIndex] - (dynamic)sortedList[startIndex]);
            distanceFromStartIndex *= (endIndex - startIndex);
            int index = (int)(startIndex + distanceFromStartIndex);
            return index;
        }
    }
}
