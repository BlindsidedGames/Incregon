using System.Collections.Generic;
using UnityEngine;
using Utilities;
using static Oracle;
using static World.RecipeQueue;

namespace World.TileStateMachine.SmelterStates
{
    public abstract class SmelterBaseState
    {
        public abstract void EnterState(TileSmelterState smelter);

        public abstract void UpdateState(TileSmelterState smelter);

        public abstract void OnExitState(TileSmelterState smelter);

        public abstract void ProcessResources(TileManager tile);

        public Data data => oracle.data;

        public void OnCompletionInfoUpdate(TileManager tile, double resource)
        {
            tile.resourcesText.text = CalcUtils.FormatNumber(resource);
            tile.levelFillImage.fillAmount = (float)tile.XpToLevel(tile.tileData.tileLevel.level,
                tile.tileData.tileLevel.experience);
            tile.levelText.text = $"Lvl: {tile.tileData.tileLevel.level}";
        }

        public bool RunBuilding(TileManager tile, TileBalancing tileBalancingData, TileCalculations tileBalance,
            Recipe recipe, bool isProcessing)
        {
            var timerData = tile.TimerInfo(tile.tileData.tileBuildingTimer,
                tileBalance.tileBuildingTimerMax);

            if (!isProcessing)
                switch (timerData.Item1)
                {
                    case true:
                        tile.tileData.tileLevel.experience += data.xpPerCompletion;
                        if (tile.Leveled(tile.tileData.tileLevel.level, tile.tileData.tileLevel.experience))
                        {
                            tile.tileData.tileLevel.experience -= tile.LevelCost(tile.tileData.tileLevel.level);
                            tile.tileData.tileLevel.level++;
                        }

                        tileBalance.tileBuildingTimerMax = tile.SetBuildingTimer(
                            tileBalancingData.tileTimer.ResourceGatherTime, tile.tileData.tileLevel.level);

                        recipeQueueStatic.recipeQueue.Add(recipe);
                        isProcessing = true;
                        break;
                    case false:
                        tile.tileData.tileBuildingTimer += Time.deltaTime;
                        break;
                }

            tile.timerFillImage.fillAmount = timerData.Item2;
            return isProcessing;
        }
    }
}