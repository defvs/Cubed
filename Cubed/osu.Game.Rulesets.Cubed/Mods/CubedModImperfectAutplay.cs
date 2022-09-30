using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Localisation;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Cubed.Replays;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Cubed.Mods
{
    public class CubedModImprefectAutoplay : ModAutoplay, IHasSeed
    {
        public override string Name => "Imperfect Autoplay";

        public override LocalisableString Description => "Watch a not so perfect automated play through the song.";

        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new Type[]
        {
            typeof(ModAutoplay),
            typeof(ModTimeRamp),  // Maybe I could change that
            typeof(ModAdaptiveSpeed)
        }).ToArray();

        [SettingSource("Margin preset")]
        public Bindable<Presets> preset { get; } = new Bindable<Presets>();

        [SettingSource("Error margin", "Maximum timing error")]
        public BindableInt ArtificialErrorMargin { get; } = new BindableInt((int) Presets.exact)
        {
            MinValue = (int) Presets.exact,
            MaxValue = (int) Presets.ok
        };

        [SettingSource("Seed", "Custom seed to use for timing randomization", SettingControlType = typeof(SettingsNumberBox))]
        public Bindable<int?> Seed { get; } = new Bindable<int?>();

        protected double getConvertedMargin(IEnumerable<Mod> mods)
        {
            foreach (Mod mod in mods)
            {
                if (mod is ModRateAdjust concierge)
                    return 1 / concierge.SpeedChange.Value * ArtificialErrorMargin.Value;
            }

            return ArtificialErrorMargin.Value;
        }

        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new CubedAutoGenerator(beatmap, getConvertedMargin(mods), Seed.Value).Generate(),
                                 new ModCreatedUser { Username = "Concierge" });

        public CubedModImprefectAutoplay()
        {
            preset.BindValueChanged(onPresetChange);
            ArtificialErrorMargin.BindValueChanged(onValueChanged);
        }

        public void onPresetChange(ValueChangedEvent<Presets> e)
        {
            if (e.NewValue != Presets.custom)
                ArtificialErrorMargin.Value = (int) e.NewValue;
        }

        public void onValueChanged(ValueChangedEvent<int> e)
        {
            switch (e.NewValue)
            {
                case (int) Presets.exact:
                case (int) Presets.perfect:
                case (int) Presets.great:
                case (int) Presets.good:
                case (int) Presets.ok:
                    preset.Value = (Presets) e.NewValue;
                    break;
                default:
                    preset.Value = Presets.custom;
                    break;
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
