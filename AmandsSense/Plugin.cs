using AmandsSense.Components;
using AmandsSense.Helpers;
using BepInEx;
using System.IO;
using System.Reflection;
using AmandsSense.Patches;

namespace AmandsSense
{
    [BepInPlugin("com.Amanda.Sense", "Sense", "3.0.0")]
    public class AmandsSensePlugin : BaseUnityPlugin
    {
        public static string PluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static AmandsSenseClass AmandsSenseClassComponent;

        public void Start()
        {
            Settings.Init(Config, Info);

            new GameStartedPatch().Enable();
            new AmandsPlayerPatch().Enable();
            new AmandsKillPatch().Enable();
            new AmandsSenseExfiltrationPatch().Enable();
            new AmandsSensePrismEffectsPatch().Enable();

            AmandsSenseHelper.Init();
        }
    }
}
