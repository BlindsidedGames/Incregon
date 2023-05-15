using UnityEngine;
using Utilities;
using static Oracle;

namespace World.TileStateMachine.PowerStates
{
    public abstract class PowerBaseState
    {
        public abstract void EnterState(TileBotControllerState botController);

        public abstract void UpdateState(TileBotControllerState botController);

        public abstract void OnExitState(TileBotControllerState botController);

        public Data data => oracle.data;
    }
}