using System;
using static Oracle;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerHydrogenState : BotControllerBaseState
    {
        private Resource resource;
        private TileBalancing tileBalancingData;

        public override void EnterState(TileManager tile)
        {
            tileBalancingData = oracle.tileBalancing[tile.tileResource];

            resource = tile.SetResource(Resources.Hydrogen);
            tile.timerFillImage.color = tile.tileResourceImage.color;
            OnCompletionInfoUpdate(tile, resource.resource);
        }

        public override void UpdateState(TileManager tile)
        {
            RunBuilding(tile, tileBalancingData, resource);
        }

        public override void OnExitState(TileManager tile)
        {
            tile.tileData.tileBuildingTimer = 0;
            OnCompletionInfoUpdate(tile, 0, false);
        }
    }
}