using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BasketButton : MonoBehaviour
{
    private GoodInfo _good;

    public void Start()
    {
        _good = transform.parent.gameObject.GetComponent<GoodInfo>();
    }

    public void AddClick()
    {
        BasketList.ItemInfo.Add(transform.parent.gameObject.GetComponent<GoodInfo>());
        StartCoroutine(AddRequest());
    }

    public void DeleteClick()
    {
        BasketList.ItemInfo.Remove(transform.parent.gameObject.GetComponent<GoodInfo>());
        StartCoroutine(DeleteRequest());
    }

    private IEnumerator AddRequest()
    {
        WWWForm form = GetForm();
        UnityWebRequest www = UnityWebRequest.Post("http://alexsergol.temp.swtest.ru/db/add_basket.php", form);
        yield return www.SendWebRequest();
    }

    private IEnumerator DeleteRequest()
    {
        var form = GetForm();
        UnityWebRequest www = UnityWebRequest.Post("http://alexsergol.temp.swtest.ru/db/delete_basket.php", form);
        yield return www.SendWebRequest();
        if (!www.isNetworkError)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private WWWForm GetForm()
    {
        var form = new WWWForm();
        form.AddField("GoodId", _good.Id);
        form.AddField("UserId", User.id);
        return form;
    }
}
