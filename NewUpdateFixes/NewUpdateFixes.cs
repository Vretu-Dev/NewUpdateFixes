using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace NewUpdateFixes
{
    public class NewUpdateFixes : Plugin<Config>
    {
        public override string Name => "NewUpdateFixes";
        public override string Description => "Display extra hint like a timers and notifications.";
        public override string Author => "Vretu";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);
        public static NewUpdateFixes Instance { get; private set; }
        public override void Enable()
        {
            Instance = this;
            EventHandlers.ColaHandler.RegisterEvents();
        }
        public override void Disable()
        {
            Instance = null;
            EventHandlers.ColaHandler.UnregisterEvents();
        }
    }
}