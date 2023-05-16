using System;
using System.Collections.Generic;
using UnityEngine;
using static Oracle;

namespace World
{
    public class RecipeQueue : MonoBehaviour
    {
        public List<Recipe> recipeQueue = new();
        public List<Recipe> failedRecipes = new();


        public void RemoveEntry(TileManager tile)
        {
            foreach (var recipe in recipeQueue)
                if (recipe.Tile == tile)
                {
                    recipeQueue.Remove(recipe);
                    break;
                }
        }

        private void LateUpdate()
        {
            try
            {
                foreach (var recipe in recipeQueue)
                    if (CanProcess(recipe))
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            var resource = oracle.saveData.ownedResources[ingredient.Key];
                            resource.resource -= ingredient.Value.resource * resource.costMultiplier;
                            oracle.saveData.ownedResources[recipe.Output].resource += recipe.OutputAmount;
                            recipe.Tile.ProcessResources();
                        }
                    else
                        failedRecipes.Add(recipe);

                recipeQueue = failedRecipes;
                failedRecipes = new List<Recipe>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static bool CanProcess(Recipe recipe)
        {
            var processResource = true;
            foreach (var ingredient in recipe.Ingredients)
            {
                var resource = oracle.saveData.ownedResources[ingredient.Key];
                switch (recipe.Tile.tileData.tileBuildingData.buildingTier)
                {
                    case BuildingTier.Tier1:
                        processResource = ingredient.Value.Tier1 * resource.costMultiplier < resource.resource;
                        break;
                    case BuildingTier.Tier2:
                        processResource = ingredient.Value.Tier2 * resource.costMultiplier < resource.resource;
                        break;
                    case BuildingTier.Tier3:
                        processResource = ingredient.Value.Tier3 * resource.costMultiplier < resource.resource;
                        break;
                }

                if (!processResource) break;
            }

            return processResource;
        }

        #region Singleton class: RecipeQueue

        public static RecipeQueue recipeQueueStatic;


        private void Awake()
        {
            if (recipeQueueStatic == null)
                recipeQueueStatic = this;
            else
                Destroy(gameObject);
        }

        #endregion
    }
}