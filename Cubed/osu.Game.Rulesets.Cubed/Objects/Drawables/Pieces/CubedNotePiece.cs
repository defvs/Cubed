using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace osu.Game.Rulesets.Cubed.Objects.Drawables.Pieces
{
    public class CubedNotePiece : Box
    {
        public CubedNotePiece()
        {
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = Vector2.One;
            Alpha = 1;
            Position = Vector2.Zero;
            Colour = Colour4.White;
            Invalidate();
        }
    }
}
