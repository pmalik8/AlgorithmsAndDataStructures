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
using System.Diagnostics.Contracts;
using AlgorithmsAndDataStructures.Decoration;

namespace AlgorithmsAndDataStructures.DataStructures.Trees.Nary.API
{
    /// <summary>
    /// Implements base B-Tree
    /// </summary>
    /// <typeparam name="TNode">Type of the nodes stored in the tree. </typeparam>
    /// <typeparam name="TKey">Type of the keys stored in the tree. </typeparam>
    /// <typeparam name="TValue">Type of the values stored in the tree. </typeparam>
    public abstract class BTreeBase<TNode, TKey, TValue>
        where TNode : IBTreeNode<TNode, TKey, TValue>, IComparable<TNode>
        where TKey : IComparable<TKey>
    {
        /// <value>The root of the tree. </value>
        public TNode Root { get; set; } = default;

        /// <value>The maximum number of children for a non-leaf node in this B-Tree.</value>
        public int MaxBranchingDegree { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxBranchingDegree">The max branching degree or the max number of children a node in this tree can have. </param>
        public BTreeBase(int maxBranchingDegree)
        {
            MaxBranchingDegree = maxBranchingDegree;
        }

        /// <summary>
        /// Given the set of key values, builds a b-tree by inserting all the key-value pairs. 
        /// </summary>
        /// <param name="keyValues">The list of key values to be inserted in the tree. </param>
        /// <returns>Root of the tree. </returns>
        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "O(nLog(n))")]
        [TimeComplexity(Case.Average, "O(n(Log(n))")]
        public TNode Build(Dictionary<TKey, TValue> keyValues)
        {
            foreach (KeyValuePair<TKey, TValue> keyValue in keyValues)
            {
                Insert(keyValue);
            }
            return Root;
        }

        /// <summary>
        /// Inserts a new key-value pair in the tree and returns root of the tree. 
        /// </summary>
        /// <param name="keyValue">The key-value pair to be inserted in the tree. </param>
        /// <returns>Root of the tree. </returns>
        [TimeComplexity(Case.Best, "O(1)", When = "Fist key in the tree is inserted.")]
        [TimeComplexity(Case.Worst, "O(D Log(n)(base:D)")] // where D is max branching factor of the tree. 
        [TimeComplexity(Case.Average, "O(d Log(n)(base d))")] // where d is min branching factor of the tree.  
        public TNode Insert(KeyValuePair<TKey, TValue> keyValue)
        {
            /* Find the leaf node that should contain the new key-value pair. The leaf is found such that the order property of the B-Tree is preserved. */
            TNode leaf = FindLeafToInsertKey(Root, keyValue.Key);

            /* Insert the new keyValue pair in the leaf node. */
            InsertInLeaf(leaf, keyValue);

            return Root;
        }

        /// <summary>
        /// Deletes the given key from the tree if it exists. 
        /// </summary>
        /// <param name="key">The key to be deleted from the tree. </param>
        /// <returns>True in case of success, and false otherwise. </returns>
        [TimeComplexity(Case.Best, "O(1)", When = "For example, there is only one key left in the tree.")]
        [TimeComplexity(Case.Worst, "O(D Log(n)(base D))")] // where D is max branching factor of the tree. 
        [TimeComplexity(Case.Average, "O(d Log(n) base d)")] // where d is min branching factor of the tree.  
        public bool Delete(TKey key)
        {
            try
            {
                /* Find the container node of the key, and if this node exists, delete key from it.*/
                TNode node = Search(Root, key);

                return Delete(node, key);
            }
            catch (KeyNotFoundException) /* This means that the key does not exist in the tree, and thus the operation is not successful.*/
            {
                return false;
            }
        }

        /// <summary>
        /// Inserts the given key-value pair in the given leaf node. 
        /// </summary>
        /// <param name="leaf">A leaf node in the tree. </param>
        /// <param name="keyValue">A key-value pair to be inserted in the tree. </param>
        /// <returns>Root of the tree. </returns>
        public abstract TNode InsertInLeaf(TNode leaf, KeyValuePair<TKey, TValue> keyValue);

