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
        SceneManager.LoadScene("MenuInicial");
    }

    public void NaoMenuInicial()
    {
        menuConfimacaoMenu.SetActive(false);
    }
}
