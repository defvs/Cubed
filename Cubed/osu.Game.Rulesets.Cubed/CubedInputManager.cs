// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Cubed
{
    public class CubedInputManager : RulesetInputManager<CubedAction>
    {
        public CubedInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique) {}
    }

    public enum CubedAction
    {
        [Description("Row 1 Col 1")] X0Y0,
        [Description("Row 1 Col 2")] X1Y0,
        [Description("Row 1 Col 3")] X2Y0,
        [Description("Row 1 Col 4")] X3Y0,

        [Description("Row 2 Col 1")] X0Y1,
        [Description("Row 2 Col 2")] X1Y1,
        [Description("Row 2 Col 3")] X2Y1,
        [Description("Row 2 Col 4")] X3Y1,

        [Description("Row 3 Col 1")] X0Y2,
        [Description("Row 3 Col 2")] X1Y2,
        [Description("Row 3 Col 3")] X2Y2,
        [Description("Row 3 Col 4")] X3Y2,

        [Description("Row 4 Col 1")] X0Y3,
        [Description("Row 4 Col 2")] X1Y3,
        [Description("Row 4 Col 3")] X2Y3,
        [Description("Row 4 Col 4")] X3Y3
    }
}
