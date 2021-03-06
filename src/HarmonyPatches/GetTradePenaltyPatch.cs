﻿using System;
using HarmonyLib;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;

namespace TyniBannerlordFixes
{
    [HarmonyPatch(typeof(DefaultTradeItemPriceFactorModel), "GetTradePenalty")]
    public class GetTradePenaltyPatch
    {
        static void Postfix(DefaultTradeItemPriceFactorModel __instance, ItemObject item, MobileParty clientParty, PartyBase merchant, bool isSelling, float inStore, float supply, float demand, ref float __result)
        {
            if (clientParty != null && clientParty.LeaderHero == Hero.MainHero && !item.IsTradeGood && !item.IsAnimal)
            {
                __result *= ConfigLoader.Instance.Config.PlayerEquipmentTradePenaltyMultiplier;
            }
        }

        static bool Prepare()
        {
            return ConfigLoader.Instance.Config.PlayerEquipmentTradePenaltyMultiplier != 1.0f; 
        }
        
        static void Finalizer(Exception __exception)
        {
            if (__exception != null)
            {
                MessageBox.Show(__exception.FlattenException());
            }
        }
    }
}
