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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSFundamentalAlgorithms.SortingAlgorithms;
using System.Collections.Generic;

namespace CSFundamentalAlgorithmsTests.SortingAlgorithmsTests
{
    [TestClass]
    public class HeapSortTests
    {
        [TestMethod]
        public void HeapSort_HeapSortAscending_Test_WithDistinctValues()
        {
            var values = new List<int>(Common.ArrayWithDistinctValues);
            HeapSort.HeapSort_Ascending(values);
            Common.CheckIfListIsSortedAscendingly(values);
        }

        [TestMethod]
        public void HeapSort_HeapSortAscending_Test_WithDuplicateValues()
        {
            var values = new List<int>(Common.ArrayWithDuplicateValues);
            HeapSort.HeapSort_Ascending(values);
            Common.CheckIfListIsSortedAscendingly(values);
        }

        [TestMethod]
        public void HeapSort_HeapSortAscending_Test_WithSortedDistinctValues()
        {
            var values = new List<int>(Common.ArrayWithSortedDistinctValues);
            HeapSort.HeapSort_Ascending(values);
            Common.CheckIfListIsSortedAscendingly(values);
        }

        [TestMethod]
        public void HeapSort_HeapSortAscending_Test_WithSortedDuplicateValues()
        {
            var values = new List<int>(Common.ArrayWithSortedDuplicateValues);
            HeapSort.HeapSort_Ascending(values);
            Common.CheckIfListIsSortedAscendingly(values);
        }

        [TestMethod]
        public void HeapSort_HeapSortAscending_Test_WithReverselySortedDistinctValues()
        {
            var values = new List<int>(Common.ArrayWithReverselySortedDistinctValues);
            HeapSort.HeapSort_Ascending(values);
            Common.CheckIfListIsSortedAscendingly(values);
        }

        [TestMethod]
        public void HeapSort_HeapSortAscending_Test_WithRevereselySortedDuplicateValues()
        {
            var values = new List<int>(Common.ArrayWithReverselySortedDuplicateValues);
            HeapSort.HeapSort_Ascending(values);
            Common.CheckIfListIsSortedAscendingly(values);
        }
    }
}