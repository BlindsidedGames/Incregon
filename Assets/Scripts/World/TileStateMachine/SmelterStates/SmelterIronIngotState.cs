using System;
using static Oracle;

namespace World.TileStateMachine.SmelterStates
{
    public class SmelterIronIngotState : SmelterBaseState
    {
        private Resource resource;
        private Recipe recipe = new();
        private TileBalancing tileBalancingData;

        private bool isProcessing;

        public override void EnterState(TileManager tile)
        {
            tile.timerFillImage.color = tile.tileResourceImage.color;

            tileBalancingData = oracle.smelterBalancing[Resources.IronIngot];
            resource = tile.SetResource(Resources.IronIngot);
            recipe = new Recipe();

            tile.tileData.tileBalancing.tileBuildingTimerMax =
                tile.SetBuildingTimer(tileBalancingData.tileTimer.ResourceGatherTime);
            OnCompletionInfoUpdate(tile, resource.resource);

            switch (tile.tileData.tileBuildingData.buildingTier)
            {
                case BuildingTier.Tier1:
                    recipe.Ingredients.Add(Resources.Iron, new Resource { resource = 1 });
                    recipe.Tile = tile;
                    break;
                case BuildingTier.Tier2:
                    recipe.Ingredients.Add(Resources.Iron, new Resource { resource = 2 });
                    recipe.Tile = tile;
                    break;
                case BuildingTier.Tier3:
                    recipe.Ingredients.Add(Resources.Iron, new Resource { resource = 5 });
                    recipe.Tile = tile;
                    break;
            }
        }

        public override void UpdateState(TileManager tile)
        {
            isProcessing = RunBuilding(tile, tileBalancingData, recipe, isProcessing);
        }

        public override void OnExitState(TileManager tile)
        {
        }

        public override void ProcessResources(TileManager tile)
        {
            tile.tileData.tileBuildingTimer -= tile.tileData.tileBalancing.tileBuildingTimerMax;
            switch (tile.tileData.tileBuildingData.buildingTier)
            {
                case BuildingTier.Tier1:
                    resource.resource += 1;
                    break;
                case BuildingTier.Tier2:
                    resource.resource += 2;
                    break;
                case BuildingTier.Tier3:
                    resource.resource += 5;
                    break;
            }

            isProcessing = false;

            OnCompletionInfoUpdate(tile, resource.resource);
        }
    }
}