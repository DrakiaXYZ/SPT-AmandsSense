using AmandsSense.Enums;
using AmandsSense.Helpers;
using AmandsSense.Models;
using EFT;
using EFT.InventoryLogic;
using SPT.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static EFT.Player;

namespace AmandsSense.Components
{
    public class AmandsSenseClass : MonoBehaviour
    {
        public static Player Player;
        public static PlayerInventoryController inventoryControllerClass;

        public static RaycastHit hit;
        public static LayerMask LowLayerMask;
        public static LayerMask HighLayerMask;
        public static LayerMask FoliageLayerMask;

        public static float CooldownTime = 0f;
        public static float AlwaysOnTime = 0f;
        public static float Radius = 0f;

        public static PrismEffects prismEffects;

        public static ItemsJsonClass itemsJsonClass;

        public static float lastDoubleClickTime = 0.0f;

        public static Dictionary<string, Sprite> LoadedSprites = new Dictionary<string, Sprite>();
        public static Dictionary<string, AudioClip> LoadedAudioClips = new Dictionary<string, AudioClip>();

        public static Vector3[] SenseOverlapLocations = new Vector3[9] { Vector3.zero, Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.forward + Vector3.left, Vector3.forward + Vector3.right, Vector3.back + Vector3.left, Vector3.back + Vector3.right };
        public static int CurrentOverlapLocation = 9;

        public static LayerMask BoxInteractiveLayerMask;
        public static LayerMask BoxDeadbodyLayerMask;
        public static int[] CurrentOverlapCount = new int[9];
        public static Collider[] CurrentOverlapLoctionColliders = new Collider[100];

        public static Dictionary<int, AmandsSenseWorld> SenseWorlds = new Dictionary<int, AmandsSenseWorld>();

        public static List<SenseDeadPlayer> DeadPlayers = new List<SenseDeadPlayer>();

        public static List<AmandsSenseExfil> SenseExfils = new List<AmandsSenseExfil>();
        public static AmandsSenseExfil ClosestAmandsSenseExfil = null;

        public static Transform parent;

        public static string scene;

        private AudioSource SenseAudioSource { get; set; }

        public void Initialize(AudioSource senseAudioSource)
        {
            this.SenseAudioSource = senseAudioSource;
        }

        public void OnGUI()
        {
            /*GUILayout.BeginArea(new Rect(20, 10, 1280, 720));
            GUILayout.Label("SenseWorlds " + SenseWorlds.Count().ToString());
            GUILayout.EndArea();*/
        }
        public void Awake()
        {
            LowLayerMask = LayerMask.GetMask("Terrain", "LowPolyCollider", "HitCollider");
            HighLayerMask = LayerMask.GetMask("Terrain", "HighPolyCollider", "HitCollider");
            FoliageLayerMask = LayerMask.GetMask("Terrain", "HighPolyCollider", "HitCollider", "Foliage");

            BoxInteractiveLayerMask = LayerMask.GetMask("Interactive");
            BoxDeadbodyLayerMask = LayerMask.GetMask("Deadbody");
        }
        public void Start()
        {
            string itemsJsonPath = Path.Combine(AmandsSensePlugin.PluginFolder, "Items.json");
            itemsJsonClass = ReadFromJsonFile<ItemsJsonClass>(itemsJsonPath);
            ReloadFiles(false);
        }

