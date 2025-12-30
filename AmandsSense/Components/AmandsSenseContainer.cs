using AmandsSense.Enums;
using AmandsSense.Helpers;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using UnityEngine;

namespace AmandsSense.Components
{
    public class AmandsSenseContainer : AmandsSenseConstructor
    {
        public LootableContainer lootableContainer;
        public bool emptyLootableContainer = false;
        public int itemCount = 0;
        public string ContainerId;
        public bool Drawer;

        public override void SetSense(LootableContainer LootableContainer)
        {
            lootableContainer = LootableContainer;
            if (lootableContainer != null && lootableContainer.gameObject.activeSelf)
            {
                Drawer = amandsSenseWorld.SenseWorldType == SenseWorldType.Drawer;
                // SenseContainer Defaults
                emptyLootableContainer = false;
                itemCount = 0;

                ContainerId = lootableContainer.Id;

                // SenseContainer Items
                SenseItemColor senseItemColor = SenseItemColor.Default;
                if (lootableContainer.ItemOwner != null && AmandsSenseClass.itemsJsonClass != null && AmandsSenseClass.itemsJsonClass.RareItems != null && AmandsSenseClass.itemsJsonClass.KappaItems != null && AmandsSenseClass.itemsJsonClass.NonFleaExclude != null && AmandsSenseClass.Player.Profile != null && AmandsSenseClass.Player.Profile.WishlistManager != null)
                {
                    CompoundItem lootItemClass = lootableContainer.ItemOwner.RootItem as CompoundItem;
                    if (lootItemClass != null)
                    {
                        foreach (StashGridClass grid in lootItemClass.Grids)
                        {
                            foreach (Item item in grid.Items)
                            {
                                itemCount += 1;
                                senseItemColor = GetItemColor(item, senseItemColor);
                            }
                        }
                    }
                }
                if (itemCount == 0)
                {
                    amandsSenseWorld.CancelSense();
                    return;
                }

                // SenseContainer Color and Sprite
                if (AmandsSenseClass.LoadedSprites.ContainsKey("LootableContainer.png"))
                {
                    sprite = AmandsSenseClass.LoadedSprites["LootableContainer.png"];
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
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter.png"];
                        }
                        break;
                    case SenseItemColor.WishList:
                        color = Settings.WishListItemsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_fav_checked.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_fav_checked.png"];
                        }
                        break;
                    case SenseItemColor.Rare:
                        color = Settings.RareItemsColor.Value;
                        break;
                }

                // SenseContainer Sprite
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprite;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
                }

                // SenseContainer Light
                if (light != null)
                {
                    light.color = new Color(color.r, color.g, color.b, 1f);
                    light.intensity = 0f;
                    light.range = Settings.LightRange.Value;
                }

                // SenseContainer Type
                if (typeText != null)
                {
                    typeText.fontSize = 0.5f;
                    typeText.text = AmandsSenseHelper.Localized("container", EStringCase.None);
                    typeText.color = new Color(color.r, color.g, color.b, 0f);
                }

                // SenseContainer Name
                if (nameText != null)
                {
                    nameText.fontSize = 1f;
                    //nameText.text = "Name";
                    nameText.text = "<b>" + lootableContainer.ItemOwner.ContainerName + "</b>";
                    nameText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                }

                // SenseContainer Description
                if (descriptionText != null)
                {
                    descriptionText.fontSize = 0.75f;
                    if (Settings.ContainerLootcount.Value)
                    {
                        descriptionText.text = AmandsSenseHelper.Localized("loot", EStringCase.None) + " " + itemCount;
                    }
                    else
                    {
                        descriptionText.text = "";
                    }
                    descriptionText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                }

