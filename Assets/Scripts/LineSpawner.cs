using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using PathCreation;
public class LineSpawner : MonoBehaviour
{
    public bool cost;
    public RessourceType type;
    public bool population;
    public bool reverse;

    float maxSize = 20;
    float minSize = 1;
    public GameObject prefab;
    public List<ProductionSO> affectedProductions;
    // Start is called before the first frame update
    void Start()
    {
        if (!population)
            FoodManager.Instance.productionEvent += SpawnObject;
        else
        {
            EventManager.Instance.fedPopulation += SpawnObject;
            EventManager.Instance.starvedPopulation += SpawnObject;
        }


    }

    public void SpawnObject()
    {


        GameObject GO = Instantiate(prefab, transform);
        GO.GetComponent<FollowPath>().pathCreator = GetComponent<PathCreator>();
        GO.GetComponent<FollowPath>().reversePath = reverse;
        float size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(GameManager.Instance.Population) * 2 + minSize), minSize, maxSize);

        GO.transform.localScale = Vector3.one * size;
    }

    public void SpawnObject(ProductionSO so)
    {
        if (affectedProductions.Contains(so))
        {


            GameObject GO = Instantiate(prefab, transform);
            GO.GetComponent<FollowPath>().pathCreator = GetComponent<PathCreator>();
            GO.GetComponent<FollowPath>().reversePath = reverse;

            Ressources val;

            float size = 10;
            if (cost)
            {
                val = UpgradeManager.Instance.CheckCost(so);

                switch (type)
                {
                    case RessourceType.Food:

                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.food) + minSize), minSize, maxSize);
                        break;
                    case RessourceType.Energy:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.energy) + minSize), minSize, maxSize);
                        break;
                    case RessourceType.Waste:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.waste) + minSize), minSize, maxSize);
                        break;
                    case RessourceType.Money:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.money) + minSize), minSize, maxSize);
                        break;
                    case RessourceType.Pollution:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.pollution) + minSize), minSize, maxSize);
                        break;
                    default:
                        break;
                }
            }

            else
            {
                val = UpgradeManager.Instance.CheckResult(so);

                switch (type)
                {
                    case RessourceType.Food:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.food) + minSize), minSize, maxSize);
                        break;
                    case RessourceType.Energy:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.energy) + minSize), minSize, maxSize);
                        break;
                    case RessourceType.Waste:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.waste) + minSize), minSize, maxSize);
                        break;
                    case RessourceType.Money:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.money) + minSize), minSize, maxSize);
                        break;
                    case RessourceType.Pollution:
                        size = Mathf.Clamp(Mathf.Floor(Mathf.Log10(val.pollution) + minSize), minSize, maxSize);
                        break;
                    default:
                        break;
                }
            }

            GO.transform.localScale = Vector3.one * size;
        }
    }

}

public enum RessourceType
{
    Food, Energy, Waste, Money, Pollution
}