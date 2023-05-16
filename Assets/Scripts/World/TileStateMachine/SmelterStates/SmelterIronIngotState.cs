using System;
using static Oracle;
using static World.RecipeQueue;

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
            recipe.Ingredients = oracle.Recipes[Resources.IronIngot].Ingredients;

            tile.tileData.tileBalancing.tileBuildingTimerMax =
                tile.SetBuildingTimer(tileBalancingData.tileTimer.ResourceGatherTime);
            OnCompletionInfoUpdate(tile, resource.resource);


            recipe.Tile = tile;
        }

        public override void UpdateState(TileManager tile)
        {
            isProcessing = RunBuilding(tile, tileBalancingData, recipe, isProcessing);
        }

        public override void OnExitState(TileManager tile)
        {
            recipeQueueStatic.RemoveEntry(tile);
            tile.tileData.tileBuildingTimer = 0;
            OnCompletionInfoUpdate(tile, 0, false);
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