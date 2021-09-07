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

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        timeManager = TimeManager.Instance;
        timeManager.advanceGameEvent += FeedPopulation;
        timeManager.advanceGameEvent += CheckGameEvents;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckGameEvents()
    {
        foreach (var item in allGameEvents)
        {
            if (item.CheckRequirements())
            {
                item.ExecuteEvent();
            }
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
        int tempFood = gameManager.Food;

        tempFood -= gameManager.Population;
        //Enough food to feed the population
        if (tempFood >= 0)
        {
            //Player receives money for each fed person
            gameManager.Money += gameManager.Population;
            gameManager.Food -= gameManager.Population;
            gameManager.Pollution += gameManager.Population;
            fedPopulation?.Invoke();
            growthCounter++;
        }
        //Not enough food to feed the population
        else
        {
            //Player receives money for each fed person
            gameManager.Money += (tempFood + gameManager.Population);
            gameManager.Food = 0;
            //Unfed population loses approval
            gameManager.Approval += tempFood;

            gameManager.Pollution += gameManager.Population;
            starvedPopulation?.Invoke();
            growthCounter--;
        }

        if (growthCounter > growthThreshold) PopulationIncrease();
        else if (growthCounter < -growthThreshold) PopulationDecrease();
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
