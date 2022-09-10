// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Cubed.Beatmaps
{
    public class CubedBeatmapConverter : BeatmapConverter<CubedHitObject>
    {
        public CubedBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset) {}

        public override bool CanConvert()
        {
            return Beatmap.HitObjects.Any(o => o is IHasPosition);
        }

        protected override IEnumerable<CubedHitObject> ConvertHitObject(
            HitObject original,
            IBeatmap beatmap,
            CancellationToken cancellationToken
        )
        {
            if (!(original is IHasPosition positionedObj)) yield break;

            yield return new CubedHitObject
            {
                PositionX = (int)(positionedObj.X / 128),
                PositionY = (int)(positionedObj.Y / 96),
                StartTime = original.StartTime,
                HitWindows = original.HitWindows
            };
        }
    }
}
