using OpenTK.Graphics.ES30;

namespace GLShit.Graphics.Shaders;

public class Shader : IDisposable
{
    public int Handle { get; set; }
    private int _vertexHandle { get; set; }
    private int _fragmentHandle { get; set; }
    private string _vertex { get; set; }
    private string _fragment { get; set; }
    
    private GameBase _game { get; set; }
    private string _name { get; set; }
    
    public Shader(GameBase gameBase, string name, string vertexPath, string fragmentPath)
    {
        _game = gameBase;
        _name = name;
        
        Handle = 0;

        var vertex = File.ReadAllText(vertexPath);
        var fragment = File.ReadAllText(fragmentPath);
        
        
        _vertex = vertex;
        _fragment = fragment;

        Compile();
    }

    private void Compile()
    {
        _vertexHandle = GL.CreateShader(ShaderType.VertexShader);
        _fragmentHandle = GL.CreateShader(ShaderType.FragmentShader);

        GL.ShaderSource(_vertexHandle, _vertex);
        GL.ShaderSource(_fragmentHandle, _fragment);

        GL.CompileShader(_vertexHandle);
        GL.CompileShader(_fragmentHandle);
        
        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, _vertexHandle);
        GL.AttachShader(Handle, _fragmentHandle);

        GL.LinkProgram(Handle);
        
        GL.GetShaderInfoLog(_vertexHandle, out var vertexLog);
        
        if (!string.IsNullOrEmpty(vertexLog))
        {
            Console.Write("Vertex shader log:");
            Console.WriteLine(vertexLog);
        }
        
        GL.GetShaderInfoLog(_fragmentHandle, out var fragmentLog);
        
        if (!string.IsNullOrEmpty(fragmentLog))
        {
            Console.Write("Fragment shader log:");
            Console.WriteLine(fragmentLog);
        }
        
        GL.GetProgramInfoLog(Handle, out var programLog);
        
        if (!string.IsNullOrEmpty(programLog))
        {
            Console.Write("Program shader log:");
            Console.WriteLine(programLog);
        }
        
        Console.WriteLine($"Shader {_name} compiled");
    }
    
    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public void Dispose()
    {
        GL.DetachShader(Handle, _vertexHandle);
        GL.DetachShader(Handle, _fragmentHandle);
        GL.DeleteShader(_vertexHandle);
        GL.DeleteShader(_fragmentHandle);
        GL.DeleteProgram(Handle);
        
        GC.SuppressFinalize(this);
    }

    public void UnUse()
    {
        GL.UseProgram(0);
    }
}