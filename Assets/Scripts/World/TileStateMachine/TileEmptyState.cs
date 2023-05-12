using System;
using UnityEngine;
using static Oracle;

namespace World.TileStateMachine
{
    public class TileEmptyState : TileBaseState
    {
        public override void EnterState(TileManager tile)
        {
            Debug.Log("entered tileEmptyState");
        }

        public override void UpdateState(TileManager tile)
        {
        }

        public override void OnExitState(TileManager tile)
        {
            Debug.Log("Left EmptyState");
        }
    }
}