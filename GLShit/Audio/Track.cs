using ManagedBass;

namespace GLShit.Audio;

public class Track : IDisposable
{
    
    public int Handle { get; set; }
    
    public string Path { get; set; }
    public Track(string path)
    {
        Path = path;

        Load();
    }

    private void Load()
    {
        Handle = Bass.CreateStream(Path, 0, 0, BassFlags.Default);
        
        if (Handle == 0)
        {
            throw new Exception("Failed to load track!");
        }
        
        Bass.ChannelSetAttribute(Handle, ChannelAttribute.Volume, 0.5f);
    }
    
    public void Play()
    {
        Bass.ChannelPlay(Handle);
    }

    public void Dispose()
    {
        Bass.StreamFree(Handle);
    }
}