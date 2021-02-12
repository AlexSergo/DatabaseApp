using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Authorize : MonoBehaviour
{
    public Text Login;
    public Text Password;
    private ResponseUser _response;

    public void RegisterOrLogin()
    {
        StartCoroutine(SendRequest());
    }

    private IEnumerator SendRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("Login", Login.text);
        form.AddField("Password", Password.text);
        UnityWebRequest www = UnityWebRequest.Post("http://alexsergol.temp.swtest.ru/db/login.php", form);
        yield return www.SendWebRequest();
        if (!www.isNetworkError)
        {
            _response = JsonUtility.FromJson<ResponseUser>(www.downloadHandler.text);

            User.id = _response.id;
            User.Login = _response.login;
            User.Password = _response.password;
            User.IsAdmin = _response.admin;
            if (User.IsAdmin)
                SceneManager.LoadScene("admin");
            transform.gameObject.SetActive(false);
            Debug.Log(transform.gameObject.activeSelf);
        }
    }
}
