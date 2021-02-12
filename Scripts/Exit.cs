using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public void ExitFromAccount()
    {
        SceneManager.LoadScene(0);
    }
}
