using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Cubed.Scoring
{
    public class CubedScoreProcessor : ScoreProcessor
    {
        public CubedScoreProcessor(Ruleset ruleset) : base(ruleset) {}

        protected override double DefaultComboPortion => 0;

        protected override double DefaultAccuracyPortion => 1;
    }
}
