using System.Collections.Generic;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using MEC;


namespace Arc3Trials.Adreniline
{
    public enum AdrenalineState
    {
        Ready,
        Active,
        Cooldown
    }

    public static class AdrenalineManager
    {
        private static readonly Dictionary<string, AdrenalineState> PlayerStates = new();

        private static readonly Dictionary<string, CoroutineHandle> Coroutines = new();

        public static void CheckAdrenaline(Player player)
        {
            if (player.MaxHealth <= 0)
                return;
            

            if (player.Health / player.MaxHealth >= Arc3Plugin.AConfig.HpThreshold)
                return;

            if (GetState(player.UserId) != AdrenalineState.Ready)
                return;

            Activate(player);
        }

        public static AdrenalineState GetState(string userId)
        {
            
            return PlayerStates.TryGetValue(userId, out AdrenalineState state) ? state : AdrenalineState.Ready;
        }

        public static void ResetPlayer(string userId)
        {
            if (Coroutines.TryGetValue(userId, out CoroutineHandle handle))
            {
                Timing.KillCoroutines(handle);
                Coroutines.Remove(userId);
            }

            PlayerStates.Remove(userId);
        }

        private static void Activate(Player player)
        {
            string id = player.UserId;
            PlayerStates[id] = AdrenalineState.Active;

            player.EnableEffect<MovementBoost>(intensity: 20, duration: Arc3Plugin.AConfig.EffectDuration);
            player.EnableEffect<Invigorated>(intensity: 1, duration: Arc3Plugin.AConfig.EffectDuration);
            player.EnableEffect<Blurred>(intensity: 25, duration: Arc3Plugin.AConfig.BlurredDuration);

            CoroutineHandle handle = Timing.RunCoroutine(AdrenalineCoroutine(id));
            Coroutines[id] = handle; 
        }

        private static IEnumerator<float> AdrenalineCoroutine(string userId)
        {
            
            yield return Timing.WaitForSeconds(Arc3Plugin.AConfig.EffectDuration);

            PlayerStates[userId] = AdrenalineState.Cooldown;

            
            yield return Timing.WaitForSeconds(Arc3Plugin.AConfig.CooldownDuration);

            PlayerStates.Remove(userId);
            Coroutines.Remove(userId);
        }

        public static void ClearCoroutines()
        {
            foreach (CoroutineHandle handle in Coroutines.Values)
            {
                Timing.KillCoroutines(handle);
            }
            Coroutines.Clear();
            PlayerStates.Clear();
        }

    }
}
