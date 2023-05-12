using System;
using UnityEngine;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerIronState : BotControllerBaseState
    {
        public TileManager tile;

        public override void EnterState(TileBotControllerState botController)
        {
            tile = botController.tile;
            Debug.Log("hi I entered IRON");
        }

        public override void UpdateState(TileBotControllerState botController)
        {
            Debug.Log(botController.tile.tileData.tileBuilding.ToString());
        }

        public override void OnExitState(TileBotControllerState botController)
        {
            throw new NotImplementedException();
        }
    }
}