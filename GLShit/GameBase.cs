using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using GLShit.Audio;
using GLShit.Graphics.Shaders;
using GLShit.Screens;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GLShit;

public class GameBase : GameWindow
{
    public ScreenStack ScreenStack { get; }
    public ShaderManager ShaderManager { get; }
    public AudioEngine AudioEngine { get; set; }
    
    public float DeltaTime { get; set; }
    
    
    public GameBase(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        ScreenStack = new ScreenStack(this);
        ShaderManager = new ShaderManager(this);
        AudioEngine = new AudioEngine(this);
    }

    protected override void OnLoad()
    {
        ShaderManager.Load();
        ScreenStack.Push(new MenuScreen());
        
      

        base.OnLoad();
        
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        DeltaTime = (float)args.Time;
        
        if (ScreenStack.CurrentScreen is null)
            return;
        ScreenStack.CurrentScreen.Update();

        base.OnUpdateFrame(args);
    }
    
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        
        GL.Clear(ClearBufferMask.ColorBufferBit);

        if (ScreenStack.CurrentScreen is null)
            return;
        ScreenStack.CurrentScreen.Draw();
        
        SwapBuffers();
        base.OnRenderFrame(args);
    }

    public void LoadScreen(IScreen? screen)
    {
        if (screen is null)
            return;
        screen.Load(this);
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        if (ScreenStack.CurrentScreen is null)
            return;
        ScreenStack.CurrentScreen.OnKeyDown(e);
        base.OnKeyDown(e);
    }

    public void ChangeScreenMode()
    {
        WindowState = WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
    }
#pragma warning disable CA1416
    public void ScreenShot()
    {
        int width = Size.X;
        int height = Size.Y;

        var filename = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        
        byte[] pixels = new byte[width * height * 4];
        
        GL.ReadPixels(0, 0, width, height, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
        using (var stream = new MemoryStream(pixels))
        {
            // Create a new Bitmap object from the MemoryStream
            using (var bitmap = new Bitmap(width, height))
            {

                var bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                try
                {
                    // Copy the pixel data from the MemoryStream to the Bitmap object
                    Marshal.Copy(pixels, 0, bmpData.Scan0, pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bmpData);
                }

                // Save the Bitmap object to a file
                bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            }
            stream.Dispose();
        }
        
        AudioEngine.LoadTrack("Resources/Audio/UI/Misc/ScreenShot.mp3").Play();
        
        //Opening screenshot
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Windows
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c start {filename}",
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // Linux
            Process.Start("xdg-open", filename);
        }
    }
#pragma warning restore CA1416
    public void Exit()
    {
        Console.WriteLine("Disposing shaders...");
        ShaderManager.Dispose();
        
        Console.WriteLine("Disposing screens...");
        ScreenStack.Dispose();
        
        base.Close();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);
        base.OnResize(e);
    }
    
    public bool SixtyDelta()
    {
        return DeltaTime >= 0.01666666666666666666666666666667f;
    }
}