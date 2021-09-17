using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Telemetry : MonoBehaviour
{
    public static Telemetry Instance;
    public static string userName;
    public static string institutionName;
    public string urlstring = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfpV516KAQCSc08pYkUMQ8CP7xDp-QhsReSVJEzdPVt2izV4A/formResponse";
    public string heatmapUrl = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSen6o8O6qNBp9K-XITU4K009scwCUhOptLbD51abzRO5OFAKQ/formResponse";
    public System.DateTime counter;
    System.Guid guid;
    public GameObject nameWindow;
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI institutionNameText;
    public bool sendTelemetry;
    public bool sendHeatmap = true;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        if (userName == null)
        {
            nameWindow.SetActive(true);
        }
        if (userName != "")
        {
            userNameText.text = userName;
        }
        if (institutionName != "")
        {
            institutionNameText.text = institutionName;
        }

        guid = System.Guid.NewGuid();
        float total_time = (float)(System.DateTime.Now - counter).TotalSeconds;
        counter = System.DateTime.Now;
        //   StartCoroutine(Post());

    }

    private void OnApplicationQuit()
    {
        //StartCoroutine(Post());
    }

    public void SetName(string s)
    {
        userName = s;
    }
    public void SetInstitution(string s)
    {
        institutionName = s;
    }
    public IEnumerator Post(ActionSO action)
    {
        float total_time = (float)(System.DateTime.Now - counter).TotalSeconds;
        //print(total_time.ToString());
        WWWForm form = new WWWForm();
        if (userName!= null)
            form.AddField("entry.408889876", userName);
        if(institutionName != null)
        form.AddField("entry.1183520150", institutionName);
        form.AddField("entry.92589871", guid.ToString());
        form.AddField("entry.1657388280", action.name.ToString());
        form.AddField("entry.1999334966", FormatTime(Time.time));
        if (AI.Instance.isAIActive)
            form.AddField("entry.1351457628", AI.Instance.behaviour.name.ToString());

        form.AddField("entry.1468488643", TimeManager.Instance.day.ToString());
        form.AddField("entry.1511430617", GameManager.Instance.Food.ToString());
        form.AddField("entry.1466101548", GameManager.Instance.Energy.ToString());
        form.AddField("entry.163088709", GameManager.Instance.Waste.ToString());
        form.AddField("entry.1142277469", GameManager.Instance.Pollution.ToString());
        form.AddField("entry.47387135", GameManager.Instance.Population.ToString());
        form.AddField("entry.229207896", GameManager.Instance.Money.ToString());
        form.AddField("entry.1543673192", GameManager.Instance.Approval.ToString());
        form.AddField("entry.601553515", GameManager.Instance.NaturalCapital.ToString());
        form.AddField("entry.500562723", GameManager.Instance.Bees.ToString());




        byte[] data = form.data;
        counter = System.DateTime.Now;
        UnityWebRequest www = UnityWebRequest.Post(urlstring, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //   Debug.Log("Form upload complete!");
        }
    }

    public IEnumerator HeatMap()
    {
        float total_time = (float)(System.DateTime.Now - counter).TotalSeconds;
        //print(total_time.ToString());
        WWWForm form = new WWWForm();


        if (AI.Instance.isAIActive)
            form.AddField("entry.438076918", AI.Instance.behaviour.name.ToString());

        form.AddField("entry.44896187", TimeManager.Instance.day.ToString());
        form.AddField("entry.861894681", GameManager.Instance.Food.ToString());
        form.AddField("entry.1263841730", GameManager.Instance.Energy.ToString());
        form.AddField("entry.442568512", GameManager.Instance.Waste.ToString());
        form.AddField("entry.1405152660", GameManager.Instance.Population.ToString());
        form.AddField("entry.1076304569", GameManager.Instance.Money.ToString());
        form.AddField("entry.10329600", GameManager.Instance.Pollution.ToString());


        byte[] data = form.data;
        UnityWebRequest www = UnityWebRequest.Post(heatmapUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //   Debug.Log("Form upload complete!");
        }
    }


    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
