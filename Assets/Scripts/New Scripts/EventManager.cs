using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
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

    public List<GameEventSO> allGameEvents;
    public List<GameEventSO> pendingGameEvents;
    public List<GameEventSO> triggeredGameEvents;
    public Ressources populationUpkeep;
    public EventDescription eventDescription;
    public List<GameEventSO> eventQueue;

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

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckGameEvents()
    {
        eventQueue.Clear();

        foreach (var item in pendingGameEvents)
        {
            if (item.CheckRequirements())
            {
                eventQueue.Add(item);
                eventDescription.gameObject.SetActive(true);
                eventDescription.DisplayEvent(item);


            }
        }

        foreach (var item in eventQueue)
        {


            pendingGameEvents.Remove(item);
            gameManager.PauseGame();
            item.ExecuteEvent();
        }

        //If requirements fullfilled, execute stuff
        if (gameManager.Pollution > gameManager.NaturalCapital)
        {
            gameManager.NaturalCapital--;
        }

        if (gameManager.NaturalCapital < gameManager.Bees)
        {
            gameManager.Bees--;
        }

        if (gameManager.Bees < 500)
        {
            gameManager.Food--;
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
