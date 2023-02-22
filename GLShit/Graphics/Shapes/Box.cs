


using System.Diagnostics;
using GLShit.Graphics.Shaders;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;

namespace GLShit.Graphics.Shapes;

public class Box : BaseDrawObject
{
    private readonly Vector2[] _shape_vertices = new[]
    {
        new Vector2(-0.5f, -0.5f),  // Bottom left
        new Vector2(0.5f, -0.5f),   // Bottom right
        new Vector2(0.5f, 0.5f),    // Top right
        new Vector2(-0.5f, 0.5f)    // Top left
    };
    
    public Vector2[] _vertices = new Vector2[4];

    public int Scale { get; set; } = 1;

    public Vector Position { get; } = new();
    public float Rotation { get; set; } = 0f;
    public Color4 Color { get; set; } = Color4.White;
    public Shader Shader { get; set; }

    public Vector Size { get; set; } = new Vector
    {
        X = 100, // Size in pixels
        Y = 100  // Size in pixels
    };

    private int _vertexBufferObject;
    private int _vertexArrayObject;


    public override void Load(GameBase @base)
    {
        _vertices = _shape_vertices;
        
        base.Load(@base);

        Shader = GameBase.ShaderManager.GetShader("Default");

        _vertexArrayObject = GL.GenVertexArray();
        _vertexBufferObject = GL.GenBuffer();
        GL.BindVertexArray(_vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(_vertices.Length * Vector2.SizeInBytes), _vertices, BufferUsageHint.StaticDraw);

        GL.BindVertexArray(_vertexArrayObject);
        GL.EnableVertexAttribArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

        
        Shader.Use();
        //Setting white color
        int vertexColorLocation = GL.GetUniformLocation(Shader.Handle, "ourColor");
        
        
        GL.Uniform4(vertexColorLocation, 1.0f, 1.0f, 1.0f, 1.0f);
        Shader.UnUse();
        
        GL.EnableVertexAttribArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

    }

    public override void Update()
    {
        Shader.Use();

        int vertexColorLocation = GL.GetUniformLocation(Shader.Handle, "ourColor");
        
        GL.Uniform4(vertexColorLocation, Color.R, Color.G, Color.B, Color.A);
        
        int vertexPosLocation = GL.GetUniformLocation(Shader.Handle, "uPos");
        var pos = new Vector3(Position.X, Position.Y, 0);
        
        //Fixing the position, to be in the left top corner
        //Because the position is 0,0 at top corner and 1,1 at bottom corner
        var width = GameBase.Size.X;
        var height = GameBase.Size.Y;
        var screenX = (pos.X / width) * 2 - 1;
        var screenY = (pos.Y / height) * 2 - 1;
        pos = new Vector3(screenX, screenY, 0);

        GL.Uniform3(vertexPosLocation, pos.X, pos.Y, 0);

        for (int i = 0; i < _vertices.Length; i++)
        {
            _vertices[i] = Vector2.Transform(_shape_vertices[i], Quaternion.FromAxisAngle(Vector3.UnitZ, Rotation));
        }
        
        //Now transform the vertices to the correct size
        //var scale = new Vector2(Size.X / GameBase.Size.X, Size.Y / GameBase.Size.Y);
        //for (int i = 0; i < _vertices.Length; i++)
        //{
        //_vertices[i] = Vector2.Multiply(_shape_vertices[i], scale);
        //}
        
        Shader.UnUse();

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vector3.SizeInBytes, _vertices, BufferUsageHint.StaticDraw);
        
    }

    public override void Unload()
    {
        base.Unload();
        
        GL.DeleteBuffer(_vertexBufferObject);
    }

    public override void Draw()
    {
        Shader.Use();
        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertices.Length);
        Shader.UnUse();
    }
}