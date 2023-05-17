using UnityEngine;
using Utilities;
using static Oracle;

namespace World.TileStateMachine.PowerStates
{
    public abstract class PowerBaseState
    {
        private EnergyManagement em => oracle.saveData.energyManagement;
        public abstract void EnterState(TileManager tile);

        public abstract void UpdateState(TileManager tile);

        public abstract void OnExitState(TileManager tile);

        public Data data => oracle.data;

        public void OnCompletionInfoUpdate(TileManager tile)
        {
            tile.levelFillImage.fillAmount = (float)tile.XpToLevel(tile.tileData.tileLevel.level,
                tile.tileData.tileLevel.experience);

            tile.timerFillImage.fillAmount = (float)(em.energy / em.energyMax);
            tile.powerSatisfactionFillImage.fillAmount = (float)em.energyModifier;
            tile.resourcesText.text = CalcUtils.FormatEnergy(em.energy, true);
        }

        public void PowerBuildingExperience(TileManager tile)
        {
            tile.tileData.tileLevel.experience += data.xpPerCompletion * Time.deltaTime;
            if (tile.Leveled(tile.tileData.tileLevel.level, tile.tileData.tileLevel.experience))
            {
                tile.tileData.tileLevel.experience -= tile.LevelCost(tile.tileData.tileLevel.level);
                PowerManager.powerManager.RemoveProductionEntry(tile);
                tile.tileData.tileLevel.level++;
                PowerManager.powerManager.RegisterProductionEntry(tile);
                tile.levelText.text = $"Lvl: {tile.tileData.tileLevel.level}";
            }
        }
    }
}