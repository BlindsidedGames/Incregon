using System;
using static Oracle;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerCoalState : BotControllerBaseState
    {
        private Resource resource;
        private TileBalancing tileBalancingData;
        private TileCalculations tileBalancing;

        public override void EnterState(TileManager tile)
        {
            tileBalancingData = oracle.tileBalancing[tile.tileResource];
            tileBalancing = tile.tileData.tileBalancing;

            if (!oracle.saveData.ownedResources.ContainsKey(Resources.Coal))
                oracle.saveData.ownedResources.Add(Resources.Coal, new Resource());
            resource = oracle.saveData.ownedResources[Resources.Coal];

            tile.timerFillImage.color = tile.tileResourceImage.color;
            OnCompletionInfoUpdate(tile, resource.resource);
        }

        public override void UpdateState(TileManager tile)
        {
            RunBuilding(tile, tileBalancingData, tileBalancing, resource);
        }


        public override void OnExitState(TileManager tile)
        {
            throw new NotImplementedException();
        }
    }
}