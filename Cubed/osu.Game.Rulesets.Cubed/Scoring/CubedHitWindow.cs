using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Cubed.Scoring
{
    public class CubedHitWindow : HitWindows
    {
        public override bool IsHitResultAllowed(HitResult result)
        {
            return result switch
            {
                HitResult.Perfect => true,
                HitResult.Great => true,
                HitResult.Good => true,
                HitResult.Ok => true,
                HitResult.Miss => true,
                _ => false
            };
        }

        protected override DifficultyRange[] GetRanges() => new[]
        {
            new DifficultyRange(HitResult.Perfect, 21, 21, 21),
            new DifficultyRange(HitResult.Great, 42, 42, 42),
            new DifficultyRange(HitResult.Good, 84, 84, 84),
            new DifficultyRange(HitResult.Ok, 168, 168, 168),
            new DifficultyRange(HitResult.Miss, 400, 400, 400)
        };
    }
}
