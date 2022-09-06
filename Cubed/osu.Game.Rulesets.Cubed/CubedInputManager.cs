// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Cubed {
	public class CubedInputManager : RulesetInputManager<CubedAction> {
		public CubedInputManager(RulesetInfo ruleset)
			: base(ruleset, 0, SimultaneousBindingMode.Unique) {}
	}

	public enum CubedAction {
		[Description("Button 1")] Button1,

		[Description("Button 2")] Button2,
	}
}
