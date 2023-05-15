using System;
using static Oracle;

namespace World.TileStateMachine.SmelterStates
{
    public class SmelterIronIngotState : SmelterBaseState
    {
        private Resource resource;
        private readonly Recipe recipe = new();
        private TileBalancing tileBalancingData;
        private TileCalculations tileBalancing;

        private bool isProcessing;

        public override void EnterState(TileManager tile)
        {
            tileBalancing = tile.tileData.tileBalancing;
            tileBalancingData = oracle.smelterBalancing[Resources.IronIngot];

            if (!oracle.saveData.ownedResources.ContainsKey(Resources.IronIngot))
                oracle.saveData.ownedResources.Add(Resources.IronIngot, new Resource());
            resource = oracle.saveData.ownedResources[Resources.IronIngot];

            tile.timerFillImage.color = tile.tileResourceImage.color;
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
            isProcessing = RunBuilding(tile, tileBalancingData, tileBalancing, recipe, isProcessing);
        }

        public override void OnExitState(TileManager tile)
        {
        }

        public override void ProcessResources(TileManager tile)
        {
            tile.tileData.tileBuildingTimer -= tileBalancing.tileBuildingTimerMax;
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