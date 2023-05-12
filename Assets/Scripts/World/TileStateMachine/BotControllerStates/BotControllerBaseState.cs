using UnityEngine;
using static Oracle;

namespace World.TileStateMachine.BotControllerStates
{
    public abstract class BotControllerBaseState
    {
        public abstract void EnterState(TileBotControllerState botController);

        public abstract void UpdateState(TileBotControllerState botController);

        public abstract void OnExitState(TileBotControllerState botController);


        public Oracle.Resources resources => oracle.saveData.resources;

        public (bool, float) RunTimer(float currentTime, float maxTime)
        {
            var completionPercent = currentTime / maxTime;
            return (completionPercent >= 1, completionPercent);
        }
    }
}