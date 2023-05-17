using System;
using static World.PowerManager;

namespace World.TileStateMachine.PowerStates
{
    public class PowerSolarPanelState : PowerBaseState
    {
        public override void EnterState(TileManager tile)
        {
            throw new NotImplementedException();
        }

        public override void UpdateState(TileManager tile)
        {
            throw new NotImplementedException();
        }

        public override void OnExitState(TileManager tile)
        {
            powerManager.RemoveProductionEntry(tile);
        }
    }
}