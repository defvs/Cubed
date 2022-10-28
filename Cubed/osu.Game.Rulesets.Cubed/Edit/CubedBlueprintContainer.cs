using osu.Game.Rulesets.Cubed.Edit.Blueprints;
using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Objects;
using osu.Game.Screens.Edit.Compose.Components;

namespace osu.Game.Rulesets.Cubed.Edit
{
    public class CubedBlueprintContainer : ComposeBlueprintContainer
    {
        public CubedBlueprintContainer(HitObjectComposer composer) : base(composer) {}

        public override HitObjectSelectionBlueprint CreateHitObjectBlueprintFor(HitObject hitObject)
            // TODO Use a better approach to this, currently not possible
            => new CubedHitObjectSelectionBlueprint((CubedHitObject) hitObject);

        protected override SelectionHandler<HitObject> CreateSelectionHandler() => new CubedSelectionHandler();
    }
}
