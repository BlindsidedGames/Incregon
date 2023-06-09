﻿namespace World.TileStateMachine.SmelterStates
{
    public class SmelterEmptyState : SmelterBaseState
    {
        public override void EnterState(TileManager tile)
        {
            OnCompletionInfoUpdate(tile, 0, false);
        }

        public override void UpdateState(TileManager tile)
        {
        }

        public override void OnExitState(TileManager tile)
        {
        }

        public override void ProcessResources(TileManager tile)
        {
        }
    }
}