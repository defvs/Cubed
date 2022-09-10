// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Cubed.UI.HUD;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Cubed.UI
{
    [Cached]
    public class CubedPlayfield : Playfield
    {
        private readonly Container gameplayContainer;

        private readonly Container mainContainer;

        public CubedPlayfield()
        {
            InternalChildren = new Drawable[]
            {
                mainContainer = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new PlayfieldBackground(),
                        gameplayContainer = new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            FillAspectRatio = 1,
                            FillMode = FillMode.Fit
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            gameplayContainer.Add(HitObjectContainer);
        }
    }
}
