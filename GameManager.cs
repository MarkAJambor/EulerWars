using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GPUInstancer;

public class GameManager : MonoBehaviour {

    public GameData gameData;
    public MainMenu main;
    public PlayerData playerData;
    public Material cherryGarySkybox;
    public Material sagASkybox;
    public Material ceresSkybox;
    public Material raceAsteroidMaterial1;
    public Material raceAsteroidMaterial2;
    public Material raceAsteroidMaterial3;
    public Material raceAsteroidMaterial4;
    public Material raceAsteroidMaterial5;
    public Material raceAsteroidMaterial6;
    public Material raceAsteroidMaterial7;
    public Material raceAsteroidMaterial8;
    public Material[] raceAsteroidMaterials;
    public GameObject healthBoost;
    public GameObject outerCage;
    public GameObject orbitingTarget1;
    public GameObject orbitingTarget2;
    public GameObject orbitingTarget3;
    public GameObject orbitingObject1;
    public GameObject orbitingObject2;
    public GameObject orbitingObject3;
    public GameObject engineModule;
    public GameObject shieldModule;
    public GameObject EMACModule;
    public GameObject machinegunModule;
    public GameObject railgunModule;
    public GameObject missileModule;
    public GameObject torpedoModule;
    public GameObject laserModule;
    public GameObject asteroid;
    public GameObject cherryGary;
    public GameObject spaceStation;
    public GameObject enemy;
    public GameObject machinegunAmmo;
    public GameObject turretMachinegunAmmo;
    public GameObject railgunLeadReticle;
    public GameObject machinegunLeadReticle;
    public GameObject EMACLeadReticle;
    public GameObject enemyIndicator;
    public GameObject spawnedEnemyShip;
    public GameObject shield;
    public GameObject missile;
    public GameObject tetrahedronShip;
    public GameObject cubeShip;
    public GameObject octahedronShip;
    public GameObject icosahedronShip;
    public GameObject dodecahedronShip;
    public GameObject truncatedTetrahedronShip;
    public GameObject truncatedOctahedronShip;
    public GameObject truncatedCubeShip;
    public GameObject cuboctahedronShip;
    public GameObject truncatedCuboctahedronShip;
    public GameObject rhombicuboctahedronShip;
    public GameObject truncatedIcosahedronShip;
    public GameObject truncatedDodecahedronShip;
    public GameObject icosidodecahedronShip;
    public GameObject snubCubeShip;
    public GameObject rhombicosidodecahedronShip;
    public GameObject truncatedIcosidodecahedronShip;
    public GameObject snubDodecahedronShip;
    public GameObject railgunEffect;
    public GameObject machinegunEffect;
    public GameObject EMACEffect;
    public GameObject plasmaEffect;
    public GameObject explosionEffect;
    public GameObject laserEffect;
    public GameObject shipDestructionEffect;
    public GameObject spawnEffect;
    public GameObject moduleDestructionEffect;
    public GameObject raceAsteroid;
    public GameObject[] raceAsteroids;
    public GameObject nestedDodecahedron;
    public GameObject nestedIcosahedron;
    public GameObject nestedTetrahedron;
    public GameObject pentagon120;
    public GameObject sagACage;
    public GameObject mirrorArray;
    public GameObject enemyInformationPanel;
    public GameObject raceInformationPanel;
    public GameObject survivalEndGamePanel;
    public GameObject raceEndGamePanel;
    public GameObject skillIncreasePanel;
    public GameObject controlsPopup;
    public GameObject enemyRadarBorder;
    public AudioMixer audioMixer;
    public AudioClip laserSoundEffect;
    public AudioClip shieldImpactSoundEffect;
    public AudioSource shieldImpactSource;
    public AudioSource backgroundMusic;
    public AudioSource obviousChickenCluck;
    public AstroidGenerator asteroidGenerator;
    public Slider volumeSlider;
    public Text finalTime;
    public Text currentTime;
    public Text raceExperience;
    public Text survivalExperience;
    public Text skillPoints;
    public Text shipTypeIndicator;
    public Text frameRate;
    public Text skillIncreaseText;
    public Text[] controlsPopupLabels;
    public int[] raceAsteroidMaterialNumbers;
    public Vector3 relativeVelocity;
    public AIController spawnedEnemyController;
    public PlayerManagement playerManager;
    public InputField[] controlInputs;
    public bool enemiesShouldBeRefreshed;
    public bool gameHasEnded = false;
    public bool asteroidsOn = false;
    public bool maxNumberOfEffectsHasChanged = false;
    public string currentAllShipSurvivalShip = "null";
    public string gameType;
    public string shipType;
    public string[][] enemyShipLoadouts;
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
    public int goalFramerate = 40;
    public int allShipSurvivalRound = 1;
    public int asteroidFailures = 0;
    public int minRange = 20;
    public int maxRange = 100;
    public int enemySpawnRadius = 250;
    public int minScale = 1;
    public int maxScale = 50;
    public int initialNumberOfEnemies = 1;
    public int gameBoundaryDistance;
    public int currentNumberOfEnemies;
    public int loadoutNumber;
    public int frameNumber = 0;
    public int framesToAverage = 0;
    public int numberOfEffects;
    public int maxNumberOfEffects;
    public float lastEffectReset = 0;
    private float[] framerates;
    public float lastFireRateAdjustment = 0;
    public float fireRateAjustmentDelay = 5;
    public float averageFramerate;
    public float startTime;
    public float score = 0f;
    public float calculatedDamage;
    public float railgunDamage = 1;
    public float railgunSpeed = 64;
    public float fixedRailgunSpeed = 128;
    public float railgunShotDelay = 0.2f;
    public float machinegunDamage = 2;
    public float machinegunSpeed = 32;
    public float fixedMachinegunSpeed = 64;
    public float machinegunShotDelay = 0.1f;
    public float EMACDamage = 16;
    public float EMACSpeed = 16;
    public float fixedEMACSpeed = 32;
    public float EMACShotDelay = 0.2f;
    public float torpedoDamage = 16;
    public float missileDamage = 5;
    public float missileSpeed = 20;
    public float missileShotDelay = 2;
    public float torpedoShotDelay = 2;
    public float turretRotationSpeed = 50;
    public float baseLaserDamage = 10;
    public float fireRateModifier = 1;
    public Vector3 randomVector;
    public Vector3 raceExitDirection;
    public Quaternion randomRotation;
    public float roundStartTime;
    public float defaultForce = 200;
    public float defaultTorque = 1;
    public float asteroidSize;
    public float tetrahedronMass = 0.1178f;
    public float cubeMass = 1f;
    public float octahedronMass = 0.4714f;
    public float dodecahedronMass = 7.6631f;
    public float icosahedronMass = 2.1817f;
    public float truncatedTetrahedronMass = 2.7106f;
    public float cuboctahedronMass = 2.357f;
    public float truncatedCubeMass = 13.5997f;
    public float truncatedOctahedronMass = 11.3137f;
    public float rhombicuboctahedronMass = 8.714f;
    public float truncatedCuboctahedronMass = 41.799f;
    public float snubCubeMass = 7.8893f;
    public float icosidodecahedronMass = 13.8355f;
    public float truncatedDodecahedronMass = 85.0397f;
    public float truncatedIcosahedronMass = 55.2877f;
    public float rhombicosidodecahedronMass = 41.6153f;
    public float truncatedIcosidodecahedronMass = 206.8034f;
    public float snubDodecahedronMass = 37.6167f;
    public float tetrahedronVSA = 14.7029f;
    public float cubeVSA = 6f;
    public float octahedronVSA = 7.3485f;
    public float dodecahedronVSA = 2.6942f;
    public float icosahedronVSA = 3.967f;
    public float truncatedTetrahedronVSA = 4.473f;
    public float cuboctahedronVSA = 4.0153f;
    public float truncatedCubeVSA = 2.385f;
    public float truncatedOctahedronVSA = 2.3674f;
    public float rhombicuboctahedronVSA = 2.4632f;
    public float truncatedCuboctahedronVSA = 1.4774f;
    public float snubCubeVSA = 2.5169f;
    public float icosidodecahedronVSA = 2.1182f;
    public float truncatedDodecahedronVSA = 1.1876f;
    public float truncatedIcosahedronVSA = 1.3133f;
    public float rhombicosidodecahedronVSA = 1.4251f;
    public float truncatedIcosidodecahedronVSA = 0.8428f;
    public float snubDodecahedronVSA = 1.4697f;
    public float triangleRadius = 0.5773f;
    public float squareRadius = 0.7071f;
    public float pentagonRadius = 0.8507f;
    public float hexagonRadius = 1f;
    public float octagonRadius = 1.3066f;
    public float decagonRadius = 1.618f;
    public float tetrahedronScore = 6f;
    public float cubeScore = 12f;
    public float octahedronScore = 12f;
    public float dodecahedronScore = 30f;
    public float icosahedronScore = 30f;
    public float truncatedTetrahedronScore = 18f;
    public float cuboctahedronScore = 24f;
    public float truncatedCubeScore = 36f;
    public float truncatedOctahedronScore = 36f;
    public float rhombicuboctahedronScore = 48f;
    public float truncatedCuboctahedronScore = 72f;
    public float snubCubeScore = 60f;
    public float icosidodecahedronScore = 60f;
    public float truncatedDodecahedronScore = 90f;
    public float truncatedIcosahedronScore = 90f;
    public float rhombicosidodecahedronScore = 120f;
    public float truncatedIcosidodecahedronScore = 180f;
    public float snubDodecahedronScore = 150f;
    public float shipScaleFactor;


