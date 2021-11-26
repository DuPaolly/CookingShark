using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] GameObject MenuOptions;
    [SerializeField] GameObject MenuPerguntaSair;
    [SerializeField] GameObject menuStart;
    [SerializeField] GameObject menuInicial;
    [SerializeField] GameObject menuCreditos;
    private void Start()
    {
        MenuOptions.SetActive(false);
        MenuPerguntaSair.SetActive(false);
        menuInicial.SetActive(false);
        menuCreditos.SetActive(false);
    }

    private void Update()
    {
        PressSpace();
    }
    public void BotaoJogar()
    {
        Debug.Log("Iniciar o Jogo");
        SceneManager.LoadScene("Jogo");
        //spritePrato.sprite = SpriteReceitas[1];
    }

    //public void BotaoHistoria()
    //{
    //    Debug.Log("Iniciar a hist�ria do Jogo");
    //    ///spritePrato.sprite = SpriteReceitas[0];

    //}
    public void BotaoOpcoes()
    {
        Debug.Log("Abre o menu de op��es");
        MenuOptions.SetActive(true);
    }
    public void BotaoSair()
    {
        MenuPerguntaSair.SetActive(true);
        Debug.Log("Fechar o jogo");
    }

    public void SairSim()
    {
        Application.Quit();
    }

    public void SairNao()
    {
        MenuPerguntaSair.SetActive(false);
    }

    void PressSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            menuStart.SetActive(false);
            menuInicial.SetActive(true);
        }
    }

    public void AbrirCreditos()
    {
        menuCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        menuCreditos.SetActive(false);
    }
}
