using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Cubed.Mods
{
    public class CubedModNightcore : ModNightcore<CubedHitObject>
    {
        public override double ScoreMultiplier => UsesDefaultConfiguration ? 1.12 : 1;
    }
}
