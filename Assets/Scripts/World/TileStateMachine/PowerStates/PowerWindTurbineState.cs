using System;
using UnityEngine;
using static World.PowerManager;

namespace World.TileStateMachine.PowerStates
{
    public class PowerWindTurbineState : PowerBaseState
    {
        public override void EnterState(TileManager tile)
        {
            tile.levelText.text = $"Lvl: {tile.tileData.tileLevel.level}";
        }

        public override void UpdateState(TileManager tile)
        {
            OnCompletionInfoUpdate(tile);
            PowerBuildingExperience(tile);
        }

        public override void OnExitState(TileManager tile)
        {
            powerManager.RemoveProductionEntry(tile);
        }
    }
}