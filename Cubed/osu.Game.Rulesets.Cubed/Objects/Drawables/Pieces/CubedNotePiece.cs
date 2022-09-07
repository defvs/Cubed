using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Rulesets.Cubed.Objects.Drawables.Pieces
{
    public class CubedNotePiece : Drawable
    {
        public CubedNotePiece()
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            FillMode = FillMode.Stretch;
            Colour = Colour4.White;
        }
    }
}
