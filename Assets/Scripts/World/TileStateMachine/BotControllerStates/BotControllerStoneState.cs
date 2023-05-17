using System;
using static Oracle;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerStoneState : BotControllerBaseState
    {
        private Resource resource;
        private TileBalancing tileBalancingData;
        private TileCalculations tileBalancing;

        public override void EnterState(TileManager tile)
        {
            tileBalancingData = oracle.tileBalancing[tile.tileResource];
            tileBalancing = tile.tileData.tileBalancing;

            resource = tile.SetResource(Resources.Stone);
            tile.timerFillImage.color = tile.tileResourceImage.color;
            OnCompletionInfoUpdate(tile, resource.resource);
            RegisterBuilding(tile);
        }

        public override void UpdateState(TileManager tile)
        {
            RunBuilding(tile, tileBalancingData, resource);
        }

        public override void OnExitState(TileManager tile)
        {
            tile.tileData.tileBuildingTimer = 0;
            DeregisterBuilding(tile);
            OnCompletionInfoUpdate(tile, 0, false);
        }
    }
}