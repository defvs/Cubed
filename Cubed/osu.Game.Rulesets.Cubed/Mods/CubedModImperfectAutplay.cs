using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Localisation;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Rulesets.Cubed.Replays;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Cubed.Mods
{
    public class CubedModImprefectAutoplay : ModAutoplay
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
        public BindableInt ArtificialErrorMargin { get; } = new BindableInt(0)
        {
            MinValue = 0,
            MaxValue = 168,
        };

        protected double getConvertedMargin(IEnumerable<Mod> mods)
        {
            foreach (Mod mod in mods)
            {
                if (mod is ModRateAdjust concierge)
                    return concierge.SpeedChange.Value / ArtificialErrorMargin.Value;
            }

            return ArtificialErrorMargin.Value;
        }

        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new CubedAutoGenerator(beatmap, getConvertedMargin(mods)).Generate(),
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
                case 0:
                case 21:
                case 42:
                case 84:
                case 168:
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
