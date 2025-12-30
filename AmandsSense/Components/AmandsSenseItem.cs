using AmandsSense.Enums;
using AmandsSense.Helpers;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using EFT.UI;
using System.Linq;
using UnityEngine;

namespace AmandsSense.Components
{
    public class AmandsSenseItem : AmandsSenseConstructor
    {
        private ObservedLootItem observedLootItem;
        private string type;

        public SenseItemType senseItemType = SenseItemType.All;

        public override void SetSense(ObservedLootItem ObservedLootItem)
        {
            senseItemType = SenseItemType.All;
            color = Settings.ObservedLootItemColor.Value;

            observedLootItem = ObservedLootItem;
            if (observedLootItem != null && observedLootItem.gameObject.activeSelf && observedLootItem.Item != null)
            {
                // If this item has 0 durability, don't show it
                if (observedLootItem.Item.TryGetItemComponent(out RepairableComponent repairableComponentCheck))
                {
                    if ((int)repairableComponentCheck.Durability == 0)
                    {
                        amandsSenseWorld.CancelSense();
                        return;
                    }
                }

                ObservedLootItem.ItemOwner.RemoveItemEvent += RemoveLootItem;

                // Weapon SenseItem Color, Sprite and Type
                Weapon weapon = observedLootItem.Item as Weapon;
                if (weapon != null)
                {
                    switch (weapon.WeapClass)
                    {
                        case "assaultCarbine":
                            senseItemType = SenseItemType.AssaultCarbines;
                            color = Settings.AssaultCarbinesColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_carbines.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_carbines.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f78e986f77447ed5636b1", EStringCase.None);
                            break;
                        case "assaultRifle":
                            senseItemType = SenseItemType.AssaultRifles;
                            color = Settings.AssaultRiflesColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_assaultrifles.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_assaultrifles.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f78fc86f77409407a7f90", EStringCase.None);
                            break;
                        case "sniperRifle":
                            senseItemType = SenseItemType.BoltActionRifles;
                            color = Settings.BoltActionRiflesColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_botaction.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_botaction.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f798886f77447ed5636b5", EStringCase.None);
                            break;
                        case "grenadeLauncher":
                            senseItemType = SenseItemType.GrenadeLaunchers;
                            color = Settings.GrenadeLaunchersColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_gl.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_gl.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f79d186f774093f2ed3c2", EStringCase.None);
                            break;
                        case "machinegun":
                            senseItemType = SenseItemType.MachineGuns;
                            color = Settings.MachineGunsColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_mg.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_mg.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f79a486f77409407a7f94", EStringCase.None);
                            break;
                        case "marksmanRifle":
                            senseItemType = SenseItemType.MarksmanRifles;
                            color = Settings.MarksmanRiflesColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_dmr.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_dmr.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f791486f774093f2ed3be", EStringCase.None);
                            break;
                        case "pistol":
                            senseItemType = SenseItemType.Pistols;
                            color = Settings.PistolsColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_pistols.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_pistols.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f792486f77447ed5636b3", EStringCase.None);
                            break;
                        case "smg":
                            senseItemType = SenseItemType.SMGs;
                            color = Settings.SMGsColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_smg.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_smg.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f796a86f774093f2ed3c0", EStringCase.None);
                            break;
                        case "shotgun":
                            senseItemType = SenseItemType.Shotguns;
                            color = Settings.ShotgunsColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_shotguns.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_shotguns.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f794b86f77409407a7f92", EStringCase.None);
                            break;
                        case "specialWeapon":
                            senseItemType = SenseItemType.SpecialWeapons;
                            color = Settings.SpecialWeaponsColor.Value;
                            if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_special.png"))
                            {
                                sprite = AmandsSenseClass.LoadedSprites["icon_weapons_special.png"];
                            }
                            type = AmandsSenseHelper.Localized("5b5f79eb86f77447ed5636b7", EStringCase.None);
                            break;
                        default:
                            senseItemType = AmandsSenseClass.GetSenseItemType(observedLootItem.Item.GetType());
                            break;
                    }
                }
                else
                {
                    senseItemType = AmandsSenseClass.GetSenseItemType(observedLootItem.Item.GetType());
                }

                // SenseItem Color, Sprite and Type
                switch (senseItemType)
                {
                    case SenseItemType.All:
                        color = Settings.ObservedLootItemColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("ObservedLootItem.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["ObservedLootItem.png"];
                        }
                        type = "ObservedLootItem";
                        break;
                    case SenseItemType.Others:
                        color = Settings.OthersColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_others.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_others.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2f4", EStringCase.None);
                        break;
                    case SenseItemType.BuildingMaterials:
                        color = Settings.BuildingMaterialsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_building.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_building.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2ee", EStringCase.None);
                        break;
                    case SenseItemType.Electronics:
                        color = Settings.ElectronicsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_electronics.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_electronics.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2ef", EStringCase.None);
                        break;
                    case SenseItemType.EnergyElements:
                        color = Settings.EnergyElementsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_energy.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_energy.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2ed", EStringCase.None);
                        break;
                    case SenseItemType.FlammableMaterials:
                        color = Settings.FlammableMaterialsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_flammable.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_flammable.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2f2", EStringCase.None);
                        break;
                    case SenseItemType.HouseholdMaterials:
                        color = Settings.HouseholdMaterialsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_household.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_household.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2f0", EStringCase.None);
                        break;
                    case SenseItemType.MedicalSupplies:
                        color = Settings.MedicalSuppliesColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_medical.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_medical.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2f3", EStringCase.None);
                        break;
                    case SenseItemType.Tools:
                        color = Settings.ToolsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_tools.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_tools.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2f6", EStringCase.None);
                        break;
                    case SenseItemType.Valuables:
                        color = Settings.ValuablesColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter_valuables.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter_valuables.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b2f1", EStringCase.None);
                        break;
                    case SenseItemType.Backpacks:
                        color = Settings.BackpacksColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_backpacks.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_backpacks.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f6f6c86f774093f2ecf0b", EStringCase.None);
                        break;
                    case SenseItemType.BodyArmor:
                        color = Settings.BodyArmorColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_armor.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_armor.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f701386f774093f2ecf0f", EStringCase.None);
                        break;
                    case SenseItemType.Eyewear:
                        color = Settings.EyewearColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_visors.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_visors.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b331", EStringCase.None);
                        break;
                    case SenseItemType.Facecovers:
                        color = Settings.FacecoversColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_facecovers.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_facecovers.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b32f", EStringCase.None);
                        break;
                    case SenseItemType.GearComponents:
                        color = Settings.GearComponentsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_components.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_components.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f704686f77447ec5d76d7", EStringCase.None);
                        break;
                    case SenseItemType.Headgear:
                        color = Settings.HeadgearColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_headwear.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_headwear.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b330", EStringCase.None);
                        break;
                    case SenseItemType.Headsets:
                        color = Settings.HeadsetsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_headsets.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_headsets.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f6f3c86f774094242ef87", EStringCase.None);
                        break;
                    case SenseItemType.SecureContainers:
                        color = Settings.SecureContainersColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_secured.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_secured.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f6fd286f774093f2ecf0d", EStringCase.None);
                        break;
                    case SenseItemType.StorageContainers:
                        color = Settings.StorageContainersColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_cases.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_cases.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f6fa186f77409407a7eb7", EStringCase.None);
                        break;
                    case SenseItemType.TacticalRigs:
                        color = Settings.TacticalRigsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_gear_rigs.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_gear_rigs.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f6f8786f77447ed563642", EStringCase.None);
                        break;
                    case SenseItemType.FunctionalMods:
                        color = Settings.FunctionalModsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_mods_functional.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_mods_functional.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f71b386f774093f2ecf11", EStringCase.None);
                        break;
                    case SenseItemType.GearMods:
                        color = Settings.GearModsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_mods_gear.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_mods_gear.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f750686f774093e6cb503", EStringCase.None);
                        break;
                    case SenseItemType.VitalParts:
                        color = Settings.VitalPartsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_mods_vital.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_mods_vital.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f75b986f77447ec5d7710", EStringCase.None);
                        break;
                    case SenseItemType.MeleeWeapons:
                        color = Settings.MeleeWeaponsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_melee.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_weapons_melee.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f7a0886f77409407a7f96", EStringCase.None);
                        break;
                    case SenseItemType.Throwables:
                        color = Settings.ThrowablesColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_weapons_throw.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_weapons_throw.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f7a2386f774093f2ed3c4", EStringCase.None);
                        break;
                    case SenseItemType.AmmoPacks:
                        color = Settings.AmmoPacksColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_ammo_boxes.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_ammo_boxes.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b33c", EStringCase.None);
                        break;
                    case SenseItemType.Rounds:
                        color = Settings.RoundsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_ammo_rounds.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_ammo_rounds.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b33b", EStringCase.None);
                        break;
                    case SenseItemType.Drinks:
                        color = Settings.DrinksColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_provisions_drinks.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_provisions_drinks.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b335", EStringCase.None);
                        break;
                    case SenseItemType.Food:
                        color = Settings.FoodColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_provisions_food.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_provisions_food.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b336", EStringCase.None);
                        break;
                    case SenseItemType.Injectors:
                        color = Settings.InjectorsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_medical_injectors.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_medical_injectors.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b33a", EStringCase.None);
                        break;
                    case SenseItemType.InjuryTreatment:
                        color = Settings.InjuryTreatmentColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_medical_injury.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_medical_injury.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b339", EStringCase.None);
                        break;
                    case SenseItemType.Medkits:
                        color = Settings.MedkitsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_medical_medkits.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_medical_medkits.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b338", EStringCase.None);
                        break;
                    case SenseItemType.Pills:
                        color = Settings.PillsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_medical_pills.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_medical_pills.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b337", EStringCase.None);
                        break;
                    case SenseItemType.ElectronicKeys:
                        color = Settings.ElectronicKeysColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_keys_electronic.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_keys_electronic.png"];
                        }
                        type = AmandsSenseHelper.Localized("5c518ed586f774119a772aee", EStringCase.None);
                        break;
                    case SenseItemType.MechanicalKeys:
                        color = Settings.MechanicalKeysColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_keys_mechanic.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_keys_mechanic.png"];
                        }
                        type = AmandsSenseHelper.Localized("5c518ec986f7743b68682ce2", EStringCase.None);
                        break;
                    case SenseItemType.InfoItems:
                        color = Settings.InfoItemsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_info.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_info.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b341", EStringCase.None);
                        break;
                    case SenseItemType.SpecialEquipment:
                        color = Settings.SpecialEquipmentColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_spec.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_spec.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b345", EStringCase.None);
                        break;
                    case SenseItemType.Maps:
                        color = Settings.MapsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_maps.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_maps.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b47574386f77428ca22b343", EStringCase.None);
                        break;
                    case SenseItemType.Money:
                        color = Settings.MoneyColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_money.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_money.png"];
                        }
                        type = AmandsSenseHelper.Localized("5b5f78b786f77447ed5636af", EStringCase.None);
                        break;
                }

