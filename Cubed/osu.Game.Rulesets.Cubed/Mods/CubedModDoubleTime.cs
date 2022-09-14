using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Cubed.Mods
{
    public class CubedModDoubleTime : ModDoubleTime
    {
        public override double ScoreMultiplier => UsesDefaultConfiguration ? 1.12 : 1;
    }
}
