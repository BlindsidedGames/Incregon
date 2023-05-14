using System;
using static Oracle;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerOilState : BotControllerBaseState
    {
        public TileManager tile;
        private Resource resource;
        private TileBalancing tileBalancingData;
        private TileCalculations tileBalancing;

        public override void EnterState(TileBotControllerState botController)
        {
            tile = botController.tile;
            tileBalancingData = oracle.tileBalancing[tile.tileResource];
            tileBalancing = tile.tileData.tileBalancing;

            if (!oracle.saveData.TileResourcesOwned.ContainsKey(tile.tileResource))
                oracle.saveData.TileResourcesOwned.Add(tile.tileResource, new Resource());
            resource = oracle.saveData.TileResourcesOwned[tile.tileResource];
            tile.timerFillImage.color = tile.tileResourceImage.color;
            OnCompletionInfoUpdate(tile, resource.resource);
        }

        public override void UpdateState(TileBotControllerState botController)
        {
            RunBuilding(tile, tileBalancingData, tileBalancing, resource);
        }

        public override void OnExitState(TileBotControllerState botController)
        {
            throw new NotImplementedException();
        }
    }
}