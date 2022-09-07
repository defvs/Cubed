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
			new KeyBinding(InputKey.Number4, CubedAction.X0Y0),
			new KeyBinding(InputKey.Number5, CubedAction.X1Y0),
            new KeyBinding(InputKey.Number6, CubedAction.X2Y0),
            new KeyBinding(InputKey.Number7, CubedAction.X3Y0),

            new KeyBinding(InputKey.R, CubedAction.X0Y1),
            new KeyBinding(InputKey.T, CubedAction.X1Y1),
            new KeyBinding(InputKey.Y, CubedAction.X2Y1),
            new KeyBinding(InputKey.U, CubedAction.X3Y1),

            new KeyBinding(InputKey.F, CubedAction.X0Y2),
            new KeyBinding(InputKey.G, CubedAction.X1Y2),
            new KeyBinding(InputKey.H, CubedAction.X2Y2),
            new KeyBinding(InputKey.J, CubedAction.X3Y2),

            new KeyBinding(InputKey.V, CubedAction.X0Y3),
            new KeyBinding(InputKey.B, CubedAction.X1Y3),
            new KeyBinding(InputKey.N, CubedAction.X2Y3),
            new KeyBinding(InputKey.M, CubedAction.X3Y3)
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
