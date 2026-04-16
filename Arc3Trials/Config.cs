namespace Arc3Trials
{
    public class Config
    {
        public bool IsEnabled { get; set; } = true;
        public float EffectDuration { get; set; } = 10f;
        public float CooldownDuration { get; set; } = 30f;
        public float HpThreshold { get; set; } = 0.25f;
        public float BlurredDuration { get; set; } = 20f;
    }
}
