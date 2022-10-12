using System;
using System.Collections.Generic;
using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;

namespace osu.Game.Rulesets.Cubed.Edit
{
    public class CubedHitObjectComposer : HitObjectComposer<CubedHitObject>
    {
        public CubedHitObjectComposer(CubedRuleset ruleset) : base(ruleset) {}

        protected override IReadOnlyList<HitObjectCompositionTool> CompositionTools => Array.Empty<HitObjectCompositionTool>();
    }
}
