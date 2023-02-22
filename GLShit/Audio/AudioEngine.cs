using ManagedBass;

namespace GLShit.Audio;

public class AudioEngine
{
    public Dictionary<string, Track> Tracks { get; set; } = new();
    public GameBase GameBase { get; set; }
    
    public AudioEngine(GameBase game)
    {
        GameBase = game;
        
        Init();
    }

    private void Init()
    {
        if (!Bass.Init())
        {
            throw new Exception("Failed to initialize audio engine!");
        }
        
        if (!Bass.Start())
        {
            throw new Exception("Failed to start audio engine!");
        }
        
        
    }
    
    public Track LoadTrack(string path)
    {
        if (Tracks.ContainsKey(path))
        {
            return Tracks[path];
        }
        var track = new Track(path);
        Tracks.Add(path, track);
        return track;
    }
    
    
    public void Dispose()
    {
        foreach (var track in Tracks)
        {
            track.Value.Dispose();
        }
        
        Tracks.Clear();
        
        Bass.Stop();
        Bass.Free();
    }
    
    
}