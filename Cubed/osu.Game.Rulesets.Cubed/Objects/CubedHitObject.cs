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

            // Désolé d'avoir écrit ça :'(
            switch (positionX)
            {
                case 0:
                    switch (positionY)
                    {
                        case 0: action = CubedAction.X0Y0; break;
                        case 1: action = CubedAction.X1Y0; break;
                        case 2: action = CubedAction.X2Y0; break;
                        case 3: action = CubedAction.X3Y0; break;
                    }; break;
                case 1:
                    switch (positionY)
                    {
                        case 0: action = CubedAction.X0Y1; break;
                        case 1: action = CubedAction.X1Y1; break;
                        case 2: action = CubedAction.X2Y1; break;
                        case 3: action = CubedAction.X3Y1; break;
                    }; break;
                case 2:
                    switch (positionY)
                    {
                        case 0: action = CubedAction.X0Y2; break;
                        case 1: action = CubedAction.X1Y2; break;
                        case 2: action = CubedAction.X2Y2; break;
                        case 3: action = CubedAction.X3Y2; break;
                    }; break;
                case 3:
                    switch (positionY)
                    {
                        case 0: action = CubedAction.X0Y3; break;
                        case 1: action = CubedAction.X1Y3; break;
                        case 2: action = CubedAction.X2Y3; break;
                        case 3: action = CubedAction.X3Y3; break;
                    }; break;
            };
        }

        // ReSharper disable once PossibleLossOfFraction
        public Vector2 PositionRelative => new Vector2(PositionX / 4f, PositionY / 4f);

        public override Judgement CreateJudgement() { return new CubedJudgement(); }

        protected override HitWindows CreateHitWindows() { return new CubedHitWindow(); }
    }
}
