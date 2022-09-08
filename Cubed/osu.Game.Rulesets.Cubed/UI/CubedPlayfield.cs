// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.Cubed.UI.HUD;
using osu.Game.Rulesets.UI;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cubed.UI
{
    [Cached]
    public class CubedPlayfield : Playfield
    {
        public CubedPlayfield()
        {
            InternalChildren = new Drawable[]
            {
                mainContainer = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    CornerRadius = 7,
                    BorderThickness = 1.2f,
                    BorderColour = Color4.White,
                    Colour = Color4.White,
                    EdgeEffect = new EdgeEffectParameters
                    {
                        Hollow = true,
                        Radius = 10,
                        Colour = Color4.Black.Opacity(0.4f),
                        Type = EdgeEffectType.Shadow,
                    },
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

        private readonly Container mainContainer;
        private readonly Container gameplayContainer;

        [BackgroundDependencyLoader]
        private void load()
        {
            gameplayContainer.Add(HitObjectContainer);
        }
    }
}
