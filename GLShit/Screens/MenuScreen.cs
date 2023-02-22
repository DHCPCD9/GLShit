using GLShit.Audio;
using GLShit.Graphics;
using GLShit.Graphics.Shapes;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GLShit.Screens;

public class MenuScreen : Screen
{
    private ExampleBox _box;
    private Track _track;
    public override void Load(GameBase game)
    {
        base.Load(game);
        
        Add(_box = new ExampleBox());
        _track = Game.AudioEngine.LoadTrack("Resources/Audio/UI/Misc/audio.mp3");
        _track.Play();
    }

    public override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        if (e is { Key: Keys.Enter, Alt: true })
        {
            Game.ChangeScreenMode();
        }
        
        if (e is { Key: Keys.Escape })
        {
            Game.Exit();
        }
        
        if (e is { Key: Keys.F12 })
        {
            Game.ScreenShot();
        }

        if (e is { Key: Keys.Right })
        {
            _box.Position.X += 1;
        }
        
        if (e is { Key: Keys.Left })
        {
            _box.Position.X -= 1;
        }
        
        if (e is { Key: Keys.Up })
        {
            _box.Position.Y += 1;
        }
        
        if (e is { Key: Keys.Down })
        {
            _box.Position.Y -= 1;
        }

        if (e is { Key: Keys.A })
        {
            _track.PlaybackPitch -= 1000f;
            _track.PlaybackSpeed -= 1000f;
            Console.WriteLine("Playback speed: " + _track.PlaybackSpeed);
            Console.WriteLine("Playback pitch: " + _track.PlaybackPitch);
        }

        if (e is { Key: Keys.S })
        {
            _track.Position -= 100;
        }

        if (e is { Key: Keys.D })
        {
            _track.PlaybackPitch += 1000f;
            _track.PlaybackSpeed += 1000f;
            Console.WriteLine("Playback speed: " + _track.PlaybackSpeed);
            Console.WriteLine("Playback pitch: " + _track.PlaybackPitch);
        }

        if (e is { Key: Keys.W }) {
            _track.Position = 0;
        }
    }
}