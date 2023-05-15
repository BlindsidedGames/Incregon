using static Oracle;
using UnityEngine;
using Utilities;

namespace World.TileStateMachine.AssemblerStates
{
    public abstract class AssemblerBaseState
    {
        public abstract void EnterState(TileBotControllerState botController);

        public abstract void UpdateState(TileBotControllerState botController);

        public abstract void OnExitState(TileBotControllerState botController);

        public Data data => oracle.data;
    }
}