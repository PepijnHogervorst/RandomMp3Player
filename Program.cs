using RandomMp3Player;

Console.WriteLine("Welcome to the random wav player! " +
    "\nThis application plays the wav files in the folder where this executable is ran! Starting now :)");

var player = new WavPlayerWorker();

player.Start();

Console.WriteLine($"Enter a key to stop the player!");
Console.ReadKey();

player.Stop();