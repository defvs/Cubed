// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Cubed.Replays {
	public class CubedReplayFrame : ReplayFrame {
		public List<CubedAction> Actions = new List<CubedAction>();
		public Vector2 Position;

		public CubedReplayFrame(CubedAction? button = null) {
			if (button.HasValue)
				Actions.Add(button.Value);
		}
	}
}