        public void Update()
        {
            if (gameObject != null && Player != null && Settings.Enabled.Value != EnableSense.Off)
            {
                if (CurrentOverlapLocation <= 8)
                {
                    int CurrentOverlapCountTest = Physics.OverlapBoxNonAlloc(Player.Position + SenseOverlapLocations[CurrentOverlapLocation] * (Settings.Radius.Value * 2f / 3f), Vector3.one * (Settings.Radius.Value * 2f / 3f), CurrentOverlapLoctionColliders, Quaternion.Euler(0f, 0f, 0f), BoxInteractiveLayerMask, QueryTriggerInteraction.Collide);
                    for (int i = 0; i < CurrentOverlapCountTest; i++)
                    {
                        if (!SenseWorlds.ContainsKey(CurrentOverlapLoctionColliders[i].GetInstanceID()))
                        {
                            GameObject SenseWorldGameObject = new GameObject("SenseWorld");
                            AmandsSenseWorld amandsSenseWorld = SenseWorldGameObject.AddComponent<AmandsSenseWorld>();
                            amandsSenseWorld.OwnerCollider = CurrentOverlapLoctionColliders[i];
                            amandsSenseWorld.OwnerGameObject = amandsSenseWorld.OwnerCollider.gameObject;
                            amandsSenseWorld.Id = amandsSenseWorld.OwnerCollider.GetInstanceID();
                            amandsSenseWorld.Delay = Vector3.Distance(Player.Position, amandsSenseWorld.OwnerCollider.transform.position) / Settings.Speed.Value;
                            SenseWorlds.Add(amandsSenseWorld.Id, amandsSenseWorld);
                        }
                        else
                        {
                            SenseWorlds[CurrentOverlapLoctionColliders[i].GetInstanceID()].RestartSense();
                        }
                    }
                    CurrentOverlapLocation++;
                }
                else if (Settings.SenseAlwaysOn.Value)
                {
                    AlwaysOnTime += Time.deltaTime;
                    if (AlwaysOnTime > Settings.AlwaysOnFrequency.Value)
                    {
                        AlwaysOnTime = 0f;
                        CurrentOverlapLocation = 0;
                        SenseDeadBodies();
                    }
                }
                if (CooldownTime < Settings.Cooldown.Value)
                {
                    CooldownTime += Time.deltaTime;
                }
                if (Input.GetKeyDown(Settings.SenseKey.Value.MainKey))
                {
                    if (Settings.DoubleClick.Value)
                    {
                        float timeSinceLastClick = Time.time - lastDoubleClickTime;
                        lastDoubleClickTime = Time.time;
                        if (timeSinceLastClick <= 0.5f && CooldownTime >= Settings.Cooldown.Value)
                        {
                            CooldownTime = 0f;
                            CurrentOverlapLocation = 0;
                            SenseDeadBodies();
                            ShowSenseExfils();
                            if (prismEffects != null)
                            {
                                Radius = 0;
                                prismEffects.useDof = Settings.useDof.Value;
                            }
                            if (LoadedAudioClips.ContainsKey("Sense.wav"))
                            {
                                SenseAudioSource.PlayOneShot(LoadedAudioClips["Sense.wav"], Settings.ActivateSenseVolume.Value);
                            }
                        }
                    }
                    else
                    {
                        if (CooldownTime >= Settings.Cooldown.Value)
                        {
                            CooldownTime = 0f;
                            CurrentOverlapLocation = 0;
                            SenseDeadBodies();
                            ShowSenseExfils();
                            if (prismEffects != null)
                            {
                                Radius = 0;
                                prismEffects.useDof = Settings.useDof.Value;
                            }
                            if (LoadedAudioClips.ContainsKey("Sense.wav"))
                            {
                                SenseAudioSource.PlayOneShot(LoadedAudioClips["Sense.wav"], Settings.ActivateSenseVolume.Value);
                            }
                        }
                    }
                }
                if (Radius < Mathf.Max(Settings.Radius.Value, Settings.DeadPlayerRadius.Value))
                {
                    Radius += Settings.Speed.Value * Time.deltaTime;
                    if (prismEffects != null)
                    {
                        prismEffects.dofFocusPoint = Radius - prismEffects.dofFocusDistance;
                        if (prismEffects.dofRadius < 0.5f)
                        {
                            prismEffects.dofRadius += 2f * Time.deltaTime;
                        }
                    }
                }
                else if (prismEffects != null && prismEffects.dofRadius > 0.001f)
                {
                    prismEffects.dofRadius -= 0.5f * Time.deltaTime;
                    if (prismEffects.dofRadius < 0.001f)
                    {
                        prismEffects.useDof = false;
                    }
                }
            }
        }
        public void SenseDeadBodies()
        {
            if (!Settings.EnableBodies.Value) return;

            foreach (SenseDeadPlayer deadPlayer in DeadPlayers)
            {
                if (Vector3.Distance(Player.Position, deadPlayer.victim.Position) < Settings.DeadPlayerRadius.Value)
                {
                    if (!SenseWorlds.ContainsKey(deadPlayer.victim.GetInstanceID()))
                    {
                        GameObject SenseWorldGameObject = new GameObject("SenseWorld");
                        AmandsSenseWorld amandsSenseWorld = SenseWorldGameObject.AddComponent<AmandsSenseWorld>();
                        amandsSenseWorld.OwnerGameObject = deadPlayer.victim.gameObject;
                        amandsSenseWorld.Id = deadPlayer.victim.GetInstanceID();
                        amandsSenseWorld.Delay = Vector3.Distance(Player.Position, deadPlayer.victim.Position) / Settings.Speed.Value;
                        amandsSenseWorld.Lazy = false;
                        amandsSenseWorld.SenseWorldType = SenseWorldType.Deadbody;
                        amandsSenseWorld.SenseDeadPlayer = deadPlayer.victim as LocalPlayer;
                        SenseWorlds.Add(amandsSenseWorld.Id, amandsSenseWorld);
                    }
                    else
                    {
                        SenseWorlds[deadPlayer.victim.GetInstanceID()].RestartSense();
                    }
                }
            }
        }
        public void ShowSenseExfils()
        {
            if (!Settings.EnableExfilSense.Value) return;

            if (scene == "Factory_Day" || scene == "Factory_Night" || scene == "Laboratory_Scripts") return;

            float ClosestDistance = 10000000000f;
            if (ClosestAmandsSenseExfil != null && ClosestAmandsSenseExfil.light != null) ClosestAmandsSenseExfil.light.shadows = LightShadows.None;
            foreach (AmandsSenseExfil senseExfil in SenseExfils)
            {
                if (Player != null && Vector3.Distance(senseExfil.transform.position,Player.gameObject.transform.position) < ClosestDistance)
                {
                    ClosestAmandsSenseExfil = senseExfil;
                    ClosestDistance = Vector3.Distance(senseExfil.transform.position, Player.gameObject.transform.position);
                }

                if (senseExfil.Intensity > 0.5f)
                {
                    senseExfil.LifeSpan = 0f;
                    senseExfil.UpdateSense();
                }
                else
                {
                    senseExfil.ShowSense();
                }
            }
            if (Settings.ExfilLightShadows.Value && ClosestAmandsSenseExfil != null && ClosestAmandsSenseExfil.light != null) ClosestAmandsSenseExfil.light.shadows = LightShadows.Hard;
        }
        public static void Clear()
        {
            foreach (KeyValuePair<int,AmandsSenseWorld> keyValuePair in SenseWorlds)
            {
                if (keyValuePair.Value != null) keyValuePair.Value.RemoveSense();
            }
            SenseWorlds.Clear();

            ClosestAmandsSenseExfil = null;
            SenseExfils = SenseExfils.Where(x => x != null).ToList();

            DeadPlayers.Clear();
        }
        public static SenseItemType GetSenseItemType(Type itemType)
        {
            if (TemplateIdToObjectMappingsClass.TypeTable["57864ada245977548638de91"].IsAssignableFrom(itemType))
            {
                return SenseItemType.BuildingMaterials;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["57864a66245977548f04a81f"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Electronics;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["57864ee62459775490116fc1"].IsAssignableFrom(itemType))
            {
                return SenseItemType.EnergyElements;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["57864e4c24597754843f8723"].IsAssignableFrom(itemType))
            {
                return SenseItemType.FlammableMaterials;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["57864c322459775490116fbf"].IsAssignableFrom(itemType))
            {
                return SenseItemType.HouseholdMaterials;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["57864c8c245977548867e7f1"].IsAssignableFrom(itemType))
            {
                return SenseItemType.MedicalSupplies;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["57864bb7245977548b3b66c2"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Tools;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["57864a3d24597754843f8721"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Valuables;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["590c745b86f7743cc433c5f2"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Others;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448e53e4bdc2d60728b4567"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Backpacks;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448e54d4bdc2dcc718b4568"].IsAssignableFrom(itemType))
            {
                return SenseItemType.BodyArmor;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448e5724bdc2ddf718b4568"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Eyewear;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5a341c4686f77469e155819e"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Facecovers;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5a341c4086f77401f2541505"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Headgear;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["57bef4c42459772e8d35a53b"].IsAssignableFrom(itemType))
            {
                return SenseItemType.GearComponents;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5b3f15d486f77432d0509248"].IsAssignableFrom(itemType))
            {
                return SenseItemType.GearComponents;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5645bcb74bdc2ded0b8b4578"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Headsets;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448bf274bdc2dfc2f8b456a"].IsAssignableFrom(itemType))
            {
                return SenseItemType.SecureContainers;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5795f317245977243854e041"].IsAssignableFrom(itemType))
            {
                return SenseItemType.StorageContainers;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448e5284bdc2dcb718b4567"].IsAssignableFrom(itemType))
            {
                return SenseItemType.TacticalRigs;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["550aa4154bdc2dd8348b456b"].IsAssignableFrom(itemType))
            {
                return SenseItemType.FunctionalMods;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["55802f3e4bdc2de7118b4584"].IsAssignableFrom(itemType))
            {
                return SenseItemType.GearMods;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5a74651486f7744e73386dd1"].IsAssignableFrom(itemType))
            {
                return SenseItemType.GearMods;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["55802f4a4bdc2ddb688b4569"].IsAssignableFrom(itemType))
            {
                return SenseItemType.VitalParts;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5447e1d04bdc2dff2f8b4567"].IsAssignableFrom(itemType))
            {
                return SenseItemType.MeleeWeapons;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["543be6564bdc2df4348b4568"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Throwables;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["543be5cb4bdc2deb348b4568"].IsAssignableFrom(itemType))
            {
                return SenseItemType.AmmoPacks;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5485a8684bdc2da71d8b4567"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Rounds;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448e8d64bdc2dce718b4568"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Drinks;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448e8d04bdc2ddf718b4569"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Food;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448f3a64bdc2d60728b456a"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Injectors;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448f3ac4bdc2dce718b4569"].IsAssignableFrom(itemType))
            {
                return SenseItemType.InjuryTreatment;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448f39d4bdc2d0a728b4568"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Medkits;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448f3a14bdc2d27728b4569"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Pills;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5c164d2286f774194c5e69fa"].IsAssignableFrom(itemType))
            {
                return SenseItemType.ElectronicKeys;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5c99f98d86f7745c314214b3"].IsAssignableFrom(itemType))
            {
                return SenseItemType.MechanicalKeys;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5448ecbe4bdc2d60728b4568"].IsAssignableFrom(itemType))
            {
                return SenseItemType.InfoItems;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5447e0e74bdc2d3c308b4567"].IsAssignableFrom(itemType))
            {
                return SenseItemType.SpecialEquipment;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["616eb7aea207f41933308f46"].IsAssignableFrom(itemType))
            {
                return SenseItemType.SpecialEquipment;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["61605ddea09d851a0a0c1bbc"].IsAssignableFrom(itemType))
            {
                return SenseItemType.SpecialEquipment;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["5f4fbaaca5573a5ac31db429"].IsAssignableFrom(itemType))
            {
                return SenseItemType.SpecialEquipment;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["567849dd4bdc2d150f8b456e"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Maps;
            }
            if (TemplateIdToObjectMappingsClass.TypeTable["543be5dd4bdc2deb348b4569"].IsAssignableFrom(itemType))
            {
                return SenseItemType.Money;
            }
            return SenseItemType.All;
        }
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = Json.Serialize(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return Json.Deserialize<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        public static void ReloadFiles(bool onlySounds)
        {
            if (!onlySounds)
            {
                string imagesPath = Path.Combine(AmandsSensePlugin.PluginFolder, "images");
                if (Directory.Exists(imagesPath))
                {
                    string[] Files = Directory.GetFiles(imagesPath, "*.png");
                    foreach (string File in Files)
                    {
                        LoadSprite(File);
                    }
                }
            }

            string soundsPath = Path.Combine(AmandsSensePlugin.PluginFolder, "sounds");
            if (Directory.Exists(soundsPath))
            {
                string[] AudioFiles = Directory.GetFiles(soundsPath);
                foreach (string File in AudioFiles)
                {
                    LoadAudioClip(File);
                }
            }
        }
        async static void LoadSprite(string path)
        {
            LoadedSprites[Path.GetFileName(path)] = await RequestSprite(path);
        }
        async static Task<Sprite> RequestSprite(string path)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);
            var SendWeb = www.SendWebRequest();

            while (!SendWeb.isDone)
                await Task.Yield();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                return null;
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                return sprite;
            }
        }
        async static void LoadAudioClip(string path)
        {
            LoadedAudioClips[Path.GetFileName(path)] = await RequestAudioClip(path);
        }
        async static Task<AudioClip> RequestAudioClip(string path)
        {
            string extension = Path.GetExtension(path);
            AudioType audioType = AudioType.WAV;
            switch (extension)
            {
                case ".wav":
                    audioType = AudioType.WAV;
                    break;
                case ".ogg":
                    audioType = AudioType.OGGVORBIS;
                    break;
            }
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, audioType);
            var SendWeb = www.SendWebRequest();

            while (!SendWeb.isDone)
                await Task.Yield();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                return null;
            }
            else
            {
                AudioClip audioclip = DownloadHandlerAudioClip.GetContent(www);
                return audioclip;
            }
        }
    }
}
