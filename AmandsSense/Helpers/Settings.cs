using AmandsSense.Enums;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace AmandsSense.Helpers
{
    internal class Settings
    {
        private static string senseSectionTitle = "AmandsSense";
        private static string effectsSectionTitle = "AmandsSense Effects";
        private static string colorsSectionTitle = "Colors";

        public static ConfigEntry<EnableSense> Enabled { get; set; }
        public static ConfigEntry<bool> EnableExfilSense { get; set; }
        public static ConfigEntry<bool> SenseAlwaysOn { get; set; }

        public static ConfigEntry<bool> EnableContainers { get; set; }
        public static ConfigEntry<bool> EnableBodies { get; set; }
        public static ConfigEntry<bool> EnableLooseLoot { get; set; }

        public static ConfigEntry<KeyboardShortcut> SenseKey { get; set; }
        public static ConfigEntry<bool> DoubleClick { get; set; }
        public static ConfigEntry<float> Cooldown { get; set; }

        public static ConfigEntry<float> Duration { get; set; }
        public static ConfigEntry<float> ExfilDuration { get; set; }
        public static ConfigEntry<int> Radius { get; set; }
        public static ConfigEntry<int> DeadPlayerRadius { get; set; }
        public static ConfigEntry<float> Speed { get; set; }
        public static ConfigEntry<float> MaxHeight { get; set; }
        public static ConfigEntry<float> MinHeight { get; set; }

        public static ConfigEntry<bool> ContainerLootcount { get; set; }
        public static ConfigEntry<bool> EnableFlea { get; set; }
        public static ConfigEntry<bool> FleaIncludeAmmo { get; set; }

        public static ConfigEntry<bool> UseBackgroundColor { get; set; }

        public static ConfigEntry<float> Size { get; set; }
        public static ConfigEntry<float> IconSize { get; set; }
        public static ConfigEntry<float> SizeClamp { get; set; }

        public static ConfigEntry<float> VerticalOffset { get; set; }
        public static ConfigEntry<float> TextOffset { get; set; }
        public static ConfigEntry<float> ExfilVerticalOffset { get; set; }

        public static ConfigEntry<float> IntensitySpeed { get; set; }

        public static ConfigEntry<float> AlwaysOnFrequency { get; set; }

        public static ConfigEntry<float> LightIntensity { get; set; }
        public static ConfigEntry<float> LightRange { get; set; }
        public static ConfigEntry<bool> LightShadows { get; set; }

        public static ConfigEntry<float> ExfilLightIntensity { get; set; }
        public static ConfigEntry<float> ExfilLightRange { get; set; }
        public static ConfigEntry<bool> ExfilLightShadows { get; set; }

        public static ConfigEntry<float> AudioDistance { get; set; }
        public static ConfigEntry<int> AudioRolloff { get; set; }
        public static ConfigEntry<float> AudioVolume { get; set; }
        public static ConfigEntry<float> ContainerAudioVolume { get; set; }
        public static ConfigEntry<float> ActivateSenseVolume { get; set; }
        public static ConfigEntry<bool> SenseRareSound { get; set; }

        public static ConfigEntry<bool> useDof { get; set; }

        public static ConfigEntry<Color> ExfilColor { get; set; }
        public static ConfigEntry<Color> ExfilUnmetColor { get; set; }
        public static ConfigEntry<Color> TextColor { get; set; }
        public static ConfigEntry<Color> RareItemsColor { get; set; }
        public static ConfigEntry<Color> WishListItemsColor { get; set; }
        public static ConfigEntry<Color> NonFleaItemsColor { get; set; }
        public static ConfigEntry<Color> KappaItemsColor { get; set; }
        public static ConfigEntry<Color> LootableContainerColor { get; set; }
        public static ConfigEntry<Color> ObservedLootItemColor { get; set; }
        public static ConfigEntry<Color> OthersColor { get; set; }
        public static ConfigEntry<Color> BuildingMaterialsColor { get; set; }
        public static ConfigEntry<Color> ElectronicsColor { get; set; }
        public static ConfigEntry<Color> EnergyElementsColor { get; set; }
        public static ConfigEntry<Color> FlammableMaterialsColor { get; set; }
        public static ConfigEntry<Color> HouseholdMaterialsColor { get; set; }
        public static ConfigEntry<Color> MedicalSuppliesColor { get; set; }
        public static ConfigEntry<Color> ToolsColor { get; set; }
        public static ConfigEntry<Color> ValuablesColor { get; set; }
        public static ConfigEntry<Color> BackpacksColor { get; set; }
        public static ConfigEntry<Color> BodyArmorColor { get; set; }
        public static ConfigEntry<Color> EyewearColor { get; set; }
        public static ConfigEntry<Color> FacecoversColor { get; set; }
        public static ConfigEntry<Color> GearComponentsColor { get; set; }
        public static ConfigEntry<Color> HeadgearColor { get; set; }
        public static ConfigEntry<Color> HeadsetsColor { get; set; }
        public static ConfigEntry<Color> SecureContainersColor { get; set; }
        public static ConfigEntry<Color> StorageContainersColor { get; set; }
        public static ConfigEntry<Color> TacticalRigsColor { get; set; }
        public static ConfigEntry<Color> FunctionalModsColor { get; set; }
        public static ConfigEntry<Color> GearModsColor { get; set; }
        public static ConfigEntry<Color> VitalPartsColor { get; set; }
        public static ConfigEntry<Color> AssaultCarbinesColor { get; set; }
        public static ConfigEntry<Color> AssaultRiflesColor { get; set; }
        public static ConfigEntry<Color> BoltActionRiflesColor { get; set; }
        public static ConfigEntry<Color> GrenadeLaunchersColor { get; set; }
        public static ConfigEntry<Color> MachineGunsColor { get; set; }
        public static ConfigEntry<Color> MarksmanRiflesColor { get; set; }
        public static ConfigEntry<Color> PistolsColor { get; set; }
        public static ConfigEntry<Color> SMGsColor { get; set; }
        public static ConfigEntry<Color> ShotgunsColor { get; set; }
        public static ConfigEntry<Color> SpecialWeaponsColor { get; set; }
        public static ConfigEntry<Color> MeleeWeaponsColor { get; set; }
        public static ConfigEntry<Color> ThrowablesColor { get; set; }
        public static ConfigEntry<Color> AmmoPacksColor { get; set; }
        public static ConfigEntry<Color> RoundsColor { get; set; }
        public static ConfigEntry<Color> DrinksColor { get; set; }
        public static ConfigEntry<Color> FoodColor { get; set; }
        public static ConfigEntry<Color> InjectorsColor { get; set; }
        public static ConfigEntry<Color> InjuryTreatmentColor { get; set; }
        public static ConfigEntry<Color> MedkitsColor { get; set; }
        public static ConfigEntry<Color> PillsColor { get; set; }
        public static ConfigEntry<Color> ElectronicKeysColor { get; set; }
        public static ConfigEntry<Color> MechanicalKeysColor { get; set; }
        public static ConfigEntry<Color> InfoItemsColor { get; set; }
        public static ConfigEntry<Color> QuestItemsColor { get; set; }
        public static ConfigEntry<Color> SpecialEquipmentColor { get; set; }
        public static ConfigEntry<Color> MapsColor { get; set; }
        public static ConfigEntry<Color> MoneyColor { get; set; }

        public static ConfigEntry<string> Version { get; set; }

        public static void Init(ConfigFile Config, PluginInfo Info)
        {
            Version = Config.Bind("Versioning", "Version", "0.0.0", new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 1, ReadOnly = true, IsAdvanced = true }));

            bool resetDefaults = false;
            if (Version.Value == "0.0.0")
            {
                // Using New Config File
                Version.Value = Info.Metadata.Version.ToString();
                resetDefaults = true;
            }
            else if (Version.Value != Info.Metadata.Version.ToString())
            {
                // Using Old Config File
                Version.Value = Info.Metadata.Version.ToString();
                resetDefaults = true;
            }
            else
            {
                // Valid Config File
            }

            Enabled = Config.Bind(senseSectionTitle, "EnableSense", EnableSense.OnText, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 380 }));
            EnableExfilSense = Config.Bind(senseSectionTitle, "EnableExfilSense", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 370 }));
            SenseAlwaysOn = Config.Bind(senseSectionTitle, "AlwaysOn", false, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 360 }));

            EnableContainers = Config.Bind(senseSectionTitle, "EnableContainers", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 355 }));
            EnableBodies = Config.Bind(senseSectionTitle, "EnableBodies", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 355 }));
            EnableLooseLoot = Config.Bind(senseSectionTitle, "EnableLooseLoot", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 355 }));

            SenseKey = Config.Bind(senseSectionTitle, "SenseKey", new KeyboardShortcut(KeyCode.F), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 350 }));
            DoubleClick = Config.Bind(senseSectionTitle, "DoubleClick", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 340, IsAdvanced = true }));
            Cooldown = Config.Bind(senseSectionTitle, "Cooldown", 2f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 330, IsAdvanced = true }));

            Duration = Config.Bind(senseSectionTitle, "Duration", 10f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 320 }));
            ExfilDuration = Config.Bind(senseSectionTitle, "Exfil Duration", 30f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 320 }));
            Radius = Config.Bind(senseSectionTitle, "Radius", 10, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 310 }));
            DeadPlayerRadius = Config.Bind(senseSectionTitle, "DeadPlayerRadius", 20, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 310 }));
            Speed = Config.Bind(senseSectionTitle, "Speed", 20f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 300 }));
            MaxHeight = Config.Bind(senseSectionTitle, "MaxHeight", 3f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 290, IsAdvanced = true }));
            MinHeight = Config.Bind(senseSectionTitle, "MinHeight", -1f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 280, IsAdvanced = true }));

            ContainerLootcount = Config.Bind(senseSectionTitle, "ContainerLootcount", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 272, IsAdvanced = true }));
            EnableFlea = Config.Bind(senseSectionTitle, "Enable Flea", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 270 }));
            FleaIncludeAmmo = Config.Bind(senseSectionTitle, "Flea Include Ammo", false, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 260 }));

            UseBackgroundColor = Config.Bind(senseSectionTitle, "Use Background Color", false, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 255 }));

            Size = Config.Bind(senseSectionTitle, "Size", 0.5f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 250 }));
            IconSize = Config.Bind(senseSectionTitle, "IconSize", 0.1f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 240 }));
            SizeClamp = Config.Bind(senseSectionTitle, "Size Clamp", 3.0f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 230, IsAdvanced = true }));

            VerticalOffset = Config.Bind(senseSectionTitle, "Vertical Offset", 0.22f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 220, IsAdvanced = true }));
            TextOffset = Config.Bind(senseSectionTitle, "Text Offset", 0.15f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 217, IsAdvanced = true }));
            ExfilVerticalOffset = Config.Bind(senseSectionTitle, "ExfilVertical Offset", 40f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 215, IsAdvanced = true }));

            IntensitySpeed = Config.Bind(senseSectionTitle, "Intensity Speed", 2f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 210, IsAdvanced = true }));

            AlwaysOnFrequency = Config.Bind(senseSectionTitle, "AlwaysOn Frequency", 2f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 200, IsAdvanced = true }));

            LightIntensity = Config.Bind(senseSectionTitle, "LightIntensity", 0.6f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 190 }));
            LightRange = Config.Bind(senseSectionTitle, "LightRange", 2.5f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 180 }));
            LightShadows = Config.Bind(senseSectionTitle, "LightShadows", false, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 170, IsAdvanced = true }));

            ExfilLightIntensity = Config.Bind(senseSectionTitle, "Exfil LightIntensity", 1.0f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 160 }));
            ExfilLightRange = Config.Bind(senseSectionTitle, "Exfil LightRange", 50f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 150 }));
            ExfilLightShadows = Config.Bind(senseSectionTitle, "Exfil LightShadows", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 140 }));

            AudioDistance = Config.Bind(senseSectionTitle, "AudioDistance", 99f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 130, IsAdvanced = true }));
            AudioRolloff = Config.Bind(senseSectionTitle, "AudioRolloff", 100, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 120, IsAdvanced = true }));
            AudioVolume = Config.Bind(senseSectionTitle, "AudioVolume", 0.5f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 112 }));
            ContainerAudioVolume = Config.Bind(senseSectionTitle, "ContainerAudioVolume", 0.5f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 110 }));
            ActivateSenseVolume = Config.Bind(senseSectionTitle, "ActivateSenseVolume", 0.5f, new ConfigDescription("requires a custom sound .wav file named Sense.wav", null, new ConfigurationManagerAttributes { Order = 108 }));
            SenseRareSound = Config.Bind(senseSectionTitle, "SenseRareSound", false, new ConfigDescription("requires a custom sound .wav file named SenseRare.wav", null, new ConfigurationManagerAttributes { Order = 106 }));

            useDof = Config.Bind(effectsSectionTitle, "useDof", true, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 100 }));

            ExfilColor = Config.Bind(colorsSectionTitle, "ExfilColor", new Color(0.01f, 1.0f, 0.01f, 1.0f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 570 }));
            ExfilUnmetColor = Config.Bind(colorsSectionTitle, "ExfilUnmetColor", new Color(1.0f, 0.01f, 0.01f, 1.0f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 560 }));

            TextColor = Config.Bind(colorsSectionTitle, "TextColor", new Color(0.84f, 0.88f, 0.95f, 1.0f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 550 }));

            RareItemsColor = Config.Bind(colorsSectionTitle, "RareItemsColor", new Color(1.0f, 0.01f, 0.01f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 540 }));
            WishListItemsColor = Config.Bind(colorsSectionTitle, "WishListItemsColor", new Color(1.0f, 0.01f, 0.2f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 530 }));
            NonFleaItemsColor = Config.Bind(colorsSectionTitle, "NonFleaItemsColor", new Color(1.0f, 0.12f, 0.01f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 520 }));
            KappaItemsColor = Config.Bind(colorsSectionTitle, "KappaItemsColor", new Color(1.0f, 1.0f, 0.01f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 510 }));

            LootableContainerColor = Config.Bind(colorsSectionTitle, "LootableContainerColor", new Color(0.36f, 0.18f, 1.0f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 500 }));
            ObservedLootItemColor = Config.Bind(colorsSectionTitle, "ObservedLootItemColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 490 }));

            OthersColor = Config.Bind(colorsSectionTitle, "OthersColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 480 }));
            BuildingMaterialsColor = Config.Bind(colorsSectionTitle, "BuildingMaterialsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 470 }));
            ElectronicsColor = Config.Bind(colorsSectionTitle, "ElectronicsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 460 }));
            EnergyElementsColor = Config.Bind(colorsSectionTitle, "EnergyElementsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 450 }));
            FlammableMaterialsColor = Config.Bind(colorsSectionTitle, "FlammableMaterialsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 440 }));
            HouseholdMaterialsColor = Config.Bind(colorsSectionTitle, "HouseholdMaterialsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 430 }));
            MedicalSuppliesColor = Config.Bind(colorsSectionTitle, "MedicalSuppliesColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 420 }));
            ToolsColor = Config.Bind(colorsSectionTitle, "ToolsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 410 }));
            ValuablesColor = Config.Bind(colorsSectionTitle, "ValuablesColor", new Color(0.36f, 0.18f, 1.0f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 400 }));

            BackpacksColor = Config.Bind(colorsSectionTitle, "BackpacksColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 390 }));
            BodyArmorColor = Config.Bind(colorsSectionTitle, "BodyArmorColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 380 }));
            EyewearColor = Config.Bind(colorsSectionTitle, "EyewearColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 370 }));
            FacecoversColor = Config.Bind(colorsSectionTitle, "FacecoversColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 360 }));
            GearComponentsColor = Config.Bind(colorsSectionTitle, "GearComponentsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 350 }));
            HeadgearColor = Config.Bind(colorsSectionTitle, "HeadgearColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 340 }));
            HeadsetsColor = Config.Bind(colorsSectionTitle, "HeadsetsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 330 }));
            SecureContainersColor = Config.Bind(colorsSectionTitle, "SecureContainersColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 320 }));
            StorageContainersColor = Config.Bind(colorsSectionTitle, "StorageContainersColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 310 }));
            TacticalRigsColor = Config.Bind(colorsSectionTitle, "TacticalRigsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 300 }));

            FunctionalModsColor = Config.Bind(colorsSectionTitle, "FunctionalModsColor", new Color(0.1f, 0.35f, 0.65f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 290 }));
            GearModsColor = Config.Bind(colorsSectionTitle, "GearModsColor", new Color(0.15f, 0.5f, 0.1f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 280 }));
            VitalPartsColor = Config.Bind(colorsSectionTitle, "VitalPartsColor", new Color(0.7f, 0.2f, 0.1f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 270 }));

            AssaultCarbinesColor = Config.Bind(colorsSectionTitle, "AssaultCarbinesColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 260 }));
            AssaultRiflesColor = Config.Bind(colorsSectionTitle, "AssaultRiflesColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 250 }));
            BoltActionRiflesColor = Config.Bind(colorsSectionTitle, "BoltActionRiflesColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 240 }));
            GrenadeLaunchersColor = Config.Bind(colorsSectionTitle, "GrenadeLaunchersColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 230 }));
            MachineGunsColor = Config.Bind(colorsSectionTitle, "MachineGunsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 220 }));
            MarksmanRiflesColor = Config.Bind(colorsSectionTitle, "MarksmanRiflesColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 210 }));
            MeleeWeaponsColor = Config.Bind(colorsSectionTitle, "MeleeWeaponsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 200 }));
            PistolsColor = Config.Bind(colorsSectionTitle, "PistolsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 190 }));
            SMGsColor = Config.Bind(colorsSectionTitle, "SMGsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 180 }));
            ShotgunsColor = Config.Bind(colorsSectionTitle, "ShotgunsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 170 }));
            SpecialWeaponsColor = Config.Bind(colorsSectionTitle, "SpecialWeaponsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 160 }));
            ThrowablesColor = Config.Bind(colorsSectionTitle, "ThrowablesColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 150 }));

            AmmoPacksColor = Config.Bind(colorsSectionTitle, "AmmoPacksColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 140 }));
            RoundsColor = Config.Bind(colorsSectionTitle, "RoundsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 130 }));
            DrinksColor = Config.Bind(colorsSectionTitle, "DrinksColor", new Color(0.13f, 0.66f, 1.0f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 120 }));
            FoodColor = Config.Bind(colorsSectionTitle, "FoodColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 110 }));
            InjectorsColor = Config.Bind(colorsSectionTitle, "InjectorsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 100 }));
            InjuryTreatmentColor = Config.Bind(colorsSectionTitle, "InjuryTreatmentColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 90 }));
            MedkitsColor = Config.Bind(colorsSectionTitle, "MedkitsColor", new Color(0.3f, 1.0f, 0.13f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 80 }));
            PillsColor = Config.Bind(colorsSectionTitle, "PillsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 70 }));

            ElectronicKeysColor = Config.Bind(colorsSectionTitle, "ElectronicKeysColor", new Color(1.0f, 0.01f, 0.01f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 60 }));
            MechanicalKeysColor = Config.Bind(colorsSectionTitle, "MechanicalKeysColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 50 }));

            InfoItemsColor = Config.Bind(colorsSectionTitle, "InfoItemsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 40 }));
            QuestItemsColor = Config.Bind(colorsSectionTitle, "QuestItemsColor", new Color(1.0f, 1.0f, 0.01f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 38 }));
            SpecialEquipmentColor = Config.Bind(colorsSectionTitle, "SpecialEquipmentColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 30 }));
            MapsColor = Config.Bind(colorsSectionTitle, "MapsColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 20 }));
            MoneyColor = Config.Bind(colorsSectionTitle, "MoneyColor", new Color(0.84f, 0.88f, 0.95f, 0.8f), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 10 }));

            if (resetDefaults) ResetDefaults();
        }

        private static void ResetDefaults()
        {
            Size.Value = (float)Size.DefaultValue;
            IconSize.Value = (float)IconSize.DefaultValue;
            SizeClamp.Value = (float)SizeClamp.DefaultValue;
            VerticalOffset.Value = (float)VerticalOffset.DefaultValue;
            TextOffset.Value = (float)TextOffset.DefaultValue;
            ExfilVerticalOffset.Value = (float)ExfilVerticalOffset.DefaultValue;
        }
    }
}
