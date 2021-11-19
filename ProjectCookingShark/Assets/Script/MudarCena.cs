using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudarCena : MonoBehaviour
{
    public void ChamarCenaCortarObjeto()
    {
        //faca.enabled = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("DudaTestes");
    }

    public void ChamarCenaInicial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TesteButton");
    }
}
