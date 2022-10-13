using System;
using System.Collections.Generic;
using osu.Game.Resources.Localisation.Web;
using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Screens.Edit.Compose.Components;

namespace osu.Game.Rulesets.Cubed.Edit
{
    public class CubedHitObjectComposer : HitObjectComposer<CubedHitObject>
    {
        public CubedHitObjectComposer(CubedRuleset ruleset) : base(ruleset) {}

        protected override IReadOnlyList<HitObjectCompositionTool> CompositionTools => Array.Empty<HitObjectCompositionTool>();

        protected override ComposeBlueprintContainer CreateBlueprintContainer() => new CubedBlueprintContainer(this);
    }
}
