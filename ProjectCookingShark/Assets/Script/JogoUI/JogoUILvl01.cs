using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JogoUILvl01 : MonoBehaviour
{
    [SerializeField] GameObject MenuDeIngredientes;
    [SerializeField] GameObject PopUpPedido;
    [SerializeField] GameObject popUpAnemona;
    [SerializeField] GameObject popUpCoral;
    [SerializeField] GameObject popUpAreia;
    [SerializeField] GameObject popUpPimenta;
    [SerializeField] GameObject popUpConchas;
    [SerializeField] GameObject popUpSal;
    [SerializeField] GameObject menuDePause;

    private void Start()
    {
        MenuDeIngredientes.SetActive(false);
        //PopUpPedido.SetActive(false);
    }

    public void BotaoIngredientes()
    {
        MenuDeIngredientes.SetActive(true);
    }

    public void FecharMenuIngrediente()
    {
        MenuDeIngredientes.SetActive(false);
    }

    public void BotaoParaVisualizarOPedido()
    {
        PopUpPedido.SetActive(true);
    }

    public void FecharOVisualizadorDePedidos()
    {
        PopUpPedido.SetActive(false);
    }

    public void AbrirAnemona()
    {
        popUpAnemona.SetActive(true);
    }

    public void FecharAnemona()
    {
        popUpAnemona.SetActive(false);
    }

    public void AbrirCoral()
    {
        popUpCoral.SetActive(true);
    }

    public void FecharCoral()
    {
        popUpCoral.SetActive(false);
    }
    public void AbrirAreia()
    {
        popUpAreia.SetActive(true);
    }

    public void FecharAreia()
    {
        popUpAreia.SetActive(false);
    }
    public void AbrirPimenta()
    {
        popUpPimenta.SetActive(true);
    }

    public void FecharPimenta()
    {
        popUpPimenta.SetActive(false);
    }
    public void AbrirConcha()
    {
        popUpConchas.SetActive(true);
    }

    public void FecharConcha()
    {
        popUpConchas.SetActive(false);
    }
    public void AbrirSal()
    {
        popUpSal.SetActive(true);
    }

    public void FecharSal()
    {
        popUpSal.SetActive(false);
    }

    public void JogarDeNovo()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void FecharJogoTelaFinal()
    {
        Application.Quit();
    }

    public void AbrirMenuDePause()
    {
        menuDePause.SetActive(true);
    }
}