                // SenseContainer Sound
                if (Settings.SenseRareSound.Value && AmandsSenseClass.LoadedAudioClips.ContainsKey("SenseRare.wav"))
                {
                    if (!Settings.SenseAlwaysOn.Value)
                    {
                        Singleton<BetterAudio>.Instance.PlayAtPoint(transform.position, AmandsSenseClass.LoadedAudioClips["SenseRare.wav"], Settings.AudioDistance.Value, BetterAudio.AudioSourceGroupType.Environment, Settings.AudioRolloff.Value, Settings.ContainerAudioVolume.Value, EOcclusionTest.Fast);
                    }
                }
                else
                {
                    if (!Settings.SenseAlwaysOn.Value && !Drawer && lootableContainer.OpenSound.Length > 0)
                    {
                        AudioClip OpenSound = lootableContainer.OpenSound[0];
                        if (OpenSound != null)
                        {
                            Singleton<BetterAudio>.Instance.PlayAtPoint(transform.position, OpenSound, Settings.AudioDistance.Value, BetterAudio.AudioSourceGroupType.Environment, Settings.AudioRolloff.Value, Settings.ContainerAudioVolume.Value, EOcclusionTest.Fast);
                        }
                    }
                }
            }
            else if (amandsSenseWorld != null)
            {
                amandsSenseWorld.CancelSense();
            }
        }
        public override void UpdateSense()
        {
            if (lootableContainer != null && lootableContainer.gameObject.activeSelf)
            {
                // SenseContainer Defaults
                emptyLootableContainer = false;
                itemCount = 0;

                ContainerId = lootableContainer.Id;

                // SenseContainer Items
                SenseItemColor senseItemColor = SenseItemColor.Default;
                if (lootableContainer.ItemOwner != null && AmandsSenseClass.itemsJsonClass != null && AmandsSenseClass.itemsJsonClass.RareItems != null && AmandsSenseClass.itemsJsonClass.KappaItems != null && AmandsSenseClass.itemsJsonClass.NonFleaExclude != null && AmandsSenseClass.Player.Profile != null && AmandsSenseClass.Player.Profile.WishlistManager != null)
                {
                    CompoundItem lootItemClass = lootableContainer.ItemOwner.RootItem as CompoundItem;
                    if (lootItemClass != null)
                    {
                        foreach (StashGridClass grid in lootItemClass.Grids)
                        {
                            foreach (Item item in grid.Items)
                            {
                                itemCount += 1;
                                senseItemColor = GetItemColor(item, senseItemColor);
                            }
                        }
                    }
                }
                if (itemCount == 0)
                {
                    amandsSenseWorld.CancelSense();
                    return;
                }

                // SenseContainer Color and Sprite
                if (AmandsSenseClass.LoadedSprites.ContainsKey("LootableContainer.png"))
                {
                    sprite = AmandsSenseClass.LoadedSprites["LootableContainer.png"];
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
                            sprite = AmandsSenseClass.LoadedSprites["icon_barter.png"];
                        }
                        break;
                    case SenseItemColor.WishList:
                        color = Settings.WishListItemsColor.Value;
                        if (AmandsSenseClass.LoadedSprites.ContainsKey("icon_fav_checked.png"))
                        {
                            sprite = AmandsSenseClass.LoadedSprites["icon_fav_checked.png"];
                        }
                        break;
                    case SenseItemColor.Rare:
                        color = Settings.RareItemsColor.Value;
                        break;
                }

                // SenseContainer Sprite
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprite;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, spriteRenderer.color.a);
                }

                // SenseContainer Light
                if (light != null)
                {
                    light.color = new Color(color.r, color.g, color.b, 1f);
                    light.range = Settings.LightRange.Value;
                }

                // SenseContainer Type
                if (typeText != null)
                {
                    typeText.fontSize = 0.5f;
                    //typeText.text = "Type";
                    typeText.color = new Color(color.r, color.g, color.b, typeText.color.a);
                }

                // SenseContainer Name
                if (nameText != null)
                {
                    nameText.fontSize = 1f;
                    //nameText.text = "Name";
                    nameText.color = new Color(textColor.r, textColor.g, textColor.b, nameText.color.a);
                }

                // SenseContainer Description
                if (descriptionText != null)
                {
                    descriptionText.fontSize = 0.75f;
                    if (Settings.ContainerLootcount.Value)
                    {
                        descriptionText.text = AmandsSenseHelper.Localized("loot", EStringCase.None) + " " + itemCount;
                    }
                    else
                    {
                        descriptionText.text = "";
                    }
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
                if (amandsSenseWorld.DisableGlow)
                {
                    light.intensity = 0;
                }
                else
                {
                    light.intensity = Settings.LightIntensity.Value * Intensity * (Drawer ? 0.25f : 1f);
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
            //Destroy(gameObject);
        }
    }
}
