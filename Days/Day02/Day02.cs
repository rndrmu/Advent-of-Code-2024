using System;
using System.Linq;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Days;

public static class Day02
{
    public static void Run()
    {
        try
        {
            string input = InputLoader.Load("Day02");
            Console.WriteLine("input loaded:");
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // uncomment the part you want to run
            Part1(lines);
            Part2(lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }

    private static void Part1(string[] lines)
    {
        int safeCount = lines.Count(line => IsValidLine(ParseLine(line)));
        Console.WriteLine($"Part 1: {safeCount}");
    }

    private static void Part2(string[] lines)
    {
        int safeCount = lines.Count(line =>
        {
            int[] digits = ParseLine(line);
            bool isSafe = IsValidLine(digits) || CanBeMadeValid(digits);
            return isSafe;
        });

        Console.WriteLine($"Part 2: {safeCount}");
    }

    private static int[] ParseLine(string line)
    {
        return line
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }

    private static bool IsValidLine(int[] digits)
    {
        bool isIncreasing = true, isDecreasing = true;

        for (int i = 0; i < digits.Length - 1; i++)
        {
            int diff = digits[i + 1] - digits[i];

            // invalid if difference is 0 or outside range 1-3
            if (diff == 0 || Math.Abs(diff) > 3)
                return false;

            // track direction consistency
            if (diff > 0) isDecreasing = false;
            if (diff < 0) isIncreasing = false;

            // invalid if neither increasing nor decreasing
            if (!isIncreasing && !isDecreasing)
                return false;
        }

        return true;
    }

    private static bool CanBeMadeValid(int[] digits)
    {
        for (int i = 0; i < digits.Length; i++)
        {
            int[] modifiedDigits = RemoveDigitAt(digits, i);

            if (IsValidLine(modifiedDigits))
            {
                return true;
            }
        }

        return false;
    }

    private static int[] RemoveDigitAt(int[] digits, int index)
    {
        return digits
            .Where((_, i) => i != index)
            .ToArray();
    }
}
