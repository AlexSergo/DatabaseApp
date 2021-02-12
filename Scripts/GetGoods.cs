using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetGoods : MonoBehaviour
{
    public GameObject GoodPref;
    private Text _goodText;
    private ResponseGoods _response;

    public void Start()
    {
        _goodText = GoodPref.transform.GetChild(0).GetComponent<Text>();
    }

    public void ShowGoods()
    {
        if (transform.childCount == 0)
          StartCoroutine(GetResponse());
    }

    private IEnumerator GetResponse()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://alexsergol.temp.swtest.ru/db/");
        yield return www.SendWebRequest();
        if (!www.isNetworkError)
        {
            _response = JsonUtility.FromJson<ResponseGoods>(www.downloadHandler.text);

            for (int i = 0; i < _response.Goods.Length; i++)
            {
                _goodText.text = _response.Goods[i].name;
                GameObject go = Instantiate(GoodPref) as GameObject;
                go.transform.SetParent(transform, false);
                go.GetComponent<GoodInfo>().Id = _response.Goods[i].id;
                go.GetComponent<GoodInfo>().Name = _goodText.text;
            }
        }
    }
}