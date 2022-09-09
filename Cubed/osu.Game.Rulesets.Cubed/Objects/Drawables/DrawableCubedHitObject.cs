// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Cubed.Objects.Drawables.Pieces;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cubed.Objects.Drawables
{
    public class DrawableCubedHitObject : DrawableHitObject<CubedHitObject>
    {
        private CubedHitObject hitObject;

        public DrawableCubedHitObject(CubedHitObject hitObject)
            : base(hitObject)
        {
            this.hitObject = hitObject;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Size = new Vector2(100);
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.Centre;
            Position = hitObject.PositionRelative;
            AddInternal(new CubedNotePiece());
        }

        protected override void UpdateInitialTransforms()
        {
            this.FadeInFromZero(200);
            this.ScaleTo(Vector2.One, 200);
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (timeOffset >= 0)
                // todo: implement judgement logic
                ApplyResult(r => r.Type = HitResult.Perfect);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            const double duration = 200;

            switch (state)
            {
                case ArmedState.Hit:
                    this.FadeOut(duration, Easing.OutQuint).Expire();
                    break;

                case ArmedState.Miss:
                    this.FadeColour(Color4.Red, duration);
                    this.FadeOut(duration, Easing.InQuint).Expire();
                    break;
            }
        }
    }
}
