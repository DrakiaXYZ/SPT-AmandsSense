using AmandsSense.Enums;
using AmandsSense.Helpers;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AmandsSense.Components
{
    public class AmandsSenseDeadPlayer : AmandsSenseConstructor
    {
        public LocalPlayer DeadPlayer;
        public Corpse corpse;

        public bool emptyDeadPlayer = true;
        public string Name;
        public string RoleName;

        public override void SetSense(LocalPlayer LocalPlayer)
        {
            DeadPlayer = LocalPlayer;
            if (DeadPlayer != null && DeadPlayer.gameObject.activeSelf)
            {
                corpse = DeadPlayer.gameObject.transform.GetComponent<Corpse>();
                // SenseDeadPlayer Defaults
                emptyDeadPlayer = false;
                SenseItemColor senseItemColor = SenseItemColor.Default;

                if (AmandsSenseClass.itemsJsonClass != null && AmandsSenseClass.itemsJsonClass.RareItems != null && AmandsSenseClass.itemsJsonClass.KappaItems != null && AmandsSenseClass.itemsJsonClass.NonFleaExclude != null && AmandsSenseClass.Player != null && AmandsSenseClass.Player.Profile != null && AmandsSenseClass.Player.Profile.WishlistManager != null)
                {
                    if (DeadPlayer.Profile != null)
                    {
                        switch (DeadPlayer.Side)
                        {
                            case EPlayerSide.Usec:
                                RoleName = "USEC";
                                Name = DeadPlayer.Profile.Nickname;
                                break;
                            case EPlayerSide.Bear:
                                RoleName = "BEAR";
                                Name = DeadPlayer.Profile.Nickname;
                                break;
                            case EPlayerSide.Savage:
                                RoleName = AmandsSenseHelper.Localized(DeadPlayer.Profile.Info.Settings.Role.GetScavRoleKey(), EStringCase.Upper);
                                Name = AmandsSenseHelper.Transliterate(DeadPlayer.Profile.Nickname);
                                break;
                        }
                        foreach (Item item in DeadPlayer.Profile.Inventory.AllRealPlayerItems)
                        {
                            if (item.CurrentAddress != null)
                            {
                                if (item.Parent.Container != null && item.Parent.Container.ParentItem != null && TemplateIdToObjectMappingsClass.TypeTable["5448bf274bdc2dfc2f8b456a"].IsAssignableFrom(item.Parent.Container.ParentItem.GetType()))
                                {
                                    continue;
                                }
                                Slot slot = item.Parent.Container as Slot;
                                if (slot != null)
                                {
                                    if (slot.Name == "Dogtag")
                                    {
                                        continue;
                                    }
                                    if (slot.Name == "SecuredContainer")
                                    {
                                        continue;
                                    }
                                    if (slot.Name == "Scabbard")
                                    {
                                        continue;
                                    }
                                    if (slot.Name == "ArmBand")
                                    {
                                        continue;
                                    }
                                }
                            }
                            if (emptyDeadPlayer)
                            {
                                emptyDeadPlayer = false;
                            }
                            senseItemColor = GetItemColor(item, senseItemColor);
                        }
                    }
                }

                switch (DeadPlayer.Side)
                {
                    case EPlayerSide.Usec:
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("Usec.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["Usec.png"];
                        }
                        break;
                    case EPlayerSide.Bear:
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("Bear.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["Bear.png"];
                        }
                        break;
                    case EPlayerSide.Savage:
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_kills_big.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_kills_big.png"];
                        }
                        break;
                }

                switch (senseItemColor)
                {
                    case SenseItemColor.Default:
                        color = Settings.ObservedLootItemColor.Value;
                        break;
                    case SenseItemColor.Kappa:
                        color = Settings.KappaItemsColor.Value;
                        break;
                    case SenseItemColor.NonFlea:
                        color = Settings.NonFleaItemsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter.png"))
                        {
                            //sprite = AmandsSenseClass.LoadedSprites["icon_barter.png"];
                        }
                        break;
                    case SenseItemColor.WishList:
                        color = Settings.WishListItemsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_fav_checked.png"))
                        {
                            //sprite = AmandsSenseClass.LoadedSprites["icon_fav_checked.png"];
                        }
                        break;
                    case SenseItemColor.Rare:
                        color = Settings.RareItemsColor.Value;
                        break;
                }

                // SenseDeadPlayer Sprite
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprite;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
                }

                // SenseDeadPlayer Light
                if (light != null)
                {
                    light.color = new Color(color.r, color.g, color.b, 1f);
                    light.intensity = 0f;
                    light.range = Settings.LightRange.Value;
                }

                // SenseDeadPlayer Type
                if (typeText != null)
                {
                    typeText.fontSize = 0.5f;
                    typeText.text = RoleName;
                    typeText.color = new Color(color.r, color.g, color.b, 0f);
                }

                // SenseDeadPlayer Name
                if (nameText != null)
                {
                    nameText.fontSize = 1f;
                    nameText.text = "<b>" + Name + "</b>";
                    nameText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                }

                // SenseDeadPlayer Description
                if (descriptionText != null)
                {
                    descriptionText.fontSize = 0.75f;
                    descriptionText.text = "";
                    descriptionText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                }
            }
            else if (amandsSenseWorld != null)
            {
                amandsSenseWorld.CancelSense();
            }
        }
        public override void UpdateSense()
        {
            if (DeadPlayer != null && DeadPlayer.gameObject.activeSelf)
            {
                // SenseDeadPlayer Defaults
                emptyDeadPlayer = false;
                SenseItemColor senseItemColor = SenseItemColor.Default;

                if (AmandsSenseClass.itemsJsonClass != null && AmandsSenseClass.itemsJsonClass.RareItems != null && AmandsSenseClass.itemsJsonClass.KappaItems != null && AmandsSenseClass.itemsJsonClass.NonFleaExclude != null && AmandsSenseClass.Player != null && AmandsSenseClass.Player.Profile != null && AmandsSenseClass.Player.Profile.WishlistManager != null)
                {
                    if (DeadPlayer.Profile != null)
                    {
                        foreach (Item item in DeadPlayer.Profile.Inventory.AllRealPlayerItems)
                        {
                            if (item.CurrentAddress != null)
                            {
                                if (item.Parent.Container != null && item.Parent.Container.ParentItem != null && TemplateIdToObjectMappingsClass.TypeTable["5448bf274bdc2dfc2f8b456a"].IsAssignableFrom(item.Parent.Container.ParentItem.GetType()))
                                {
                                    continue;
                                }
                                Slot slot = item.Parent.Container as Slot;
                                if (slot != null)
                                {
                                    if (slot.Name == "Dogtag")
                                    {
                                        continue;
                                    }
                                    if (slot.Name == "SecuredContainer")
                                    {
                                        continue;
                                    }
                                    if (slot.Name == "Scabbard")
                                    {
                                        continue;
                                    }
                                    if (slot.Name == "ArmBand")
                                    {
                                        continue;
                                    }
                                }
                            }
                            if (emptyDeadPlayer)
                            {
                                emptyDeadPlayer = false;
                            }
                            senseItemColor = GetItemColor(item, senseItemColor);
                        }
                    }
                }
                switch (DeadPlayer.Side)
                {
                    case EPlayerSide.Usec:
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("Usec.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["Usec.png"];
                        }
                        break;
                    case EPlayerSide.Bear:
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("Bear.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["Bear.png"];
                        }
                        break;
                    case EPlayerSide.Savage:
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_kills_big.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_kills_big.png"];
                        }
                        break;
                }

                switch (senseItemColor)
                {
                    case SenseItemColor.Default:
                        color = Settings.ObservedLootItemColor.Value;
                        break;
                    case SenseItemColor.Kappa:
                        color = Settings.KappaItemsColor.Value;
                        break;
                    case SenseItemColor.NonFlea:
                        color = Settings.NonFleaItemsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_barter.png"))
                        {
                            //sprite = AmandsSenseClass.LoadedSprites["icon_barter.png"];
                        }
                        break;
                    case SenseItemColor.WishList:
                        color = Settings.WishListItemsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_fav_checked.png"))
                        {
                            //sprite = AmandsSenseClass.LoadedSprites["icon_fav_checked.png"];
                        }
                        break;
                    case SenseItemColor.Rare:
                        color = Settings.RareItemsColor.Value;
                        break;
                }

                // SenseDeadPlayer Sprite
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprite;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, spriteRenderer.color.a);
                }

                // SenseDeadPlayer Light
                if (light != null)
                {
                    light.color = new Color(color.r, color.g, color.b, 1f);
                    light.range = Settings.LightRange.Value;
                }

                // SenseDeadPlayer Type
                if (typeText != null)
                {
                    typeText.fontSize = 0.5f;
                    //typeText.text = corpse.Side.ToString();
                    typeText.color = new Color(color.r, color.g, color.b, typeText.color.a);
                }

                // SenseDeadPlayer Name
                if (nameText != null)
                {
                    nameText.fontSize = 1f;
                    //nameText.text = Name;
                    nameText.color = new Color(textColor.r, textColor.g, textColor.b, nameText.color.a);
                }

                // SenseDeadPlayer Description
                if (descriptionText != null)
                {
                    descriptionText.fontSize = 0.75f;
                    descriptionText.text = "";
                    descriptionText.color = new Color(textColor.r, textColor.g, textColor.b, descriptionText.color.a);
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
                light.intensity = Settings.LightIntensity.Value * Intensity;
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
        public override void UpdateSenseLocation()
        {
            if (corpse != null)
            {
                gameObject.transform.parent.position = corpse.TrackableTransform.position + (Vector3.up * 3f * Settings.VerticalOffset.Value);
            }
        }
        public override void RemoveSense()
        {
            //Destroy(gameObject);
        }
    }
}
