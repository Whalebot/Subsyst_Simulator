using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Telemetry : MonoBehaviour
{
    //    // Start is called before the first frame update
    //    Amount[] amountItems;
    public string urlstring = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfpV516KAQCSc08pYkUMQ8CP7xDp-QhsReSVJEzdPVt2izV4A/formResponse";
    public System.DateTime counter;
    System.Guid guid;
    //    private string userName = "";
    //    private string insittutionName = "";
    void Start()
    {
        guid = System.Guid.NewGuid();
        float total_time = (float)(System.DateTime.Now - counter).TotalSeconds;
        counter = System.DateTime.Now;
     //   StartCoroutine(Post());

    }

    private void OnApplicationQuit()
    {
        StartCoroutine(Post());
    }

    public IEnumerator Post()
    {
        float total_time = (float)(System.DateTime.Now - counter).TotalSeconds;
        //print(total_time.ToString());
        WWWForm form = new WWWForm();
        form.AddField("entry.408889876", "Bob");


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
}
