using System.Linq;
using osu.Framework.Input.Events;
using osu.Game.Extensions;
using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Screens.Edit.Compose.Components;
using osuTK;

namespace osu.Game.Rulesets.Cubed.Edit
{
    public class CubedSelectionHandler : EditorSelectionHandler
    {
        private CubedHitObject[] selectedHitObjects => SelectedItems.OfType<CubedHitObject>().ToArray();

        public override bool HandleMovement(MoveSelectionEvent<HitObject> moveEvent)
        {
            foreach (CubedHitObject hitObject in selectedHitObjects)
            {
                Vector2 pos = new Vector2(hitObject.DrawPosition.Value.X * DrawWidth, hitObject.DrawPosition.Value.Y * DrawHeight);
                pos += this.ScreenSpaceDeltaToParentSpace(moveEvent.ScreenSpaceDelta);

                pos.X = System.Math.Clamp(pos.X, 0, DrawWidth) / DrawWidth;
                pos.Y = System.Math.Clamp(pos.Y, 0, DrawHeight) / DrawHeight;

                hitObject.DrawPosition.Value = pos;
            }
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            foreach (CubedHitObject hitObject in selectedHitObjects)
                hitObject.updateDrawPosition();
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            // TODO Refactor this
            bool shouldStopPropagation = false;

            if (e.ControlPressed)
            {
                if (e.PressedKeys.Contains(osuTK.Input.Key.Left))
                {
                    foreach (CubedHitObject hitObject in selectedHitObjects)
                    {
                        if (hitObject.PositionX.Value > 0)
                        {
                            hitObject.PositionX.Value--;
                            hitObject.updateDrawPosition();
                        }
                    } shouldStopPropagation = true;
                }

                if (e.PressedKeys.Contains(osuTK.Input.Key.Right))
                {
                    foreach (CubedHitObject hitObject in selectedHitObjects)
                    {
                        if (hitObject.PositionX.Value < 3)
                        {
                            hitObject.PositionX.Value++;
                            hitObject.updateDrawPosition();
                        }
                    } shouldStopPropagation = true;
                }

                if (e.PressedKeys.Contains(osuTK.Input.Key.Down))
                {
                    foreach (CubedHitObject hitObject in selectedHitObjects)
                    {
                        if (hitObject.PositionY.Value < 3)
                        {
                            hitObject.PositionY.Value++;
                            hitObject.updateDrawPosition();
                        }
                    } shouldStopPropagation = true;
                }

                if (e.PressedKeys.Contains(osuTK.Input.Key.Up))
                {
                    foreach (CubedHitObject hitObject in selectedHitObjects)
                    {
                        if (hitObject.PositionY.Value > 0)
                        {
                            hitObject.PositionY.Value--;
                            hitObject.updateDrawPosition();
                        }
                    } shouldStopPropagation = true;
                }
            }
            return shouldStopPropagation;
        }
    }
}
