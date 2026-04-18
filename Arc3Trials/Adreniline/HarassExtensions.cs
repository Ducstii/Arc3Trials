using System.Collections.Generic;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using MEC;

namespace Arc3Trials.Adreniline
{
    public static class HarassExtensions
    {
        extension(Player player)
        {
            // applies concussed and slowness then starts the flashbang coroutine for however many flashes were passed in
            public void Harrass(int duration)
            {
                player.EnableEffect<Concussed>(255, 10, true);
                player.EnableEffect<Slowness>(255, 10, true);
                Timing.RunCoroutine(FlashbangCoroutine(player, duration));
            }
        }

        // flashes the player by toggling the flashed effect on and off, repeats for however many times were passed in
        private static IEnumerator<float> FlashbangCoroutine(Player player, int times)
        {
            for (int i = 0; i < times; i++)
            {
                player.EnableEffect<Flashed>(1, 0.1f);
                yield return Timing.WaitForSeconds(0.1f);
                player.DisableEffect<Flashed>();
                yield return Timing.WaitForSeconds(0.1f);
            }
        }
    }
}