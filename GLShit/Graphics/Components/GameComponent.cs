

using GLShit;
using GLShit.Graphics;

class GameComponent : DrawableObject {
    public override void Load(GameBase @base)
    {
        // Ensuring there is parent is null
        if (Parent != null) {
            throw new GameComponentHasParentException("GameComponent cannot have parent");
        }

        base.Load(@base);
    }
}