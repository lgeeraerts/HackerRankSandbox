using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapNodes
{
    class Solution
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());

            var algorithm = new Algorithm();
            var nodes = new int[t][];

            for (int i = 0; i < t; i++)
            {
                var childNodes = Console.ReadLine().Split(' ');
                nodes[i] = new int[] { Convert.ToInt32(childNodes[0]), Convert.ToInt32(childNodes[1]) };
            }

            algorithm.BuildTree(nodes);

            var s = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < s; i++)
            {
                var swaps = Convert.ToInt32(Console.ReadLine());

                algorithm.SwapTree(swaps);
                algorithm.PrintTreeInOrder();
            }
        }

        private class Algorithm
        {
            TreeNode Root = null;

            public void BuildTree(int[][] nodes)
            {
                Root = BuildNode(1, nodes[0], nodes);
            }

            public void SwapTree(int k)
            {
                DoSwapTree(Root, 1, k);
            }

            public void PrintTreeInOrder()
            {
                DoPrintTreeInOrder(Root);

                Console.WriteLine();
            }

            private TreeNode BuildNode(int key, int[] childNodes, int[][] nodes)
            {
                var newNode = new TreeNode(key);
                var leftNodeKey = childNodes[0];
                var rightNodeKey = childNodes[1];

                if (leftNodeKey != -1)
                {
                    newNode.Left = BuildNode(leftNodeKey, nodes[leftNodeKey - 1], nodes);
                }

                if (rightNodeKey != -1)
                {
                    newNode.Right = BuildNode(rightNodeKey, nodes[rightNodeKey - 1], nodes);
                }

                return newNode;
            }

            private void DoPrintTreeInOrder(TreeNode node)
            {
                if(node.Left != null)
                {
                    DoPrintTreeInOrder(node.Left);

                    Console.Write(" " + node.Key);
                }
                else
                {
                    Console.Write(node.Key);
                }
                
                if(node.Right != null)
                {
                    Console.Write(" ");

                    DoPrintTreeInOrder(node.Right);
                }
            }

            private void DoSwapTree(TreeNode treeNode, int level, int k)
            {
                var isSwapNeeded = level % k == 0;

                if (isSwapNeeded)
                {
                    treeNode.SwapNodes();
                } 

                if(treeNode.Left != null)
                {
                    DoSwapTree(treeNode.Left, level + 1, k);
                }

                if (treeNode.Right != null)
                {
                    DoSwapTree(treeNode.Right, level + 1, k);
                }
            }
        }

        private class TreeNode
        {
            public TreeNode(int key)
            {
                Key = key;
            }

            public int Key;
            public TreeNode Left;
            public TreeNode Right;

            public void SwapNodes()
            {
                var tempNode = Left;
                Left = Right;
                Right = tempNode;
            }
        }
    }
}
