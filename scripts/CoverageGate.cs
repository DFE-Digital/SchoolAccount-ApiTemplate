using System.Xml.Linq;
using static System.Globalization.CultureInfo;
using static System.Globalization.NumberStyles;

if (!TryParseArguments(args, out string filePath, out double threshold))
{
    return 2;
}

if (!TryGetLineRate(filePath, out double lineRate))
{
    return 2;
}

double coverage = lineRate * 100;
bool passed = coverage >= threshold;

Console.WriteLine($"Line coverage: {coverage:F2}% (Minimum required: {threshold:F2}%)");

WriteGitHubStepSummary(coverage, threshold, passed);

if (!passed)
{
    Console.Error.WriteLine($"::error::Line coverage {coverage:F2}% is below the minimum required threshold of {threshold:F2}%!");
    return 1;
}

Console.WriteLine("Coverage gate check passed!");
return 0;

static bool TryParseArguments(string[] args, out string filePath, out double threshold)
{
    filePath = string.Empty;
    threshold = 0;

    if (args.Length != 2)
    {
        Console.Error.WriteLine("Error: Invalid argument count.");
        Console.Error.WriteLine("Usage: dotnet run scripts/CoverageGate.cs -- <cobertura-xml-path> <min-coverage-percent>");
        return false;
    }

    filePath = args[0];

    if (!double.TryParse(args[1], Float, InvariantCulture, out threshold) || threshold is < 0 or > 100)
    {
        Console.Error.WriteLine($"Error: Minimum threshold '{args[1]}' must be a valid number between 0 and 100.");
        return false;
    }

    if (!File.Exists(filePath))
    {
        Console.Error.WriteLine($"Error: Cobertura XML file not found at path '{filePath}'.");
        return false;
    }

    return true;
}

static bool TryGetLineRate(string filePath, out double lineRate)
{
    lineRate = 0;

    try
    {
        var doc = XDocument.Load(filePath);
        string? rateAttr = doc.Root?.Attribute("line-rate")?.Value;

        if (rateAttr == null || !double.TryParse(rateAttr, Float, InvariantCulture, out lineRate))
        {
            Console.Error.WriteLine("Error: Failed to find or parse 'line-rate' attribute in the XML root element.");
            return false;
        }

        return true;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error reading XML file: {ex.Message}");
        return false;
    }
}

static void WriteGitHubStepSummary(double coverage, double threshold, bool passed)
{
    string? stepSummary = Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY");

    if (string.IsNullOrEmpty(stepSummary))
    {
        return;
    }

    string statusBadge = passed ? "✅ **PASSED**" : "❌ **FAILED**";

    string markdown = $"""
        ### Coverage Gate Results {statusBadge}

        | Metric | Value | Minimum Threshold | Status |
        | :--- | :---: | :---: | :---: |
        | **Line Coverage** | **{coverage:F2}%** | {threshold:F2}% | {(passed ? "Pass" : "Fail")} |
        """;

    File.AppendAllText(stepSummary, markdown);
}