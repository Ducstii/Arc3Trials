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
            public void Harrass(int duration)
            {
                player.EnableEffect<Concussed>(255, 10, true);
                player.EnableEffect<Slowness>(255, 10, true);
                Timing.RunCoroutine(FlashbangCoroutine(player, duration));
            }
        }

        private static IEnumerator<float> FlashbangCoroutine(Player player, int times)
        {
            for (int i = 0; i < times; i++)
            {
                player.EnableEffect<Blindness>(255, 0.1f);
                yield return Timing.WaitForSeconds(0.1f);
                player.DisableEffect<Blindness>();
                yield return Timing.WaitForSeconds(0.1f);
            }
        }
    }
}