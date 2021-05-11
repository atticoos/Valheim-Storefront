// JotunnModStub
// a Valheim mod skeleton using Jötunn
// 
// File:    JotunnModStub.cs
// Project: JotunnModStub

using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using Jotunn.Utils;
using Jotunn.Entities;
using Jotunn.Configs;
using Jotunn.Managers;
using HarmonyLib;
using System.Collections.Generic;

namespace JotunnModStub
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class JotunnModStub : BaseUnityPlugin
    {
        public const string PluginGUID = "com.atticoos.valheim.storefront";
        public const string PluginName = "Storefront";
        public const string PluginVersion = "0.2";
        private readonly Harmony harmony = new Harmony(PluginGUID);

        private void Awake()
        {
            // Do all your init stuff here
            // Acceptable value ranges can be defined to allow configuration via a slider in the BepInEx ConfigurationManager: https://github.com/BepInEx/BepInEx.ConfigurationManager
            Config.Bind<int>("Main Section", "Example configuration integer", 1, new ConfigDescription("This is an example config, using a range limitation for ConfigurationManager", new AcceptableValueRange<int>(0, 100)));

            Jotunn.Logger.LogInfo("Storefront mod has landed");

            harmony.PatchAll();

            ItemManager.OnVanillaItemsAvailable += initializePieces;
        }

       private void initializePieces()
        {
            Jotunn.Logger.LogWarning("Storefront - registering pieces");
            StorefrontPieces.InitializeAllPieces();
            ItemManager.OnVanillaItemsAvailable -= initializePieces;
        }


        [HarmonyPatch(typeof(Container))]
        class StorefrontChestPiece
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(Container.Interact))]
            private static bool InteractPrefix (ref Container __instance)
            {
                if (!StorefrontPieces.IsPointOfSalePiece(__instance.m_piece))
                {
                    return true;
                }

                StoreManager.GetInstance().TogglePointOfSale(__instance);
                return false;
            }
        }
        public void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}