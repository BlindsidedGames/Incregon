using System;
using UnityEngine;
using World.TileStateMachine.SmelterStates;
using static Oracle;
using static World.PowerManager;

namespace World.TileStateMachine
{
    public class TileSmelterState : TileBaseState
    {
        public SmelterBaseState currentState;
        public SmelterEmptyState emptyState = new();
        public SmelterIronIngotState ironIngotState = new();

        public override void EnterState(TileManager tile)
        {
            tile.smelterImageGameObject.SetActive(true);
            tile.CalculateEnergyRequirement();
            switch (tile.tileData.tileBuildingData.smelterRecipe)
            {
                case SmelterRecipe.None:
                    tile.buildingEnergyValue = 0;
                    currentState = emptyState;
                    currentState.EnterState(tile);
                    break;
                case SmelterRecipe.IronIngot:
                    currentState = ironIngotState;
                    currentState.EnterState(tile);
                    break;
            }
        }


        public override void UpdateState(TileManager tile)
        {
            currentState.UpdateState(tile);
        }

        public void SwitchState(SmelterBaseState state, TileManager tile)
        {
            currentState.OnExitState(tile);
            tile.DisableAllSmelterImages();
            currentState = state;
            EnterState(tile);
        }

        public override void OnExitState(TileManager tile)
        {
            currentState.OnExitState(tile);
            tile.smelterImageGameObject.SetActive(false);
        }

        public override void ProcessResources(TileManager tile)
        {
            currentState.ProcessResources(tile);
        }
    }
}