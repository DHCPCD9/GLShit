namespace GLShit.Screens;

public class ScreenStack
{
    
    
    public List<IScreen?> Screens { get; set; }
    
    public IScreen? CurrentScreen => Screens.Count > 0 ? Screens[Screens.Count - 1] : null;
    public GameBase GameBase { get; }
    
    public ScreenStack(GameBase game)
    {
        Screens = new List<IScreen?>();
        GameBase = game;
    }
    
    public void Push(IScreen? screen)
    {
        Screens.Add(screen);
        
        //Going to screen
        
        GameBase.LoadScreen(screen);
    }
    
    public void Pop()
    {

        //Unloading old one
        CurrentScreen.Unload();
        
        Screens.RemoveAt(Screens.Count - 1);
        
        
        //Going to screen
        
        GameBase.LoadScreen(CurrentScreen);
    }


    public void Dispose()
    {
        UnloadEverything();
    }

    private void UnloadEverything()
    {
        foreach (var screen in Screens)
        {
            if (screen is null)
                continue;
            screen.Unload();
        }
        
        Screens.Clear();
    }
}