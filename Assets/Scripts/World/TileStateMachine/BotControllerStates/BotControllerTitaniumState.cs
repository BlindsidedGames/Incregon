using System;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerTitaniumState : BotControllerBaseState
    {
        public TileManager tile;

        public override void EnterState(TileBotControllerState botController)
        {
            tile = botController.tile;
            throw new NotImplementedException();
        }

        public override void UpdateState(TileBotControllerState botController)
        {
            throw new NotImplementedException();
        }

        public override void OnExitState(TileBotControllerState botController)
        {
            throw new NotImplementedException();
        }
    }
}