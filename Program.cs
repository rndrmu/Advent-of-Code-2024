using System;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("usage: dotnet run -- <day>");
            return;
        }

        string dayArg = args[0].ToLower();

        try
        {
            // dynamically find the class for the specified day
            var dayType = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .FirstOrDefault(t => t.Name.Equals(dayArg, StringComparison.OrdinalIgnoreCase));

            if (dayType == null)
            {
                Console.WriteLine($"error: no class found for '{dayArg}'.");
                return;
            }

            // ensure the class has a `Run` method
            var runMethod = dayType.GetMethod("Run", BindingFlags.Public | BindingFlags.Static);
            if (runMethod == null)
            {
                Console.WriteLine($"error: '{dayType.Name}' does not have a static 'Run()' method.");
                return;
            }

            // invoke the `Run` method
            Console.WriteLine($"running {dayType.Name}...");
            runMethod.Invoke(null, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }
}
