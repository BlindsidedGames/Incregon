using System.Text;
using UnityEngine;
using UnityEngine.UI;
using World.TileStateMachine.SmelterStates;
using static Oracle;

public class BuildingManager : MonoBehaviour
{
    public TileManager tileManager;
    [SerializeField] private GameObject buildingMenu;
    [SerializeField] private GameObject SmelterMenu;

    public Button botControllerButton;
    public Button smelterButton;
    [SerializeField] private Button sellBuilding;
    [SerializeField] private Button sellSmelter;
    [Space(10)] [SerializeField] private Button smelterIronIngotButton;
    [SerializeField] private Button smelterNoneButton;

    private void Start()
    {
        botControllerButton.onClick.AddListener(() => BuildBuilding(TileBuilding.BotController));
        smelterButton.onClick.AddListener(() => BuildBuilding(TileBuilding.Smelter));
        smelterIronIngotButton.onClick.AddListener(() =>
        {
            tileManager.tileData.tileBuildingData.smelterRecipe = SmelterRecipe.IronIngot;
            tileManager.smelterState.EnterState(tileManager);
            SmelterMenu.SetActive(false);
        });
        smelterNoneButton.onClick.AddListener(() =>
        {
            tileManager.tileData.tileBuildingData.smelterRecipe = SmelterRecipe.None;
            tileManager.smelterState.EnterState(tileManager);
            SmelterMenu.SetActive(false);
        });
        sellSmelter.onClick.AddListener(() =>
        {
            tileManager.tileData.tileBuildingData.smelterRecipe = SmelterRecipe.None;
            tileManager.SellBuilding();
        });
        sellBuilding.onClick.AddListener(() => tileManager.SellBuilding());
    }

    public void OpenMenu(TileBuilding building)
    {
        switch (building)
        {
            case TileBuilding.None:
                buildingMenu.SetActive(true);
                break;
            case TileBuilding.Smelter:
                SmelterMenu.SetActive(true);
                break;
        }
    }

    public void CloseMenu()
    {
        buildingMenu.SetActive(false);
        SmelterMenu.SetActive(false);
    }

    private void BuildBuilding(TileBuilding building)
    {
        tileManager.BuildBuilding(building);
        buildingMenu.SetActive(false);
        tileManager = null;
    }

    #region Singleton class: BuildManager

    public static BuildingManager buildingManager;


    private void Awake()
    {
        if (buildingManager == null)
            buildingManager = this;
        else
            Destroy(gameObject);
    }

    #endregion
}