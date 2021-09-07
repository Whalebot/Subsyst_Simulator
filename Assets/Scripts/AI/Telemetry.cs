using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Telemetry : MonoBehaviour
{
    public static Telemetry Instance;
    //    // Start is called before the first frame update
    //    Amount[] amountItems;
    public string urlstring = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfpV516KAQCSc08pYkUMQ8CP7xDp-QhsReSVJEzdPVt2izV4A/formResponse";
    public System.DateTime counter;
    System.Guid guid;
    //    private string userName = "";
    //    private string insittutionName = "";

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        guid = System.Guid.NewGuid();
        float total_time = (float)(System.DateTime.Now - counter).TotalSeconds;
        counter = System.DateTime.Now;
        //   StartCoroutine(Post());

    }

    private void OnApplicationQuit()
    {
        //StartCoroutine(Post());
    }

    public IEnumerator Post(ActionSO action)
    {
        float total_time = (float)(System.DateTime.Now - counter).TotalSeconds;
        //print(total_time.ToString());
        WWWForm form = new WWWForm();
        form.AddField("entry.408889876", "Bob");
        form.AddField("entry.92589871", guid.ToString());
        form.AddField("entry.1657388280", action.name.ToString());
        print(FormatTime(Time.time));
        form.AddField("entry.1999334966", FormatTime(Time.time));
        if (AI.Instance.isAIActive)
            form.AddField("entry.1351457628", AI.Instance.behaviour.name.ToString());

        form.AddField("entry.1468488643", TimeManager.Instance.day.ToString());
        form.AddField("entry.1511430617", GameManager.Instance.Food.ToString());
        form.AddField("entry.1466101548", GameManager.Instance.Energy.ToString());
        form.AddField("entry.163088709", GameManager.Instance.Waste.ToString());
        form.AddField("entry.1142277469", GameManager.Instance.Pollution.ToString());
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
            Debug.Log("Form upload complete!");
        }
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
