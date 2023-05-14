using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

//using Unity.Services.CloudSave;

public class Oracle : SerializedMonoBehaviour
{
    private readonly string fileName = "betaTest";
    private readonly string saveExtension = ".beta";
    [SerializeField] public BuildNumberChecker buildNumber;

    public bool Loaded;

    private void Start()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        BsNewsGet();
        Loaded = false;
        Load();
        Loaded = true;
        InvokeRepeating(nameof(Save), 60, 60);
    }


    private void OnApplicationQuit()
    {
        Save();
    }

#if !UNITY_EDITOR
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
#if UNITY_IOS
            Load();
#elif UNITY_ANDROID
            Load();
#endif
        }
        if (!focus)
        {
            Save();
        }
    }
#endif


    #region NewsTicker

    public BsGamesData bsGamesData;
    public bool gotNews;

    [ContextMenu("BsNewsGet")]
    public async void BsNewsGet()
    {
        var url = "https://www.blindsidedgames.com/newsTicker";

        using var www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/jason");
        var operation = www.SendWebRequest();

        while (!operation.isDone) await Task.Yield();

        if (www.result == UnityWebRequest.Result.Success)
        {
            bsGamesData = new BsGamesData();

            var newsjson = www.downloadHandler.text;
            //Debug.Log(json);
            bsGamesData = JsonUtility.FromJson<BsGamesData>(newsjson);
            gotNews = true;
        }
        else
        {
            Debug.Log($"error {www.error}");
        }
    }

    [Serializable]
    public class BsGamesData
    {
        public string latestGameName;
        public string latestGameLink;
        public string latestGameAppStore;
        public string newsTicker;
        public string patreons;
        public string idleDysonSwarm;
    }

    #endregion

    #region Oracle

    public SaveData saveData;

    private string _json;

    #region SaveMethods

    [ContextMenu("WipeAllData")]
    public void WipeAllData()
    {
        var savePrefs = saveData.preferences;
        File.Delete(Application.persistentDataPath + "/" + fileName + saveExtension);
        Load();
        saveData.preferences = savePrefs;
        Save();
        SceneManager.LoadScene(0);
    }

    [ContextMenu("Save")]
    public void Save()
    {
        saveData.dateQuitString = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        saveData.buildNumber = buildNumber;
        SaveState(Application.persistentDataPath + "/" + fileName + saveExtension);
    }

    public void SaveState(string filePath)
    {
        var bytes = SerializationUtility.SerializeValue(saveData, DataFormat.JSON);
        File.WriteAllBytes(filePath, bytes);
    }

    public void Load()
    {
        Loaded = false;
        saveData = new SaveData();

        if (File.Exists(Application.persistentDataPath + "/" + fileName + saveExtension))
        {
            LoadState(Application.persistentDataPath + "/" + fileName + saveExtension);
        }

        else
        {
            saveData.dateStarted = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            Loaded = true;
        }

        Tiles = saveData.Tiles;
        TileResourcesOwned = saveData.TileResourcesOwned;
    }

    public void LoadState(string filePath)
    {
        if (!File.Exists(filePath)) return;

        var bytes = File.ReadAllBytes(filePath);
        saveData = SerializationUtility.DeserializeValue<SaveData>(bytes, DataFormat.JSON);
        Loaded = true;
    }


    /*private void AwayForSeconds()
    {
        if (string.IsNullOrEmpty(oracle.saveSettings.dateQuitString)) return;
        var dateStarted = DateTime.Parse(oracle.saveSettings.dateQuitString, CultureInfo.InvariantCulture);
        var dateNow = DateTime.UtcNow;
        var timespan = dateNow - dateStarted;
        var seconds = (float)timespan.TotalSeconds;
        if (seconds < 0) seconds = 0;
        saveSettings.sdPrestige.doubleTime += seconds;
        AwayFor?.Invoke(seconds);
    }*/

    #endregion

    #endregion

    #region LevelData

    public Data data;
    public Dictionary<TileResource, Resource> TileResourcesOwned = new();

    [Space(10)] public Dictionary<int, Tile> Tiles = new();
    public Dictionary<TileResource, TileBalancing> tileBalancing = new();

    [Serializable]
    public class Data
    {
        public float xpPerCompletion = 10;
        public double xpForFirstLevel = 100;
        public float exponent = 1.29f;
    }

    [Serializable]
    public class TileBalancing
    {
        public TimerData tileTimer;
    }

    [Serializable]
    public class TimerData
    {
        public float ResourceGatherTime;
    }

    #endregion

    #region SaveData

    [Serializable]
    public class SaveData
    {
        public BuildNumberChecker buildNumber;
        public string dateStarted;
        public string dateQuitString;
        public NumberTypes notation;

        public Statistics statistics = new();
        public Preferences preferences = new();


        public Resources resources = new();

        public Dictionary<TileResource, Resource> TileResourcesOwned = new();
        public Dictionary<int, Tile> Tiles = new();
    }

    [Serializable]
    public class BuildNumberChecker
    {
        public long buildNumber;
    }

    [Serializable]
    public class Resource
    {
        public double resource;
    }

    [Serializable]
    public class Resources
    {
        public double energy;
        [Space(10)] public double water;
        public double wood;
        public double coal;
        public double stone;
        public double iron;
        public double copper;
        public double titanium;
        public double silicon;
        public double oil;
        public double hydrogen;
        public double rareMetals;
        public double uranium;
        public double sulfuricAcid;
        [Space(10)] public double ironIngot;
        public double copperIngot;
        public double planks;
        public double concrete;
        public double glass;
        public double reinforcedGlass;
        public double steel;
        public double carbon;
        public double titaniumIngot;
        public double titaniumAlloy;
        public double gear;
        public double magnet;
        public double electricMotor;
        public double magneticCoil;
        public double electroMagnet;
        public double thruster;
        public double circuitBoard;
        public double cpu;
        public double diamondChipset;
        public double quantumChipset;
        public double spaceProbe;
        public double spaceShip;
        public double plastic;
        public double superconductor;
        public double uranium235;
        public double graphene;
        public double carbonNanotubes;
        public double battery;
        public double swarmSolarPanel;
        public double dysonFramePart;
        public double dysonNanoLattice;
        public double dysonSolarPanel;
        public double deuterium;
    }

    [Serializable]
    public class Tile
    {
        public LevelingData tileLevel = new();
        public TileStats tileStats = new();

        public TileBuilding tileBuilding = TileBuilding.None;
        public TileBuildingData tileBuildingData = new();
        public float tileBuildingTimer;
        public TileCalculations tileBalancing = new();
    }

    [Serializable]
    public class TileCalculations
    {
        public float tileBuildingTimerMax;
    }

    [Serializable]
    public class TileBuildingData
    {
        public BuildingTier buildingTier = BuildingTier.Tier1;

        public SmelterRecipe smelterRecipe = SmelterRecipe.None;
        public ChemicalPlantRecipe chemicalPlantRecipe = ChemicalPlantRecipe.None;
        public AssemblerRecipe assemblerRecipe = AssemblerRecipe.None;
        public SpacePortRecipe spacePortRecipe = SpacePortRecipe.None;
        public PowerBuilding powerBuilding = PowerBuilding.None;
    }

    [Serializable]
    public class LevelingData
    {
        public double experience;
        public long level;
    }


    [Serializable]
    public class TileStats
    {
        public double timeSpentProducing;
        public double resourcesProduced;
    }

    [Serializable]
    public class Statistics
    {
    }

    [Serializable]
    public class Preferences
    {
        public Tab menuTabs = Tab.Tab1;
    }

    #endregion

    #region Enums

    public enum BuildingTier
    {
        Tier1,
        Tier2,
        Tier3
    }

    public enum TileResource
    {
        None,
        Wood,
        Iron,
        Copper,
        Coal,
        Stone,
        Silicon,
        Titanium,
        Uranium,
        RareMetals,
        Oil,
        SulfuricAcid,
        Water,
        Hydrogen
    }

    public enum TileBuilding
    {
        None,
        Headquarters,
        BotController,
        Smelter,
        ChemicalPlant,
        Assembler,
        SpacePort,
        Observatory,
        ResearchStation,
        Railgun,
        Power
    }

    public enum SmelterRecipe
    {
        None,
        Glass,
        ReinforcedGlass,
        IronIngot,
        CopperIngot,
        Carbon,
        TitaniumIngot,
        Steel,
        TitaniumAlloy,
        PurifiedSilicon,
        Diamonds
    }

    public enum AssemblerRecipe
    {
        None,
        Planks,
        Concrete,
        CircuitBoard,
        CPU,
        DiamondChipset,
        QuantumChipset,
        Battery,
        CarbonNanotubes,
        Gear,
        Magnet,
        MagneticCoil,
        ElectroMagnet,
        ElectricMotor,
        Thruster,
        SwarmSolarPanel,
        DysonFramePart,
        DysonNanoLattice,
        DysonSpherePanel
    }

    public enum ChemicalPlantRecipe
    {
        None,
        Plastic,
        Hydrogen,
        Deuterium,
        Uranium235,
        Graphene,
        Superconductor
    }

    public enum SpacePortRecipe
    {
        None,
        SpaceProbe,
        SpaceShip
    }

    public enum PowerBuilding
    {
        None,
        WindTurbine,
        SolarPanel,
        NuclearReactor,
        MicroStar
    }


    public enum Tab
    {
        Tab1,
        Tab2,
        Tab3,
        Tab4
    }

    public enum BuyMode
    {
        Buy1,
        Buy10,
        Buy50,
        Buy100,
        BuyMax
    }

    public enum NumberTypes
    {
        Standard,
        Scientific,
        Engineering
    }

    #endregion


    #region Singleton class: Oracle

    public static Oracle oracle;


    private void Awake()
    {
        if (oracle == null)
            oracle = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    #endregion

    /*#region CloudSaving/loading

    [ContextMenu("SaveTOCloud")]
    public void SaveToCloud()
    {
        SaveSomeData("test");
    }

    [ContextMenu("LoadFromCloud")]
    public void LoadFromCloud()
    {
        LoadSomeData("test");
    }

    public event Action<string> SaveDataSuccessful;
    public event Action<string> SaveLoadError;
    public event Action<string> LoadDataSuccessful;

    public async void SaveSomeData(string saveName)
    {
        saveData.dateQuitString = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        var dataString = Encoding.UTF8.GetString(SerializationUtility.SerializeValue(saveData, DataFormat.JSON));
        var data = new Dictionary<string, object> { { saveName, dataString } };
        try
        {
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.HResult);
            SaveLoadError?.Invoke(e.ToString());
            throw;
        }

        SaveDataSuccessful?.Invoke(saveName);
        FindObjectOfType<SettingsManager>().RetrieveKeys();
    }

    public async void LoadSomeData(string saveName)
    {
        try
        {
            var savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { saveName });
            Loaded = false;
            var bytes = Encoding.UTF8.GetBytes(savedData[saveName]);
            saveData = SerializationUtility.DeserializeValue<SaveData>(bytes, DataFormat.JSON);
            Loaded = true;
            StartCoroutine(ReloadScene(saveName));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException());
            SaveLoadError?.Invoke(e.GetBaseException().Message);
            throw;
        }
    }

    private IEnumerator ReloadScene(string saveName)
    {
        SceneManager.LoadScene(0);
        yield return new WaitForSeconds(0.1f);
        LoadDataSuccessful?.Invoke(saveName);
    }

    #endregion*/
}