                // Quest SenseItem Color
                if (observedLootItem.Item.QuestItem) color = Settings.QuestItemsColor.Value;

                // JSON SenseItem Color
                if (AmandsSenseClass.itemsJsonClass != null)
                {
                    if (AmandsSenseClass.itemsJsonClass.KappaItems != null)
                    {
                        if (AmandsSenseClass.itemsJsonClass.KappaItems.Contains(observedLootItem.Item.TemplateId))
                        {
                            color = Settings.KappaItemsColor.Value;
                        }
                    }
                    if (Settings.EnableFlea.Value && !observedLootItem.Item.CanSellOnRagfair && !AmandsSenseClass.itemsJsonClass.NonFleaExclude.Contains(observedLootItem.Item.TemplateId))
                    {
                        color = Settings.NonFleaItemsColor.Value;
                    }
                    if (AmandsSenseClass.Player != null && AmandsSenseClass.Player.Profile != null && AmandsSenseClass.Player.Profile.WishlistManager != null && AmandsSenseClass.Player.Profile.WishlistManager.IsInWishlist(observedLootItem.Item.TemplateId, true, out var _))
                    {
                        color = Settings.WishListItemsColor.Value;
                    }
                    if (AmandsSenseClass.itemsJsonClass.RareItems != null)
                    {
                        if (AmandsSenseClass.itemsJsonClass.RareItems.Contains(observedLootItem.Item.TemplateId))
                        {
                            color = Settings.RareItemsColor.Value;
                        }
                    }
                }

