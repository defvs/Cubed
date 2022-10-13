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
        public CubedPlayfield()
        {
            InternalChild = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                FillAspectRatio = 1,
                FillMode = FillMode.Fit,
                Children = new Drawable[]
                {
                    new PlayfieldBackground(),
                    HitObjectContainer
                }
            };
        }
    }
}
