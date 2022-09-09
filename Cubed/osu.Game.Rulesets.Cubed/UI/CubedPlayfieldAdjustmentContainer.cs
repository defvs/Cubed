using osu.Framework.Graphics;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Cubed.UI
{
    public class CubedPlayfieldAdjustmentContainer : PlayfieldAdjustmentContainer
    {
        public CubedPlayfieldAdjustmentContainer() {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            // TODO Adjust playfield size as a 90% cube is an arbitrary value
            Size = new Vector2(0.9f);
        }
    }
}
