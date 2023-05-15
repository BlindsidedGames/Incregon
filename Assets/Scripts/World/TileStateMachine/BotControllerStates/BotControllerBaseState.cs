using UnityEngine;
using Utilities;
using static Oracle;

namespace World.TileStateMachine.BotControllerStates
{
    public abstract class BotControllerBaseState
    {
        public abstract void EnterState(TileManager tile);

        public abstract void UpdateState(TileManager tile);

        public abstract void OnExitState(TileManager tile);

        public Data data => oracle.data;

        public void OnCompletionInfoUpdate(TileManager tile, double resource)
        {
            tile.resourcesText.text = CalcUtils.FormatNumber(resource);
            tile.levelFillImage.fillAmount = (float)tile.XpToLevel(tile.tileData.tileLevel.level,
                tile.tileData.tileLevel.experience);
            tile.levelText.text = $"Lvl: {tile.tileData.tileLevel.level}";
        }

        public void RunBuilding(TileManager tile, TileBalancing tileBalancingData, TileCalculations tileBalance,
            Resource resource)
        {
            var timerData = tile.TimerInfo(tile.tileData.tileBuildingTimer,
                tileBalance.tileBuildingTimerMax);

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
                        tileBalance.tileBuildingTimerMax;

                    tileBalance.tileBuildingTimerMax = tile.SetBuildingTimer(
                        tileBalancingData.tileTimer.ResourceGatherTime, tile.tileData.tileLevel.level);
                    break;
                case false:
                    tile.tileData.tileBuildingTimer += Time.deltaTime;
                    break;
            }

            tile.timerFillImage.fillAmount = timerData.Item2;
        }
    }
}