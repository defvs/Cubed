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
         * X position ranges from 0 to 3, starting from 0 on the left and increasing rightwards
         */
        public readonly byte PositionX;

        /**
         * Y position ranges from 0 to 3, 0 is the top and it increases downwards
         */
        public readonly byte PositionY;

        public readonly CubedAction action;

        public CubedHitObject(byte positionX, byte positionY)
        {
            if (PositionX > 3 || PositionY > 3) throw new ArgumentException("CubedHitObject's position is illegal !\n" +
                "The allowed value range is from 0 to 3.");

            PositionX = positionX;
            PositionY = positionY;

            action = (CubedAction)(positionX + positionY * 4);
        }

        public Vector2 PositionRelative => new Vector2(PositionX / 4f, PositionY / 4f);

        public override Judgement CreateJudgement() => new CubedJudgement();

        protected override HitWindows CreateHitWindows() => new CubedHitWindow();
    }
}
