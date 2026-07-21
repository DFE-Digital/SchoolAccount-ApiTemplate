// Fails (exit 1) when line coverage in a Cobertura report is below the threshold.
// Usage: dotnet run scripts/CoverageGate.cs -- <cobertura-file> <min-percent>
using System.Globalization;
using System.Xml.Linq;

if (args.Length != 2)
{
    await Console.Error.WriteLineAsync("Usage: dotnet run scripts/CoverageGate.cs -- <cobertura-file> <min-percent>");
    return 2;
}

string lineRate = XDocument.Load(args[0]).Root!.Attribute("line-rate")!.Value;
double coverage = double.Parse(lineRate, CultureInfo.InvariantCulture) * 100;
double threshold = double.Parse(args[1], CultureInfo.InvariantCulture);

await Console.Out.WriteLineAsync($"Line coverage: {coverage:F1}% (minimum {threshold}%)");
if (coverage < threshold)
{
    await Console.Error.WriteLineAsync($"::error::Line coverage {coverage:F1}% is below the minimum {threshold}%");
    return 1;
}

return 0;
