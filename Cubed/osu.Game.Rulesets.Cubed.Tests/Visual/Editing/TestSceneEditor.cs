using NUnit.Framework;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Cubed.Tests.Visual.Editing
{
    [TestFixture]
    public class TestSceneEditor : EditorTestScene
    {
        public TestSceneEditor() : base() {}

        protected override Ruleset CreateEditorRuleset() => new CubedRuleset();
    }
}