    // Use this for initialization
    void Start ()
    {
        roundStartTime = Time.time;
        maxNumberOfEffects = 70 - goalFramerate;
        asteroidGenerator = FindObjectOfType<AstroidGenerator>();
        AudioListener.volume = 1;
        startTime = Time.time;
        Cursor.visible = false;
        main = FindObjectOfType<MainMenu>();
        raceAsteroidMaterials = new Material[8];
        raceAsteroidMaterials[0] = raceAsteroidMaterial1;
        raceAsteroidMaterials[1] = raceAsteroidMaterial2;
        raceAsteroidMaterials[2] = raceAsteroidMaterial3;
        raceAsteroidMaterials[3] = raceAsteroidMaterial4;
        raceAsteroidMaterials[4] = raceAsteroidMaterial5;
        raceAsteroidMaterials[5] = raceAsteroidMaterial6;
        raceAsteroidMaterials[6] = raceAsteroidMaterial7;
        raceAsteroidMaterials[7] = raceAsteroidMaterial8;
        if (main == null)
        {
            this.loadPlayerData();
            this.loadGameData();
            volumeSlider.value = playerData.volumeLevel;
            goalFramerate = gameData.framerateLimit;
            framerates = new float[gameData.framerateLimit];
            asteroidGenerator.initializeAsteroidGenerator();
            gameType = PlayerPrefs.GetString("gameType");
            shipType = PlayerPrefs.GetString("shipType");
            loadoutNumber = PlayerPrefs.GetInt("loadoutNumber");
            asteroidSize = PlayerPrefs.GetFloat("asteroidSize");
            asteroidsOn = playerData.asteroidsOn;
            playerManager = FindObjectOfType<PlayerManagement>();
            playerManager.shipType = shipType;
            playerManager.GetComponentInChildren<Camera>().fieldOfView = playerData.fieldOfView;
            this.setPlayerLoadout();
            this.setLocation();
            this.setGametypeItems();
            this.setEnemyLoadouts(shipType);
            playerManager.initializeAtStart();
            if (gameType == "Survival")
            {
                initialNumberOfEnemies = 1;
                enemyInformationPanel.SetActive(true);
                enemyRadarBorder.SetActive(true);
                this.spawnEnemies(initialNumberOfEnemies);
            }
            else if (gameType == "Race")
            {
                //playerManager.GetComponentInChildren<Camera>().farClipPlane = 15000 / asteroidSize;
                outerCage.SetActive(false);
                playerManager.GetComponentInChildren<Camera>().layerCullSpherical = true;
                float[] distances = new float[32];
                distances[0] = 15000/asteroidSize;
                playerManager.GetComponentInChildren<Camera>().layerCullDistances = distances;
                gameBoundaryDistance *= 2;
                raceInformationPanel.SetActive(true);
                enemyInformationPanel.SetActive(false);
                playerManager.gameObject.transform.position = new Vector3(0, 0, 0);
                enemyRadarBorder.SetActive(false);
                //this.spawnNewRaceAsteroids();
                this.spawnRaceAsteroids();
            }
            else if(gameType == "AllShipSurvival")
            {
                currentAllShipSurvivalShip = "null";
                allShipSurvivalRound = 1;
                enemyInformationPanel.SetActive(true);
                enemyRadarBorder.SetActive(true);
                this.spawnEnemies(allShipSurvivalRound);
            }
            if (playerData.musicEnabled)
            {
                backgroundMusic.Play();
            }
            else
            {
                backgroundMusic.Stop();
            }
        }
        if (main == null)
        {
            if (playerData.showControlsPopup)
            {
                FindObjectOfType<CustomPauseMenu>().gameIsPaused = true;
                Time.timeScale = 0;
                controlsPopup.SetActive(true);
                this.updateControls();
                Cursor.visible = true;
            }
        }
        Debug.Log(shipType);
    }

