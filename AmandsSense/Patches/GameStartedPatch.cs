using AmandsSense.Components;
using EFT;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;

namespace AmandsSense.Patches
{
    public class GameStartedPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
        }

        [PatchPostfix]
        public static void PatchPostfix(GameWorld __instance)
        {
            var senseClass = __instance.gameObject.AddComponent<AmandsSenseClass>();
            var senseAudioSource = __instance.gameObject.AddComponent<AudioSource>();
            senseClass.Initialize(senseAudioSource);
        }
    }
}
