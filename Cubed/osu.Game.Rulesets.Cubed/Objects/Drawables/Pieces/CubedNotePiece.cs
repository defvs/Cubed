using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Animations;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;

namespace osu.Game.Rulesets.Cubed.Objects.Drawables.Pieces
{
    public class CubedNotePiece : CompositeDrawable
    {
        public enum CubedMarkerAnimation
        {
            Approach,
            EarlyMiss,
            Perfect,
            Great,
            Good,
            Ok,
            Disappear
        }

        private readonly Dictionary<CubedMarkerAnimation, TextureAnimation> animations =
            new Dictionary<CubedMarkerAnimation, TextureAnimation>();

        public CubedNotePiece()
        {
            RelativeSizeAxes = Axes.Both;

            AddRangeInternal(Enum.GetValues(typeof(CubedMarkerAnimation)).Cast<CubedMarkerAnimation>().Select(createTextureAnimation));
        }

        private TextureAnimation createTextureAnimation(CubedMarkerAnimation animation) =>
            animations[animation] = new TextureAnimation
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                FillMode = FillMode.Fit,
                RelativeSizeAxes = Axes.Both,
                Loop = false,
                Alpha = 0,
                DefaultFrameLength = 1000.0 / 32.0
            };

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            animations[CubedMarkerAnimation.Approach]
                .AddFrames(Enumerable.Range(0, 16).Select(i => textures.Get($"Marker/Approach/Approach_{i:D}")));
            animations[CubedMarkerAnimation.EarlyMiss]
                .AddFrames(Enumerable.Range(0, 16).Select(i => textures.Get($"Marker/EarlyMiss/EarlyMiss_{i:D}")));
            animations[CubedMarkerAnimation.Perfect]
                .AddFrames(Enumerable.Range(0, 16).Select(i => textures.Get($"Marker/Perfect/Perfect_{i:D}")));
            animations[CubedMarkerAnimation.Great]
                .AddFrames(Enumerable.Range(0, 16).Select(i => textures.Get($"Marker/Great/Great_{i:D}")));
            animations[CubedMarkerAnimation.Good].AddFrames(Enumerable.Range(0, 16).Select(i => textures.Get($"Marker/Good/Good_{i:D}")));
            animations[CubedMarkerAnimation.Ok].AddFrames(Enumerable.Range(0, 16).Select(i => textures.Get($"Marker/Ok/Ok_{i:D}")));
            animations[CubedMarkerAnimation.Disappear]
                .AddFrames(Enumerable.Range(0, 8).Select(i => textures.Get($"Marker/Disappear/Disappear_{i:D}")));
        }

        public void StopAllExcept(TextureAnimation animation) => InternalChildren.OfType<TextureAnimation>()
            .Where(a => !a.Equals(animation))
            .ForEach(a => a.FadeOutFromOne());

        public void PlayAnimation(CubedMarkerAnimation animation)
        {
            animations[animation].FadeInFromZero().OnComplete(a => a.Restart());
            StopAllExcept(animations[animation]);
        }
    }
}
