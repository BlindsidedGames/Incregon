using System;
using UnityEngine;
using World.TileStateMachine.BotControllerStates;
using static Oracle;

namespace World.TileStateMachine
{
    public class TileBotControllerState : TileBaseState
    {
        public TileManager tile;

        public BotControllerBaseState currentState;
        public BotControllerWoodState woodState = new();
        public BotControllerIronState ironState = new();
        public BotControllerCopperState copperState = new();
        public BotControllerCoalState coalState = new();
        public BotControllerStoneState stoneState = new();
        public BotControllerSiliconState siliconState = new();
        public BotControllerTitaniumState titaniumState = new();
        public BotControllerUraniumState uraniumState = new();
        public BotControllerRareMetalsState rareMetalsState = new();
        public BotControllerOilState oilState = new();
        public BotControllerSulfuricAcidState sulfuricAcidState = new();
        public BotControllerWaterState waterState = new();
        public BotControllerHydrogenState hydrogenState = new();

        public override void EnterState(TileManager tile)
        {
            this.tile = tile;
            switch (tile.tileResource)
            {
                case TileResource.Wood:
                    currentState = woodState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Iron:
                    currentState = ironState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Copper:
                    currentState = copperState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Coal:
                    currentState = coalState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Stone:
                    currentState = stoneState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Silicon:
                    currentState = siliconState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Titanium:
                    currentState = titaniumState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Uranium:
                    currentState = uraniumState;
                    currentState.EnterState(this);
                    break;
                case TileResource.RareMetals:
                    currentState = rareMetalsState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Oil:
                    currentState = oilState;
                    currentState.EnterState(this);
                    break;
                case TileResource.SulfuricAcid:
                    currentState = sulfuricAcidState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Water:
                    currentState = waterState;
                    currentState.EnterState(this);
                    break;
                case TileResource.Hydrogen:
                    currentState = hydrogenState;
                    currentState.EnterState(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void UpdateState(TileManager tile)
        {
            currentState.UpdateState(this);
        }

        public override void OnExitState(TileManager tile)
        {
        }

        public override void ProcessResources(TileManager tile)
        {
            throw new NotImplementedException();
        }
    }
}