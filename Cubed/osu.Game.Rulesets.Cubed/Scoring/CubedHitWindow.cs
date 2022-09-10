using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Cubed.Scoring
{
    public class CubedHitWindow : HitWindows
    {
        public override bool IsHitResultAllowed(HitResult result)
        {
            switch (result)
            {
                case HitResult.Great:
                case HitResult.Good:
                case HitResult.Ok:
                case HitResult.Meh:
                case HitResult.Miss:
                    return true;
                default:
                    return false;
            }
        }

        // TODO ? : Implement GetRanges
    }
}
