using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    public GameObject ItemPrefab;
    private bool loaded = false;

    public void ShowItems()
    {
        foreach (var good in BasketList.ItemInfo)
        {
            GameObject item = Instantiate(ItemPrefab);
            item.transform.GetChild(0).GetComponent<Text>().text = good.Name;
            item.transform.SetParent(transform, false);
            item.GetComponent<GoodInfo>().Id = good.Id;
            item.GetComponent<GoodInfo>().Name = good.Name;
        }
        BasketList.ItemInfo.Clear();
    }

    public void Start()
    {
        StartCoroutine(GetBasket());
    }

    private IEnumerator GetBasket()
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", User.id);
        UnityWebRequest request = UnityWebRequest.Post("http://alexsergol.temp.swtest.ru/db/get_basket.php", form);
        yield return request.SendWebRequest();
        if (!request.isNetworkError)
        {
            var response = JsonUtility.FromJson<ResponseGoods>(request.downloadHandler.text);
            foreach (var good in response.Goods)
            {
                var item = Instantiate(ItemPrefab) as GameObject;
                item.GetComponent<GoodInfo>().Id = good.id;
                item.transform.SetParent(transform, false);
                item.transform.GetChild(0).GetComponent<Text>().text = good.name;
            }
            loaded = true;
        }
    }

    public void OnEnable()
    {
        if (loaded)
            ShowItems();
        HideAndShowItems(true);
    }

    public void OnDisable()
    {
        HideAndShowItems(false);
    }

    private void HideAndShowItems(bool active)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(active);
    }
}
