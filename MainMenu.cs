using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GPUInstancer;

public class MainMenu : MonoBehaviour {

    public string selectedGameType;
    public string shipType;
    public string selectedModuleType;
    public string location;
    public string[] currentLoadout;
    public int[] raceAsteroidMaterialNumbers;
    public int loadoutNumber = 0;
    public int skillRankLevelRequirement = 5;
    public int moduleNumber;
    public int numberOfUnusedModules;
    public int scrollSensitivity;
    public int bonusSkill;
    int pilotRankValue = 0;
    int currentLevel = 0;
    int currentSkillRank = 0;
    int currentXPNeeded = 1;
    int lastXPNeeded = 0;
    int tempXPNeeded;
    public float shipDistance = 1.125f;
    public float asteroidSize;
    public bool shouldRotateLeft = false;
    public bool shouldRotateRight = false;
    public bool shouldRotateUp = false;
    public bool shouldRotateDown = false;
    public bool asteroidsOn = true;
    public bool shipReset = false;
    public bool weaponExists = false;
    public bool[] shipUnlockTracker;
    public RaycastHit hitInfo;
    public Highscores highscoreManager;
    public Image fadeImage;
    public Animator fadeAnimation;
    public ScrollRect[] allRects;
    public AudioSource backgroundMusic;
    public Camera backgroundCamera;
    public Camera shipBuilderCamera;
    public Dropdown loadoutSelecter;
    public Dropdown resolutionSelecter;
    public Color selectedColor;
    public Color gametypeSelectedColor;
    public RectTransform shipLoadoutBuilder;
    public RectTransform shipLoadoutBuilderObject;
    public GameObject outerCage;
    public GameObject noWeaponsWarning;
    public GameObject moduleSelectionIndicator;
    public GameObject selectionIndicator;
    public GameObject playerNameEntryPanel;
    public GameObject playButton;
    public GameObject enterNameButton;
    public GameObject settingsMenu;
    public GameObject myStatsMenu;
    public GameObject leaderboardMenu;
    public GameObject ship;
    public GameObject[] asteroids;
    public GameObject[] raceAsteroids;
    public GameObject raceAsteroid;
    public GameObject lockedImage;
    public GameObject forwardIndicator;
    public GameObject skillLevelIncreasePopup;
    public GameObject levelIncreasePopup;
    public GameObject raceExperiencePopup;
    public GameObject arenaDescription;
    public GameObject allShipArenaDescription;
    public GameObject raceDescription;
    public GameObject extraAsteroidsToggle;
    public GameObject[] shipPopups;
    public Quaternion randomRotation;
    public Material[] raceAsteroidMaterials;
    public Material raceAsteroidMaterial1;
    public Material raceAsteroidMaterial2;
    public Material raceAsteroidMaterial3;
    public Material raceAsteroidMaterial4;
    public Material raceAsteroidMaterial5;
    public Material raceAsteroidMaterial6;
    public Material raceAsteroidMaterial7;
    public Material raceAsteroidMaterial8;
    public RectTransform shipListContent;
    public Button[] shipListButtons;
    public RectTransform locationListContent;
    public Button[] locationListButtons;
    public RectTransform moduleListContent;
    public Button[] moduleListButtons;
    public GameObject moduleInfoContent;
    public ScrollRect[] moduleScrollers;
    public GameObject shipInfoContent;
    public ScrollRect[] shipInfoScrollers;
    public ScrollRect tetrahedronInfo;
    public ScrollRect cubeInfo;
    public ScrollRect octahedronInfo;
    public ScrollRect icosahedronInfo;
    public ScrollRect dodecahedronInfo;
    public ScrollRect truncatedTetrahedronInfo;
    public ScrollRect truncatedOctahedronInfo;
    public ScrollRect truncatedCubeInfo;
    public ScrollRect cuboctahedronInfo;
    public ScrollRect truncatedCuboctahedronInfo;
    public ScrollRect rhombicuboctahedronInfo;
    public ScrollRect truncatedIcosahedronInfo;
    public ScrollRect truncatedDodecahedronInfo;
    public ScrollRect icosidodecahedronInfo;
    public ScrollRect snubCubeInfo;
    public ScrollRect rhombicosidodecahedronInfo;
    public ScrollRect truncatedIcosidodecahedronInfo;
    public ScrollRect snubDodecahedronInfo;
    public ScrollRect locationListScroller;
    public ScrollRect shipListScroller;
    public ModuleActivater[] modules;
    public ModuleActivater selectedModule;
    public ScrollRect shieldScroller;
    public Button survivalButton;
    public Button allShipSurvivalButton;
    public Button raceButton;
    public Button leaderboardButton;
    public Button playButtonButton;
    public Text totalExperience;
    public Text totalSkill;
    public Text statisticsTitle;
    public Text pilotRank;
    public Text level;
    public Text levelProgress;
    public Text skillRank;
    public Text skillRankProgress;
    public Text asteroidRadius;
    public Text unusedModules;
    public Text tetrahedronRecord;
    public Text cubeRecord;
    public Text octahedronRecord;
    public Text icosahedronRecord;
    public Text dodecahedronRecord;
    public Text truncatedTetrahedronRecord;
    public Text truncatedOctahedronRecord;
    public Text truncatedCubeRecord;
    public Text cuboctahedronRecord;
    public Text truncatedCuboctahedronRecord;
    public Text rhombicuboctahedronRecord;
    public Text truncatedIcosahedronRecord;
    public Text truncatedDodecahedronRecord;
    public Text icosidodecahedronRecord;
    public Text snubCubeRecord;
    public Text rhombicosidodecahedronRecord;
    public Text truncatedIcosidodecahedronRecord;
    public Text snubDodecahedronRecord;
    public Text raceLargestSize;
    public Text highestXPFromRace;
    public Text highestXPSphereRadius;
    public Text allShipSurvivalShip;
    public Text allShipSurvivalScore;
    public Text allShipSurvivalSkillScore;
    public Text physicsDetectionIterationsValue;
    public Text shipSkillIndicator;
    public Text fieldOfView;
    public Text framerateLimitValue;
    public Text levelIncreaseText;
    public Text skillLevelIncreaseText;
    public Text raceExperienceIncreaseText;
    public Text gametypeLabel;
    public Slider framerateLimit;
    public Slider levelProgressSlider;
    public Slider skillRankProgressSlider;
    public Slider volumeSlider;
    public Slider physicsIterationsSlider;
    public Slider asteroidRadiusSlider;
    public Slider fieldOfViewSlider;
    public AudioMixer audioMixer;
    public GameData gameData;
    public PlayerData playerData;
    public GameManager manager;
    public GameObject locationObject;
    public GameObject locationTitle;
    public Toggle shieldToggle;
    public Toggle musicToggle;
    public Toggle asteroidToggle;
    public Toggle extraZoom;
    public Toggle beginnerModeToggle;
    public Toggle fullscreenToggle;
    public InputField playerName;
    public Vector3 mouseReference;
    public Vector3 mouseMovement;
    public InputField[] controlInputs;
    public Resolution[] resolutions;

