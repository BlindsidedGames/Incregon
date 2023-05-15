using UnityEngine;
using Utilities;
using static Oracle;

namespace World.TileStateMachine.ChemicalPlantStates
{
    public abstract class ChemicalPlantBaseState
    {
        public abstract void EnterState(TileBotControllerState botController);

        public abstract void UpdateState(TileBotControllerState botController);

        public abstract void OnExitState(TileBotControllerState botController);

        public Data data => oracle.data;
    }
}