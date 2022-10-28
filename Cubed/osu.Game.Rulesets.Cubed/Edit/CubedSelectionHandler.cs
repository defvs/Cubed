using System.Linq;
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
                hitObject.DrawPosition.Value = findDrawPosition(moveEvent, hitObject);
            }
            return true;
        }

        private Vector2 findDrawPosition(MoveSelectionEvent<HitObject> moveEvent, CubedHitObject hitObject)
        {
            Vector2 pos = new Vector2(hitObject.DrawPosition.Value.X * DrawWidth, hitObject.DrawPosition.Value.Y * DrawHeight);
            pos += this.ScreenSpaceDeltaToParentSpace(moveEvent.ScreenSpaceDelta);

            pos.X = System.Math.Clamp(pos.X, 0, DrawWidth) / DrawWidth;
            pos.Y = System.Math.Clamp(pos.Y, 0, DrawHeight) / DrawHeight;

            return pos;
        }
    }
}
