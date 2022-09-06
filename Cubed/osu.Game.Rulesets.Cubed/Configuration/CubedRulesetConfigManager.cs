using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Cubed.Configuration
{
    public class CubedRulesetConfigManager : RulesetConfigManager<CubedRulesetSetting>
    {
        public CubedRulesetConfigManager(SettingsStore settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();
            SetDefault(CubedRulesetSetting.PlayfieldDim, 0.5, 0, 1);
        }
    }

    public enum CubedRulesetSetting
    {
        PlayfieldDim
    }
}
