// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Cubed.Objects.Drawables.Pieces;
using osu.Game.Rulesets.Cubed.UI;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cubed.Objects.Drawables
{
    public class DrawableCubedHitObject : DrawableHitObject<CubedHitObject>, IKeyBindingHandler<CubedAction>
    {
        [Resolved(CanBeNull = false)]
        private CubedPlayfield playfield { get; set; }
        private readonly CubedHitObject hitObject;

        public DrawableCubedHitObject(CubedHitObject hitObject)
            : base(hitObject)
        {
            this.hitObject = hitObject;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1 / 4f);
            Scale = new Vector2(0.9f);
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            Position = hitObject.PositionRelative;
            Alpha = 0;
            AddInternal(new CubedNotePiece());
            AddInternal(new CubedTouchInput(hitObject.action));
        }

        protected override void UpdateInitialTransforms()
        {
            this.FadeInFromZero(200);
        }

        public virtual bool OnPressed(KeyBindingPressEvent<CubedAction> e)
        {
            if (e.Action != hitObject.action) return false;

            if (!isHittable(this, Time.Current)) return false;

            return UpdateResult(true);
        }

        public virtual void OnReleased(KeyBindingReleaseEvent<CubedAction> e) { }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (!userTriggered)
            {
                if (!hitObject.HitWindows.CanBeHit(timeOffset))
                    ApplyResult(r => r.Type = HitResult.Miss);

                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);

            if (result == HitResult.None)
                return;

            ApplyResult(r => r.Type = result);
        }

        public bool isHittable(DrawableCubedHitObject drawableHitObject, double time /* haha */)
        {
            var nextObject = playfield.hitObjectContainer.AliveObjects.GetNext(drawableHitObject);
            return nextObject == null || time < nextObject.HitObject.StartTime;
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

        public class CubedTouchInput : Drawable
        {
            private readonly CubedAction action;

            public CubedTouchInput(CubedAction action)
            {
                RelativeSizeAxes = Axes.Both;

                this.action = action;
            }

            [Resolved(canBeNull: true)]
            private CubedInputManager cubedInputManager { get; set; }

            private KeyBindingContainer<CubedAction> keyBindingContainer;

            protected override void LoadComplete()
            {
                keyBindingContainer = cubedInputManager.KeyBindingContainer;
            }

            protected override bool OnTouchDown(TouchDownEvent e)
            {
                keyBindingContainer?.TriggerPressed(action);
                return true;
            }

            protected override void OnTouchUp(TouchUpEvent e)
            {
                keyBindingContainer?.TriggerReleased(action);
            }

            protected override bool OnMouseDown(MouseDownEvent e)
            {
                keyBindingContainer?.TriggerPressed(action);
                return base.OnMouseDown(e);
            }

            protected override void OnMouseUp(MouseUpEvent e)
            {
                keyBindingContainer?.TriggerReleased(action);
                base.OnMouseUp(e);
            }
        }
    }
}
