// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Cubed.Replays
{
    public class CubedAutoGenerator : AutoGenerator<CubedReplayFrame>
    {
        public new Beatmap<CubedHitObject> Beatmap => (Beatmap<CubedHitObject>)base.Beatmap;

        public CubedAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        protected override void GenerateFrames()
        {
            Frames.Add(new CubedReplayFrame());

            foreach (CubedHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new CubedReplayFrame
                {
                    Time = hitObject.StartTime,
                    Position = hitObject.Position,
                    // todo: add required inputs and extra frames.
                });
            }
        }
    }
}
