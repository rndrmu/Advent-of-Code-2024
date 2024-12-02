using System;
using System.IO;

namespace AdventOfCode2024.Utils;

public static class InputLoader
{
    public static string Load(string dayName)
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string dayFolder = Path.Combine(basePath, "Days", dayName);
        string inputPath = Path.Combine(dayFolder, "input.txt");

        // write path to stdout for debugging

        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"input file not found for {dayName}.");

        return File.ReadAllText(inputPath);
    }
}
