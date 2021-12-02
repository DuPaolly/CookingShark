using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuOpcoes;
    [SerializeField] GameObject menuConfimacaoMenu;

    public void FecharMenuPause()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void AbrirMenuOpcoes()
    {
        menuOpcoes.SetActive(true);
    }

    public void IrParaMenuInicial()
    {
        menuConfimacaoMenu.SetActive(true);
    }

    public void SimMenuInicial()
    {
        Application.Quit();
    }

    public void NaoMenuInicial()
    {
        menuConfimacaoMenu.SetActive(false);
    }
}
