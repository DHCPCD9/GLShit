using ManagedBass;

namespace GLShit.Audio;

public class Track : IDisposable
{
    
    public int Handle { get; set; }
    
    public string Path { get; set; }
    public double Volume
    {
        get => Bass.ChannelGetAttribute(Handle, ChannelAttribute.Volume);
        set => Bass.ChannelSetAttribute(Handle, ChannelAttribute.Volume, value);
    }

    public double PlaybackSpeed
    {
        get => Bass.ChannelGetAttribute(Handle, ChannelAttribute.Frequency);
        set => Bass.ChannelSetAttribute(Handle, ChannelAttribute.Frequency, value);
    }

    public double Position
    {
        get => Bass.ChannelBytes2Seconds(Handle, Bass.ChannelGetPosition(Handle));
        set => Bass.ChannelSetPosition(Handle, Bass.ChannelSeconds2Bytes(Handle, value));
    }
    public double Length => Bass.ChannelBytes2Seconds(Handle, Bass.ChannelGetLength(Handle));
    public bool IsPlaying => Bass.ChannelIsActive(Handle) == PlaybackState.Playing;

    public double PlaybackPitch
    {
        get => Bass.ChannelGetAttribute(Handle, ChannelAttribute.Pitch);
        set => Bass.ChannelSetAttribute(Handle, ChannelAttribute.Pitch, value);
    }

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