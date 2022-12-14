// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Framework.Platform;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cubed.Beatmaps;
using osu.Game.Rulesets.Cubed.Mods;
using osu.Game.Rulesets.Cubed.Objects;
using osu.Game.Rulesets.Cubed.Scoring;
using osu.Game.Rulesets.Cubed.UI;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;
using osu.Game.Screens.Ranking.Statistics;

namespace osu.Game.Rulesets.Cubed
{
    public class CubedRuleset : Ruleset
    {
        public override string Description => "Cubed";

        public override string ShortName => "cubedruleset";

        public override string PlayingVerb => "Tapping cubes";

        public override string RulesetAPIVersionSupported => CURRENT_RULESET_API_VERSION;

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) => new DrawableCubedRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => new CubedBeatmapConverter(beatmap, this);

        public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) => new CubedDifficultyCalculator(RulesetInfo, beatmap);

        public override ScoreProcessor CreateScoreProcessor() => new CubedScoreProcessor(this);

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new[] { new MultiMod(new CubedModAutoplay(), new CubedModImprefectAutoplay()) };

                case ModType.DifficultyIncrease:
                    return new[] { new MultiMod(new CubedModDoubleTime(), new CubedModNightcore()) };

                default:
                    return Array.Empty<Mod>();
            }
        }

        protected override IEnumerable<HitResult> GetValidHitResults() => new[]
        {
            HitResult.Perfect,
            HitResult.Great,
            HitResult.Good,
            HitResult.Ok,
            HitResult.Miss
        };

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
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

        public override Drawable CreateIcon() => new ConciergeIcon(this);

        public override StatisticRow[] CreateStatisticsForScore(ScoreInfo score, IBeatmap playableBeatmap)
        {
            var timedHitEvents = score.HitEvents.Where(e => e.HitObject is CubedHitObject).ToList();

            return new[]
            {
                new StatisticRow
                {
                    Columns = new[]
                    {
                        new StatisticItem("Timing Distribution", () => new HitEventTimingDistributionGraph(timedHitEvents)
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 250
                        }, true)
                    }
                },
                new StatisticRow
                {
                    Columns = new[]
                    {
                        new StatisticItem(string.Empty, () => new SimpleStatisticTable(2, new SimpleStatisticItem[]
                        {
                            new AverageHitError(timedHitEvents),
                            new UnstableRate(timedHitEvents)
                        }), true)
                    }
                }
            };
        }

        private class ConciergeIcon : Sprite {
            private readonly CubedRuleset ruleset;

            public ConciergeIcon(CubedRuleset ruleset) {
                this.ruleset = ruleset;
            }

            [BackgroundDependencyLoader]
            private void load(GameHost host) {
                Texture = new TextureStore(host.Renderer, new TextureLoaderStore(ruleset.CreateResourceStore())).Get("Cubed-logo");
            }
        }
    }
}
