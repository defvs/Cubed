// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Cubed.Beatmaps;
using osu.Game.Rulesets.Cubed.Mods;
using osu.Game.Rulesets.Cubed.UI;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cubed {
	public class CubedRuleset : Ruleset {
		public override string Description => "Cubed";

		public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
			new DrawableCubedRuleset(this, beatmap, mods);

		public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
			new CubedBeatmapConverter(beatmap, this);

		public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) =>
			new CubedDifficultyCalculator(RulesetInfo, beatmap);

		public override IEnumerable<Mod> GetModsFor(ModType type) {
			switch (type) {
				case ModType.Automation:
					return new[] { new CubedModAutoplay() };

				default:
					return Array.Empty<Mod>();
			}
		}

		public override string ShortName => "cubedruleset";

		public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[] {
			new KeyBinding(InputKey.Z, CubedAction.Button1),
			new KeyBinding(InputKey.X, CubedAction.Button2),
		};

		public override Drawable CreateIcon() => new Icon(ShortName[0]);

		public class Icon : CompositeDrawable {
			public Icon(char c) {
				InternalChildren = new Drawable[] {
					new Circle {
						Size = new Vector2(20),
						Colour = Color4.White,
					},
					new SpriteText {
						Anchor = Anchor.Centre,
						Origin = Anchor.Centre,
						Text = c.ToString(),
						Font = OsuFont.Default.With(size: 18)
					}
				};
			}
		}

		// Leave this line intact. It will bake the correct version into the ruleset on each build/release.
		public override string RulesetAPIVersionSupported => CURRENT_RULESET_API_VERSION;
	}
}
