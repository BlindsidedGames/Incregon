﻿using System;
using static Oracle;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerIronState : BotControllerBaseState
    {
        private Resource resource;
        private TileBalancing tileBalancingData;

        public override void EnterState(TileManager tile)
        {
            tileBalancingData = oracle.tileBalancing[tile.tileResource];

            resource = tile.SetResource(Resources.Iron);
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