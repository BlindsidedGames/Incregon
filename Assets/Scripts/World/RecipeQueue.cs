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

        private void LateUpdate()
        {
            foreach (var recipe in recipeQueue)
            {
                var processResource = true;
                foreach (var expr in recipe.Ingredients)
                    if (expr.Value.resource > oracle.saveData.ownedResources[expr.Key].resource)
                        processResource = false;

                if (processResource)
                    foreach (var expr in recipe.Ingredients)
                    {
                        oracle.saveData.ownedResources[expr.Key].resource -= expr.Value.resource;
                        recipe.Tile.ProcessResources();
                    }
                else
                    failedRecipes.Add(recipe);
            }

            recipeQueue = failedRecipes;
            failedRecipes = new List<Recipe>();
        }

        #region Singleton class: BuildManager

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