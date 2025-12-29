using AmandsSense.Components;
using AmandsSense.Models;
using EFT;
using SPT.Reflection.Patching;
using System.Reflection;

namespace AmandsSense.Patches
{
    public class AmandsKillPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player).GetMethod("OnBeenKilledByAggressor", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        public static void PatchPostFix(ref Player __instance, Player aggressor, DamageInfoStruct damageInfo, EBodyPart bodyPart, EDamageType lethalDamageType)
        {
            AmandsSenseClass.DeadPlayers.Add(new SenseDeadPlayer(__instance, aggressor));
        }
    }
}
