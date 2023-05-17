using UnityEngine;
using Utilities;
using static Oracle;
using static World.PowerManager;

namespace World.TileStateMachine.BotControllerStates
{
    public abstract class BotControllerBaseState
    {
        public abstract void EnterState(TileManager tile);

        public abstract void UpdateState(TileManager tile);

        public abstract void OnExitState(TileManager tile);


        public Data data => oracle.data;
        private EnergyManagement em => oracle.saveData.energyManagement;

        public void RegisterBuilding(TileManager tile)
        {
            powerManager.RegisterBuildingEntry(tile);
        }

        public void DeregisterBuilding(TileManager tile)
        {
            powerManager.RemoveBuildingEntry(tile);
        }

        public void OnCompletionInfoUpdate(TileManager tile, double resource, bool set = true)
        {
            tile.resourcesText.text = set ? CalcUtils.FormatNumber(resource) : "Not Set";
            tile.levelFillImage.fillAmount = (float)tile.XpToLevel(tile.tileData.tileLevel.level,
                tile.tileData.tileLevel.experience);
            tile.levelText.text = $"Lvl: {tile.tileData.tileLevel.level}";
            if (!set) tile.timerFillImage.fillAmount = 0;
        }

        public void RunBuilding(TileManager tile, TileBalancing tileBalancingData,
            Resource resource)
        {
            var timerData = tile.TimerInfo(tile.tileData.tileBuildingTimer,
                tile.tileData.tileBalancing.tileBuildingTimerMax);

            switch (timerData.Item1)
            {
                case true:
                    OnCompletionInfoUpdate(tile, resource.resource);
                    resource.resource++;
                    tile.tileData.tileLevel.experience += data.xpPerCompletion;
                    if (tile.Leveled(tile.tileData.tileLevel.level, tile.tileData.tileLevel.experience))
                    {
                        tile.tileData.tileLevel.experience -= tile.LevelCost(tile.tileData.tileLevel.level);
                        tile.tileData.tileLevel.level++;
                    }

                    tile.tileData.tileBuildingTimer -=
                        tile.tileData.tileBalancing.tileBuildingTimerMax;

                    tile.tileData.tileBalancing.tileBuildingTimerMax =
                        tile.SetBuildingTimer(tileBalancingData.tileTimer.ResourceGatherTime);
                    break;
                case false:
                    tile.tileData.tileBuildingTimer += Time.deltaTime * (float)em.energyModifier;
                    break;
            }

            tile.timerFillImage.fillAmount = timerData.Item2;
        }
    }
}