        /// <summary>
        /// Deletes the given key from the given node. 
        /// </summary>
        /// <param name="node">A node in the tree. </param>
        /// <param name="key">A key in the tree. </param>
        /// <returns>True in case of success and false otherwise. </returns>
        public abstract bool Delete(TNode node, TKey key);

        /// <summary>
        /// Gets the sorted list of all the key-values in the tree rooted at <paramref name="node"/>. 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public abstract List<KeyValuePair<TKey, TValue>> GetSortedKeyValues(TNode node);

        /// <summary>
        /// Finds a leaf node to insert a new given key. 
        /// </summary>
        /// <param name="root">The node at which the tree is rooted. </param>
        /// <param name="key">A new key to be inserted in the tree. </param>
        /// <returns>Leaf node to insert the key. </returns>
        public abstract TNode FindLeafToInsertKey(TNode root, TKey key);

        /// <summary>
        /// Searches tree rooted at <paramref name="root"/> node for the given key. 
        /// </summary>
        /// <param name="root">The node at which tree is rooted. </param>
        /// <param name="key">The key that is being searched. </param>
        /// <returns>Tree node containing <paramref name="key"/></returns>
        public abstract TNode Search(TNode root, TKey key);

        /// <summary>
        /// Finds the node that contains the maximum key of the subtree rooted at node.
        /// </summary>
        /// <param name="node">The node at which (sub)tree is rooted. </param>
        /// <returns>The node containing the maximum key of the (sub)tree rooted at <paramref name="node"/>. </returns>
        [TimeComplexity(Case.Best, "O(1)", When = "when node is leaf.")]
        [TimeComplexity(Case.Worst, "O(Log(n))")] 
        [TimeComplexity(Case.Average, "O(Log(n))")]
        public TNode GetMaxNode(TNode node)
        {
            if (node.IsLeaf())
            {
                return node;
            }

            return GetMaxNode(node.GetChild(node.ChildrenCount - 1));
        }

        /// <summary>
        /// Finds the node that contains the minimum key of the subtree rooted at <paramref name="node"/>.
        /// </summary>
        /// <param name="node">The node at which (sub)tree is rooted.</param>
        /// <returns>The node containing the minimum key of the (sub)tree rooted at <paramref name="node"/>.</returns>
        public TNode GetMinNode(TNode node)
        {
            if (node.IsLeaf())
            {
                return node;
            }
            return GetMinNode(node.GetChild(0));
        }

