using System;
using System.Collections.Generic;
using System.IO;

using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Days;

public static class Day01
{
    public static void Run()
    {
        try
        {
            string input = InputLoader.Load("Day01");
            Console.WriteLine("input loaded:");
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            List<int> arrayOne = new();
            List<int> arrayTwo = new();

            foreach (string line in lines)
            {
                // split on triple space
                string[] parts = line.Split("   ", StringSplitOptions.RemoveEmptyEntries);

                arrayOne.Add(int.Parse(parts[0]));
                arrayTwo.Add(int.Parse(parts[1]));
            }

            // sort smallest to largest
            arrayOne.Sort();
            arrayTwo.Sort();

            // compare each element in arrayOne with arrayTwo and get the distance between them
            List<int> distances = new();
            for (int i = 0; i < arrayOne.Count; i++)
            {
                distances.Add(Math.Abs(arrayOne[i] - arrayTwo[i]));
            }

            Console.WriteLine($"Part 1: {distances.Sum()}");

            Part2();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }

    static void Part2()
    {
        try
        {
            string input = InputLoader.Load("Day01");
            Console.WriteLine("input loaded:");
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            List<int> arrayOne = new();
            List<int> arrayTwo = new();

            foreach (string line in lines)
            {
                // split on triple space
                string[] parts = line.Split("   ", StringSplitOptions.RemoveEmptyEntries);

                arrayOne.Add(int.Parse(parts[0]));
                arrayTwo.Add(int.Parse(parts[1]));
            }

            // create a hashmap out of array #2 so we know how often a value of array #1 is present in array #2
            Dictionary<int, int> arrayTwoMap = new();
            foreach (int value in arrayTwo)
            {
                if (arrayTwoMap.ContainsKey(value))
                {
                    arrayTwoMap[value]++;
                }
                else
                {
                    arrayTwoMap.Add(value, 1);
                }
            }

            // count the number of times a value of array #1 is present in array #2
            // e.g. if 3 is present 3 times in array #2, we have 3 * 3 = 9
            int sum = 0;
            foreach (int value in arrayOne)
            {
                if (arrayTwoMap.ContainsKey(value))
                {
                    sum += value * arrayTwoMap[value];
                }
            }

            Console.WriteLine($"Part 2: {sum}");


        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }
}
