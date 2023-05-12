using System;
using UnityEngine;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerWoodState : BotControllerBaseState
    {
        public TileManager tile;

        public override void EnterState(TileBotControllerState botController)
        {
            tile = botController.tile;
            Debug.Log("hi I entered WOOD");
        }

        public override void UpdateState(TileBotControllerState botController)
        {
            Debug.Log("Updating Wood");
        }

        public override void OnExitState(TileBotControllerState botController)
        {
            throw new NotImplementedException();
        }
    }
}