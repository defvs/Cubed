// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Input.StateChanges;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Cubed.Replays
{
    public class CubedFramedReplayInputHandler : FramedReplayInputHandler<CubedReplayFrame>
    {
        public CubedFramedReplayInputHandler(Replay replay)
            : base(replay) {}

        protected override bool IsImportant(CubedReplayFrame frame)
        {
            return frame.Actions.Any();
        }

        protected override void CollectReplayInputs(List<IInput> inputs)
        {
            inputs.Add(new ReplayState<CubedAction>
            {
                PressedActions = CurrentFrame?.Actions ?? new List<CubedAction>()
            });
        }
    }
}