                if (Settings.UseBackgroundColor.Value) color = AmandsSenseHelper.ToColor(observedLootItem.Item.BackgroundColor);

                // SenseItem Sprite
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprite;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
                }

                // SenseItem Light
                if (light != null)
                {
                    light.color = new Color(color.r, color.g, color.b, 1f);
                    light.intensity = 0f;
                    light.range = Settings.LightRange.Value;
                }

                // SenseItem Type
                if (typeText != null)
                {
                    typeText.fontSize = 0.5f;
                    typeText.text = type;
                    typeText.color = new Color(color.r, color.g, color.b, 0f);
                }

                if (AmandsSenseClass.inventoryControllerClass != null && !AmandsSenseClass.inventoryControllerClass.Examined(observedLootItem.Item))
                {
                    // SenseItem Unexamined Name
                    if (nameText != null)
                    {
                        nameText.fontSize = 1f;
                        nameText.text = "<b>???</b>";
                        nameText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                    }
                    // SenseItem Unexamined Description
                    if (descriptionText != null)
                    {
                        descriptionText.text = "";
                        descriptionText.fontSize = 0.75f;
                        descriptionText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                    }
                }
                else
                {
                    // SenseItem Name
                    if (nameText != null)
                    {
                        nameText.fontSize = 1f;
                        string Name = "<b>" + AmandsSenseHelper.Localized(observedLootItem.Item.Name, 0) + "</b>";
                        if (Name.Count() > 16) Name = "<b>" + AmandsSenseHelper.Localized(observedLootItem.Item.ShortName, 0) + "</b>";
                        if (observedLootItem.Item.StackObjectsCount > 1) Name = Name + " (" + observedLootItem.Item.StackObjectsCount + ")";
                        nameText.text = Name + "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + "<size=50%><voffset=0.5em> " + observedLootItem.Item.Weight + "kg";
                        nameText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                    }

                    // SenseItem Description
                    if (descriptionText != null)
                    {
                        if (observedLootItem.Item.TryGetItemComponent(out FoodDrinkComponent foodDrinkComponent) && ((int)foodDrinkComponent.MaxResource) > 1)
                        {
                            descriptionText.text = ((int)foodDrinkComponent.HpPercent) + "/" + ((int)foodDrinkComponent.MaxResource);
                        }
                        if (observedLootItem.Item.TryGetItemComponent(out KeyComponent keyComponent))
                        {
                            int maximumNumberOfUsages = keyComponent.Template.MaximumNumberOfUsage;
                            descriptionText.text = (maximumNumberOfUsages - keyComponent.NumberOfUsages) + "/" + maximumNumberOfUsages;
                        }
                        if (observedLootItem.Item.TryGetItemComponent(out MedKitComponent medKitComponent) && medKitComponent.MaxHpResource > 1)
                        {
                            descriptionText.text = ((int)medKitComponent.HpResource) + "/" + medKitComponent.MaxHpResource;
                        }
                        if (observedLootItem.Item.TryGetItemComponent(out RepairableComponent repairableComponent))
                        {
                            descriptionText.text = ((int)repairableComponent.Durability) + "/" + ((int)repairableComponent.MaxDurability);
                        }
                        MagazineItemClass magazineClass = observedLootItem.Item as MagazineItemClass;
                        if (magazineClass != null)
                        {
                            descriptionText.text = magazineClass.Count + "/" + magazineClass.MaxCount;
                        }
                        descriptionText.fontSize = 0.75f;
                        descriptionText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                    }
                }

                // SenseItem Sound
                if (Settings.SenseRareSound.Value && AmandsSenseClass.LoadedAudioClips.ContainsKey("SenseRare.wav"))
                {
                    if (!Settings.SenseAlwaysOn.Value)
                    {
                        Singleton<BetterAudio>.Instance.PlayAtPoint(transform.position, AmandsSenseClass.LoadedAudioClips["SenseRare.wav"], Settings.AudioDistance.Value, BetterAudio.AudioSourceGroupType.Environment, Settings.AudioRolloff.Value, Settings.AudioVolume.Value, EOcclusionTest.Fast);
                    }
                }
                else
                {
                    if (!Settings.SenseAlwaysOn.Value)
                    {
                        AudioClip itemClip = Singleton<GUISounds>.Instance.GetItemClip(observedLootItem.Item.ItemSound, EInventorySoundType.pickup);
                        if (itemClip != null)
                        {
                            Singleton<BetterAudio>.Instance.PlayAtPoint(transform.position, itemClip, Settings.AudioDistance.Value, BetterAudio.AudioSourceGroupType.Environment, Settings.AudioRolloff.Value, Settings.AudioVolume.Value, EOcclusionTest.Fast);
                        }
                    }
                }
            }
            else if (amandsSenseWorld != null)
            {
                amandsSenseWorld.CancelSense();
            }
        }

        public override void UpdateIntensity(float Intensity)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(color.r, color.g, color.b, color.a * Intensity);
            }
            if (light != null)
            {
                if (amandsSenseWorld.DisableGlow)
                {
                    light.intensity = 0;
                }
                else
                {
                    light.intensity = Settings.LightIntensity.Value * Intensity;
                }
            }
            if (typeText != null)
            {
                typeText.color = new Color(color.r, color.g, color.b, Intensity);
            }
            if (nameText != null)
            {
                nameText.color = new Color(textColor.r, textColor.g, textColor.b, Intensity);
            }
            if (descriptionText != null)
            {
                descriptionText.color = new Color(textColor.r, textColor.g, textColor.b, Intensity);
            }
        }
        public override void RemoveSense()
        {
            if (observedLootItem != null && observedLootItem.ItemOwner != null)
            {
                observedLootItem.ItemOwner.RemoveItemEvent -= RemoveLootItem;
            }

            //Destroy(gameObject);
        }

        private void RemoveLootItem(GEventArgs3 args)
        {
            if (args.Status != CommandStatus.Succeed)
            {
                return;
            }

            amandsSenseWorld.RemoveSense();
        }
    }
}
