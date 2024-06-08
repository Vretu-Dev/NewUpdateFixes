using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NewUpdateFixes
{
    public class Config : IConfig
    {

        [Description("Whether or not this plugin is enabled. ")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not debug messages should be shown in the console.")]
        public bool Debug { get; set; } = false;

        [Description("revert scp207 speeds back to its pre 13.5 speeds")]
        public bool OldColaSpeed { get; set; } = true;

        [Description("revert scp207 health drain to its pre 13.5 values")]
        public bool OldColaHealthDrain { get; set; } = true;

        [Description("If SCP500 cures the traumatised affect given by 106. - suggested by follow the owl")]
        public bool Scp500CuresTrauma { get; set; } = false;

    }
}
