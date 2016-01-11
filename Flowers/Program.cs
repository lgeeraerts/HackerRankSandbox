/* Sample program illustrating input/output methods */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution
{
    static void Main(String[] args)
    {
        int N, K;
        string NK = Console.ReadLine();
        string[] NandK = NK.Split(new Char[] { ' ', '\t', '\n' });
        N = Convert.ToInt32(NandK[0]);
        K = Convert.ToInt32(NandK[1]);

        int[] C = new int[N];

        string numbers = Console.ReadLine();
        string[] split = numbers.Split(new Char[] { ' ', '\t', '\n' });

        int i = 0;

        foreach (string s in split)
        {
            if (s.Trim() != "")
            {
                C[i++] = Convert.ToInt32(s);
            }
        }

        Process(C, K);
    }
    
    static void Process(int[] flowers, int numberOfBuddies)
    {
        var buddyPurchaseCounts = new int[numberOfBuddies];
        var flowersList = flowers.ToList();

        var totalCost = 0;

        while (flowersList.Count > 0)
        {
            var costliestFlower = flowersList.Max();
            var purchaseCount = AddOneToBuddyWithLeastPurchases(buddyPurchaseCounts);

            totalCost += costliestFlower * purchaseCount;
            flowersList.Remove(costliestFlower);
        }

        Console.WriteLine(totalCost);
    }

    static int AddOneToBuddyWithLeastPurchases(int[] buddiesPurchaseCount)
    {
        var index = 0;
        var smallest = buddiesPurchaseCount[0];

        for (var i = 1; i < buddiesPurchaseCount.Length; i++)
        {
            if (buddiesPurchaseCount[i] < smallest)
            {
                index = i;
            }
        }
        
        buddiesPurchaseCount[index] = buddiesPurchaseCount[index] + 1;

        return buddiesPurchaseCount[index];
    }

}