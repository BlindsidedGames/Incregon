using System;
using System.Collections.Generic;
using UnityEngine;
using static Oracle;

namespace World
{
    public class PowerManager : MonoBehaviour
    {
        public List<TileManager> registeredBuilding = new();
        public List<TileManager> registeredProduction = new();
        private EnergyManagement energyManagement => oracle.saveData.energyManagement;

        private void Start()
        {
            energyManagement.energyMax = 0;
        }

        public void RegisterBuildingEntry(TileManager tile)
        {
            registeredBuilding.Add(tile);
            CalculatePowerLevels();
        }

        public void RegisterProductionEntry(TileManager tile)
        {
            registeredProduction.Add(tile);
            energyManagement.energyMax +=
                oracle.powerBuildingBalancing[tile.tileData.tileBuildingData.powerBuilding].storage;
            CalculatePowerLevels();
        }


        public void RemoveBuildingEntry(TileManager tile)
        {
            foreach (var building in registeredBuilding)
                if (building == tile)
                {
                    registeredBuilding.Remove(building);
                    break;
                }

            CalculatePowerLevels();
        }

        public void RemoveProductionEntry(TileManager tile)
        {
            foreach (var building in registeredProduction)
                if (building == tile)
                {
                    energyManagement.energyMax -=
                        oracle.powerBuildingBalancing[tile.tileData.tileBuildingData.powerBuilding].storage;
                    registeredProduction.Remove(building);
                    break;
                }

            CalculatePowerLevels();
        }

        private void Update()
        {
            var powerRating = Math.Clamp(energyManagement.energyPerSecond / energyManagement.energyConsumptionPerSecond,
                0, 1);

            energyManagement.energy = Math.Clamp(energyManagement.energy += (energyManagement.energyPerSecond -
                                                                             energyManagement
                                                                                 .energyConsumptionPerSecond) *
                                                                            Time.deltaTime, 0,
                energyManagement.energyMax);
            energyManagement.energyModifier = energyManagement.energy > 0 ? 1 : powerRating;
        }

        public void CalculatePowerLevels()
        {
            energyManagement.energyConsumptionPerSecond = 0;
            energyManagement.energyPerSecond = 0;
            foreach (var building in registeredBuilding)
                energyManagement.energyConsumptionPerSecond += building.buildingEnergyValue;

            foreach (var building in registeredProduction)
                energyManagement.energyPerSecond += building.buildingEnergyValue *
                                                    (1 + building.tileData.tileLevel.level * 0.05);
        }

        #region Singleton class: PowerManager

        public static PowerManager powerManager;


        private void Awake()
        {
            if (powerManager == null)
                powerManager = this;
            else
                Destroy(gameObject);
        }

        #endregion
    }
}