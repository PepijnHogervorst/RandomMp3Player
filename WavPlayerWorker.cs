using System.Media;
using System.Reflection;

namespace RandomMp3Player;
/// <summary>
/// Startable background service that plays mp3 files every x seconds
/// </summary>
internal class WavPlayerWorker
{
    #region Private fields
    private readonly Random _random = new();
    private readonly SoundPlayer _soundPlayer = new();

    private Task? _task = null;
    private string[] _wavFilePaths = [];
    private CancellationTokenSource _tokenSource;
    #endregion


    #region Public properties
    #endregion


    #region Constructor
    public WavPlayerWorker()
    {
        _tokenSource = new CancellationTokenSource();
    }
    #endregion


    #region Public methods
    public void Start()
    {
        if (_task is not null) return;

        IndexAllMp3FilesOfCurrentExeDirectory();

        _tokenSource = new();
        _task = TaskToRun(_tokenSource.Token);
    }

    public void Stop()
    {
        _tokenSource.Cancel();
    }
    #endregion


    #region Private methods
    private void IndexAllMp3FilesOfCurrentExeDirectory()
    {
        var exeFilePath = Environment.ProcessPath;
        var exeDirPath = Path.GetDirectoryName(exeFilePath);
        if (string.IsNullOrEmpty(exeDirPath)) return;

        _wavFilePaths = Directory.GetFiles(exeDirPath, "*.wav", SearchOption.AllDirectories);
    }

    private async Task TaskToRun(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(_random.Next(5, 20)), token);

            PlaySoundFromDirectory();
        }
    }

    private void PlaySoundFromDirectory()
    {
        IndexAllMp3FilesOfCurrentExeDirectory();

        _soundPlayer.SoundLocation = _wavFilePaths[_random.Next(0, _wavFilePaths.Length)];
        ClearCurrentConsoleLine();
        var exeFilePath = Assembly.GetEntryAssembly()?.Location;
        var exeDirPath = Path.GetDirectoryName(exeFilePath);
        Console.Write($"Playing '{Path.GetFileName(_soundPlayer.SoundLocation)}' from folder {exeDirPath}");
        _soundPlayer.PlaySync();
        ClearCurrentConsoleLine();
    }

    private static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }
    #endregion
}
