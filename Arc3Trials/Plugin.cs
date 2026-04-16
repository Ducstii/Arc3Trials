using System;
using Arc3Trials.Adreniline;
using Arc3Trials.Events;
using LabApi.Loader.Features.Plugins;

namespace Arc3Trials
{
    public class Arc3Plugin : Plugin<Config>
    {
        public static Arc3Plugin Instance { get; private set; }  
        public static Config AConfig { get; private set; }
        public override string Author => "Ducstii";
        public override string Name => "Arc3Trials";
        public override string Description => "Arc 3 trial developer plugin";
        public override Version RequiredApiVersion =>  new Version(1, 0);
        public override Version Version => new Version(1, 0,0);

        public override void Enable()
        {
            Instance = this;
            AConfig = Config;
            if (Config.IsEnabled)
            {
                NameHandler.NameHandler.LoadNames();
                EventRegistrar.Register();
            }
            
        }

        public override void Disable()
        {
            AdrenalineManager.ClearCoroutines();
            EventRegistrar.Unregister();
            Instance = null;
        }
    }
}