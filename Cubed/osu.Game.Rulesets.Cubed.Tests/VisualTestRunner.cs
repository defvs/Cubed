﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework;
using osu.Game.Tests;

namespace osu.Game.Rulesets.Cubed.Tests;

public static class VisualTestRunner
{
    [STAThread]
    public static int Main(string[] args)
    {
        using (var host = Host.GetSuitableDesktopHost(@"osu", new HostOptions { BindIPC = true }))
        {
            host.Run(new OsuTestBrowser());
            return 0;
        }
    }
}
