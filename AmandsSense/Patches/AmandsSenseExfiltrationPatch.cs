using AmandsSense.Components;
using EFT.Interactive;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;

namespace AmandsSense.Patches
{
    public class AmandsSenseExfiltrationPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(ExfiltrationPoint).GetMethod("Awake", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        public static void PatchPostFix(ref ExfiltrationPoint __instance)
        {
            GameObject amandsSenseExfiltrationGameObject = new GameObject("SenseExfil");
            AmandsSenseExfil amandsSenseExfil = amandsSenseExfiltrationGameObject.AddComponent<AmandsSenseExfil>();
            amandsSenseExfil.SetSense(__instance);
            amandsSenseExfil.Construct();
            amandsSenseExfil.ShowSense();
            AmandsSenseClass.SenseExfils.Add(amandsSenseExfil);
        }
    }
}
