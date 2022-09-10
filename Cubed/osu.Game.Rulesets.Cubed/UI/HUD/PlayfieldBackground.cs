using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Cubed.Configuration;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cubed.UI.HUD
{
    public class PlayfieldBackground : CompositeDrawable
    {
        private readonly Box bg;
        private readonly Bindable<double> bgDim = new Bindable<double>();

        public PlayfieldBackground()
        {
            RelativeSizeAxes = Axes.Both;
            AddInternal(bg = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black,
                AlwaysPresent = true
            });
        }

        [Resolved(canBeNull: true)] private CubedRulesetConfigManager config { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            config?.BindWith(CubedRulesetSetting.PlayfieldDim, bgDim);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            bgDim.BindValueChanged(dim => onDimChanged(dim.NewValue), true);
        }

        private void onDimChanged(double newDim)
        {
            bg.Alpha = (float)newDim;
        }
    }
}
