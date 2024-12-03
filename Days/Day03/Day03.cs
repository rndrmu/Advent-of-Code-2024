using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Days;

public static class Day03
{
    public static void Run()
    {
        try
        {
            string input = InputLoader.Load("Day03");
            Console.WriteLine("input loaded:");
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Part1(lines);
            Part2(lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }
    public static void Part1(string[] lines)
    {
        Console.WriteLine("Part 1:");
        string pattern = @"mul\((\d+),(\d+)\)"; // mul(3,4)
        Regex regex = new(pattern);

        // find all matches
        List<(int, int)> multiplierList = new();
        foreach (string line in lines)
        {
            MatchCollection matches = regex.Matches(line);
            foreach (Match match in matches)
            {
                multiplierList.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
            }
        }

        // calculate the total
        int total = 0;
        foreach ((int x, int y) in multiplierList)
        {
            total += x * y;
        }

        Console.WriteLine($"total: {total}");
    }
    public static void Part2(string[] lines)
        {
            Console.WriteLine("Part 2:");
            string pattern = @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)"; // mul(3,4)
            Regex regex = new(pattern);

            // Find all mul() calls first
            List<(int, int)> multiplierList = new();

            // Iterate through each line,
            // setting a flag if we're inside a don't() ... do() block
            // (we'll ignore mul() calls inside these blocks)
            bool ignoreInstructions = false;

            foreach (string line in lines)
            {
                MatchCollection matches = regex.Matches(line);
                int doCount = 0;
                int dontCount = 0;

                foreach (Match match in matches)
                {
                    if (match.Groups[0].Value == "do()")
                    {
                        Console.WriteLine($"found a do() at {match.Index}");
                        ignoreInstructions = false;
                    }
                    else if (match.Groups[0].Value == "don't()")
                    {
                        Console.WriteLine($"found a don't() at {match.Index}");
                        ignoreInstructions = true;
                    }
                    else if (!ignoreInstructions)
                    {
                        multiplierList.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
                    }
                }
            }


            // Calculate the total sum
            int total = 0;
            foreach ((int x, int y) in multiplierList)
            {
                total += x * y;
            }

            Console.WriteLine($"total: {total}");
        }

        private static bool IsInsideBlock(string line, int index, ref int doCount, ref int dontCount)
        {
            // Iterate from the start to the current index
            for (int i = 0; i < index; i++)
            {
                // If we encounter a do(), increase doCount
                if (i + 3 < line.Length && line.Substring(i, 3) == "do(")
                {
                    doCount++;
                }
                // If we encounter a don't(), increase dontCount
                if (i + 5 < line.Length && line.Substring(i, 5) == "don't")
                {
                    dontCount++;
                }
                // If we're closing a do() or don't() block, decrease the count
                if (line[i] == ')' && doCount > 0)
                {
                    doCount--;
                }
                if (line[i] == ')' && dontCount > 0)
                {
                    dontCount--;
                }
            }

            // If we're inside a do() or don't() block, return true
            return doCount > 0 || dontCount > 0;
        }


}
