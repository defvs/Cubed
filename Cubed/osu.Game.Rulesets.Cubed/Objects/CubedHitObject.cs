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

        public Bindable<Vector2> DrawPosition { get; } = new Bindable<Vector2>();

        public readonly CubedAction action;

        public CubedHitObject(byte positionX, byte positionY)
        {
            PositionX.Value = Math.Clamp(positionX, (byte)0, (byte)3);
            PositionY.Value = Math.Clamp(positionY, (byte)0, (byte)3);

            updateDrawPosition();
            DrawPosition.BindValueChanged(onDrawablePositionChange);

            action = (CubedAction)(positionX + positionY * 4);
        }

        public void updateDrawPosition()
        {
            DrawPosition.Value = new Vector2(PositionX.Value / 4f + 1 / 8f, PositionY.Value / 4f + 1 / 8f);
        }

        private void onDrawablePositionChange(ValueChangedEvent<Vector2> e)
        {
            PositionX.Value = e.NewValue.X == 1 ? (byte)3 : (byte)(e.NewValue.X * 4);
            PositionY.Value = e.NewValue.Y == 1 ? (byte)3 : (byte)(e.NewValue.Y * 4);
        }

        public override Judgement CreateJudgement() => new CubedJudgement();

        protected override HitWindows CreateHitWindows() => new CubedHitWindow();
    }
}
