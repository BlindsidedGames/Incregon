using System;
using UnityEngine;

namespace World.TileStateMachine.BotControllerStates
{
    public class BotControllerCoalState : BotControllerBaseState
    {
        public TileManager tile;

        public override void EnterState(TileBotControllerState botController)
        {
            tile = botController.tile;
        }

        public override void UpdateState(TileBotControllerState botController)
        {
            var timerData = TimerInfo(tile.tileData.tileBuildingTimer,
                tile.timerBalancing.coalTimerData.baseResourceGatherTime);

            switch (timerData.Item1)
            {
                case true:
                    resources.coal++;
                    tile.tileData.tileBuildingTimer -= tile.timerBalancing.coalTimerData.baseResourceGatherTime;
                    break;
                case false:
                    tile.tileData.tileBuildingTimer += Time.deltaTime;
                    break;
            }

            tile.timerFillImage.fillAmount = timerData.Item2;
        }

        public override void OnExitState(TileBotControllerState botController)
        {
            throw new NotImplementedException();
        }
    }
}