namespace GLShit.Graphics.Textures;

public class TextureManager
{
    public GameBase GameBase { get; set; }

    public TextureManager(GameBase game)
    {
        GameBase = game;
    }
    
    public Dictionary<string, Texture> Textures { get; set; } = new Dictionary<string, Texture>();
}