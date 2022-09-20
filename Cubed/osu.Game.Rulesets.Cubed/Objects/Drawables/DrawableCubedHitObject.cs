// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
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
        [Resolved(CanBeNull = false)] private CubedPlayfield playfield { get; set; }
        private readonly CubedHitObject hitObject;
        private CubedNotePiece notePiece;

        public DrawableCubedHitObject(CubedHitObject hitObject)
            : base(hitObject)
        {
            this.hitObject = hitObject;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.25f);
            Scale = new Vector2(0.9f);
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.Centre;

            var actualPosition = hitObject.PositionRelative;
            actualPosition.X += 1 / 8f; /* Centers the object because of Origin = Center */
            actualPosition.Y += 1 / 8f;

            Position = actualPosition;
            AddInternal(notePiece = new CubedNotePiece());
            AddInternal(new CubedTouchInput(hitObject.action));
        }

        protected override double InitialLifetimeOffset => 500f;

        private bool playedFadeIn;
        protected override void UpdateInitialTransforms()
        {
            if (playedFadeIn) return;
            playedFadeIn = true;
            notePiece.PlayAnimation(CubedNotePiece.CubedMarkerAnimation.Approach);
        }

        private bool playedFadeOut;
        protected override void UpdateStartTimeStateTransforms()
        {
            if (playedFadeOut) return;
            playedFadeOut = true;
            notePiece.PlayAnimation(CubedNotePiece.CubedMarkerAnimation.Disappear);
        }

        public virtual bool OnPressed(KeyBindingPressEvent<CubedAction> e)
        {
            if (e.Action != hitObject.action) return false;

            return isHittable(this, Time.Current) && UpdateResult(true);
        }

        public virtual void OnReleased(KeyBindingReleaseEvent<CubedAction> e) {}

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (!userTriggered)
            {
                if (!hitObject.HitWindows.CanBeHit(timeOffset))
                {
                    ApplyResult(r => r.Type = HitResult.Miss);
                }

                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);

            switch (result)
            {
                case HitResult.None: return;
                /*case HitResult.Miss:
                    if (timeOffset < 0) // Early Miss
                        notePiece.PlayAnimation(CubedNotePiece.CubedMarkerAnimation.EarlyMiss);
                    break;
                case HitResult.Ok:
                    notePiece.PlayAnimation(CubedNotePiece.CubedMarkerAnimation.Ok);
                    break;
                case HitResult.Good:
                    notePiece.PlayAnimation(CubedNotePiece.CubedMarkerAnimation.Good);
                    break;
                case HitResult.Great:
                    notePiece.PlayAnimation(CubedNotePiece.CubedMarkerAnimation.Great);
                    break;
                case HitResult.Perfect:
                    notePiece.PlayAnimation(CubedNotePiece.CubedMarkerAnimation.Perfect);
                    break;*/
            }

            ApplyResult(r => r.Type = result);
        }

        public bool isHittable(DrawableCubedHitObject drawableHitObject, double time)
        {
            var nextObject = playfield.hitObjectContainer.AliveObjects.GetNext(drawableHitObject);
            return nextObject == null || time < nextObject.HitObject.StartTime;
        }

        protected override double MaximumJudgementOffset => 1000;

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            this.Delay(1000).Expire();
        }

        public class CubedTouchInput : Drawable
        {
            private readonly CubedAction action;

            public CubedTouchInput(CubedAction action)
            {
                RelativeSizeAxes = Axes.Both;

                this.action = action;
            }

            [Resolved(canBeNull: true)] private CubedInputManager cubedInputManager { get; set; }

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
