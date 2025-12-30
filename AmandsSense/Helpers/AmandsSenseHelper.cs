using EFT;
using EFT.InventoryLogic;
using JsonType;
using SPT.Reflection.Utils;
using System;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace AmandsSense.Helpers
{
    public class AmandsSenseHelper
    {
        private static Type LocalizedType;
        private static MethodInfo LocalizedMethod;

        private static Type TransliterateType;
        private static MethodInfo TransliterateMethod;

        private static Type ToColorType;
        private static MethodInfo ToColorMethod;

        public static void Init()
        {
            LocalizedType = PatchConstants.EftTypes.Single((x) => x.GetMethod("ParseLocalization", BindingFlags.Static | BindingFlags.Public) != null);
            LocalizedMethod = LocalizedType.GetMethods().First((x) => x.Name == "Localized" && x.GetParameters().Length == 2 && x.GetParameters()[0].ParameterType == typeof(string) && x.GetParameters()[1].ParameterType == typeof(EStringCase));

            TransliterateType = PatchConstants.EftTypes.Single(x => x.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).Any(t => t.Name == "Transliterate"));
            TransliterateMethod = TransliterateType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).Single(x => x.Name == "Transliterate" && x.GetParameters().Length == 1);

            ToColorType = PatchConstants.EftTypes.Single((x) => x.GetMethod("ToColor", BindingFlags.Static | BindingFlags.Public) != null);
            ToColorMethod = ToColorType.GetMethods().First((x) => x.Name == "ToColor");

        }

        public static string Localized(string id, EStringCase @case)
        {
            return (string)LocalizedMethod.Invoke(null, new object[]
            {
                id,
                @case
            });
        }

        public static string Transliterate(string text)
        {
            return (string)TransliterateMethod.Invoke(null, new object[]
            {
                text
            });
        }

        public static Color ToColor(TaxonomyColor taxonomyColor)
        {
            return (Color)ToColorMethod.Invoke(null, new object[]
            {
                taxonomyColor
            });
        }

        public static bool IsNightVisionActive(Player player)
        {
            NightVisionComponent nvComponent = player.NightVisionObserver.Component;
            if (nvComponent != null && nvComponent.Togglable.On)
            {
                return true;
            }

            return false;
        }

        public static void SetTextOutline(TextMeshPro textMeshPro, float outlineWidth, Color outlineColor)
        {
            Material fontMaterial = textMeshPro.fontMaterial;
            fontMaterial.EnableKeyword("OUTLINE_ON");
            fontMaterial.SetFloat("_OutlineWidth", outlineWidth);
            fontMaterial.SetColor("_OutlineColor", outlineColor);

            textMeshPro.UpdateMeshPadding();
        }
    }
}
