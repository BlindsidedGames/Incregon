using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
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
            foreach (var recipe in recipeQueue)
                if (CalcUtils.CanProcess(recipe))
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