﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.ComponentModel;
using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Rulesets.Cubed.Replays;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Cubed.Mods
{
    public class CubedModAutoplay : ModAutoplay
    {
        [SettingSource("Error margin", "Maximum timing error")]
        public BindableInt ArtificialErrorMargin { get; } = new BindableInt(0)
        {
            MinValue = 0,
            MaxValue = 168,  // TODO fetch the value because it could be changed by stuff like double time
        };

    public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
             => new ModReplayData(new CubedAutoGenerator(beatmap, ArtificialErrorMargin.Value).Generate(),
                                  new ModCreatedUser { Username = "Concierge" });
    }
}
