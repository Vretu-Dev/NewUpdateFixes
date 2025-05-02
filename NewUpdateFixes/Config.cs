﻿using System.ComponentModel;

namespace NewUpdateFixes
{
    public class Config
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        [Description("Whether or not debug messages should be shown in the console.")]
        public bool Debug { get; set; } = false;
        [Description("Revert SCP-207 speeds back to its pre 13.5 speeds.")]
        public bool OldColaSpeed { get; set; } = true;
        [Description("Revert SCP-207 health drain to its pre 13.5 values.")]
        public bool OldColaHealthDrain { get; set; } = true;
        [Description("If SCP-500 cures the traumatised effect caused by SCP-106 - suggested by follow The Owl.")]
        public bool Scp500CuresTrauma { get; set; } = false;
    }
}