// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Cubed.Judgements;
using osu.Game.Rulesets.Cubed.Scoring;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Scoring;
using osuTK;
using System;

namespace osu.Game.Rulesets.Cubed.Objects
{
    public class CubedHitObject : HitObject
    {
        /**
         * X position goes from 0 to 3, starting from X=0 on the left and increasing horizontally rightwards
         */
        public readonly uint PositionX;

        /**
         * Y position goes from 0 to 3, starting from Y=0 at the top and increasing vertically downwards
         */
        public readonly uint PositionY;

        public readonly CubedAction action;

        public CubedHitObject(uint positionX, uint positionY)
        {
            if (PositionX > 3 || PositionY > 3) throw new ArgumentException("CubedHitObject constucted with an illegal position !");

            PositionX = positionX;
            PositionY = positionY;

            action = (CubedAction)(positionX + positionY * 4);
        }

        public Vector2 PositionRelative => new Vector2(PositionX / 4f, PositionY / 4f);

        public override Judgement CreateJudgement() { return new CubedJudgement(); }

        protected override HitWindows CreateHitWindows() { return new CubedHitWindow(); }
    }
}
