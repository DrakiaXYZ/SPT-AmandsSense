using AmandsSense.Components;
using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine.SceneManagement;
using static EFT.Player;

namespace AmandsSense.Patches
{
    public class AmandsPlayerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player), nameof(Player.Init));
        }

        [PatchPostfix]
        public static void PatchPostFix(ref Player __instance)
        {
            if (__instance != null && __instance.IsYourPlayer)
            {
                AmandsSenseClass.Player = __instance;
                AmandsSenseClass.inventoryControllerClass = __instance.InventoryController as PlayerInventoryController;
                AmandsSenseClass.Clear();
                AmandsSenseClass.scene = SceneManager.GetActiveScene().name;
                AmandsSenseClass.ReloadFiles(true);
            }
        }
    }
}
