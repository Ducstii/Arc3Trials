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
        private static readonly Dictionary<int, AdrenalineState> PlayerStates = new();

        private static readonly Dictionary<int, CoroutineHandle> Coroutines = new();

        public static void CheckAdrenaline(Player player)
        {
            if (player.MaxHealth <= 0)
                return;


            if (player.Health / player.MaxHealth >= Arc3Plugin.AConfig.HpThreshold)
                return;

            if (GetState(player.PlayerId) != AdrenalineState.Ready)
                return;

            Activate(player);
        }

        public static AdrenalineState GetState(int playerId)
        {

            return PlayerStates.TryGetValue(playerId, out AdrenalineState state) ? state : AdrenalineState.Ready;
        }

        public static void ResetPlayer(int playerId)
        {
            if (Coroutines.TryGetValue(playerId, out CoroutineHandle handle))
            {
                Timing.KillCoroutines(handle);
                Coroutines.Remove(playerId);
            }

            PlayerStates.Remove(playerId);
        }

        private static void Activate(Player player)
        {
            int id = player.PlayerId;
            PlayerStates[id] = AdrenalineState.Active;

            player.EnableEffect<MovementBoost>(intensity: 20, duration: Arc3Plugin.AConfig.EffectDuration);
            player.EnableEffect<Invigorated>(intensity: 1, duration: Arc3Plugin.AConfig.EffectDuration);
            player.EnableEffect<Blurred>(intensity: 25, duration: Arc3Plugin.AConfig.BlurredDuration);

            CoroutineHandle handle = Timing.RunCoroutine(AdrenalineCoroutine(id));
            Coroutines[id] = handle;
        }

        private static IEnumerator<float> AdrenalineCoroutine(int playerId)
        {
            
            yield return Timing.WaitForSeconds(Arc3Plugin.AConfig.EffectDuration);

            PlayerStates[playerId] = AdrenalineState.Cooldown;

            yield return Timing.WaitForSeconds(Arc3Plugin.AConfig.CooldownDuration);

            PlayerStates.Remove(playerId);
            Coroutines.Remove(playerId);
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
