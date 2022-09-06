// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osuTK;

namespace osu.Game.Rulesets.Cubed.Objects
{
    public class CubedHitObject : HitObject
    {
        public override Judgement CreateJudgement() => new Judgement();

        /**
         * X position goes from 0 to 3, starting from X=0 on the left and increasing horizontally rightwards
         */
        public int PositionX;

        /**
         * Y position goes from 0 to 3, starting from Y=0 at the top and increasing vertically downwards
         */
        public int PositionY;
    }
}
