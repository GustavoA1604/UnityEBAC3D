using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    private SaveManager _instance;
    private SaveSetup _saveSetup;
    public GameObject menu;
    public Button resumeGameMenuButton;
    public Button startNewGameMenuButton;
    public Button loadSaveMenuButton;
    public Button saveCurrentStateMenuButton;
    private string _savePath;
    private bool _isFirstMenu = true;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        _savePath = Application.dataPath + "/save.txt";
    }

    private void Start()
    {
        ShowMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isFirstMenu = false;
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        menu.SetActive(true);
        resumeGameMenuButton.gameObject.SetActive(!_isFirstMenu);
        startNewGameMenuButton.gameObject.SetActive(true);
        loadSaveMenuButton.gameObject.SetActive(File.Exists(_savePath));
        saveCurrentStateMenuButton.gameObject.SetActive(!_isFirstMenu);
    }

    public void CreateNewSave()
    {
        Player._instance.healthBase.Init();
        Player._instance.MoveToPosition(Player._instance.GetInitialPosition());
        CheckpointManager.ResetCheckpoints();
    }

    public void SaveCurrentState()
    {
        if (_saveSetup == null)
        {
            _saveSetup = new SaveSetup();
        }
        _saveSetup.lastCheckpoint = CheckpointManager.GetLastCheckpoint();
        _saveSetup.playerHealth = Player._instance.healthBase.GetCurrentLife();
        _saveSetup.clothSetup = Player._instance.clothChanger.GetCurrentClothSetup();
        _saveSetup.clothSetupDuration = Player._instance.clothChanger.GetCurrentDuration();
        SaveCurrentSetup();
    }

    private void SaveCurrentSetup()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup);
        File.WriteAllText(_savePath, setupToJson);
        loadSaveMenuButton.gameObject.SetActive(true);
    }

    public void Load()
    {
        if (File.Exists(_savePath))
        {
            string fileLoaded = File.ReadAllText(_savePath);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            CheckpointManager.ResetCheckpoints();
            CheckpointManager.SaveCheckpoint(_saveSetup.lastCheckpoint);
            Player._instance.RespawnInCheckpoint();
            Player._instance.healthBase.SetCurrentLife(_saveSetup.playerHealth);
            Player._instance.clothChanger.ChangeTexture(_saveSetup.clothSetup, _saveSetup.clothSetupDuration);
        }
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastCheckpoint = 0;
    public float playerHealth = 0;
    public ClothSetup clothSetup = null;
    public float clothSetupDuration = 0;
}