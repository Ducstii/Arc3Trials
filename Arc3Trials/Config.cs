using System.ComponentModel;

namespace Arc3Trials
{
    public class Config
    {
        [Description("If the plugin is enabled or not")]
        public bool IsEnabled { get; set; } = true;
        [Description("The effect duration for the adrenaline boost")]
        public float EffectDuration { get; set; } = 10f;
        [Description("The cooldown for the adrenaline boost")]
        public float CooldownDuration { get; set; } = 30f;
        [Description("The threshold percentage for the adrenaline boost")]
        public float HpThreshold { get; set; } = 0.25f;
        [Description("How long the player should recieve blurred for afterwards")]
        public float BlurredDuration { get; set; } = 20f;
    }
}
