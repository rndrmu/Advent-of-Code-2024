using System;
using System.Collections.Generic;
using System.IO;

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

            //Part1(lines);
            Part2(lines);


        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }

 static void Part1(string[] lines)
{
    int safeCount = 0;

    foreach (string line in lines)
    {
        // convert line to integer array
        int[] digits = Array.ConvertAll(line.Split(" ", StringSplitOptions.RemoveEmptyEntries), int.Parse);
        bool isIncreasing = true, isDecreasing = true, isValid = true;

        for (int i = 0; i < digits.Length - 1; i++)
        {
            int diff = digits[i + 1] - digits[i];

            // ensure adjacent digits differ by 1, 2, or 3
            // if its 0 or greater than 3, invalid
            if (diff == 0 || diff > 3)
            {
                isValid = false;
                break;
            }
            else if (Math.Abs(diff) > 3)
            {
                isValid = false;
                break;
            }

            // track direction consistency
            if (diff > 0) isDecreasing = false;
            if (diff < 0) isIncreasing = false;

            // if both increasing and decreasing are false, invalid
            if (!isIncreasing && !isDecreasing)
            {
                isValid = false;
                break;
            }
        }

        // update safeCount if the line meets all conditions
        if (isValid)
        {
            Console.WriteLine($"line is safe: {line}");
            safeCount++;
        }
        else
        {
            Console.WriteLine($"line is not safe: {line}");
        }
    }

    Console.WriteLine($"Part 1: {safeCount}");
}


static void Part2(string[] lines)
{
    int safeCount = 0;

    foreach (string line in lines)
    {
        int[] digits = Array.ConvertAll(line.Split(" ", StringSplitOptions.RemoveEmptyEntries), int.Parse);

        // check if the line is valid outright, or can be made valid by removing one digit
        if (IsValidLine(digits) || BruteForceRemoveAndCheck(digits))
        {
            Console.WriteLine($"line is safe (or can be made safe): {line}");
            safeCount++;
        }
        else
        {
            Console.WriteLine($"line is not safe: {line}");
        }
    }

    Console.WriteLine($"Part 2: {safeCount}");
}

static bool IsValidLine(int[] digits)
{
    bool isIncreasing = true, isDecreasing = true;

    for (int i = 0; i < digits.Length - 1; i++)
    {
        int diff = digits[i + 1] - digits[i];

        // adjacent digits must differ by 1, 2, or 3, and not be equal
        if (diff == 0 || Math.Abs(diff) > 3) return false;

        // track direction consistency
        if (diff > 0) isDecreasing = false;
        if (diff < 0) isIncreasing = false;

        // if neither increasing nor decreasing, invalid
        if (!isIncreasing && !isDecreasing) return false;
    }

    return true;
}

static bool BruteForceRemoveAndCheck(int[] digits)
{
    for (int i = 0; i < digits.Length; i++)
    {
        // create a modified array without the i-th digit
        int[] modifiedDigits = new int[digits.Length - 1];
        Array.Copy(digits, 0, modifiedDigits, 0, i);
        Array.Copy(digits, i + 1, modifiedDigits, i, digits.Length - i - 1);

        // check if the modified array is valid
        if (IsValidLine(modifiedDigits))
        {
            Console.WriteLine($"removing {digits[i]} makes line safe");
            return true;
        }
    }

    return false;
}

}