    public void saveRaceAsteroids()
    {
        gameData.raceAsteroidXPositions = new float[raceAsteroids.Length];
        gameData.raceAsteroidYPositions = new float[raceAsteroids.Length];
        gameData.raceAsteroidZPositions = new float[raceAsteroids.Length];
        gameData.raceAsteroidMaterials = new int[raceAsteroids.Length];
        for (int i = 0; i < raceAsteroids.Length; i++)
        {
            gameData.raceAsteroidXPositions[i] = raceAsteroids[i].transform.position.x;
            gameData.raceAsteroidYPositions[i] = raceAsteroids[i].transform.position.y;
            gameData.raceAsteroidZPositions[i] = raceAsteroids[i].transform.position.z;
            gameData.raceAsteroidMaterials[i] = raceAsteroidMaterialNumbers[i];
        }
        this.saveGameData();
    }

    public void spawnRaceAsteroids()
    {
        //playerManager.transform.position = new Vector3(0, 0, 0);
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
        asteroidGenerator.raceAsteroids(gameData);
    }

    public void spawnNewRaceAsteroids()
    {
        playerManager.transform.position = new Vector3(0, 0, 0);
        raceAsteroids = new GameObject[42000];
        raceAsteroidMaterialNumbers = new int[raceAsteroids.Length];
        float xAxis;
        float yAxis;
        float zAxis;

        for (int i = 0; i < 42000; i++)
        {
            xAxis = UnityEngine.Random.Range(0, 100);
            yAxis = UnityEngine.Random.Range(0, 100);
            zAxis = UnityEngine.Random.Range(0, 100);
            randomRotation.Set(xAxis, yAxis, zAxis, 0);
            randomVector = UnityEngine.Random.insideUnitSphere * gameBoundaryDistance;

            if (Vector3.Distance(randomVector, playerManager.transform.position) > 75)
            {
                raceAsteroids[i] = Instantiate(raceAsteroid, randomVector, randomRotation);
                raceAsteroidMaterialNumbers[i] = UnityEngine.Random.Range(0, 8);
                raceAsteroids[i].GetComponent<Renderer>().material = raceAsteroidMaterials[raceAsteroidMaterialNumbers[i]];
                raceAsteroids[i].transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
            }
            else
            {
                i--;
            }

            if (asteroidFailures >= 50)
            {
                break;
            }
        }
    }

    public void EndGame()
    {
        //Time.timeScale = 0f;
        Cursor.visible = true;
        float experience;
        gameHasEnded = true;
        if (gameType == "Survival")
        {
            experience = score;
            playerData.experience += experience;
            survivalEndGamePanel.SetActive(true);
            survivalExperience.text = "Experience: " + experience.ToString("0");
        }
        else if (gameType == "AllShipSurvival")
        {
            experience = score;
            playerData.experience += experience;
            survivalEndGamePanel.SetActive(true);
            survivalExperience.text = "Experience: " + experience.ToString("0");
            skillPoints.gameObject.SetActive(true);
            skillPoints.text = "Skill Points: " + (score / this.getShipScore(shipType)).ToString("0.000");
        }
        else if (gameType == "Race")
        {
            experience = (int)Mathf.Pow(asteroidSize, 3) / (500 * (Time.time - startTime));
            playerData.experience += experience;
            raceEndGamePanel.SetActive(true);
            finalTime.text = "Final Time: " + (Time.time - startTime).ToString("0.00");
            raceExperience.text = "Experience: " + experience.ToString("0");
            if ((int)(asteroidSize) > playerData.raceLargestSize)
            {
                playerData.raceLargestSize = (int)(asteroidSize);
            }
            if (experience > playerData.highestXPFromRace)
            {
                playerData.highestXPFromRace = (int)experience;
                playerData.highestXPSphereRadius = asteroidSize / 2;
            }
        }
        this.setHighScores();
        this.savePlayerData();
    }

    private void FixedUpdate()
    {
        if (gameType == "Race")
        {
            raceExitDirection = playerManager.transform.position * 1000;
            EMACLeadReticle.transform.position = raceExitDirection;
            machinegunLeadReticle.transform.position = raceExitDirection;
            railgunLeadReticle.transform.position = raceExitDirection;
            enemyIndicator.transform.position = raceExitDirection;
            if (!gameHasEnded)
            {
                currentTime.text = "Time: " + (Time.time - startTime).ToString("0.00") + "s";
            }
            if (Vector3.Distance(playerManager.transform.position, new Vector3(0, 0, 0)) >= gameBoundaryDistance && gameHasEnded != true)
            {
                this.EndGame();
            }
            orbitingObject1.gameObject.SetActive(false);
            orbitingObject2.gameObject.SetActive(false);
            orbitingObject3.gameObject.SetActive(false);
        }
        else
        {
            if (main == null && !gameHasEnded)
            {
                orbitingTarget1.transform.position = new Vector3(Mathf.Cos((Time.time - roundStartTime)/10) * 400, 0, Mathf.Sin((Time.time - roundStartTime) / 10) * 400);
                orbitingObject1.GetComponent<Rigidbody>().AddForce((orbitingTarget1.transform.position - orbitingObject1.transform.position)*1000f, ForceMode.Force);
                orbitingTarget2.transform.position = new Vector3(0, Mathf.Cos((Time.time - roundStartTime - 1.57f) / 10) * 400, Mathf.Sin((Time.time - roundStartTime - 1.57f) / 10) * 400);
                orbitingObject2.GetComponent<Rigidbody>().AddForce((orbitingTarget2.transform.position - orbitingObject2.transform.position) * 1000f, ForceMode.Force);
                orbitingTarget3.transform.position = new Vector3(Mathf.Cos((Time.time - roundStartTime - 1.57f) / 10) * 400, Mathf.Sin((Time.time - roundStartTime - 1.57f) / 10) * 400, 0);
                orbitingObject3.GetComponent<Rigidbody>().AddForce((orbitingTarget3.transform.position - orbitingObject3.transform.position) * 1000f, ForceMode.Force);
            }
        }
        if (Time.time - lastEffectReset > 3)
        {
            lastEffectReset = Time.time;
            numberOfEffects = 0;
        }
        if (fireRateModifier > 5 && !maxNumberOfEffectsHasChanged)
        {
            maxNumberOfEffects /= 2;
            if (fireRateModifier > 20)
            {
                maxNumberOfEffects /= 2;
            }
            maxNumberOfEffectsHasChanged = true;
        }
    }

