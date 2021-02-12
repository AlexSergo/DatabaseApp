using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangeDb : MonoBehaviour
{
    public Text NameOfGood;
    public Dropdown Dropdown;
    public Text Message;

    public void ChangeGood()
    {
        StartCoroutine(SendRequire());
    }

    private IEnumerator SendRequire()
    {
        WWWForm form = new WWWForm();
        form.AddField("Good", NameOfGood.text);
        
        if (Dropdown.value == 0)
            form.AddField("Change", "Add");
        else if (Dropdown.value == 1)
            form.AddField("Change", "Delete");

        UnityWebRequest www = UnityWebRequest.Post("http://alexsergol.temp.swtest.ru/db/change.php", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
            Debug.Log(www.error);
        Debug.Log(www.downloadHandler.text);
        Message.text = www.downloadHandler.text;
    }
}
