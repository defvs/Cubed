using System.Collections.Generic;
using osu.Game.Rulesets.Cubed.Replays;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Cubed.UI
{
    public class CubedReplayRecorder : ReplayRecorder<CubedAction>
    {
        public CubedReplayRecorder(Score target) : base(target) {}

        protected override ReplayFrame HandleFrame(Vector2 mousePosition, List<CubedAction> actions, ReplayFrame previousFrame)
            => new CubedReplayFrame(Time.Current, actions.ToArray());
    }
}