    // Use this for initialization
    void Start()
    {
        //this.clearPlayer();
        ////begin player clearing section
        //this.loadPlayerData();
        //this.resetAllLoadouts();
        //this.savePlayerData();
        ////end player clearing section
        //this.clearPlayer();

        raceAsteroidMaterials = new Material[8];
        raceAsteroidMaterials[0] = raceAsteroidMaterial1;
        raceAsteroidMaterials[1] = raceAsteroidMaterial2;
        raceAsteroidMaterials[2] = raceAsteroidMaterial3;
        raceAsteroidMaterials[3] = raceAsteroidMaterial4;
        raceAsteroidMaterials[4] = raceAsteroidMaterial5;
        raceAsteroidMaterials[5] = raceAsteroidMaterial6;
        raceAsteroidMaterials[6] = raceAsteroidMaterial7;
        raceAsteroidMaterials[7] = raceAsteroidMaterial8;

        weaponExists = false;
        highscoreManager = GetComponent<Highscores>();
        skillRankLevelRequirement = 10;
        scrollSensitivity = 15;
        selectedColor = Color.white;
        gametypeSelectedColor = Color.red;
        manager = FindObjectOfType<GameManager>();
        shipListButtons = shipListContent.GetComponentsInChildren<Button>();
        locationListButtons = locationListContent.GetComponentsInChildren<Button>();
        moduleListButtons = moduleListContent.GetComponentsInChildren<Button>();
        moduleScrollers = moduleInfoContent.GetComponentsInChildren<ScrollRect>(true);
        shipInfoScrollers = shipInfoContent.GetComponentsInChildren<ScrollRect>(true);
        asteroids = new GameObject[1];
        Cursor.visible = true;
        FindObjectOfType<AstroidGenerator>().initializeAsteroidGenerator();
        this.loadPlayerData();
        if (playerData.controls == null)
        {
            playerData.controls = new string[15];
            playerData.controls[0] = "w";
            playerData.controls[1] = "s";
            playerData.controls[2] = "a";
            playerData.controls[3] = "d";
            playerData.controls[4] = "e";
            playerData.controls[5] = "q";
            playerData.controls[6] = "u";
            playerData.controls[7] = "n";
            playerData.controls[8] = "j";
            playerData.controls[9] = "l";
            playerData.controls[10] = "i";
            playerData.controls[11] = "k";
            playerData.controls[12] = "f";
            playerData.controls[13] = "m";
            playerData.controls[14] = "r";
        }
        this.updateControls();
        this.playMusic(playerData.musicEnabled);
        if (playerData.playerName != null)
        {
            enterNameButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            statisticsTitle.text += " for " + playerData.playerName;
            leaderboardButton.enabled = true;
        }
        else
        {
            enterNameButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            leaderboardButton.enabled = false;
        }
        resolutions = Screen.resolutions;
        resolutionSelecter.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && playerData.resolution == 123456789)
            {
                playerData.resolution = i;
            }
        }
        resolutionSelecter.AddOptions(options);
        this.setResolution(playerData.resolution);
        if (playerData.resolution < resolutions.Length)
        {
            resolutionSelecter.value = playerData.resolution;
        }
        musicToggle.isOn = playerData.musicEnabled;
        fullscreenToggle.isOn = playerData.fullscreenOn;
        beginnerModeToggle.isOn = playerData.beginnerModeOn;
        asteroidToggle.isOn = playerData.asteroidsOn;
        asteroidsOn = playerData.asteroidsOn;
        physicsIterationsSlider.value = playerData.physicsCollisionIterations;
        fieldOfViewSlider.value = playerData.fieldOfView;
        this.newLoadGameData();
        this.loadSettings();
        this.setHighscores();
        this.setPlayerShip();
        //this.cherryGary();
        this.setLevelAndRank();
        this.unlockShips();
        this.shieldModule();
        this.setScrollSensitivity();
        this.setPhysicsCollisionIterations(playerData.physicsCollisionIterations);
        if (gameData.selectedGametype == null || gameData.selectedGametype == "Survival")
        {
            this.survival();
        }
        if (gameData.selectedGametype == "Race")
        {
            this.race();
        }
        if (gameData.selectedGametype == "AllShipSurvival")
        {
            this.allShipSurvival();
        }
        this.setLocation();
        this.setScrollSensitivity();
        //if (playerData.playerName == null)
        //{
        //    string randomPlayerName = "P";
        //    string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        //    char[] alphabetCharacters = alphabet.ToCharArray();
        //    for (int i = 0; i < 20; i++)
        //    {
        //        randomPlayerName += alphabetCharacters[UnityEngine.Random.Range(0, alphabetCharacters.Length-1)];
        //    }
        //    playerData.playerName = randomPlayerName;
        //}
        //playerName.text = playerData.playerName;
        //Highscores.AddNewHighscore(10, "test", "testShip");
        //this.newSaveGameData();
        //this.clearPlayer();
        playerData.playerID = "!198276341923846";
        this.savePlayerData();
    }

    private void Update()
    {
        if (!shipReset)
        {
            this.setPlayerShip();
            shipReset = true;
        }
        if (shipBuilderCamera.isActiveAndEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit)
                {
                    //hitInfo = new RaycastHit();
                    //hit = Physics.Raycast(shipBuilderCamera.ScreenPointToRay(hitInfo.textureCoord), out hitInfo);

                    selectedModule = hitInfo.transform.gameObject.GetComponentInChildren<ModuleActivater>();
                    if (selectedModule != null && !lockedImage.gameObject.activeSelf)
                    {
                        if (selectedModuleType != selectedModule.type)
                        {
                            selectedModule.type = selectedModuleType;
                            selectedModule.clearModule();
                            selectedModule.moduleInitializer();
                        }
                        else
                        {
                            selectedModule.type = "null";
                            selectedModule.clearModule();
                        }
                    }
                    selectedModule = null;
                }
            }
        }

        numberOfUnusedModules = 0;
        foreach (ModuleActivater module in modules)
        {
            if (module.type == "null")
            {
                numberOfUnusedModules++;
            }
        }
        unusedModules.text = "Unused modules: " + numberOfUnusedModules.ToString("0");

        if (shouldRotateRight)
        {
            ship.transform.Rotate(0, -1, 0, Space.World);
        }
        if (shouldRotateUp)
        {
            ship.transform.Rotate(1, 0, 0, Space.World);
        }
        if (shouldRotateDown)
        {
            ship.transform.Rotate(-1, 0, 0, Space.World);
        }
        if (shouldRotateLeft)
        {
            ship.transform.Rotate(0, 1, 0, Space.World);
        }
        asteroidRadius.text = "Asteroid Radius: " + ((int)(asteroidSize / 2)).ToString();

        //check to rotate ship
        if (Input.GetMouseButtonDown(1))
        {
            mouseReference = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            mouseMovement = mouseReference - Input.mousePosition;
            ship.transform.Rotate(-mouseMovement.y, mouseMovement.x, mouseMovement.z, Space.World);
            mouseReference = Input.mousePosition;
        }
    }

    public void leaderboards()
    {
        if (playerData.allShipSurvivalSkill != 0)
        {
            Highscores.AddNewHighscore((int)(playerData.allShipSurvivalSkill * 10000), playerData.playerName + playerData.playerID, playerData.allShipSurvivalShip);
        }
    }

    public void myStats()
    {

    }

    public void exit()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    public void toggleAsteroids()
    {
        if (asteroidToggle.isOn)
        {
            asteroidsOn = true;
            if (selectedGameType != "Race")
            {
                manager.asteroidGenerator.spawnAsteroids(350);
                if (location == "asteroidField")
                {
                    manager.asteroidGenerator.spawnAsteroids(2000);
                }
            }
        }
        else
        {
            asteroidsOn = false;
            if (selectedGameType != "Race")
            {
                manager.asteroidGenerator.spawnAsteroids(0);
                if (location == "asteroidField")
                {
                    manager.asteroidGenerator.spawnAsteroids(1000);
                }
            }
        }
        playerData.asteroidsOn = asteroidsOn;
        this.savePlayerData();
    }

    public void toggleZoom()
    {
        if (extraZoom.isOn)
        {
            ship.transform.position = new Vector3(-2000, 2000, ship.transform.position.z * 5);
        }
        else
        {
            ship.transform.position = new Vector3(-2000, 2000, ship.transform.position.z / 5);
        }
    }

    public void setLocation()
    {
        if (gameData.selectedGametype != "Race")
        {
            if (gameData.selectedLocation == null || gameData.selectedLocation == "cherryGary")
            {
                this.cherryGary();
            }
            else if (gameData.selectedLocation == "spaceStation")
            {
                this.spaceStation();
            }
            else if (gameData.selectedLocation == "mirrorField")
            {
                this.mirrorField();
            }
            else if (gameData.selectedLocation == "nestedTetrahedron")
            {
                this.nestedTetrahedron();
            }
            else if (gameData.selectedLocation == "nestedIcosahedron")
            {
                this.nestedIcosahedron();
            }
            else if (gameData.selectedLocation == "nestedDodecahedron")
            {
                this.nestedDodecahedron();
            }
            else if (gameData.selectedLocation == "sagA*")
            {
                this.sagAStar();
            }
            else if (gameData.selectedLocation == "pentagon120")
            {
                this.pentagon120();
            }
            else if (gameData.selectedLocation == "asteroidField")
            {
                this.asteroidField();
            }
        }
    }

    public void survival()
    {
        outerCage.SetActive(true);
        locationTitle.gameObject.SetActive(true);
        selectedGameType = "Survival";
        gameData.selectedGametype = "Survival";
        gametypeLabel.text = "Arena";
        //survivalButton.image.color = gametypeSelectedColor;
        //allShipSurvivalButton.image.color = Color.white;
        //raceButton.image.color = Color.white;
        selectionIndicator.transform.position = survivalButton.transform.position;
        this.setLocation();
        if (raceAsteroids != null)
        {
            foreach (GameObject asteroid in raceAsteroids)
            {
                Destroy(asteroid);
            }
        }
        backgroundCamera.transform.position = new Vector3(-250, 0, 0);
        asteroidRadiusSlider.gameObject.SetActive(false);
        locationListContent.gameObject.SetActive(true);
        arenaDescription.SetActive(true);
        allShipArenaDescription.SetActive(false);
        raceDescription.SetActive(false);
        extraAsteroidsToggle.SetActive(true);
        this.newSaveGameData();
    }

    public void allShipSurvival()
    {
        outerCage.SetActive(true);
        locationTitle.gameObject.SetActive(true);
        selectedGameType = "AllShipSurvival";
        gameData.selectedGametype = "AllShipSurvival";
        gametypeLabel.text = "All Ship Arena";
        //survivalButton.image.color = Color.white;
        //allShipSurvivalButton.image.color = gametypeSelectedColor;
        //raceButton.image.color = Color.white;
        selectionIndicator.transform.position = allShipSurvivalButton.transform.position;
        this.setLocation();
        if (raceAsteroids != null)
        {
            foreach (GameObject asteroid in raceAsteroids)
            {
                Destroy(asteroid);
            }
        }
        backgroundCamera.transform.position = new Vector3(-250, 0, 0);
        asteroidRadiusSlider.gameObject.SetActive(false);
        locationListContent.gameObject.SetActive(true);
        arenaDescription.SetActive(false);
        allShipArenaDescription.SetActive(true);
        raceDescription.SetActive(false);
        extraAsteroidsToggle.SetActive(true);
        this.newSaveGameData();
    }

    public void race()
    {
        if (gameData.raceAsteroidSize == 0)
        {
            gameData.raceAsteroidSize = 60;
        }
        locationTitle.gameObject.SetActive(false);
        gameData.selectedGametype = "Race";
        gametypeLabel.text = "Time Trial";
        if (selectedGameType != "Race")
        {
            this.clearLocationButtons();
            this.clearLocation();
            selectedGameType = "Race";
            outerCage.SetActive(false);
            //survivalButton.image.color = Color.white;
            //allShipSurvivalButton.image.color = Color.white;
            //raceButton.image.color = gametypeSelectedColor;
            selectionIndicator.transform.position = raceButton.transform.position;
            backgroundCamera.GetComponent<Skybox>().material = manager.cherryGarySkybox;

            backgroundCamera.transform.position = new Vector3(0, 0, 0);

            //raceAsteroids = new GameObject[42000];
            //raceAsteroidMaterialNumbers = gameData.raceAsteroidMaterials;
            //float xAxis;
            //float yAxis;
            //float zAxis;

            //for (int i = 0; i < 42000; i++)
            //{
            //    xAxis = UnityEngine.Random.Range(0, 100);
            //    yAxis = UnityEngine.Random.Range(0, 100);
            //    zAxis = UnityEngine.Random.Range(0, 100);
            //    randomRotation.Set(xAxis, yAxis, zAxis, 0);

            //    raceAsteroids[i] = Instantiate(raceAsteroid, new Vector3(gameData.raceAsteroidXPositions[i], gameData.raceAsteroidYPositions[i], gameData.raceAsteroidZPositions[i]), randomRotation);
            //    raceAsteroids[i].GetComponent<Renderer>().material = raceAsteroidMaterials[raceAsteroidMaterialNumbers[i]];
            //    raceAsteroids[i].transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
            //}
        }
        asteroidSize = gameData.raceAsteroidSize;
        asteroidRadiusSlider.value = gameData.raceAsteroidSize;
        manager.asteroidSize = asteroidRadiusSlider.value;
        FindObjectOfType<AstroidGenerator>().raceAsteroids(gameData);
        asteroidRadiusSlider.gameObject.SetActive(true);
        locationListContent.gameObject.SetActive(false);
        arenaDescription.SetActive(false);
        allShipArenaDescription.SetActive(false);
        raceDescription.SetActive(true);
        extraAsteroidsToggle.SetActive(false);
        this.newSaveGameData();
    }

    public void play()
    {
        //this.initializeModules();
        if (selectedGameType == "Race")
        {
            this.saveCurrentLoadout();
            //set gametype and ship loadout that will be used in the next scene
            PlayerPrefs.SetString("gameType", selectedGameType);
            PlayerPrefs.SetString("shipType", shipType);
            PlayerPrefs.SetString("location", location);
            PlayerPrefs.SetInt("loadoutNumber", loadoutNumber);
            PlayerPrefs.SetFloat("asteroidSize", asteroidSize);
            StartCoroutine(this.loadLevel());
        }
        else
        {
            foreach (ModuleActivater module in modules)
            {
                if (module.type == "FixedRailgun" || module.type == "TurretedRailgun" || module.type == "FixedMachinegun" || module.type == "TurretedMachinegun" || module.type == "FixedEMAC" ||
                    module.type == "TurretedEMAC" || module.type == "FixedLaser" || module.type == "TurretedLaser" || module.type == "Missile" || module.type == "Torpedo")
                {
                    this.saveCurrentLoadout();
                    //set gametype and ship loadout that will be used in the next scene
                    PlayerPrefs.SetString("gameType", selectedGameType);
                    PlayerPrefs.SetString("shipType", shipType);
                    PlayerPrefs.SetString("location", location);
                    PlayerPrefs.SetInt("loadoutNumber", loadoutNumber);
                    PlayerPrefs.SetFloat("asteroidSize", asteroidSize);
                    StartCoroutine(this.loadLevel());
                    weaponExists = true;
                }
            }
            if (!weaponExists)
            {
                noWeaponsWarning.SetActive(true);
            }
        }
    }

    IEnumerator loadLevel()
    {
        fadeAnimation.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadScene(2);
    }

    public void settingsBack()
    {
        playerData.volumeLevel = volumeSlider.value;
        playerData.beginnerModeOn = beginnerModeToggle.isOn;
        this.savePlayerData();
        this.newSaveGameData();
        settingsMenu.SetActive(false);
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        if (volume == -35)
        {
            audioMixer.SetFloat("volume", -80);
        }
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        playerData.fullscreenOn = isFullscreen;
    }

    public void setResolution(int resolutionIndex)
    {
        if (resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            playerData.resolution = resolutionIndex;
        }
    }

    public void setFramerate(float frames)
    {
        framerateLimitValue.text = ((int)frames).ToString();
        gameData.framerateLimit = (int)frames;
    }

    public void setFieldOfView(float FOV)
    {
        playerData.fieldOfView = FOV;
        fieldOfView.text = ((int)FOV).ToString();
        this.savePlayerData();
    }

    public void setPhysicsCollisionIterations(float iterations)
    {
        Physics.defaultSolverIterations = (int)iterations;
        physicsDetectionIterationsValue.text = iterations.ToString();
        playerData.physicsCollisionIterations = (int)iterations;
    }

    public void playMusic(bool enabled)
    {
        if (enabled)
        {
            backgroundMusic.Play();
            playerData.musicEnabled = true;
        }
        else
        {
            backgroundMusic.Pause();
            playerData.musicEnabled = false;
        }
    }

    public void setAsteroidSize(float size)
    {
        //asteroidSize = size;
        //foreach (GameObject asteroid in raceAsteroids)
        //{
        //    backgroundCamera.farClipPlane = 20000 / asteroidSize;
        //    asteroid.transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
        //}
        manager.asteroidSize = size;
        asteroidSize = size;
        gameData.raceAsteroidSize = size;
        manager.asteroidGenerator.raceAsteroids(gameData);
        this.newSaveGameData();
    }

    public void rotateRight()
    {
        shouldRotateRight = true;
    }

    public void stopRotateRight()
    {
        shouldRotateRight = false;
    }

    public void rotateUp()
    {
        shouldRotateUp = true;
    }

    public void stopRotateUp()
    {
        shouldRotateUp = false;
    }

    public void rotateDown()
    {
        shouldRotateDown = true;
    }

    public void stopRotateDown()
    {
        shouldRotateDown = false;
    }

    public void rotateLeft()
    {
        shouldRotateLeft = true;
    }

    public void stopRotateLeft()
    {
        shouldRotateLeft = false;
    }

    public void toggleShields()
    {
        if (shieldToggle.isOn == true)
        {
            foreach (ModuleActivater module in modules)
            {
                if (module.shield != null)
                {
                    module.shield.GetComponent<Renderer>().enabled = true;
                }
            }
        }
        else
        {
            foreach (ModuleActivater module in modules)
            {
                if (module.shield != null)
                {
                    module.shield.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }

    public void setScrollSensitivity()
    {
        allRects = FindObjectsOfType<ScrollRect>();
        foreach (ScrollRect r in allRects)
        {
            r.scrollSensitivity = scrollSensitivity;
        }
        locationListScroller.scrollSensitivity = -scrollSensitivity;
        shipListScroller.scrollSensitivity = -scrollSensitivity;
    }

    public void setLevelAndRank()
    {
        float experience = playerData.experience;
        totalExperience.text = "Total: " + ((int)experience).ToString() + " exp";

        //process level
        while (true)
        {
            if (experience > currentXPNeeded)
            {
                currentLevel++;
                experience -= currentXPNeeded;
                tempXPNeeded = currentXPNeeded;
                currentXPNeeded += lastXPNeeded;
                lastXPNeeded = tempXPNeeded;
            }
            else
            {
                levelProgressSlider.maxValue = currentXPNeeded;
                levelProgressSlider.value = experience;
                levelProgress.text = experience.ToString("0") + "/" + currentXPNeeded.ToString("0");
                break;
            }
        }

        //add 100000000 to unlock everything
        currentSkillRank = bonusSkill + currentSkillRank + playerData.cubeHighScore + playerData.cuboctahedronHighScore + playerData.dodecahedronHighScore + playerData.icosahedronHighScore +
            playerData.icosidodecahedronHighScore + playerData.octahedronHighScore + playerData.rhombicosidodecahedronHighScore + playerData.rhombicuboctahedronHighScore +
            playerData.snubCubeHighScore + playerData.snubDodecahedronHighScore + playerData.tetrahedronHighScore + playerData.truncatedCubeHighScore + playerData.truncatedCuboctahedronHighScore +
            playerData.truncatedDodecahedronHighScore + playerData.truncatedIcosahedronHighScore + playerData.truncatedIcosidodecahedronHighScore + playerData.truncatedOctahedronHighScore +
            playerData.truncatedTetrahedronHighScore + (playerData.raceLargestSize / 10) + (int)playerData.allShipSurvivalSkill;

        totalSkill.text = "Total: " + currentSkillRank.ToString() + " skill points";
        level.text = "Level: " + currentLevel;
        skillRankProgressSlider.maxValue = skillRankLevelRequirement;
        skillRankProgressSlider.value = currentSkillRank % skillRankLevelRequirement;
        skillRank.text = "Skill Rank: " + ((currentSkillRank / skillRankLevelRequirement) + 1).ToString("0");
        skillRankProgress.text = (currentSkillRank % skillRankLevelRequirement).ToString("0") + "/" + skillRankLevelRequirement.ToString("0");
        pilotRankValue = (((currentSkillRank / skillRankLevelRequirement) + 1) * currentLevel);
        pilotRank.text = "Pilot Rank: " + pilotRankValue.ToString("0");
        levelIncreaseText.text = "Congratulations! You are now level " + currentLevel.ToString() + ".";
        skillLevelIncreaseText.text = "Congratulations! You now have a skill rank of " + ((currentSkillRank / skillRankLevelRequirement) + 1).ToString("0") + ".";
        raceExperienceIncreaseText.text = "Congratulations! You've set a new time trial record with " + playerData.highestXPFromRace.ToString() + " experience.";

        //activate popups
        if (currentLevel > playerData.lastLevel)
        {
            levelIncreasePopup.SetActive(true);
            playerData.lastLevel = currentLevel;
        }
        if (currentSkillRank > playerData.lastSkillLevel)
        {
            skillLevelIncreasePopup.SetActive(true);
            playerData.lastSkillLevel = currentSkillRank;
        }
        if (playerData.highestXPFromRace > playerData.lastRaceExperience)
        {
            raceExperiencePopup.SetActive(true);
            playerData.lastRaceExperience = playerData.highestXPFromRace;
        }

    }

    public void unlockShips()
    {
        for (int i = 0; i < shipListButtons.Length; i++)
        {
            shipListButtons[i].enabled = true;
            if (pilotRankValue >= (Mathf.Pow(i, 2) + i))
            {
                shipListButtons[i].GetComponentInChildren<Text>().enabled = false;
                if (!shipUnlockTracker[i])
                {
                    shipPopups[i-1].SetActive(true);
                    shipUnlockTracker[i] = true;
                }
            }
            else
            {
                shipListButtons[i].GetComponentInChildren<Text>().enabled = true;
                shipListButtons[i].GetComponentInChildren<Text>().text = "Unlocked at pilot rank: " + (Mathf.Pow(i, 2) + i).ToString();
            }
        }
        playerData.tetrahedronHasBeenUnlocked = shipUnlockTracker[0];
        playerData.octahedronHasBeenUnlocked = shipUnlockTracker[1];
        playerData.cubeHasBeenUnlocked = shipUnlockTracker[2];
        playerData.icosahedronHasBeenUnlocked = shipUnlockTracker[3];
        playerData.cuboctahedronHasBeenUnlocked = shipUnlockTracker[4];
        playerData.truncatedTetrahedronHasBeenUnlocked = shipUnlockTracker[5];
        playerData.snubCubeHasBeenUnlocked = shipUnlockTracker[6];
        playerData.rhombicuboctahedronHasBeenUnlocked = shipUnlockTracker[7];
        playerData.dodecahedronHasBeenUnlocked = shipUnlockTracker[8];
        playerData.truncatedOctahedronHasBeenUnlocked = shipUnlockTracker[9];
        playerData.icosidodecahedronHasBeenUnlocked = shipUnlockTracker[10];
        playerData.truncatedCubeHasBeenUnlocked = shipUnlockTracker[11];
        playerData.snubDodecahedronHasBeenUnlocked = shipUnlockTracker[12];
        playerData.rhombicosidodecahedronHasBeenUnlocked = shipUnlockTracker[13];
        playerData.truncatedCuboctahedronHasBeenUnlocked = shipUnlockTracker[14];
        playerData.truncatedIcosahedronHasBeenUnlocked = shipUnlockTracker[15];
        playerData.truncatedDodecahedronHasBeenUnlocked = shipUnlockTracker[16];
        playerData.truncatedIcosidodecahedronHasBeenUnlocked = shipUnlockTracker[17];
        this.savePlayerData();
        this.loadPlayerData();
    }

    public void initializeModules()
    {
        modules = ship.transform.GetComponentsInChildren<ModuleActivater>();
        moduleNumber = 0;
        foreach (ModuleActivater module in modules)
        {
            module.moduleNumber = moduleNumber;
            module.type = currentLoadout[moduleNumber];
            moduleNumber++;
            module.isEnemyModule = false;
        }
        foreach (ModuleActivater module in modules)
        {
            module.moduleInitializer();
        }
        this.toggleShields();
        ship.transform.rotation = new Quaternion(0, -1, 0, 0);
        GameObject temp = Instantiate(forwardIndicator, ship.transform);
        temp.transform.up = ship.transform.forward;
        if (shipType == "cube")
        {
            temp.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y * 2, temp.transform.localScale.z);
        }
    }

    public void clearShip()
    {
        extraZoom.isOn = false;
        Destroy(ship.gameObject);
        this.clearShipButtons();
        foreach (ScrollRect scroller in shipInfoScrollers)
        {
            scroller.gameObject.SetActive(false);
        }
    }

    public void clearShipButtons()
    {
        foreach (Button button in shipListButtons)
        {
            button.image.color = Color.gray;
        }
    }

    public void saveCurrentSelectedShip()
    {
        gameData.selectedShip = shipType;
        this.newSaveGameData();
    }

    public void tetrahedronShip()
    {
        this.clearShip();
        shipType = "tetrahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.tetrahedronHighScore;
        shipListButtons[0].image.color = selectedColor;
        if (shipListButtons[0].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.tetrahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 2);
        currentLoadout = playerData.tetrahedronLoadouts[loadoutNumber];
        this.initializeModules();
        tetrahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void octahedronShip()
    {
        this.clearShip();
        shipType = "octahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.octahedronHighScore;
        shipListButtons[1].image.color = selectedColor;
        if (shipListButtons[1].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.octahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 2.2f);
        currentLoadout = playerData.octahedronLoadouts[loadoutNumber];
        this.initializeModules();
        octahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void cubeShip()
    {
        this.clearShip();
        shipType = "cube";
        shipSkillIndicator.text = "Ship skill: " + playerData.cubeHighScore;
        shipListButtons[2].image.color = selectedColor;
        if (shipListButtons[2].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.cubeShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 2.5f);
        currentLoadout = playerData.cubeLoadouts[loadoutNumber];
        this.initializeModules();
        cubeInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void icosahedronShip()
    {
        this.clearShip();
        shipType = "icosahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.icosahedronHighScore;
        shipListButtons[3].image.color = selectedColor;
        if (shipListButtons[3].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.icosahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 3);
        currentLoadout = playerData.icosahedronLoadouts[loadoutNumber];
        this.initializeModules();
        icosahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void cuboctahedronShip()
    {
        this.clearShip();
        shipType = "cuboctahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.cuboctahedronHighScore;
        shipListButtons[4].image.color = selectedColor;
        if (shipListButtons[4].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.cuboctahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 3.2f);
        currentLoadout = playerData.cuboctahedronLoadouts[loadoutNumber];
        this.initializeModules();
        cuboctahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void truncatedTetrahedronShip()
    {
        this.clearShip();
        shipType = "truncatedTetrahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.truncatedTetrahedronHighScore;
        shipListButtons[5].image.color = selectedColor;
        if (shipListButtons[5].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.truncatedTetrahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 3.4f);
        currentLoadout = playerData.truncatedTetrahedronLoadouts[loadoutNumber];
        this.initializeModules();
        truncatedTetrahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void snubCubeShip()
    {
        this.clearShip();
        shipType = "snubCube";
        shipSkillIndicator.text = "Ship skill: " + playerData.snubCubeHighScore;
        shipListButtons[6].image.color = selectedColor;
        if (shipListButtons[6].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.snubCubeShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 4);
        currentLoadout = playerData.snubCubeLoadouts[loadoutNumber];
        this.initializeModules();
        snubCubeInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void rhombicuboctahedronShip()
    {
        this.clearShip();
        shipType = "rhombicuboctahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.rhombicuboctahedronHighScore;
        shipListButtons[7].image.color = selectedColor;
        if (shipListButtons[7].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.rhombicuboctahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 4.3f);
        currentLoadout = playerData.rhombicuboctahedronLoadouts[loadoutNumber];
        this.initializeModules();
        rhombicuboctahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void dodecahedronShip()
    {
        this.clearShip();
        shipType = "dodecahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.dodecahedronHighScore;
        shipListButtons[8].image.color = selectedColor;
        if (shipListButtons[8].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.dodecahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 4.3f);
        currentLoadout = playerData.dodecahedronLoadouts[loadoutNumber];
        this.initializeModules();
        dodecahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void truncatedOctahedronShip()
    {
        this.clearShip();
        shipType = "truncatedOctahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.truncatedOctahedronHighScore;
        shipListButtons[9].image.color = selectedColor;
        if (shipListButtons[9].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.truncatedOctahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 5);
        currentLoadout = playerData.truncatedOctahedronLoadouts[loadoutNumber];
        this.initializeModules();
        truncatedOctahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void icosidodecahedronShip()
    {
        this.clearShip();
        shipType = "icosidodecahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.icosidodecahedronHighScore;
        shipListButtons[10].image.color = selectedColor;
        if (shipListButtons[10].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.icosidodecahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 5.2f);
        currentLoadout = playerData.icosidodecahedronLoadouts[loadoutNumber];
        this.initializeModules();
        icosidodecahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void truncatedCubeShip()
    {
        this.clearShip();
        shipType = "truncatedCube";
        shipSkillIndicator.text = "Ship skill: " + playerData.truncatedCubeHighScore;
        shipListButtons[11].image.color = selectedColor;
        if (shipListButtons[11].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.truncatedCubeShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 5.5f);
        currentLoadout = playerData.truncatedCubeLoadouts[loadoutNumber];
        this.initializeModules();
        truncatedCubeInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void snubDodecahedronShip()
    {
        this.clearShip();
        shipType = "snubDodecahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.snubDodecahedronHighScore;
        shipListButtons[12].image.color = selectedColor;
        if (shipListButtons[12].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.snubDodecahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 6.5f);
        currentLoadout = playerData.snubDodecahedronLoadouts[loadoutNumber];
        this.initializeModules();
        snubDodecahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void rhombicosidodecahedronShip()
    {
        this.clearShip();
        shipType = "rhombicosidodecahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.rhombicosidodecahedronHighScore;
        shipListButtons[13].image.color = selectedColor;
        if (shipListButtons[13].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.rhombicosidodecahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 6.8f);
        currentLoadout = playerData.rhombicosidodecahedronLoadouts[loadoutNumber];
        this.initializeModules();
        rhombicosidodecahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void truncatedCuboctahedronShip()
    {
        this.clearShip();
        shipType = "truncatedCuboctahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.truncatedCuboctahedronHighScore;
        shipListButtons[14].image.color = selectedColor;
        if (shipListButtons[14].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.truncatedCuboctahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 6.95f);
        currentLoadout = playerData.truncatedCuboctahedronLoadouts[loadoutNumber];
        this.initializeModules();
        truncatedCuboctahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void truncatedIcosahedronShip()
    {
        this.clearShip();
        shipType = "truncatedIcosahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.truncatedIcosahedronHighScore;
        shipListButtons[15].image.color = selectedColor;
        if (shipListButtons[15].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.truncatedIcosahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 7.5f);
        currentLoadout = playerData.truncatedIcosihedronLoadouts[loadoutNumber];
        this.initializeModules();
        truncatedIcosahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void truncatedDodecahedronShip()
    {
        this.clearShip();
        shipType = "truncatedDodecahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.truncatedDodecahedronHighScore;
        shipListButtons[16].image.color = selectedColor;
        if (shipListButtons[16].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.truncatedDodecahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 9);
        currentLoadout = playerData.truncatedDodecahedronLoadouts[loadoutNumber];
        this.initializeModules();
        truncatedDodecahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void truncatedIcosidodecahedronShip()
    {
        this.clearShip();
        shipType = "truncatedIcosidodecahedron";
        shipSkillIndicator.text = "Ship skill: " + playerData.truncatedIcosidodecahedronHighScore;
        shipListButtons[17].image.color = selectedColor;
        if (shipListButtons[17].GetComponentInChildren<Text>().enabled == true)
        {
            lockedImage.SetActive(true);
            playButtonButton.enabled = false;
        }
        else
        {
            lockedImage.SetActive(false);
            playButtonButton.enabled = true;
        }
        ship = Instantiate(manager.truncatedIcosidodecahedronShip);
        ship.transform.position = new Vector3(-2000, 2000, shipDistance * 11.6f);
        currentLoadout = playerData.truncatedIcosidodecahedronLoadouts[loadoutNumber];
        this.initializeModules();
        truncatedIcosidodecahedronInfo.gameObject.SetActive(true);
        this.saveCurrentSelectedShip();
    }

    public void selectLoadout(int num)
    {
        loadoutNumber = num;
        if (shipType == "tetrahedron")
        {
            this.tetrahedronShip();
        }
        if (shipType == "cube")
        {
            this.cubeShip();
        }
        if (shipType == "octahedron")
        {
            this.octahedronShip();
        }
        if (shipType == "dodecahedron")
        {
            this.dodecahedronShip();
        }
        if (shipType == "icosahedron")
        {
            this.icosahedronShip();
        }
        if (shipType == "truncatedTetrahedron")
        {
            this.truncatedTetrahedronShip();
        }
        if (shipType == "cuboctahedron")
        {
            this.cuboctahedronShip();
        }
        if (shipType == "truncatedCube")
        {
            this.truncatedCubeShip();
        }
        if (shipType == "truncatedOctahedron")
        {
            this.truncatedOctahedronShip();
        }
        if (shipType == "rhombicuboctahedron")
        {
            this.rhombicuboctahedronShip();
        }
        if (shipType == "truncatedCuboctahedron")
        {
            this.truncatedCuboctahedronShip();
        }
        if (shipType == "snubCube")
        {
            this.snubCubeShip();
        }
        if (shipType == "icosidodecahedron")
        {
            this.icosidodecahedronShip();
        }
        if (shipType == "truncatedDodecahedron")
        {
            this.truncatedDodecahedronShip();
        }
        if (shipType == "truncatedIcosahedron")
        {
            this.truncatedIcosahedronShip();
        }
        if (shipType == "rhombicosidodecahedron")
        {
            this.rhombicosidodecahedronShip();
        }
        if (shipType == "truncatedIcosidodecahedron")
        {
            this.truncatedIcosidodecahedronShip();
        }
        if (shipType == "snubDodecahedron")
        {
            this.snubDodecahedronShip();
        }
    }

    public void clearLocationButtons()
    {
        foreach (Button button in locationListButtons)
        {
            button.image.color = Color.gray;
        }
    }

    public void clearLocation()
    {
        if (locationObject != null)
        {
            Destroy(locationObject);
        }
        if (asteroids != null)
        {
            if (asteroids[0] != null)
            {
                foreach (GameObject asteroid in asteroids)
                {
                    Destroy(asteroid);
                }
            }
        }
        manager.asteroidGenerator.clearAsteroids();
        outerCage.SetActive(true);
    }

    public void cherryGary()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[0].image.color = selectedColor;
        location = "cherryGary";
        backgroundCamera.GetComponent<Skybox>().material = manager.cherryGarySkybox;
        locationObject = Instantiate(manager.cherryGary);
        gameData.selectedLocation = location;
        if (asteroidsOn)
        {
            manager.asteroidGenerator.spawnAsteroids(350);
        }
        this.newSaveGameData();
    }

    public void spaceStation()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[1].image.color = selectedColor;
        location = "spaceStation";
        backgroundCamera.GetComponent<Skybox>().material = manager.ceresSkybox;
        locationObject = Instantiate(manager.spaceStation);
        gameData.selectedLocation = location;
        if (asteroidsOn)
        {
            manager.asteroidGenerator.spawnAsteroids(350);
        }
        this.newSaveGameData();
    }

    public void sagAStar()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[2].image.color = selectedColor;
        location = "sagA*";
        locationObject = Instantiate(manager.sagACage);
        backgroundCamera.GetComponent<Skybox>().material = manager.ceresSkybox;
        gameData.selectedLocation = location;
        if (asteroidsOn)
        {
            manager.asteroidGenerator.spawnAsteroids(350);
        }
        this.newSaveGameData();
    }

    public void asteroidField()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[3].image.color = selectedColor;
        location = "asteroidField";
        //asteroids = manager.spawnAsteroids(250);
        manager.asteroidGenerator.spawnAsteroids(2000);
        backgroundCamera.GetComponent<Skybox>().material = manager.cherryGarySkybox;
        gameData.selectedLocation = location;
        this.newSaveGameData();
    }

    public void mirrorField()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[4].image.color = selectedColor;
        location = "mirrorField";
        locationObject = Instantiate(manager.mirrorArray);
        backgroundCamera.GetComponent<Skybox>().material = manager.sagASkybox;
        gameData.selectedLocation = location;
        if (asteroidsOn)
        {
            manager.asteroidGenerator.spawnAsteroids(350);
        }
        this.newSaveGameData();
    }

    public void nestedTetrahedron()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[5].image.color = selectedColor;
        location = "nestedTetrahedron";
        locationObject = Instantiate(manager.nestedTetrahedron);
        backgroundCamera.GetComponent<Skybox>().material = manager.cherryGarySkybox;
        gameData.selectedLocation = location;
        if (asteroidsOn)
        {
            manager.asteroidGenerator.spawnAsteroids(350);
        }
        this.newSaveGameData();
    }
    public void nestedDodecahedron()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[6].image.color = selectedColor;
        location = "nestedDodecahedron";
        locationObject = Instantiate(manager.nestedDodecahedron);
        backgroundCamera.GetComponent<Skybox>().material = manager.cherryGarySkybox;
        gameData.selectedLocation = location;
        if (asteroidsOn)
        {
            manager.asteroidGenerator.spawnAsteroids(350);
        }
        this.newSaveGameData();
    }
    public void nestedIcosahedron()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[7].image.color = selectedColor;
        location = "nestedIcosahedron";
        locationObject = Instantiate(manager.nestedIcosahedron);
        backgroundCamera.GetComponent<Skybox>().material = manager.cherryGarySkybox;
        gameData.selectedLocation = location;
        if (asteroidsOn)
        {
            manager.asteroidGenerator.spawnAsteroids(350);
        }
        this.newSaveGameData();
    }
    public void pentagon120()
    {
        if (selectedGameType == "Race")
        {
            this.survival();
        }
        this.clearLocation();
        this.clearLocationButtons();
        locationListButtons[8].image.color = selectedColor;
        location = "pentagon120";
        locationObject = Instantiate(manager.pentagon120);
        backgroundCamera.GetComponent<Skybox>().material = manager.cherryGarySkybox;
        gameData.selectedLocation = location;
        if (asteroidsOn)
        {
            manager.asteroidGenerator.spawnAsteroids(350);
        }
        this.newSaveGameData();
    }

    public void clearModuleButtons()
    {
        foreach (Button button in moduleListButtons)
        {
            button.image.color = Color.gray;
        }
        foreach (ScrollRect scroller in moduleScrollers)
        {
            scroller.gameObject.SetActive(false);
        }
    }

    public void shieldModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[0].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[0].GetComponent<RectTransform>().rect.size;
        moduleListButtons[0].image.color = gametypeSelectedColor;
        selectedModuleType = "Shield";
        shieldScroller.gameObject.SetActive(true);
    }

    public void engineModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[1].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[1].GetComponent<RectTransform>().rect.size;
        moduleListButtons[1].image.color = gametypeSelectedColor;
        selectedModuleType = "Engine";
    }

    public void torpedoModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[2].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[2].GetComponent<RectTransform>().rect.size;
        moduleListButtons[2].image.color = gametypeSelectedColor;
        selectedModuleType = "Torpedo";
    }

    public void missileModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[3].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[3].GetComponent<RectTransform>().rect.size;
        moduleListButtons[3].image.color = gametypeSelectedColor;
        selectedModuleType = "Missile";
    }

    public void fixedRailgunModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[4].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[4].GetComponent<RectTransform>().rect.size;
        moduleListButtons[4].image.color = gametypeSelectedColor;
        selectedModuleType = "FixedRailgun";
    }

    public void turretedRailgunModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[5].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[5].GetComponent<RectTransform>().rect.size;
        moduleListButtons[5].image.color = gametypeSelectedColor;
        selectedModuleType = "TurretedRailgun";
    }

    public void fixedMachinegunModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[6].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[6].GetComponent<RectTransform>().rect.size;
        moduleListButtons[6].image.color = gametypeSelectedColor;
        selectedModuleType = "FixedMachinegun";
    }

    public void turretedMachinegunModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[7].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[7].GetComponent<RectTransform>().rect.size;
        moduleListButtons[7].image.color = gametypeSelectedColor;
        selectedModuleType = "TurretedMachinegun";
    }

    public void fixedEMACModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[8].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[8].GetComponent<RectTransform>().rect.size;
        moduleListButtons[8].image.color = gametypeSelectedColor;
        selectedModuleType = "FixedEMAC";
    }

    public void turretedEMACModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[9].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[9].GetComponent<RectTransform>().rect.size;
        moduleListButtons[9].image.color = gametypeSelectedColor;
        selectedModuleType = "TurretedEMAC";
    }

    public void fixedLaserModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[10].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[10].GetComponent<RectTransform>().rect.size;
        moduleListButtons[10].image.color = gametypeSelectedColor;
        selectedModuleType = "FixedLaser";
    }

    public void turretedLaserModule()
    {
        this.clearModuleButtons();
        moduleSelectionIndicator.transform.position = moduleListButtons[11].transform.position;
        moduleSelectionIndicator.GetComponent<RectTransform>().sizeDelta = moduleListButtons[11].GetComponent<RectTransform>().rect.size;
        moduleListButtons[11].image.color = gametypeSelectedColor;
        selectedModuleType = "TurretedLaser";
    }

    public void resetCuboctahedron()
    {
        string[] temporary = new string[100];
        for (int i = 0; i < 100; i++)
        {
            temporary[i] = "null";
        }
        playerData.truncatedCuboctahedronLoadouts = new string[3][];
        for (int i = 0; i < 3; i++)
        {
            playerData.truncatedCuboctahedronLoadouts[i] = temporary;
        }
        this.savePlayerData();
        Debug.Log("cuboctahedron reset");
    }

    public void resetAllLoadouts()
    {
        string[] temporary = new string[100];
        for(int i = 0; i < 100; i++)
        {
            temporary[i] = "null";
        }
        playerData.tetrahedronLoadouts = new string[3][];
        playerData.cubeLoadouts = new string[3][];
        playerData.octahedronLoadouts = new string[3][];
        playerData.dodecahedronLoadouts = new string[3][];
        playerData.icosahedronLoadouts = new string[3][];
        playerData.truncatedTetrahedronLoadouts = new string[3][];
        playerData.cuboctahedronLoadouts = new string[3][];
        playerData.truncatedCubeLoadouts = new string[3][];
        playerData.truncatedOctahedronLoadouts = new string[3][];
        playerData.rhombicuboctahedronLoadouts = new string[3][];
        playerData.truncatedCuboctahedronLoadouts = new string[3][];
        playerData.snubCubeLoadouts = new string[3][];
        playerData.icosidodecahedronLoadouts = new string[3][];
        playerData.truncatedDodecahedronLoadouts = new string[3][];
        playerData.rhombicosidodecahedronLoadouts = new string[3][];
        playerData.truncatedIcosidodecahedronLoadouts = new string[3][];
        playerData.snubDodecahedronLoadouts = new string[3][];
        playerData.truncatedIcosihedronLoadouts = new string[3][];
        for (int i = 0; i < 3; i++)
        {
            playerData.tetrahedronLoadouts[i] = temporary;
            playerData.cubeLoadouts[i] = temporary;
            playerData.octahedronLoadouts[i] = temporary;
            playerData.dodecahedronLoadouts[i] = temporary;
            playerData.icosahedronLoadouts[i] = temporary;
            playerData.truncatedTetrahedronLoadouts[i] = temporary;
            playerData.cuboctahedronLoadouts[i] = temporary;
            playerData.truncatedCubeLoadouts[i] = temporary;
            playerData.truncatedOctahedronLoadouts[i] = temporary;
            playerData.rhombicuboctahedronLoadouts[i] = temporary;
            playerData.truncatedCuboctahedronLoadouts[i] = temporary;
            playerData.snubCubeLoadouts[i] = temporary;
            playerData.icosidodecahedronLoadouts[i] = temporary;
            playerData.truncatedDodecahedronLoadouts[i] = temporary;
            playerData.rhombicosidodecahedronLoadouts[i] = temporary;
            playerData.truncatedIcosidodecahedronLoadouts[i] = temporary;
            playerData.snubDodecahedronLoadouts[i] = temporary;
            playerData.truncatedIcosihedronLoadouts[i] = temporary;
        }
        Debug.Log("Player Loadouts reset.");
        this.savePlayerData();
    }

    public void resetAllEnemyLoadouts()
    {
        this.newLoadGameData();
        string[][] init = new string[3][];
        string[] temp = new string[100];
        for (int i = 0; i < 100; i++)
        {
            temp[i] = "null";
        }
        gameData.enemyTetrahedronLoadouts = new string[3][];
        gameData.enemyCubeLoadouts = new string[3][];
        gameData.enemyOctahedronLoadouts = new string[3][];
        gameData.enemyDodecahedronLoadouts = new string[3][];
        gameData.enemyIcosahedronLoadouts = new string[3][];
        gameData.enemyTruncatedTetrahedronLoadouts = new string[3][];
        gameData.enemyCuboctahedronLoadouts = new string[3][];
        gameData.enemyTruncatedCubeLoadouts = new string[3][];
        gameData.enemyTruncatedOctahedronLoadouts = new string[3][];
        gameData.enemyRhombicuboctahedronLoadouts = new string[3][];
        gameData.enemyTruncatedCuboctahedronLoadouts = new string[3][];
        gameData.enemySnubCubeLoadouts = new string[3][];
        gameData.enemyIcosidodecahedronLoadouts = new string[3][];
        gameData.enemyTruncatedDodecahedronLoadouts = new string[3][];
        gameData.enemyRhombicosidodecahedronLoadouts = new string[3][];
        gameData.enemyTruncatedIcosidodecahedronLoadouts = new string[3][];
        gameData.enemySnubDodecahedronLoadouts = new string[3][];
        gameData.enemyTruncatedIcosihedronLoadouts = new string[3][];
        for (int i = 0; i < 3; i++)
        {
            gameData.enemyTetrahedronLoadouts[i] = temp;
            gameData.enemyCubeLoadouts[i] = temp;
            gameData.enemyOctahedronLoadouts[i] = temp;
            gameData.enemyDodecahedronLoadouts[i] = temp;
            gameData.enemyIcosahedronLoadouts[i] = temp;
            gameData.enemyTruncatedTetrahedronLoadouts[i] = temp;
            gameData.enemyCuboctahedronLoadouts[i] = temp;
            gameData.enemyTruncatedCubeLoadouts[i] = temp;
            gameData.enemyTruncatedOctahedronLoadouts[i] = temp;
            gameData.enemyRhombicuboctahedronLoadouts[i] = temp;
            gameData.enemyTruncatedCuboctahedronLoadouts[i] = temp;
            gameData.enemySnubCubeLoadouts[i] = temp;
            gameData.enemyIcosidodecahedronLoadouts[i] = temp;
            gameData.enemyTruncatedDodecahedronLoadouts[i] = temp;
            gameData.enemyRhombicosidodecahedronLoadouts[i] = temp;
            gameData.enemyTruncatedIcosidodecahedronLoadouts[i] = temp;
            gameData.enemySnubDodecahedronLoadouts[i] = temp;
            gameData.enemyTruncatedIcosihedronLoadouts[i] = temp;
        }
        Debug.Log("Enemy loadouts reset.");
        this.saveEnemyLoadouts();
    }

    public void resetCurrentLoadout()
    {
        string[] temp = new string[100];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = "null";
        }
        if (shipType == "tetrahedron")
        {
            playerData.tetrahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "cube")
        {
            playerData.cubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "octahedron")
        {
            playerData.octahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "dodecahedron")
        {
            playerData.dodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "icosahedron")
        {
            playerData.icosahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedTetrahedron")
        {
            playerData.truncatedTetrahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "cuboctahedron")
        {
            playerData.cuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedCube")
        {
            playerData.truncatedCubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedOctahedron")
        {
            playerData.truncatedOctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "rhombicuboctahedron")
        {
            playerData.rhombicuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedCuboctahedron")
        {
            playerData.truncatedCuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "snubCube")
        {
            playerData.snubCubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "icosidodecahedron")
        {
            playerData.icosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedDodecahedron")
        {
            playerData.truncatedDodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedIcosahedron")
        {
            playerData.truncatedIcosihedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "rhombicosidodecahedron")
        {
            playerData.rhombicosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedIcosidodecahedron")
        {
            playerData.truncatedIcosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "snubDodecahedron")
        {
            playerData.snubDodecahedronLoadouts[loadoutNumber] = temp;
        }
        this.savePlayerData();
        this.selectLoadout(loadoutNumber);
    }

    public void setHighscores()
    {
        tetrahedronRecord.text = playerData.tetrahedronHighScore.ToString();
        cubeRecord.text = playerData.cubeHighScore.ToString();
        octahedronRecord.text = playerData.octahedronHighScore.ToString();
        icosahedronRecord.text = playerData.icosahedronHighScore.ToString();
        dodecahedronRecord.text = playerData.dodecahedronHighScore.ToString();
        truncatedTetrahedronRecord.text = playerData.truncatedTetrahedronHighScore.ToString();
        truncatedOctahedronRecord.text = playerData.truncatedOctahedronHighScore.ToString();
        truncatedCubeRecord.text = playerData.truncatedCubeHighScore.ToString();
        cuboctahedronRecord.text = playerData.cuboctahedronHighScore.ToString();
        truncatedCuboctahedronRecord.text = playerData.truncatedCuboctahedronHighScore.ToString();
        rhombicuboctahedronRecord.text = playerData.rhombicuboctahedronHighScore.ToString();
        truncatedIcosahedronRecord.text = playerData.truncatedIcosahedronHighScore.ToString();
        truncatedDodecahedronRecord.text = playerData.truncatedDodecahedronHighScore.ToString();
        icosidodecahedronRecord.text = playerData.icosidodecahedronHighScore.ToString();
        snubCubeRecord.text = playerData.snubCubeHighScore.ToString();
        rhombicosidodecahedronRecord.text = playerData.rhombicosidodecahedronHighScore.ToString();
        truncatedIcosidodecahedronRecord.text = playerData.truncatedIcosidodecahedronHighScore.ToString();
        snubDodecahedronRecord.text = playerData.snubDodecahedronHighScore.ToString();

        raceLargestSize.text = (playerData.raceLargestSize / 2).ToString();
        highestXPFromRace.text = playerData.highestXPFromRace.ToString();
        highestXPSphereRadius.text = playerData.highestXPSphereRadius.ToString("0");
        allShipSurvivalShip.text = playerData.allShipSurvivalShip;
        allShipSurvivalScore.text = playerData.allShipSurvivalScore.ToString();
        allShipSurvivalSkillScore.text = playerData.allShipSurvivalSkill.ToString("0.000");
    }

    public void saveCurrentLoadout()
    {
        string[] temp = new string[modules.Length];
        moduleNumber = 0;
        foreach (ModuleActivater module in modules)
        {
            temp[moduleNumber] = module.type;
            moduleNumber++;
        }
        if (shipType == "tetrahedron")
        {
            playerData.tetrahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "cube")
        {
            playerData.cubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "octahedron")
        {
            playerData.octahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "dodecahedron")
        {
            playerData.dodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "icosahedron")
        {
            playerData.icosahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedTetrahedron")
        {
            playerData.truncatedTetrahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "cuboctahedron")
        {
            playerData.cuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedCube")
        {
            playerData.truncatedCubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedOctahedron")
        {
            playerData.truncatedOctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "rhombicuboctahedron")
        {
            playerData.rhombicuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedCuboctahedron")
        {
            playerData.truncatedCuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "snubCube")
        {
            playerData.snubCubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "icosidodecahedron")
        {
            playerData.icosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedDodecahedron")
        {
            playerData.truncatedDodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedIcosahedron")
        {
            playerData.truncatedIcosihedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "rhombicosidodecahedron")
        {
            playerData.rhombicosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedIcosidodecahedron")
        {
            playerData.truncatedIcosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "snubDodecahedron")
        {
            playerData.snubDodecahedronLoadouts[loadoutNumber] = temp;
        }
        this.savePlayerData();
    }

    public void setPlayerShip()
    {
        if (gameData.selectedShip == "tetrahedron")
        {
            this.tetrahedronShip();
        }
        if (gameData.selectedShip == "cube")
        {
            this.cubeShip();
        }
        if (gameData.selectedShip == "octahedron")
        {
            this.octahedronShip();
        }
        if (gameData.selectedShip == "dodecahedron")
        {
            this.dodecahedronShip();
        }
        if (gameData.selectedShip == "icosahedron")
        {
            this.icosahedronShip();
        }
        if (gameData.selectedShip == "truncatedTetrahedron")
        {
            this.truncatedTetrahedronShip();
        }
        if (gameData.selectedShip == "cuboctahedron")
        {
            this.cuboctahedronShip();
        }
        if (gameData.selectedShip == "truncatedCube")
        {
            this.truncatedCubeShip();
        }
        if (gameData.selectedShip == "truncatedOctahedron")
        {
            this.truncatedOctahedronShip();
        }
        if (gameData.selectedShip == "rhombicuboctahedron")
        {
            this.rhombicuboctahedronShip();
        }
        if (gameData.selectedShip == "truncatedCuboctahedron")
        {
            this.truncatedCuboctahedronShip();
        }
        if (gameData.selectedShip == "snubCube")
        {
            this.snubCubeShip();
        }
        if (gameData.selectedShip == "icosidodecahedron")
        {
            this.icosidodecahedronShip();
        }
        if (gameData.selectedShip == "truncatedDodecahedron")
        {
            this.truncatedDodecahedronShip();
        }
        if (gameData.selectedShip == "truncatedIcosahedron")
        {
            this.truncatedIcosahedronShip();
        }
        if (gameData.selectedShip == "rhombicosidodecahedron")
        {
            this.rhombicosidodecahedronShip();
        }
        if (gameData.selectedShip == "truncatedIcosidodecahedron")
        {
            this.truncatedIcosidodecahedronShip();
        }
        if (gameData.selectedShip == "snubDodecahedron")
        {
            this.snubDodecahedronShip();
        }
        if (gameData.selectedShip == null)
        {
            this.tetrahedronShip();
        }
    }

    public void saveCurrentEnemyLoadout()
    {
        string[] temp = new string[modules.Length];
        moduleNumber = 0;
        foreach (ModuleActivater module in modules)
        {
            temp[moduleNumber] = module.type;
            moduleNumber++;
        }
        if (shipType == "tetrahedron")
        {
            gameData.enemyTetrahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "cube")
        {
            gameData.enemyCubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "octahedron")
        {
            gameData.enemyOctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "dodecahedron")
        {
            gameData.enemyDodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "icosahedron")
        {
            gameData.enemyIcosahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedTetrahedron")
        {
            gameData.enemyTruncatedTetrahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "cuboctahedron")
        {
            gameData.enemyCuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedCube")
        {
            gameData.enemyTruncatedCubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedOctahedron")
        {
            gameData.enemyTruncatedOctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "rhombicuboctahedron")
        {
            gameData.enemyRhombicuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedCuboctahedron")
        {
            gameData.enemyTruncatedCuboctahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "snubCube")
        {
            gameData.enemySnubCubeLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "icosidodecahedron")
        {
            gameData.enemyIcosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedDodecahedron")
        {
            gameData.enemyTruncatedDodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedIcosahedron")
        {
            gameData.enemyTruncatedIcosihedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "rhombicosidodecahedron")
        {
            gameData.enemyRhombicosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "truncatedIcosidodecahedron")
        {
            gameData.enemyTruncatedIcosidodecahedronLoadouts[loadoutNumber] = temp;
        }
        if (shipType == "snubDodecahedron")
        {
            gameData.enemySnubDodecahedronLoadouts[loadoutNumber] = temp;
        }
        this.saveEnemyLoadouts();
    }

    public void updateControls()
    {
        for (int i = 0; i < playerData.controls.Length; i++)
        {
            controlInputs[i].text = playerData.controls[i];
        }
    }

    public void saveControls()
    {
        for (int i = 0; i < playerData.controls.Length; i++)
        {
            playerData.controls[i] = controlInputs[i].text.ToLower();
        }
        this.savePlayerData();
    }

    public void savePlayerName()
    {
        if (playerName.text != null && playerName.text != "")
        {
            playerData.playerName = playerName.text;
            this.savePlayerData();
            enterNameButton.gameObject.SetActive(false);
            playerNameEntryPanel.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            leaderboardButton.enabled = true;
        }
    }

    public void saveEnemyLoadouts()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "gameData.dat");
        if(gameData == null)
        {
            gameData = new GameData();
        }
        bf.Serialize(file, gameData);
        file.Close();
        Debug.Log("Enemy loadouts saved successfully");
    }

    public void savePlayerData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "playerData.dat");
        if(playerData == null)
        {
            playerData = new PlayerData();
        }
        bf.Serialize(file, playerData);
        file.Close();
        Debug.Log("Player data saved successfully");
    }

    public void clearPlayer()
    {
        playerData = new PlayerData();
        this.resetAllLoadouts();
        playerData.playerID = "!" + ((int)(UnityEngine.Random.Range(1000, 1000000))).ToString();
        this.savePlayerData();
    }

    public void loadGameData()
    {
        if(File.Exists(Application.persistentDataPath + "gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "gameData.dat");
            gameData = (GameData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Game data loaded successfully");
        }
    }

    public void newSaveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(System.IO.Path.Combine(Application.streamingAssetsPath, "gameData.dat"));
        if (gameData == null)
        {
            gameData = new GameData();
        }
        bf.Serialize(file, gameData);
        file.Close();
        Debug.Log("Game data saved successfully");
    }

    public void newLoadGameData()
    {
        if (File.Exists(System.IO.Path.Combine(Application.streamingAssetsPath, "gameData.dat")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(System.IO.Path.Combine(Application.streamingAssetsPath, "gameData.dat"));
            gameData = (GameData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Game data streamed successfully");
        }
    }

    public void loadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "playerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "playerData.dat");
            playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Player data loaded successfully");
        }
        else
        {
            this.savePlayerData();
            this.clearPlayer();
            this.resetAllLoadouts();
            this.savePlayerData();
        }
        shipUnlockTracker = new bool[18] { playerData.tetrahedronHasBeenUnlocked, playerData.octahedronHasBeenUnlocked, playerData.cubeHasBeenUnlocked, playerData.icosahedronHasBeenUnlocked,
        playerData.cuboctahedronHasBeenUnlocked, playerData.truncatedTetrahedronHasBeenUnlocked, playerData.snubCubeHasBeenUnlocked, playerData.rhombicuboctahedronHasBeenUnlocked, playerData.dodecahedronHasBeenUnlocked,
        playerData.truncatedOctahedronHasBeenUnlocked, playerData.icosidodecahedronHasBeenUnlocked, playerData.truncatedCubeHasBeenUnlocked, playerData.snubDodecahedronHasBeenUnlocked, playerData.rhombicosidodecahedronHasBeenUnlocked,
        playerData.truncatedCuboctahedronHasBeenUnlocked, playerData.truncatedIcosahedronHasBeenUnlocked, playerData.truncatedDodecahedronHasBeenUnlocked, playerData.truncatedIcosidodecahedronHasBeenUnlocked};
    }

    public void loadSettings()
    {
        if (gameData.framerateLimit == 0)
        {
            gameData.framerateLimit = 20;
        }
        framerateLimit.value = gameData.framerateLimit;
        volumeSlider.value = playerData.volumeLevel;
        physicsIterationsSlider.value = playerData.physicsCollisionIterations;
        this.setVolume(playerData.volumeLevel);
        this.setPhysicsCollisionIterations(playerData.physicsCollisionIterations);
    }
}

[Serializable]
public class GameData
{
    public string selectedShip;
    public string selectedGametype;
    public string selectedLocation;
    public string[][] enemyTetrahedronLoadouts;
    public string[][] enemyCubeLoadouts;
    public string[][] enemyOctahedronLoadouts;
    public string[][] enemyDodecahedronLoadouts;
    public string[][] enemyIcosahedronLoadouts;
    public string[][] enemyTruncatedTetrahedronLoadouts;
    public string[][] enemyCuboctahedronLoadouts;
    public string[][] enemyTruncatedCubeLoadouts;
    public string[][] enemyTruncatedOctahedronLoadouts;
    public string[][] enemyRhombicuboctahedronLoadouts;
    public string[][] enemyTruncatedCuboctahedronLoadouts;
    public string[][] enemySnubCubeLoadouts;
    public string[][] enemyIcosidodecahedronLoadouts;
    public string[][] enemyTruncatedDodecahedronLoadouts;
    public string[][] enemyRhombicosidodecahedronLoadouts;
    public string[][] enemyTruncatedIcosidodecahedronLoadouts;
    public string[][] enemySnubDodecahedronLoadouts;
    public string[][] enemyTruncatedIcosihedronLoadouts;
    public float[] raceAsteroidXPositions;
    public float[] raceAsteroidYPositions;
    public float[] raceAsteroidZPositions;
    public int[] raceAsteroidMaterials;
    public float raceAsteroidSize;
    public int framerateLimit;
}

[Serializable]
public class PlayerData
{
    public string playerName;
    public string playerID;
    public float experience;
    public int skillRankExperience;
    public float fieldOfView = 85;
    public float volumeLevel = 0;
    public int physicsCollisionIterations = 6;
    public bool musicEnabled;
    public bool asteroidsOn = true;
    public bool beginnerModeOn = false;
    public bool fullscreenOn = true;
    public bool showControlsPopup = true;
    public int resolution = 123456789;

    public int raceLargestSize;
    public int highestXPFromRace;
    public float highestXPSphereRadius;

    public string allShipSurvivalShip;
    public int allShipSurvivalScore = 0;
    public float allShipSurvivalSkill = 0;

    public int lastLevel = 1;
    public int lastSkillLevel = 1;
    public int lastRaceExperience = 0;

    public int tetrahedronHighScore;
    public int cubeHighScore;
    public int octahedronHighScore;
    public int dodecahedronHighScore;
    public int icosahedronHighScore;
    public int truncatedTetrahedronHighScore;
    public int cuboctahedronHighScore;
    public int truncatedCubeHighScore;
    public int truncatedOctahedronHighScore;
    public int rhombicuboctahedronHighScore;
    public int truncatedCuboctahedronHighScore;
    public int snubCubeHighScore;
    public int icosidodecahedronHighScore;
    public int truncatedDodecahedronHighScore;
    public int rhombicosidodecahedronHighScore;
    public int truncatedIcosidodecahedronHighScore;
    public int snubDodecahedronHighScore;
    public int truncatedIcosahedronHighScore;

    public bool tetrahedronHasBeenUnlocked = true;
    public bool cubeHasBeenUnlocked = false;
    public bool octahedronHasBeenUnlocked = false;
    public bool dodecahedronHasBeenUnlocked = false;
    public bool icosahedronHasBeenUnlocked = false;
    public bool truncatedTetrahedronHasBeenUnlocked = false;
    public bool cuboctahedronHasBeenUnlocked = false;
    public bool truncatedCubeHasBeenUnlocked = false;
    public bool truncatedOctahedronHasBeenUnlocked = false;
    public bool rhombicuboctahedronHasBeenUnlocked = false;
    public bool truncatedCuboctahedronHasBeenUnlocked = false;
    public bool snubCubeHasBeenUnlocked = false;
    public bool icosidodecahedronHasBeenUnlocked = false;
    public bool truncatedDodecahedronHasBeenUnlocked = false;
    public bool rhombicosidodecahedronHasBeenUnlocked = false;
    public bool truncatedIcosidodecahedronHasBeenUnlocked = false;
    public bool snubDodecahedronHasBeenUnlocked = false;
    public bool truncatedIcosahedronHasBeenUnlocked = false;

    public string[] controls;
    public string[][] tetrahedronLoadouts;
    public string[][] cubeLoadouts;
    public string[][] octahedronLoadouts;
    public string[][] dodecahedronLoadouts;
    public string[][] icosahedronLoadouts;
    public string[][] truncatedTetrahedronLoadouts;
    public string[][] cuboctahedronLoadouts;
    public string[][] truncatedCubeLoadouts;
    public string[][] truncatedOctahedronLoadouts;
    public string[][] rhombicuboctahedronLoadouts;
    public string[][] truncatedCuboctahedronLoadouts;
    public string[][] snubCubeLoadouts;
    public string[][] icosidodecahedronLoadouts;
    public string[][] truncatedDodecahedronLoadouts;
    public string[][] rhombicosidodecahedronLoadouts;
    public string[][] truncatedIcosidodecahedronLoadouts;
    public string[][] snubDodecahedronLoadouts;
    public string[][] truncatedIcosihedronLoadouts;
}