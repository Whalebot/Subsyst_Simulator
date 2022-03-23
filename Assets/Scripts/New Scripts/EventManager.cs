using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    public bool enableEvents;
    GameManager gameManager;
    TimeManager timeManager;
    public int growthThreshold;
    public int growthCounter;
    public AudioClip populationGrowthSound;
    public delegate void GameEvent();
    public GameEvent populationGrowth;
    public GameEvent populationShrink;
    public GameEvent fedPopulation;
    public GameEvent starvedPopulation;
    public delegate void CataclysmEvent(GameEventSO p);
    public CataclysmEvent cataclysmTrigger;

    public Animator protest;

    public List<GameEventSO> allGameEvents;
    public List<GameEventSO> pendingGameEvents;
    public List<GameEventSO> triggeredGameEvents;
    public Ressources populationUpkeep;
    public EventDescription eventDescription;
    public List<GameEventSO> eventQueue;

    public GameObject paper;

    int approvalCap;

    [TabGroup("Zero Hunger Day")] public int zeroHungerDayThreshold;
    [TabGroup("Zero Hunger Day")] public int zeroHungerDayCounter;
    [TabGroup("Zero Hunger Day")] public bool zeroHungerDayTriggered;
    [TabGroup("Zero Hunger Day")] public GameEventSO zeroHungerDayEvent;

    [TabGroup("Trust in Government")] public int trustInGovernmentThreshold;
    [TabGroup("Trust in Government")] public int trustInGovernmentCounter;
    [TabGroup("Trust in Government")] public bool trustInGovernmentTriggered;
    [TabGroup("Trust in Government")] public GameEventSO trustInGovernmentEvent;
    [TabGroup("Trust in Government")] public GameEventSO foodProtest;
    [TabGroup("Trust in Government")] public GameEventSO climateProtest;
    [TabGroup("Trust in Government")] public GameObject trustVisuals;


    [TabGroup("Clear Air")] public bool clearAirTriggered;
    [TabGroup("Clear Air")] public GameEventSO clearAirEvent;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        timeManager = TimeManager.Instance;
        foreach (var item in allGameEvents)
        {
            item.thresholdCounter = 0;
        }

        cataclysmTrigger += CheckCataclysm;
        timeManager.advanceGameEvent += FeedPopulation;
        timeManager.advanceGameEvent += UniqueEvents;
        timeManager.advanceGameEvent += RegenerateRessources;
        timeManager.advanceGameEvent += CheckGameEvents;
        timeManager.advanceTimeEvent += UpdateUpkeep;

    }

    void RegenerateRessources()
    {
        if (gameManager.NaturalCapital < 1500)
            gameManager.NaturalCapital += 1;
        if (gameManager.Bees < 1500)
            gameManager.Bees += 1;
    }

    public void CheckCataclysm(GameEventSO p)
    {
        if (p == foodProtest || p == climateProtest)
        {
            trustInGovernmentCounter = 0;
        }
    }

    public void ShowEvent(GameEventSO item)
    {
        eventDescription.gameObject.SetActive(true);
        eventDescription.DisplayEvent(item);
        gameManager.PauseGame();
    }

    public void CheckGameEvents()
    {
        if (!enableEvents) return;
        eventQueue.Clear();

        foreach (var item in pendingGameEvents)
        {
            if (item.CheckRequirements())
            {
                if (item.showInPaper)
                {
                    NewsManager.Instance.ActivateNotification();
                    NewsManager.Instance.SpawnNews(item);
                    eventDescription.gameObject.SetActive(true);
                    eventDescription.DisplayEvent(item);

                }
                eventQueue.Add(item);
            }
        }

        foreach (var item in eventQueue)
        {
            TriggerEvent(item);
        }

        //If requirements fullfilled, execute stuff
        if (gameManager.Pollution > 5000)
        {
            gameManager.NaturalCapital -= gameManager.Pollution / 5000;
        }

        if (gameManager.NaturalCapital < 1000)
        {
            gameManager.Bees -= 1500 / Mathf.Clamp(gameManager.NaturalCapital, 15, 1500);
        }

        if (gameManager.Bees < 500)
        {
            gameManager.Food = gameManager.Food / 2;
        }
    }

    [Button]
    public void TriggerEvent(GameEventSO item) {
        pendingGameEvents.Remove(item);

        if (cataclysmTrigger != null) cataclysmTrigger.Invoke(item);
        gameManager.PauseGame();
        item.ExecuteEvent();
        triggeredGameEvents.Add(item);

         CameraManager.Instance.SetCameraTarget(item.cameraTarget);

        if (Telemetry.Instance.sendTelemetry)
            StartCoroutine(Telemetry.Instance.PostEvent(item));
    }

    public void FeedPopulation()
    {
        int tempFood = Mathf.Clamp(gameManager.Food, -gameManager.Population, gameManager.Population);
        approvalCap = gameManager.Population;
        tempFood -= gameManager.Population;
        //Enough food to feed the population
        if (tempFood >= 0)
        {
            //Player receives money for each fed person
            //gameManager.Money += gameManager.Population - (gameManager.Population - gameManager.Approval);
            gameManager.Food -= gameManager.Population;
            gameManager.Pollution += gameManager.Population;

            if (zeroHungerDayTriggered) approvalCap = gameManager.Population * 2;


            gameManager.Approval += gameManager.Population;
            if (gameManager.Approval > approvalCap) gameManager.Approval = approvalCap;

            fedPopulation?.Invoke();
            if (gameManager.Food > gameManager.Population * 1.5F)
                growthCounter++;

            //populationUpkeep.approval = gameManager.Population;

            zeroHungerDayCounter++;
        }
        //Not enough food to feed the population
        else
        {
            //Player receives money for each fed person
            //gameManager.Money += (tempFood + gameManager.Population - (gameManager.Population - gameManager.Approval));
            gameManager.Food = 0;
            //Unfed population loses approval
            gameManager.Approval += tempFood;
            if (gameManager.Approval > approvalCap) gameManager.Approval = approvalCap;

            gameManager.Pollution += gameManager.Population;
            starvedPopulation?.Invoke();
            growthCounter--;

            zeroHungerDayCounter = 0;
        }


        if (growthCounter > growthThreshold) PopulationIncrease();
        else if (growthCounter < -growthThreshold) PopulationDecrease();

        //Pussied out and gives money per population regardless
        gameManager.Money += gameManager.Population;

    }

    //Updates the values for the upkeep that the events/population requires
    void UpdateUpkeep()
    {
        populationUpkeep.money = gameManager.Population - (gameManager.Population - gameManager.Approval);
        populationUpkeep.food = gameManager.Population;
        populationUpkeep.pollution = gameManager.Population;

    }

    void UniqueEvents()
    {

        ZeroHungerDayEvent();
        CleanAirEvent();
        TrustInGovernmentEvent();
    }

    void ZeroHungerDayEvent()
    {
        if (zeroHungerDayCounter >= zeroHungerDayThreshold && !zeroHungerDayTriggered)
        {
            pendingGameEvents.Add(zeroHungerDayEvent);
            zeroHungerDayTriggered = true;
        }
    }
    void CleanAirEvent()
    {
        if (FoodManager.Instance.income.waste >= 300 && !clearAirTriggered)
        {
            pendingGameEvents.Add(clearAirEvent);
            clearAirTriggered = true;
        }
    }
    void TrustInGovernmentEvent()
    {
        trustInGovernmentCounter++;
        if (trustInGovernmentCounter >= trustInGovernmentThreshold && !trustInGovernmentTriggered)
        {
            pendingGameEvents.Add(trustInGovernmentEvent);
            trustInGovernmentTriggered = true;
            trustVisuals.SetActive(true);
        }
    }



    public void PopulationIncrease()
    {
        growthCounter = 0;
        gameManager.Population = (int)(gameManager.Population * 1.5F);
        AudioManager.Instance.PlaySound(populationGrowthSound);
        populationGrowth?.Invoke();

    }

    public void PopulationDecrease()
    {
        growthCounter = 0;
        gameManager.Population = (int)(gameManager.Population / 1.5F);
        populationShrink?.Invoke();
    }
}
