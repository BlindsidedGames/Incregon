using System;
using UnityEngine;
using World.TileStateMachine.SmelterStates;
using static Oracle;

namespace World.TileStateMachine
{
    public class TileSmelterState : TileBaseState
    {
        public TileManager tile;

        public SmelterBaseState currentState;
        public SmelterEmptyState emptyState = new();
        public SmelterIronIngotState ironIngotState = new();

        public override void EnterState(TileManager tile)
        {
            this.tile = tile;
            switch (tile.tileData.tileBuildingData.smelterRecipe)
            {
                case SmelterRecipe.None:
                    currentState = emptyState;
                    currentState.EnterState(this);
                    break;
                case SmelterRecipe.IronIngot:
                    currentState = ironIngotState;
                    currentState.EnterState(this);
                    break;
            }
        }

        public override void UpdateState(TileManager tile)
        {
            currentState.UpdateState(this);
        }

        public override void OnExitState(TileManager tile)
        {
        }

        public override void ProcessResources(TileManager tile)
        {
            currentState.ProcessResources(tile);
        }
    }
}