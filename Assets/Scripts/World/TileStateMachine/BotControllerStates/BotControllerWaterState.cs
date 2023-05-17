using System;
using UnityEngine;
using static Oracle;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerWaterState : BotControllerBaseState
    {
        private Resource resource;
        private TileBalancing tileBalancingData;

        public override void EnterState(TileManager tile)
        {
            tileBalancingData = oracle.tileBalancing[tile.tileResource];

            resource = tile.SetResource(Oracle.Resources.Water);
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