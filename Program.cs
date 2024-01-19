using RandomMp3Player;

Console.WriteLine("Welcome to the random wav player! " +
    "\nThis application plays the wav files in the folder where this executable is ran! Starting now :)");

WavPlayerWorker player = new();
if (ParseArguments.GetLowerAndUpperLimitValues(out int lowerLimit, out int upperLimit))
{
    player.SetDelayTimeLimits(lowerLimit, upperLimit);
}
Console.WriteLine($"(Delay is between {player.MinimumDelayInSeconds} - {player.MaximumDelayInSeconds} seconds)");

player.Start();

Console.WriteLine($"Enter a key to stop the player!");
Console.ReadKey();

player.Stop();