    private void Update()
    {
        if (main == null)
        {
            framesToAverage = 0;
            if (frameNumber == framerates.Length)
            {
                frameNumber = 0;
            }
            framerates[frameNumber] = 1f / Time.unscaledDeltaTime;
            frameNumber++;
            averageFramerate = 0;
            for (int i = 0; i < framerates.Length; i++)
            {
                if (framerates[i] != 0)
                {
                    averageFramerate += framerates[i];
                    framesToAverage++;
                }
            }
            averageFramerate /= framesToAverage;
            if (main == null)
            {
                frameRate.text = averageFramerate.ToString();
            }
        }
    }

    public GameObject[] spawnAsteroids(int numberOfAsteroids)
    {
        GameObject[] asteroids = new GameObject[numberOfAsteroids];
        float xAxis;
        float yAxis;
        float zAxis;
        asteroidFailures = 0;

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            xAxis = UnityEngine.Random.Range(0, 360);
            yAxis = UnityEngine.Random.Range(0, 360);
            zAxis = UnityEngine.Random.Range(0, 360);
            randomRotation.Set(xAxis, yAxis, zAxis, 0);
            randomVector = UnityEngine.Random.insideUnitSphere * gameBoundaryDistance;
            float scale = UnityEngine.Random.Range(minScale, maxScale);
            if (asteroidFailures >= 100)
            {
                Debug.Log("quitting asteroids");
                break;
            }
            if (!Physics.CheckSphere(randomVector, scale))
            {
                asteroids[i] = Instantiate(asteroid, randomVector, randomRotation);
                asteroids[i].transform.localScale = new Vector3(scale * (UnityEngine.Random.Range(50,100)/100f), scale * (UnityEngine.Random.Range(50, 100) / 100f), scale * (UnityEngine.Random.Range(50, 100) / 100f));
                asteroids[i].GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(UnityEngine.Random.Range(100, 1000), UnityEngine.Random.Range(100, 1000), 0));
                asteroidFailures = 0;
            }
            else
            {
                i--;
                asteroidFailures++;
            }
        }
        return asteroids;
    }

    public void spawnEnemies(int numberOfEnemies)
    {
        Debug.Log("Spawning enemies: " + numberOfEnemies.ToString());
        if (gameType == "Survival")
        {

        }
        else if (gameType == "AllShipSurvival")
        {
            if (currentAllShipSurvivalShip == "null")
            {
                currentAllShipSurvivalShip = "tetrahedron";
            }
            else if (currentAllShipSurvivalShip == "tetrahedron")
            {
                currentAllShipSurvivalShip = "octahedron";
            }
            else if (currentAllShipSurvivalShip == "cube")
            {
                currentAllShipSurvivalShip = "icosahedron";
            }
            else if (currentAllShipSurvivalShip == "octahedron")
            {
                currentAllShipSurvivalShip = "cube";
            }
            else if (currentAllShipSurvivalShip == "dodecahedron")
            {
                currentAllShipSurvivalShip = "truncatedOctahedron";
            }
            else if (currentAllShipSurvivalShip == "icosahedron")
            {
                currentAllShipSurvivalShip = "cuboctahedron";
            }
            else if (currentAllShipSurvivalShip == "truncatedTetrahedron")
            {
                currentAllShipSurvivalShip = "snubCube";
            }
            else if (currentAllShipSurvivalShip == "cuboctahedron")
            {
                currentAllShipSurvivalShip = "truncatedTetrahedron";
            }
            else if (currentAllShipSurvivalShip == "truncatedCube")
            {
                currentAllShipSurvivalShip = "snubDodecahedron";
            }
            else if (currentAllShipSurvivalShip == "truncatedOctahedron")
            {
                currentAllShipSurvivalShip = "icosidodecahedron";
            }
            else if (currentAllShipSurvivalShip == "rhombicuboctahedron")
            {
                currentAllShipSurvivalShip = "dodecahedron";
            }
            else if (currentAllShipSurvivalShip == "truncatedCuboctahedron")
            {
                currentAllShipSurvivalShip = "truncatedIcosahedron";
            }
            else if (currentAllShipSurvivalShip == "snubCube")
            {
                currentAllShipSurvivalShip = "rhombicuboctahedron";
            }
            else if (currentAllShipSurvivalShip == "icosidodecahedron")
            {
                currentAllShipSurvivalShip = "truncatedCube";
            }
            else if (currentAllShipSurvivalShip == "truncatedDodecahedron")
            {
                currentAllShipSurvivalShip = "truncatedIcosidodecahedron";
            }
            else if (currentAllShipSurvivalShip == "truncatedIcosahedron")
            {
                currentAllShipSurvivalShip = "truncatedDodecahedron";
            }
            else if (currentAllShipSurvivalShip == "rhombicosidodecahedron")
            {
                currentAllShipSurvivalShip = "truncatedCuboctahedron";
            }
            else if (currentAllShipSurvivalShip == "truncatedIcosidodecahedron")
            {
                allShipSurvivalRound++;
                numberOfEnemies++;
                currentAllShipSurvivalShip = "tetrahedron";
            }
            else if (currentAllShipSurvivalShip == "snubDodecahedron")
            {
                currentAllShipSurvivalShip = "rhombicosidodecahedron";
            }
            this.setEnemyLoadouts(currentAllShipSurvivalShip);
        }
        currentNumberOfEnemies = numberOfEnemies;
        float xAxis;
        float yAxis;
        float zAxis;
        Vector3 position;
        int loadoutNumber;
        float randomRadius;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            loadoutNumber = UnityEngine.Random.Range(0, enemyShipLoadouts.Length);
            xAxis = 0;
            yAxis = 0;
            zAxis = 0;
            randomRotation.Set(xAxis, yAxis, zAxis, 0);
            while (true)
            {
                randomRadius = UnityEngine.Random.Range(0, gameBoundaryDistance / 2);
                position = (UnityEngine.Random.insideUnitSphere * randomRadius);
                if (Vector3.Distance(position, new Vector3(0, 0, 0)) > 200)
                {
                    break;
                }

            }
            position = (UnityEngine.Random.insideUnitSphere * randomRadius);
            
            if (!Physics.CheckSphere(position, 50))
            {
                spawnedEnemyShip = Instantiate(enemy, position, randomRotation);
                spawnedEnemyController = spawnedEnemyShip.GetComponent<AIController>();
                if (gameType == "Survival")
                {
                    spawnedEnemyController.shipType = shipType;
                }
                else if (gameType == "AllShipSurvival")
                {
                    spawnedEnemyController.shipType = currentAllShipSurvivalShip;
                }
                spawnedEnemyController.moduleTypeArray = enemyShipLoadouts[loadoutNumber];
                spawnedEnemyController.initializeShip();
            }
            else
            {
                i--;
            }
        }
        playerManager.numberOfEnemiesSlider.enabled = true;
        playerManager.numberOfEnemies.enabled = true;
        playerManager.enemies = FindObjectsOfType<AIController>();
        playerManager.numberOfEnemiesSlider.maxValue = numberOfEnemies;
        playerManager.numberOfEnemies.text = "Enemies remaining: " + playerManager.enemies.Length.ToString();
        playerManager.selectFowardmostEnemy();
    }

    public void updateControls()
    {
        for (int i = 0; i < playerData.controls.Length; i++)
        {
            controlInputs[i].text = playerData.controls[i];
        }
        for (int i = 0; i < controlsPopupLabels.Length; i++)
        {
            controlsPopupLabels[i].text = playerData.controls[i];
        }
    }

    public void neverShowControls()
    {
        playerData.showControlsPopup = false;
        this.savePlayerData();
        controlsPopup.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void closeControlsPopup()
    {
        controlsPopup.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void saveControls()
    {
        for (int i = 0; i < playerData.controls.Length; i++)
        {
            playerData.controls[i] = controlInputs[i].text.ToLower();
        }
        this.savePlayerData();
    }

    public float fireDelay()
    {
        if (Time.time > 2)
        {
            float adjustedFireRate = goalFramerate / averageFramerate;
            if (frameNumber > 0)
            {
                framerates[frameNumber - 1] = 50000;
            }
            else
            {
                framerates[framerates.Length - 1] = 50000;
            }
            return adjustedFireRate;
        }
        else
        {
            return 1;
        }
    }

    public float damage(string name)
    {
        if (name == "FixedRailgun")
        {
            //Debug.Log("Hit by Rail Gun");
            return 2 * railgunDamage;
        }
        else if (name == "FixedMachinegun")
        {
            //Debug.Log("Hit by Machinegun");
            return 2 * machinegunDamage;
        }
        else if (name == "FixedEMAC")
        {
            //Debug.Log("Hit by EMAC");
            return 2 * EMACDamage;
        }
        if (name == "TurretedRailgun")
        {
            //Debug.Log("Hit by Turreted Rail Gun");
            return railgunDamage * 0.5f;
        }
        else if (name == "TurretedMachinegun")
        {
            //Debug.Log("Hit by Turreted Machinegun");
            return machinegunDamage * 0.5f;
        }
        else if (name == "TurretedEMAC")
        {
            //Debug.Log("Hit by Turreted EMAC");
            return EMACDamage * 0.5f;
        }
        else if (name == "Torpedo")
        {
            return torpedoDamage;
        }
        else if (name == "Missile")
        {
            return missileDamage;
        }
        else
        {
            return 0;
        }
    }

    public float damageMultiplier(string shape)
    {
        if (shape == "triangle")
        {
            return 0.43f;
        }
        else if (shape == "square")
        {
            return 1f;
        }
        else if (shape == "pentagon")
        {
            return 1.72f;
        }
        else if (shape == "hexagon")
        {
            return 2.6f;
        }
        else if (shape == "octagon")
        {
            return 4.83f;
        }
        else if (shape == "decagon")
        {
            return 7.69f;
        }
        else
        {
            return 1f;
        }
    }

    public float plateHealth(string shape)
    {
        if (shape == "triangle")
        {
            return 0.43f;
        }
        else if (shape == "square")
        {
            return 1f;
        }
        else if (shape == "pentagon")
        {
            return 1.72f;
        }
        else if (shape == "hexagon")
        {
            return 2.6f;
        }
        else if (shape == "octagon")
        {
            return 4.83f;
        }
        else if (shape == "decagon")
        {
            return 7.69f;
        }
        else
        {
            return 1f;
        }
    }

    public float shieldSize(string shape)
    {
        if (shape == "triangle")
        {
            return triangleRadius/triangleRadius;
        }
        else if (shape == "square")
        {
            return squareRadius/triangleRadius;
        }
        else if (shape == "pentagon")
        {
            return pentagonRadius/triangleRadius;
        }
        else if (shape == "hexagon")
        {
            return hexagonRadius/triangleRadius;
        }
        else if (shape == "octagon")
        {
            return octagonRadius/triangleRadius;
        }
        else if (shape == "decagon")
        {
            return decagonRadius/triangleRadius;
        }
        else
        {
            return 1f;
        }
    }

    public float edgeNumber(string shape)
    {
        if (shape == "triangle")
        {
            return 3;
        }
        else if (shape == "square")
        {
            return 4;
        }
        else if (shape == "pentagon")
        {
            return 5;
        }
        else if (shape == "hexagon")
        {
            return 6;
        }
        else if (shape == "octagon")
        {
            return 8;
        }
        else if (shape == "decagon")
        {
            return 10;
        }
        else
        {
            return 1f;
        }
    }

    public int getShipScore(string x)
    {
        if (x == "tetrahedron")
        {
            return (int)tetrahedronScore;
        }
        else if (x == "cube")
        {
            return (int)cubeScore;
        }
        else if (x == "octahedron")
        {
            return (int)octahedronScore;
        }
        else if (x == "dodecahedron")
        {
            return (int)dodecahedronScore;
        }
        else if (x == "icosahedron")
        {
            return (int)icosahedronScore;
        }
        else if (x == "truncatedTetrahedron")
        {
            return (int)truncatedTetrahedronScore;
        }
        else if (x == "cuboctahedron")
        {
            return (int)cuboctahedronScore;
        }
        else if (x == "truncatedCube")
        {
            return (int)truncatedCubeScore;
        }
        else if (x == "truncatedOctahedron")
        {
            return (int)truncatedOctahedronScore;
        }
        else if (x == "rhombicuboctahedron")
        {
            return (int)rhombicuboctahedronScore;
        }
        else if (x == "truncatedCuboctahedron")
        {
            return (int)truncatedCuboctahedronScore;
        }
        else if (x == "snubCube")
        {
            return (int)snubCubeScore;
        }
        else if (x == "icosidodecahedron")
        {
            return (int)icosidodecahedronScore;
        }
        else if (x == "truncatedDodecahedron")
        {
            return (int)truncatedDodecahedronScore;
        }
        else if (x == "truncatedIcosahedron")
        {
            return (int)truncatedIcosahedronScore;
        }
        else if (x == "rhombicosidodecahedron")
        {
            return (int)rhombicosidodecahedronScore;
        }
        else if (x == "truncatedIcosidodecahedron")
        {
            return (int)truncatedIcosidodecahedronScore;
        }
        else if (x == "snubDodecahedron")
        {
            return (int)snubDodecahedronScore;
        }
        else
        {
            return 1000000;
        }
    }

    public string convertShipTypeToReadable(string oldShipType)
    {
        if (oldShipType == "tetrahedron")
        {
            return "Tetrahedron";
        }
        else if (oldShipType == "cube")
        {
            return "Cube";
        }
        else if (oldShipType == "octahedron")
        {
            return "Octahedron";
        }
        else if (oldShipType == "dodecahedron")
        {
            return "Dodecahedron";
        }
        else if (oldShipType == "icosahedron")
        {
            return "Icosahedron";
        }
        else if (oldShipType == "truncatedTetrahedron")
        {
            return "Truncated Tetrahedron";
        }
        else if (oldShipType == "cuboctahedron")
        {
            return "Cuboctahedron";
        }
        else if (oldShipType == "truncatedCube")
        {
            return "Truncated Cube";
        }
        else if (oldShipType == "truncatedOctahedron")
        {
            return "Truncated Octahedron";
        }
        else if (oldShipType == "rhombicuboctahedron")
        {
            return "Rhombicuboctahedron";
        }
        else if (oldShipType == "truncatedCuboctahedron")
        {
            return "Truncated Cuboctahedron";
        }
        else if (oldShipType == "snubCube")
        {
            return "Snub Cube";
        }
        else if (oldShipType == "icosidodecahedron")
        {
            return "Icosidodecahedron";
        }
        else if (oldShipType == "truncatedDodecahedron")
        {
            return "Truncated Dodecahedron";
        }
        else if (oldShipType == "truncatedIcosahedron")
        {
            return "Truncated Icosahedron";
        }
        else if (oldShipType == "rhombicosidodecahedron")
        {
            return "Rhombicosidodecahedron";
        }
        else if (oldShipType == "truncatedIcosidodecahedron")
        {
            return "Truncated Icosidodecahedron";
        }
        else if (oldShipType == "snubDodecahedron")
        {
            return "Snub Dodecahedron";
        }
        else
        {
            return "ERROR";
        }
    }

    public void addScore(string shipShape)
    {
        score += this.getShipScore(shipShape);
        if (playerData.beginnerModeOn)
        {
            playerManager.scoreText.text = "Score: " + (score / 2).ToString();
        }
        else
        {
            playerManager.scoreText.text = "Score: " + score.ToString();
        }
    }
    
    public void setGametypeItems()
    {
        if (gameType == "Survival")
        {
            playerManager.numberOfEnemies.enabled = true;
            playerManager.numberOfEnemiesSlider.enabled = true;
        }
        else
        {
            playerManager.numberOfEnemies.enabled = false;
            playerManager.numberOfEnemiesSlider.enabled = false;
        }
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        if (volume == -35)
        {
            audioMixer.SetFloat("volume", -80);
        }
        playerData.volumeLevel = volumeSlider.value;
        this.savePlayerData();
    }

    public void setPlayerLoadout()
    {
        if (shipType == "tetrahedron")
        {
            playerManager.moduleTypeArray = playerData.tetrahedronLoadouts[loadoutNumber];
        }
        if (shipType == "cube")
        {
            playerManager.moduleTypeArray = playerData.cubeLoadouts[loadoutNumber];
        }
        if (shipType == "octahedron")
        {
            playerManager.moduleTypeArray = playerData.octahedronLoadouts[loadoutNumber];
        }
        if (shipType == "dodecahedron")
        {
            playerManager.moduleTypeArray = playerData.dodecahedronLoadouts[loadoutNumber];
        }
        if (shipType == "icosahedron")
        {
            playerManager.moduleTypeArray = playerData.icosahedronLoadouts[loadoutNumber];
        }
        if (shipType == "truncatedTetrahedron")
        {
            playerManager.moduleTypeArray = playerData.truncatedTetrahedronLoadouts[loadoutNumber];
        }
        if (shipType == "cuboctahedron")
        {
            playerManager.moduleTypeArray = playerData.cuboctahedronLoadouts[loadoutNumber];
        }
        if (shipType == "truncatedCube")
        {
            playerManager.moduleTypeArray = playerData.truncatedCubeLoadouts[loadoutNumber];
        }
        if (shipType == "truncatedOctahedron")
        {
            playerManager.moduleTypeArray = playerData.truncatedOctahedronLoadouts[loadoutNumber];
        }
        if (shipType == "rhombicuboctahedron")
        {
            playerManager.moduleTypeArray = playerData.rhombicuboctahedronLoadouts[loadoutNumber];
        }
        if (shipType == "truncatedCuboctahedron")
        {
            playerManager.moduleTypeArray = playerData.truncatedCuboctahedronLoadouts[loadoutNumber];
        }
        if (shipType == "snubCube")
        {
            playerManager.moduleTypeArray = playerData.snubCubeLoadouts[loadoutNumber];
        }
        if (shipType == "icosidodecahedron")
        {
            playerManager.moduleTypeArray = playerData.icosidodecahedronLoadouts[loadoutNumber];
        }
        if (shipType == "truncatedDodecahedron")
        {
            playerManager.moduleTypeArray = playerData.truncatedDodecahedronLoadouts[loadoutNumber];
        }
        if (shipType == "truncatedIcosahedron")
        {
            playerManager.moduleTypeArray = playerData.truncatedIcosihedronLoadouts[loadoutNumber];
        }
        if (shipType == "rhombicosidodecahedron")
        {
            playerManager.moduleTypeArray = playerData.rhombicosidodecahedronLoadouts[loadoutNumber];
        }
        if (shipType == "truncatedIcosidodecahedron")
        {
            playerManager.moduleTypeArray = playerData.truncatedIcosidodecahedronLoadouts[loadoutNumber];
        }
        if (shipType == "snubDodecahedron")
        {
            playerManager.moduleTypeArray = playerData.snubDodecahedronLoadouts[loadoutNumber];
        }
    }

    public void setEnemyLoadouts(string ship)
    {
        if (ship == "tetrahedron")
        {
            enemyShipLoadouts = gameData.enemyTetrahedronLoadouts;
        }
        if (ship == "cube")
        {
            enemyShipLoadouts = gameData.enemyCubeLoadouts;
        }
        if (ship == "octahedron")
        {
            enemyShipLoadouts = gameData.enemyOctahedronLoadouts;
        }
        if (ship == "dodecahedron")
        {
            enemyShipLoadouts = gameData.enemyDodecahedronLoadouts;
        }
        if (ship == "icosahedron")
        {
            enemyShipLoadouts = gameData.enemyIcosahedronLoadouts;
        }
        if (ship == "truncatedTetrahedron")
        {
            enemyShipLoadouts = gameData.enemyTruncatedTetrahedronLoadouts;
        }
        if (ship == "cuboctahedron")
        {
            enemyShipLoadouts = gameData.enemyCuboctahedronLoadouts;
        }
        if (ship == "truncatedCube")
        {
            enemyShipLoadouts = gameData.enemyTruncatedCubeLoadouts;
        }
        if (ship == "truncatedOctahedron")
        {
            enemyShipLoadouts = gameData.enemyTruncatedOctahedronLoadouts;
        }
        if (ship == "rhombicuboctahedron")
        {
            enemyShipLoadouts = gameData.enemyRhombicuboctahedronLoadouts;
        }
        if (ship == "truncatedCuboctahedron")
        {
            enemyShipLoadouts = gameData.enemyTruncatedCuboctahedronLoadouts;
        }
        if (ship == "snubCube")
        {
            enemyShipLoadouts = gameData.enemySnubCubeLoadouts;
        }
        if (ship == "icosidodecahedron")
        {
            enemyShipLoadouts = gameData.enemyIcosidodecahedronLoadouts;
        }
        if (ship == "truncatedDodecahedron")
        {
            enemyShipLoadouts = gameData.enemyTruncatedDodecahedronLoadouts;
        }
        if (ship == "truncatedIcosahedron")
        {
            enemyShipLoadouts = gameData.enemyTruncatedIcosihedronLoadouts;
        }
        if (ship == "rhombicosidodecahedron")
        {
            enemyShipLoadouts = gameData.enemyRhombicosidodecahedronLoadouts;
        }
        if (ship == "truncatedIcosidodecahedron")
        {
            enemyShipLoadouts = gameData.enemyTruncatedIcosidodecahedronLoadouts;
        }
        if (ship == "snubDodecahedron")
        {
            enemyShipLoadouts = gameData.enemySnubDodecahedronLoadouts;
        }
    }
    
    public void setLocation()
    {
        if (gameType != "Race")
        {
            if (PlayerPrefs.GetString("location") == "cherryGary")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.cherryGarySkybox;
                Instantiate(cherryGary);
            }
            else if (PlayerPrefs.GetString("location") == "sagA*")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.ceresSkybox;
                Instantiate(sagACage);
            }
            else if (PlayerPrefs.GetString("location") == "spaceStation")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.ceresSkybox;
                Instantiate(spaceStation);
            }
            else if (PlayerPrefs.GetString("location") == "asteroidField")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.cherryGarySkybox;
                if (asteroidsOn)
                {
                    //this.spawnAsteroids(600);
                    asteroidGenerator.spawnAsteroids(2000);
                }
                else
                {
                    asteroidGenerator.spawnAsteroids(1000);
                }
            }
            else if (PlayerPrefs.GetString("location") == "mirrorField")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.ceresSkybox;
                Instantiate(mirrorArray);
            }
            else if (PlayerPrefs.GetString("location") == "nestedTetrahedron")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.cherryGarySkybox;
                Instantiate(nestedTetrahedron);
            }
            else if (PlayerPrefs.GetString("location") == "nestedDodecahedron")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.cherryGarySkybox;
                Instantiate(nestedDodecahedron);
            }
            else if (PlayerPrefs.GetString("location") == "nestedIcosahedron")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.cherryGarySkybox;
                Instantiate(nestedIcosahedron);
            }
            else if (PlayerPrefs.GetString("location") == "pentagon120")
            {
                playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.cherryGarySkybox;
                Instantiate(pentagon120);
            }
            if (asteroidsOn)
            {
                if (PlayerPrefs.GetString("location") != "asteroidField")
                {
                    asteroidGenerator.spawnAsteroids(350);
                }
            }
        }
        else
        {
            playerManager.GetComponentInChildren<Camera>().GetComponent<Skybox>().material = this.cherryGarySkybox;
        }
    }

    public void setHighScores()
    {
        bool survivalSkillIncreased = false;
        float increasedSkill = 1;
        if (playerData.beginnerModeOn)
        {
            score /= 2;
        }
        if (gameType == "AllShipSurvival")
        {
            if (score > playerData.allShipSurvivalScore)
            {
                playerData.allShipSurvivalScore = (int)score;
            }
            if ((score / this.getShipScore(shipType)) > playerData.allShipSurvivalSkill)
            {
                playerData.allShipSurvivalSkill = (score / this.getShipScore(shipType));
                playerData.allShipSurvivalShip = shipType;
                skillIncreasePanel.SetActive(true);
                skillIncreaseText.text = "You've increased your All Ship Arena skill rating to " + playerData.allShipSurvivalSkill.ToString("0.000");
            }
        }
        else if (gameType == "Survival")
        {
            if (shipType == "tetrahedron")
            {
                if (playerData.tetrahedronHighScore < (int)(score / tetrahedronScore))
                {
                    playerData.tetrahedronHighScore = (int)(score / tetrahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.tetrahedronHighScore;
                }
            }
            if (shipType == "cube")
            {
                if (playerData.cubeHighScore < (int)(score / cubeScore))
                {
                    playerData.cubeHighScore = (int)(score / cubeScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.cubeHighScore;
                }
            }
            if (shipType == "octahedron")
            {
                if (playerData.octahedronHighScore < (int)(score / octahedronScore))
                {
                    playerData.octahedronHighScore = (int)(score / octahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.octahedronHighScore;
                }
            }
            if (shipType == "dodecahedron")
            {
                if (playerData.dodecahedronHighScore < (int)(score / dodecahedronScore))
                {
                    playerData.dodecahedronHighScore = (int)(score / dodecahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.dodecahedronHighScore;
                }
            }
            if (shipType == "icosahedron")
            {
                if (playerData.icosahedronHighScore < (int)(score / icosahedronScore))
                {
                    playerData.icosahedronHighScore = (int)(score / icosahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.icosahedronHighScore;
                }
            }
            if (shipType == "truncatedTetrahedron")
            {
                if (playerData.truncatedTetrahedronHighScore < (int)(score / truncatedTetrahedronScore))
                {
                    playerData.truncatedTetrahedronHighScore = (int)(score / truncatedTetrahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.truncatedTetrahedronHighScore;
                }
            }
            if (shipType == "cuboctahedron")
            {
                if (playerData.cuboctahedronHighScore < (int)(score / cuboctahedronScore))
                {
                    playerData.cuboctahedronHighScore = (int)(score / cuboctahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.cuboctahedronHighScore;
                }
            }
            if (shipType == "truncatedCube")
            {
                if (playerData.truncatedCubeHighScore < (int)(score / truncatedCubeScore))
                {
                    playerData.truncatedCubeHighScore = (int)(score / truncatedCubeScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.truncatedCubeHighScore;
                }
            }
            if (shipType == "truncatedOctahedron")
            {
                if (playerData.truncatedOctahedronHighScore < (int)(score / truncatedOctahedronScore))
                {
                    playerData.truncatedOctahedronHighScore = (int)(score / truncatedOctahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.truncatedOctahedronHighScore;
                }
            }
            if (shipType == "rhombicuboctahedron")
            {
                if (playerData.rhombicuboctahedronHighScore < (int)(score / rhombicuboctahedronScore))
                {
                    playerData.rhombicuboctahedronHighScore = (int)(score / rhombicuboctahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.rhombicuboctahedronHighScore;
                }
            }
            if (shipType == "truncatedCuboctahedron")
            {
                if (playerData.truncatedCuboctahedronHighScore < (int)(score / truncatedCuboctahedronScore))
                {
                    playerData.truncatedCuboctahedronHighScore = (int)(score / truncatedCuboctahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.truncatedCuboctahedronHighScore;
                }
            }
            if (shipType == "snubCube")
            {
                if (playerData.snubCubeHighScore < (int)(score / snubCubeScore))
                {
                    playerData.snubCubeHighScore = (int)(score / snubCubeScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.snubCubeHighScore;
                }
            }
            if (shipType == "icosidodecahedron")
            {
                if (playerData.icosidodecahedronHighScore < (int)(score / icosidodecahedronScore))
                {
                    playerData.icosidodecahedronHighScore = (int)(score / icosidodecahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.icosidodecahedronHighScore;
                }
            }
            if (shipType == "truncatedDodecahedron")
            {
                if (playerData.truncatedDodecahedronHighScore < (int)(score / truncatedDodecahedronScore))
                {
                    playerData.truncatedDodecahedronHighScore = (int)(score / truncatedDodecahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.truncatedDodecahedronHighScore;
                }
            }
            if (shipType == "truncatedIcosahedron")
            {
                if (playerData.truncatedIcosahedronHighScore < (int)(score / truncatedIcosahedronScore))
                {
                    playerData.truncatedIcosahedronHighScore = (int)(score / truncatedIcosahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.truncatedIcosahedronHighScore;
                }
            }
            if (shipType == "rhombicosidodecahedron")
            {
                if (playerData.rhombicosidodecahedronHighScore < (int)(score / rhombicosidodecahedronScore))
                {
                    playerData.rhombicosidodecahedronHighScore = (int)(score / rhombicosidodecahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.rhombicosidodecahedronHighScore;
                }
            }
            if (shipType == "truncatedIcosidodecahedron")
            {
                if (playerData.truncatedIcosidodecahedronHighScore < (int)(score / truncatedIcosidodecahedronScore))
                {
                    playerData.truncatedIcosidodecahedronHighScore = (int)(score / truncatedIcosidodecahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.truncatedIcosidodecahedronHighScore;
                }
            }
            if (shipType == "snubDodecahedron")
            {
                if (playerData.snubDodecahedronHighScore < (int)(score / snubDodecahedronScore))
                {
                    playerData.snubDodecahedronHighScore = (int)(score / snubDodecahedronScore);
                    survivalSkillIncreased = true;
                    increasedSkill = playerData.snubDodecahedronHighScore;
                }
            }
            if (survivalSkillIncreased)
            {
                skillIncreasePanel.SetActive(true);
                skillIncreaseText.text = "You've increased your skill rating for the " + this.convertShipTypeToReadable(shipType) + " to " + increasedSkill.ToString("0");
                survivalSkillIncreased = false;
            }
        }
    }

    public float collisionCalculator(Rigidbody rb, Vector3 velocity, float damage)
    {
        relativeVelocity = velocity - rb.velocity;
        calculatedDamage = damage * Mathf.Abs(relativeVelocity.magnitude);
        //calculatedDamage = slug.damage;
        return calculatedDamage;
    }

    public Vector3 getInterceptPoint(Vector3 shooterPosition, Vector3 shooterVelocity, float shotSpeed, Vector3 targetPosition, Vector3 targetVelocity)
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime(shotSpeed, targetRelativePosition, targetRelativeVelocity);
        return targetPosition + t * (targetRelativeVelocity);
    }

    public static float FirstOrderInterceptTime(float shotSpeed, Vector3 targetRelativePosition, Vector3 targetRelativeVelocity)
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
        {
            return 0f;
        }

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude / (2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition));
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }

    public void saveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(System.IO.Path.Combine(Application.streamingAssetsPath, "gameData.dat"));
        if (gameData == null)
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
        bf.Serialize(file, playerData);
        file.Close();
        Debug.Log("Player data saved successfully");
    }

    public void loadGameData()
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
    }
}