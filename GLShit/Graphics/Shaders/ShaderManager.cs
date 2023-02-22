namespace GLShit.Graphics.Shaders;

public class ShaderManager
{
    public GameBase GameBase { get; set; }
    
    public Dictionary<string, Shader> Shaders { get; set; } = new Dictionary<string, Shader>();
    public ShaderManager(GameBase game)
    {
        GameBase = game;
    }
    
    public void Load()
    {
        var shader = new Shader(GameBase, "Default", "Shaders/Default.vert", "Shaders/Default.frag");
        Shaders.Add("Default", shader);
    }
    
    public void Unload()
    {
        foreach (var shader in Shaders)
        {
            shader.Value.Dispose();
        }
    }
    
    public Shader GetShader(string name)
    {
        return Shaders[name];
    }

    public void Dispose()
    {
        Unload();
    }
}