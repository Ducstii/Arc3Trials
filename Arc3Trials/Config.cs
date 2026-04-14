namespace Arc3Trials
{
    public class Config
    {
        public static bool IsEnabled { get; set; } = true;
        public static float EffectDuration { get; set; } = 10f;
        public static float CooldownDuration { get; set; } = 30f;
        public static float HpThreshold { get; set; } = 0.25f;
        public static float BlurredDuration { get; set; } = 20f;
    }
}
