using System.Collections.Generic;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using MEC;


namespace Arc3Trials.Adreniline
{
    // enum for the 3 states a player can be in for adrenaline
    public enum AdrenalineState
    {
        Ready,
        Active,
        Cooldown
    }

    public static class AdrenalineManager
    {
        // tracks each players adrenaline state by player id
        private static readonly Dictionary<int, AdrenalineState> PlayerStates = new();

        // tracks the running coroutine handle for each player so we can kill it if needed
        private static readonly Dictionary<int, CoroutineHandle> Coroutines = new();

        public static void CheckAdrenaline(Player player)
        {
            // maxhealth shouldnt be 0 but just in case
            if (player.MaxHealth <= 0)
                return;

            // if the player is above the hp threshold they dont get adrenaline
            if (player.Health / player.MaxHealth >= Arc3Plugin.AConfig.HpThreshold)
                return;

            // only trigger if they are in the ready state
            if (GetState(player.PlayerId) != AdrenalineState.Ready)
                return;

            Activate(player);
        }

        // looks up the players state, returns ready if they arent in the dict
        public static AdrenalineState GetState(int playerId)
        {
            return PlayerStates.TryGetValue(playerId, out AdrenalineState state) ? state : AdrenalineState.Ready;
        }

        // kills the coroutine and clears the player from both dicts
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
            // mark them as active before applying effects
            PlayerStates[id] = AdrenalineState.Active;

            // apply the 3 effects
            player.EnableEffect<MovementBoost>(intensity: 20, duration: Arc3Plugin.AConfig.EffectDuration);
            player.EnableEffect<Invigorated>(intensity: 1, duration: Arc3Plugin.AConfig.EffectDuration);
            player.EnableEffect<Blurred>(intensity: 25, duration: Arc3Plugin.AConfig.BlurredDuration);

            // start the coroutine to handle the state
            CoroutineHandle handle = Timing.RunCoroutine(AdrenalineCoroutine(id));
            Coroutines[id] = handle;
        }

        private static IEnumerator<float> AdrenalineCoroutine(int playerId)
        {
            // wait for the effect to finish then move to cooldown
            yield return Timing.WaitForSeconds(Arc3Plugin.AConfig.EffectDuration);

            PlayerStates[playerId] = AdrenalineState.Cooldown;

            // wait out the cooldown then remove the player from both dicts so they are ready again
            yield return Timing.WaitForSeconds(Arc3Plugin.AConfig.CooldownDuration);

            PlayerStates.Remove(playerId);
            Coroutines.Remove(playerId);
        }

        // kills all running coroutines and wipes both dicts
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