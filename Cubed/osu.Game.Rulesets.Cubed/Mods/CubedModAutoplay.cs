// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
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
        [SettingSource("Margin preset")]
        public Bindable<Presets> preset { get; } = new Bindable<Presets>();

        [SettingSource("Error margin", "Maximum timing error")]
        public BindableInt ArtificialErrorMargin { get; } = new BindableInt(0)
        {
            MinValue = 0,
            MaxValue = 168,  // TODO fetch the value because it could be changed by stuff like double time
        };

        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
             => new ModReplayData(new CubedAutoGenerator(beatmap, ArtificialErrorMargin.Value).Generate(),
                                  new ModCreatedUser { Username = "Concierge" });

        public CubedModAutoplay()
        {
            preset.BindValueChanged(onPresetChange);
            ArtificialErrorMargin.BindValueChanged(onValueChanged);
        }

        // From here, constants 21, 42, 84, 168 are hitwindow sizes
        public void onPresetChange(ValueChangedEvent<Presets> e)
        {
            if (e.NewValue != Presets.custom) 
                ArtificialErrorMargin.Value = (int) e.NewValue;
        }

        public void onValueChanged(ValueChangedEvent<int> e)
        {
            switch (e.NewValue)
            {
                case 0:   preset.Value = Presets.exact;    break;
                case 21:  preset.Value = Presets.perfect;  break;
                case 42:  preset.Value = Presets.great;    break;
                case 84:  preset.Value = Presets.good;     break;
                case 168: preset.Value = Presets.ok;       break;
                default:  preset.Value = Presets.custom;   break;
            }
        }
    }

    public enum Presets
    {
        [Description("Exact hits")]
        exact = 0,
        [Description("Perfect")]
        perfect = 21,
        [Description("Great")]
        great = 42,
        [Description("Good")]
        good = 84,
        [Description("Ok")]
        ok = 168,
        [Description("Custom")]
        custom = 573
    }
}
