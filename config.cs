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
        /// <summary>
        /// Gets or sets a value indicating whether the plugin is enabled or not.
        /// </summary>
        [Description("Whether or not this plugin is enabled. ")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debug messages should be displayed in the console or not.
        /// </summary>
        [Description("Whether or not debug messages should be shown in the console.")]
        public bool Debug { get; set; } = false;

        /// <summary>
        /// If SCP500 cures the traumatised affect given by 106.
        /// </summary>
        [Description("If SCP500 cures the traumatised affect given by 106. - suggested by follow the owl")]
        public bool Scp500CuresTrauma { get; set; } = true;

        /// <summary>
        /// revert scp207 back to its pre 13.5 speeds
        /// </summary>
        [Description("revert scp207 back to its pre 13.5 speeds")]
        public bool OldColaSpeed { get; set; } = true;

        /// <summary>
        /// If pre 13.5 recipes to get cola should still work in 914 - UPGRADES ITEMS IN INVENTORY
        /// </summary>
        //[Description("If pre 13.5 recipes to get cola should still work in 914 - UPGRADES ITEMS IN INVENTORY")]
        //public bool OldColaRecipes914Inv { get; set; } 

        /// <summary>
        /// "If pre 13.5 recipes to get cola should still work in 914 - UPGRADES DROPPED ITEMS
        /// </summary>
        [Description("If pre 13.5 recipes to get cola should still work in 914 - UPGRADES DROPPED ITEMS")]
        public bool OldColaRecipes914Dropped { get; set; } = true;

        /// <summary>
        /// If pre 13.5 recipes to get cola should still work in 914 - UPGRADES ITEM ONLY IN HAND
        /// </summary>
        [Description("If pre 13.5 recipes to get cola should still work in 914 - UPGRADES ITEM ONLY IN HAND")]
        public bool OldColaRecipes914Hand { get; set; } = true;
    }
}
