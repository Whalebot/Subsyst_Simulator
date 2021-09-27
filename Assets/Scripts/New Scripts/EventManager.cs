using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<GameEventSO> allGameEvents;
    public List<GameEventSO> pendingGameEvents;
    public List<GameEventSO> triggeredGameEvents;
    public Ressources populationUpkeep;
    public EventDescription eventDescription;
    public List<GameEventSO> eventQueue;

    public GameObject paper;

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

        timeManager.advanceGameEvent += FeedPopulation;
        timeManager.advanceGameEvent += CheckGameEvents;
        timeManager.advanceTimeEvent += UpdateUpkeep;
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
                }
                else if(item.type != GameEventSO.EventType.DoNotShow)
                {
                    eventDescription.gameObject.SetActive(true);
                    eventDescription.DisplayEvent(item);
                    gameManager.PauseGame();
                }
                eventQueue.Add(item);
            }
        }

        foreach (var item in eventQueue)
        {
            pendingGameEvents.Remove(item);
            //gameManager.PauseGame();
            if (cataclysmTrigger != null) cataclysmTrigger.Invoke(item);
            item.ExecuteEvent();
            if (Telemetry.Instance.sendTelemetry)
                StartCoroutine(Telemetry.Instance.PostEvent(item));
        }

        //If requirements fullfilled, execute stuff
        if (gameManager.Pollution > 5000)
        {
            gameManager.NaturalCapital -= gameManager.Pollution/5000;
        }

        if (gameManager.NaturalCapital < 1000)
        {
            gameManager.Bees -= 1500 / Mathf.Clamp(gameManager.NaturalCapital, 15, 1500);
        }

        if (gameManager.Bees < 500)
        {
            gameManager.Food = gameManager.Food/2;
        }
    }

    public void FeedPopulation()
    {
        int tempFood = Mathf.Clamp(gameManager.Food, -gameManager.Population, gameManager.Population);

        tempFood -= gameManager.Population;
        //Enough food to feed the population
        if (tempFood >= 0)
        {
            //Player receives money for each fed person
            gameManager.Money += gameManager.Population - (gameManager.Population - gameManager.Approval);
            gameManager.Food -= gameManager.Population;
            gameManager.Pollution += gameManager.Population;
            gameManager.Approval = gameManager.Population;
            fedPopulation?.Invoke();
            if (gameManager.Food > gameManager.Population * 1.5F)
                growthCounter++;

            //populationUpkeep.approval = gameManager.Population;
        }
        //Not enough food to feed the population
        else
        {
            //Player receives money for each fed person
            gameManager.Money += (tempFood + gameManager.Population - (gameManager.Population - gameManager.Approval));
            gameManager.Food = 0;
            //Unfed population loses approval
            gameManager.Approval -= tempFood;

            gameManager.Pollution += gameManager.Population;
            starvedPopulation?.Invoke();
            growthCounter--;
        }


        if (growthCounter > growthThreshold) PopulationIncrease();
        else if (growthCounter < -growthThreshold) PopulationDecrease();
    }

    //Updates the values for the upkeep that the events/population requires
    void UpdateUpkeep()
    {
        populationUpkeep.money = gameManager.Population - (gameManager.Population - gameManager.Approval);
        populationUpkeep.food = gameManager.Population;
        populationUpkeep.pollution = gameManager.Population;

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
