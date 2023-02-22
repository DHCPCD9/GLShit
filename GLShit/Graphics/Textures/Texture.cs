using OpenTK.Graphics.ES30;
using OpenTK.Windowing.Common.Input;
using SixLabors.ImageSharp.PixelFormats;
using Image = SixLabors.ImageSharp.Image;

namespace GLShit.Graphics.Textures;

public class Texture : IDisposable
{
    public string Name { get; set; }
    
    public int Handle { get; set; }
    
    public Texture(string name)
    {
        Name = name;
        Handle = 0;
    }

    public void Load()
    {
        
    }
    
    
    public void Dispose()
    {
    }
}