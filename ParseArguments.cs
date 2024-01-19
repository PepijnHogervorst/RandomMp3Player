namespace RandomMp3Player;

internal class ParseArguments
{
    public static bool GetLowerAndUpperLimitValues(out int lowerLimitSeconds, out int upperLimitSeconds)
    {
        lowerLimitSeconds = 0;
        upperLimitSeconds = 0;

        string[] arguments = Environment.GetCommandLineArgs();
        if (arguments.Length < 3) return false;

        if (!int.TryParse(arguments[^2], out lowerLimitSeconds)) return false;
        if (!int.TryParse(arguments[^1], out upperLimitSeconds)) return false;

        return true;
    }
}
