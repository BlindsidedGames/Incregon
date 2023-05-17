using System;
using UnityEngine;
using World.TileStateMachine.PowerStates;
using static Oracle;
using static World.PowerManager;

namespace World.TileStateMachine
{
    public class TilePowerState : TileBaseState
    {
        public PowerBaseState currentState;
        public PowerWindTurbineState windTurbineState = new();
        public PowerSolarPanelState solarPanelState = new();

        public override void EnterState(TileManager tile)
        {
            switch (tile.tileData.tileBuildingData.powerBuilding)
            {
                case PowerBuilding.WindTurbine:
                    tile.buildingEnergyValue = oracle.powerBuildingBalancing[PowerBuilding.WindTurbine].production;
                    powerManager.RegisterProductionEntry(tile);
                    currentState = windTurbineState;
                    currentState.EnterState(tile);
                    break;
                case PowerBuilding.SolarPanel:
                    tile.buildingEnergyValue = oracle.powerBuildingBalancing[PowerBuilding.SolarPanel].production;
                    powerManager.RegisterProductionEntry(tile);
                    currentState = solarPanelState;
                    currentState.EnterState(tile);
                    break;
            }
        }

        public override void UpdateState(TileManager tile)
        {
            currentState.UpdateState(tile);
        }

        public override void OnExitState(TileManager tile)
        {
            currentState.OnExitState(tile);
        }

        public void SwitchState(PowerBaseState state, TileManager tile)
        {
            currentState.OnExitState(tile);
            currentState = state;
            EnterState(tile);
        }

        public override void ProcessResources(TileManager tile)
        {
        }
    }
}