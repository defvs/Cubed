// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Cubed.Replays
{
    public class CubedAutoGenerator : AutoGenerator<CubedReplayFrame>
    {
        private readonly Random random = new Random();
        private readonly double maxOffset;

        public CubedAutoGenerator(IBeatmap beatmap, double maxOffset)
            : base(beatmap)
        {
            this.maxOffset = maxOffset;
        }

        public new Beatmap<CubedHitObject> Beatmap => (Beatmap<CubedHitObject>)base.Beatmap;

        protected override void GenerateFrames()
        {
            Frames.Add(new CubedReplayFrame(0));

            double offset;
            foreach (var hitObject in Beatmap.HitObjects)
            {
                offset = maxOffset == 0 ? maxOffset : getOffset();
                Frames.Add(new CubedReplayFrame(hitObject.StartTime + offset, hitObject.action));
                Frames.Add(new CubedReplayFrame(hitObject.StartTime + KEY_UP_DELAY + offset));
            }
        }

        private double getOffset()
        {
            double offset = random.NextDouble() * maxOffset;
            return random.Next(2) == 0 ? offset : -offset;
        }
    }
}
