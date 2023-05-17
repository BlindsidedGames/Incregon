using System.Collections.Generic;
using UnityEngine;
using Utilities;
using static Oracle;
using static World.RecipeQueue;
using static World.PowerManager;

namespace World.TileStateMachine.SmelterStates
{
    public abstract class SmelterBaseState
    {
        public abstract void EnterState(TileManager tile);

        public abstract void UpdateState(TileManager tile);

        public abstract void OnExitState(TileManager tile);

        public abstract void ProcessResources(TileManager tile);

        public Data data => oracle.data;
        private EnergyManagement em => oracle.saveData.energyManagement;

        public void OnCompletionInfoUpdate(TileManager tile, double resource, bool set = true)
        {
            tile.resourcesText.text = set ? CalcUtils.FormatNumber(resource) : "Not Set";
            tile.levelFillImage.fillAmount = (float)tile.XpToLevel(tile.tileData.tileLevel.level,
                tile.tileData.tileLevel.experience);
            tile.levelText.text = $"Lvl: {tile.tileData.tileLevel.level}";
            if (!set) tile.timerFillImage.fillAmount = 0;
        }

        public void RegisterBuilding(TileManager tile)
        {
            powerManager.RegisterBuildingEntry(tile);
        }

        public void DeregisterBuilding(TileManager tile)
        {
            powerManager.RemoveBuildingEntry(tile);
        }

        public bool RunBuilding(TileManager tile, TileBalancing tileBalancingData,
            Recipe recipe, bool isProcessing)
        {
            var timerData = tile.TimerInfo(tile.tileData.tileBuildingTimer,
                tile.tileData.tileBalancing.tileBuildingTimerMax);

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

                        tile.tileData.tileBalancing.tileBuildingTimerMax =
                            tile.SetBuildingTimer(tileBalancingData.tileTimer.ResourceGatherTime);

                        recipeQueueStatic.recipeQueue.Add(recipe);
                        isProcessing = true;
                        break;
                    case false:
                        tile.tileData.tileBuildingTimer += Time.deltaTime * (float)em.energyModifier;
                        break;
                }

            tile.timerFillImage.fillAmount = timerData.Item2;
            return isProcessing;
        }
    }
}