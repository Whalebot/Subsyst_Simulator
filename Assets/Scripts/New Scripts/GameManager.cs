using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool gameStart;
    public static bool paused;
    public bool disableGraphics;
    [TabGroup("Ressources")]
    [InlineProperty] public Ressources ressources;
    [TabGroup("Start Ressources")]
    [InlineProperty] public Ressources startRessources;

    public delegate void GameEvent();
    public GameEvent updateGameState;

    public int Energy
    {
        get
        {
            return ressources.energy;
        }
        set
        {
            ressources.energy = value;
        }
    }
    public int Food
    {
        get
        {
            return ressources.food;
        }
        set
        {
            ressources.food = value;
            if (value <= 0) ressources.food = 0;
        }
    }
    public int Waste
    {
        get
        {
            return ressources.waste;
        }
        set
        {
            ressources.waste = value;
        }
    }
    public int Approval
    {
        get
        {
            return ressources.approval;
        }
        set
        {

            ressources.approval = value;
            if (value > Population) ressources.approval = Population;
        }
    }
    public int Population
    {
        get
        {
            return ressources.population;
        }
        set
        {
            ressources.population = value;
        }
    }
    public int Money
    {
        get
        {
            return ressources.money;
        }
        set
        {
            ressources.money = value;
        }
    }
    public int Pollution
    {
        get
        {
            return ressources.pollution;
        }
        set
        {
            ressources.pollution = value;
        }
    }
    public int Bees
    {
        get
        {
            return ressources.bees;
        }
        set
        {
            ressources.bees = value;
            if (value <= 0) ressources.bees = 0;
        }
    }
    public int NaturalCapital
    {
        get
        {
            return ressources.naturalCapital;
        }
        set
        {
            ressources.naturalCapital = value;
            if (value <= 0) ressources.naturalCapital = 0;
        }
    }


    private void Awake()
    {
        Instance = this;
        gameStart = false;
        paused = true;
        SetStartRessources();
    }

    public void StartGame()
    {
        gameStart = true;
        paused = false;
    }

    public void PauseGame()
    {
        paused = true;
    }

    public void ResumeGame()
    {
        paused = false;
    }

    [Button]
    public void SetStartRessources()
    {
        SetRessources(startRessources);
    }

    public bool CheckMissingRessources(Ressources r)
    {
        //Compare incoming ressources with available ressources and return true/false depending on whether the player has enough ressources.

        FieldInfo[] defInfo1 = ressources.GetType().GetFields();
        FieldInfo[] defInfo2 = r.GetType().GetFields();

        bool foundLackOfRessources = false;

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = ressources;
            object obj2 = r;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);

            if (var1 is int)
            {
                if ((int)var2 <= 0) continue;
                if ((int)var2 < (int)var1) foundLackOfRessources = true;
            }
        }
        return !foundLackOfRessources;
    }

    public bool CheckRessources(Ressources r)
    {
        //Compare incoming ressources with available ressources and return true/false depending on whether the player has enough ressources.

        FieldInfo[] defInfo1 = ressources.GetType().GetFields();
        FieldInfo[] defInfo2 = r.GetType().GetFields();

        bool foundLackOfRessources = false;

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = ressources;
            object obj2 = r;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);

            if (var1 is int)
            {
                if ((int)var2 <= 0) continue;
                if ((int)var2 > (int)var1) foundLackOfRessources = true;
            }
        }
        return !foundLackOfRessources;
    }
    public bool[] FindMissingRessources(Ressources r)
    {
        bool[] canAffordRessource = new bool[10];
        //Compare incoming ressources with available ressources and return true/false depending on whether the player has enough ressources.

        FieldInfo[] defInfo1 = ressources.GetType().GetFields();
        FieldInfo[] defInfo2 = r.GetType().GetFields();

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = ressources;
            object obj2 = r;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);

            if (var1 is int)
            {
                if ((int)var2 > (int)var1) canAffordRessource[i] = true;
            }
        }
        return canAffordRessource;
    }
    public void SubtractRessources(Ressources r)
    {
        FieldInfo[] defInfo1 = ressources.GetType().GetFields();
        FieldInfo[] defInfo2 = r.GetType().GetFields();

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = ressources;
            object obj2 = r;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);


            //ADDING VALUES
            if (var1 is int)
            {
                defInfo1[i].SetValue(obj, (int)var1 - (int)var2);
                if ((int)defInfo1[i].GetValue(obj) < 0) defInfo1[i].SetValue(obj, (int)0);
            }
        }
        r.approval = Mathf.Clamp(r.approval, -Population, Population);
    }
    public void AddRessources(Ressources r)
    {
        FieldInfo[] defInfo1 = ressources.GetType().GetFields();
        FieldInfo[] defInfo2 = r.GetType().GetFields();

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = ressources;
            object obj2 = r;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);


            //ADDING VALUES
            if (var1 is int)
            {

                defInfo1[i].SetValue(obj, (int)var1 + (int)var2);
                if ((int)defInfo1[i].GetValue(obj) < 0) defInfo1[i].SetValue(obj, (int)0);
            }
            else if (var1 is float)
            {
                defInfo1[i].SetValue(obj, (float)var1 + (float)var2);
            }
            else if (var1 is bool)
            {
                //SET VALUES
                if ((bool)var2)
                    defInfo1[i].SetValue(obj, defInfo2[i].GetValue(obj2));
            }
        }
        r.approval = Mathf.Clamp(r.approval, -Population, Population);
    }
    public void AddRessources(Ressources r, Ressources r2)
    {
        FieldInfo[] defInfo1 = r.GetType().GetFields();
        FieldInfo[] defInfo2 = r2.GetType().GetFields();

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = r;
            object obj2 = r2;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);


            //ADDING VALUES
            if (var1 is int)
            {

                defInfo1[i].SetValue(obj, (int)var1 + (int)var2);
            }
            else if (var1 is float)
            {
                defInfo1[i].SetValue(obj, (float)var1 + (float)var2);
            }
        }
    }
    public void SubtractRessources(Ressources r, Ressources r2)
    {
        FieldInfo[] defInfo1 = r.GetType().GetFields();
        FieldInfo[] defInfo2 = r2.GetType().GetFields();

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = r;
            object obj2 = r2;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);


            //ADDING VALUES
            if (var1 is int)
            {
                defInfo1[i].SetValue(obj, (int)var1 - (int)var2);
            }
        }
    }
    public void MultiplyRessources(Ressources r, float f)
    {

        FieldInfo[] defInfo = r.GetType().GetFields();

        for (int i = 0; i < defInfo.Length; i++)
        {
            object obj = r;

            object var1 = defInfo[i].GetValue(obj);
            //ADDING VALUES
            if (var1 is int)
            {

                defInfo[i].SetValue(obj, (int)((int)var1 * f));
            }
            else if (var1 is float)
            {
                defInfo[i].SetValue(obj, (float)var1 * f);
            }
        }
    }
    public void SetRessources(Ressources r)
    {
        FieldInfo[] defInfo1 = ressources.GetType().GetFields();
        FieldInfo[] defInfo2 = r.GetType().GetFields();

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = ressources;
            object obj2 = r;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);


            //ADDING VALUES
            if (var1 is int)
            {

                defInfo1[i].SetValue(obj, (int)var2);
            }
            else if (var1 is float)
            {
                defInfo1[i].SetValue(obj, (float)var2);
            }
            else if (var1 is bool)
            {
                //SET VALUES
                if ((bool)var2)
                    defInfo1[i].SetValue(obj, defInfo2[i].GetValue(obj2));
            }
        }
        r.approval = Mathf.Clamp(r.approval, -Population, Population);
    }
    public void SetRessources(Ressources r, Ressources r2)
    {
        FieldInfo[] defInfo1 = r2.GetType().GetFields();
        FieldInfo[] defInfo2 = r.GetType().GetFields();

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = r2;
            object obj2 = r;

            object var1 = defInfo1[i].GetValue(obj);
            object var2 = defInfo2[i].GetValue(obj2);


            //ADDING VALUES
            if (var1 is int)
            {

                defInfo1[i].SetValue(obj, (int)var2);
            }
            else if (var1 is float)
            {
                defInfo1[i].SetValue(obj, (float)var2);
            }
            else if (var1 is bool)
            {
                //SET VALUES
                if ((bool)var2)
                    defInfo1[i].SetValue(obj, defInfo2[i].GetValue(obj2));
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

[System.Serializable]
public class Ressources
{
    public int energy;
    public int food;
    public int waste;
    public int approval;
    public int population;
    public int money;
    public int pollution;
    public int bees;
    public int naturalCapital;
}