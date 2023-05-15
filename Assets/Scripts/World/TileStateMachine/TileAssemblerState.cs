using System;
using UnityEngine;

namespace World.TileStateMachine
{
    public class TileAssemblerState : TileBaseState
    {
        public override void EnterState(TileManager tile)
        {
        }

        public override void UpdateState(TileManager tile)
        {
        }

        public override void OnExitState(TileManager tile)
        {
            throw new NotImplementedException();
        }

        public override void ProcessResources(TileManager tile)
        {
            throw new NotImplementedException();
        }
    }
}