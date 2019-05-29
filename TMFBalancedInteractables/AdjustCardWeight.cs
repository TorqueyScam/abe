﻿#define DEBUG
using BepInEx;
using BepInEx.Configuration;
using RoR2;
using System.Collections.Generic;

namespace paddywan
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.paddywan.TMFBalancedInteractables", "TMFBalancedInteractables", "1.0.0")]
    public class AdjustCardWeight : BaseUnityPlugin
    {
        private static ConfigWrapper<double> confScalar;

        public void Awake()
        {            
            On.RoR2.Console.Awake += (orig, self) =>
            {
                CommandHelper.RegisterCommands(self);
                orig(self);
            };

            confScalar = Config.Wrap("Scalars",
                "printerScalar",
                "1.0 = Reduces spawn-rate of printers for each extra player over 4. 16 players results in 25% of the normal spawns, 8 players results in 50% of the normal. Using a scalar value of 4.0 with 16 players results in no change to the spawnrate.",
                1.0
                );

            On.RoR2.DirectorCardCategorySelection.SumAllWeightsInCategory += (orig, self, category) =>
            {
                var numberOfPlayers = RoR2.Run.instance.participatingPlayerCount;
                if (numberOfPlayers > 4)
                {
                    AddInfoLog($"Making changes based on {numberOfPlayers} players");
                    var cardsToBeAdjusted = new List<string>
                    {
                        CardNames.Duplicator,
                        CardNames.DuplicatorLarge,
                        CardNames.DuplicatorMilitary,
                        CardNames.LunarChest
                    };
                    foreach (var card in cardsToBeAdjusted)
                    {
                        AddDebugLog($"Player count is > 4. Adjusting value for {card}");
                        AdjustSelectionWeight(category, card, numberOfPlayers);
                    }
                }
                else
                {
                    AddDebugLog("No changes made to weighting.");
                }
                float num = 0.0f;
                for (int index = 0; index < category.cards.Length; ++index)
                {
                    var name = category.cards[index].spawnCard.name;
                    int weight = category.cards[index].selectionWeight;
                    num += (float)category.cards[index].selectionWeight;
                }
                return num;
            };
        }

        private void AdjustSelectionWeight(DirectorCardCategorySelection.Category category, string cardName, int numberOfPlayers)
        {
            foreach(var card in category.cards)
            {
                if(card.spawnCard.name == cardName)
                {
                    var adjustmentConstant = (double)4 / (double)numberOfPlayers;
                    var adjustedWeight = (int)((double)card.selectionWeight * confScalar.Value * (double)adjustmentConstant);

                    if (adjustedWeight < 1)
                    {
                        AddInfoLog($"Set {cardName} = 1 (default) [down from {card.selectionWeight}]");
                        card.selectionWeight = 1;
                    }
                    else
                    {
                        AddInfoLog($"Set {cardName} = {adjustedWeight} [down from {card.selectionWeight}]");
                        card.selectionWeight = adjustedWeight;
                    }
                }
            }            
        }

        private void AddDebugLog(string message)
        {
#if DEBUG
            Logger.Log(BepInEx.Logging.LogLevel.Debug, message);
#endif
        }
        private void AddInfoLog(string message)
        {
#if DEBUG
            Logger.Log(BepInEx.Logging.LogLevel.Info, message);
#endif
        }
    }
}