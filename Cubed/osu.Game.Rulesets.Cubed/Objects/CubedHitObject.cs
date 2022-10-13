// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
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
         * X position ranges from 0 to 3, starting from 0 from the left and increasing rightwards
         */
        public BindableNumber<byte> PositionX { get; } = new BindableNumber<byte>()
        {
            MaxValue = 3
        };

        /**
         * Y position ranges from 0 to 3, 0 is the top and it increases downwards
         */
        public BindableNumber<byte> PositionY { get; } = new BindableNumber<byte>()
        {
            MaxValue = 3
        };

        public readonly CubedAction action;

        public CubedHitObject(byte positionX, byte positionY)
        {
            PositionX.Value = Math.Clamp(positionX, (byte)0, (byte)3);
            PositionY.Value = Math.Clamp(positionY, (byte)0, (byte)3);

            action = (CubedAction)(positionX + positionY * 4);
        }

        public Vector2 PositionRelative => new Vector2(PositionX.Value / 4f, PositionY.Value / 4f);

        public override Judgement CreateJudgement() => new CubedJudgement();

        protected override HitWindows CreateHitWindows() => new CubedHitWindow();
    }
}
