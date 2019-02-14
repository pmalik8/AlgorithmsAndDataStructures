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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSFundamentalAlgorithms.SearchingAlgorithms;

namespace CSFundamentalAlgorithmsTests.SearchingAlgorithmsTests
{
    [TestClass]
    public class TernarySearchTests
    {
        [TestMethod]
        public void TernarySearch_Search_Test()
        {
            List<int> values = new List<int> { 1, 3, 10, 14, 25, 27, 34, 78, 90, 90, 120 };

            Assert.AreEqual(0, TernarySearch.Search(values, 0, values.Count - 1, 1));
            Assert.AreEqual(1, TernarySearch.Search(values, 0, values.Count - 1, 3));
            Assert.AreEqual(2, TernarySearch.Search(values, 0, values.Count - 1, 10));
            Assert.AreEqual(3, TernarySearch.Search(values, 0, values.Count - 1, 14));
            Assert.AreEqual(4, TernarySearch.Search(values, 0, values.Count - 1, 25));
            Assert.AreEqual(5, TernarySearch.Search(values, 0, values.Count - 1, 27));
            Assert.AreEqual(6, TernarySearch.Search(values, 0, values.Count - 1, 34));
            Assert.AreEqual(7, TernarySearch.Search(values, 0, values.Count - 1, 78));
            Assert.AreEqual(8, TernarySearch.Search(values, 0, values.Count - 1, 90));
            Assert.AreEqual(8, TernarySearch.Search(values, 0, values.Count - 1, 90));
            Assert.AreEqual(10, TernarySearch.Search(values, 0, values.Count - 1, 120));
            Assert.AreEqual(-1, TernarySearch.Search(values, 0, values.Count - 1, -20));
            Assert.AreEqual(-1, TernarySearch.Search(values, 0, values.Count - 1, 15));
            Assert.AreEqual(-1, TernarySearch.Search(values, 0, values.Count - 1, 456));
        }
    }
}