        /// <summary>
        /// Rotates a key from the right sibling of the node via their parent to the node. 
        /// The cost of this operation is at inserting keys and children, in right position (to preserve order), Which at worst is O(K), Where K is the maximum number of keys in a node, and thus is constant. 
        /// </summary>
        /// <param name="node">The receiver of a new key. </param>
        /// <param name="rightSibling">The node that lends a key to the process. This key moves to parent, and a key from parent moves to node. </param>
        /// <param name="separatorIndex">The index of the key separating <paramref name="node"/> and <paramref name="rightSibling"/> in the parent children array. </param>
        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "O(K)")] // Constant time as is independent of n: number of keys in tree. 
        [TimeComplexity(Case.Average, "O(K)")] // Constant time as is independent of n: number of keys in tree. 
        public TNode RotateLeft(TNode node, TNode rightSibling, int separatorIndex)
        {
            /* 1- Move the separator key in the parent to the underFlown node. */
            node.InsertKeyValue(node.GetParent().GetKeyValue(separatorIndex));
            node.GetParent().RemoveKeyByIndex(separatorIndex);

            /* 2- Replace separator key in the parent with the first key of the right sibling.*/
            node.GetParent().InsertKeyValue(rightSibling.GetMinKey());

            /* 3- Remove the first (minimum) key from the right sibling, and move its child to node. */
            rightSibling.RemoveKey(rightSibling.GetMinKey().Key);
            if (rightSibling.ChildrenCount >= 1)
            {
                node.InsertChild(rightSibling.GetChild(0));
                rightSibling.RemoveChildByIndex(0);
            }

            /* Check Validity. At this point both the node and its right sibling must be MinFull (have exactly MinKeys keys). */
            Contract.Assert(rightSibling.IsMinFull());
            Contract.Assert(node.IsMinFull());

            return node.GetParent();
        }

        /// <summary>
        /// Rotates a key from the left sibling of the node via their parent to the node.
        /// The cost of this operation is at inserting keys and children, in right position (to preserve order), Which at worst is O(K), Where K is the maximum number of keys in a node, and thus is constant. 
        /// </summary>
        /// <param name="node">The receiver of a new key. </param>
        /// <param name="leftSibling">The node that lends a key to the process. This key moves to parent, and a key from parent moves to node.</param>
        /// <param name="separatorIndex">The index of the key separating <paramref name="node"/> and <paramref name="leftSibling"/> in the parent children array. </param>
        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "O(K)")] // Constant time as is independent of n: number of keys in tree. 
        [TimeComplexity(Case.Average, "O(K)")] // Constant time as is independent of n: number of keys in tree. 
        public TNode RotateRight(TNode node, TNode leftSibling, int separatorIndex)
        {
            /* 1- Move the separator key in the parent to the underFlown node. */
            node.InsertKeyValue(node.GetParent().GetKeyValue(separatorIndex));
            node.GetParent().RemoveKeyByIndex(separatorIndex);

            /* 2- Replace separator key in the parent with the last key of the left sibling. */
            node.GetParent().InsertKeyValue(leftSibling.GetMaxKey());

            /* 3- Remove the last (maximum) key from the left sibling, and move its child to node. */
            leftSibling.RemoveKey(leftSibling.GetMaxKey().Key);
            if (leftSibling.ChildrenCount >= 1)
            {
                node.InsertChild(leftSibling.GetChild(leftSibling.ChildrenCount - 1));
                leftSibling.RemoveChildByIndex(leftSibling.ChildrenCount - 1);
            }

            /* Check validity. At this point both the node and its left sibling must be MinFull (have exactly MinKeys keys). */
            Contract.Assert(leftSibling.IsMinFull());
            Contract.Assert(node.IsMinFull());

            return node.GetParent();
        }

        /// <summary>
        /// Merges node with its left sibling, such that node can be dropped. Also borrows a key from parent. 
        /// </summary>
        /// <param name="node">The node that will be dissolved at the end of operation. </param>
        /// <param name="leftSibling">The node that will contain keys of the node, its current keys, and a key from parent. </param>
        /// <returns>Parent of the nodes. </returns>
        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "O(K)")] // Constant time as is independent of n: number of keys in tree. 
        [TimeComplexity(Case.Average, "O(K)")] // Constant time as is independent of n: number of keys in tree. 
        public TNode Join(TNode node, TNode leftSibling)
        {
            // 1- Move separator key to the left node
            int nodeAndLeftSiblingSeparatorKeyAtParentIndex = leftSibling.GetIndexAtParentChildren();
            leftSibling.InsertKeyValue(node.GetParent().GetKeyValue(nodeAndLeftSiblingSeparatorKeyAtParentIndex));

            // 2- Remove separator key in the parent, and disconnect parent from node. 
            node.GetParent().RemoveKeyByIndex(nodeAndLeftSiblingSeparatorKeyAtParentIndex);
            node.GetParent().RemoveChildByIndex(nodeAndLeftSiblingSeparatorKeyAtParentIndex + 1);

            // 3- Join node with leftSibling: Move all the keys and children of node to its left sibling.
            for (int i = 0; i < node.KeyCount; i++)
            {
                leftSibling.InsertKeyValue(node.GetKeyValue(i));
            }
            for (int i = 0; i < node.ChildrenCount; i++)
            {
                leftSibling.InsertChild(node.GetChild(i));
            }

            /* Clear node. */
            node.Clear();

            if (node.GetParent().IsEmpty() && node.GetParent().IsRoot()) /* Can happen if parent is root*/
            {
                leftSibling.SetParent(default);
                Root = leftSibling;
            }

            // Since parent has lent a key to its children, it might be UnderFlown now, thus return the parent for additional checks.
            return leftSibling.GetParent();
        }

    }
}
