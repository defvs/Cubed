// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cubed.Replays;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Cubed.Mods {
	public class CubedModAutoplay : ModAutoplay {
		public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
			=> new ModReplayData(new CubedAutoGenerator(beatmap).Generate(), new ModCreatedUser { Username = "Concierge" });
	}
}
