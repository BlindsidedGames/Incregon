using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using World.TileStateMachine;
using static Oracle;
using static BuildingManager;

public class TileManager : MonoBehaviour
{
    public TileResource tileResource;
    public int tileID;
    public Tile tileData;

    [SerializeField] private Button tileButton;
    [Space(10)] [SerializeField] private GameObject[] allBuildings;
    [SerializeField] private GameObject BotController;
    [SerializeField] private GameObject Smelter;

    [SerializeField] private GameObject buildingLevelInfo;
    public Image timerFillImage;
    public TMP_Text resourcesText;
    public Image levelFillImage;
    public TMP_Text levelText;

    public Image tileResourceImage;


    private void Start()
    {
        if (!oracle.saveData.Tiles.ContainsKey(tileID)) oracle.saveData.Tiles.Add(tileID, new Tile());
        tileData = oracle.saveData.Tiles[tileID];
        NullChecks();

        tileButton.onClick.AddListener(OpenTileMenu);

        currentState = emptyState;
        currentState.EnterState(this);
        BuildBuilding(tileData.tileBuilding);
    }

    private void NullChecks()
    {
        tileData.tileBuildingData ??= new TileBuildingData();
    }

    private void OpenTileMenu()
    {
        buildingManager.tileManager = this;
        switch (tileData.tileBuilding)
        {
            case TileBuilding.None:
                if (tileResource == TileResource.None) buildingManager.botControllerButton.interactable = false;
                else buildingManager.botControllerButton.interactable = true;
                buildingManager.OpenMenu(TileBuilding.None);
                break;
            case TileBuilding.Smelter:
                buildingManager.OpenMenu(TileBuilding.Smelter);
                break;
        }
    }

    public void SellBuilding()
    {
        tileData.tileBuilding = TileBuilding.None;
        foreach (var building in allBuildings) building.SetActive(false);
        tileData.tileLevel = new LevelingData();
        buildingLevelInfo.SetActive(false);
        SwitchState(emptyState);
        buildingManager.CloseMenu();
    }

    public void BuildBuilding(TileBuilding buildingToBuild)
    {
        tileData.tileBuilding = buildingToBuild;
        switch (tileData.tileBuilding)
        {
            case TileBuilding.BotController:
                foreach (var building in allBuildings) building.SetActive(false);
                BotController.SetActive(true);
                buildingLevelInfo.SetActive(true);
                SwitchState(botControllerState);
                break;
            case TileBuilding.Smelter:
                foreach (var building in allBuildings) building.SetActive(false);
                BotController.SetActive(true);
                Smelter.SetActive(true);
                buildingLevelInfo.SetActive(true);
                SwitchState(smelterState);
                break;
        }
    }

    #region StateStuff

    public TileBaseState currentState;
    public TileEmptyState emptyState = new();
    public TileBotControllerState botControllerState = new();
    public TileSmelterState smelterState = new();
    public TileChemicalPlantState chemicalPlantState = new();
    public TileAssemblerState assemblerState = new();
    public TileSpacePortState spacePortState = new();
    public TileObservatoryState observatoryState = new();
    public TileResearchStationState researchStationState = new();
    public TileRailgunState railgunState = new();
    public TilePowerState powerState = new();

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(TileBaseState state)
    {
        currentState.OnExitState(this);
        currentState = state;
        state.EnterState(this);
    }

    public void ProcessResources()
    {
        currentState.ProcessResources(this);
    }

    #endregion

    #region SharedFunctions

    public (bool, float) TimerInfo(float currentTime, float maxTime)
    {
        var completionPercent = currentTime / maxTime;
        return (completionPercent >= 1, completionPercent);
    }

    public float SetBuildingTimer(float balancingTime, float buildingLevel)
    {
        return balancingTime / (1 + buildingLevel * 0.1f);
    }

    #region Levelling

    public bool Leveled(long level, double xp)
    {
        return xp >= LevelCost(level);
    }

    public double LevelCost(long level)
    {
        return CalcUtils.BuyX(1, oracle.data.xpForFirstLevel, oracle.data.exponent, level);
    }

    public double XpToLevel(long currentLevel, double currentXp)
    {
        return currentXp / LevelCost(currentLevel);
    }

    #endregion

    #endregion
}