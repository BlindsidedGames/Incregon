using System.Text;
using UnityEngine;
using UnityEngine.UI;
using static Oracle;

public class BuildManager : MonoBehaviour
{
    public TileManager tileManager;
    [SerializeField] private GameObject buildingMenu;
    public Button botControllerButton;
    [SerializeField] private Button sellBuilding;

    private void Start()
    {
        botControllerButton.onClick.AddListener(() => BuildBuilding(TileBuilding.BotController));
        sellBuilding.onClick.AddListener(() => tileManager.SellBuilding());
    }

    public void OpenMenu()
    {
        buildingMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        buildingMenu.SetActive(false);
    }

    private void BuildBuilding(TileBuilding building)
    {
        tileManager.BuildBuilding(building);
        buildingMenu.SetActive(false);
        tileManager = null;
    }

    #region Singleton class: BuildManager

    public static BuildManager buildManager;


    private void Awake()
    {
        if (buildManager == null)
            buildManager = this;
        else
            Destroy(gameObject);
    }

    #endregion
}