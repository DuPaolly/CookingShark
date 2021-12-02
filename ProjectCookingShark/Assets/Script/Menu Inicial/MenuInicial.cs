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
    [SerializeField] GameObject pag1Tutorial;
    [SerializeField] GameObject pag2Tutorial;
    [SerializeField] GameObject pag3Tutorial;
    [SerializeField] GameObject pag4Tutorial;
    private void Start()
    {
        MenuOptions.SetActive(false);
        MenuPerguntaSair.SetActive(false);
        menuInicial.SetActive(false);
        menuCreditos.SetActive(false);
        pag1Tutorial.SetActive(false);
        pag2Tutorial.SetActive(false);
        pag3Tutorial.SetActive(false);
        pag4Tutorial.SetActive(false);
    }

    private void Update()
    {
        PressSpace();
    }
    public void BotaoJogar()
    {
        pag1Tutorial.SetActive(true);
        pag2Tutorial.SetActive(false);
        pag3Tutorial.SetActive(false);
        pag4Tutorial.SetActive(false);
        //spritePrato.sprite = SpriteReceitas[1];
    }

    public void BotaoContinuarPag1()
    {
        pag1Tutorial.SetActive(false);
        pag2Tutorial.SetActive(true);
        pag3Tutorial.SetActive(false);
        pag4Tutorial.SetActive(false);
    }

    public void BotaoContinuarPag2()
    {
        pag1Tutorial.SetActive(false);
        pag2Tutorial.SetActive(false);
        pag3Tutorial.SetActive(true);
        pag4Tutorial.SetActive(false);
    }

    public void BotaoContinuarPag3()
    {
        pag1Tutorial.SetActive(false);
        pag2Tutorial.SetActive(false);
        pag3Tutorial.SetActive(false);
        pag4Tutorial.SetActive(true);
    }
    public void BotaoContinuarPag4()
    {
        SceneManager.LoadScene("Jogo");
        pag1Tutorial.SetActive(false);
        pag2Tutorial.SetActive(false);
        pag3Tutorial.SetActive(false);
        pag4Tutorial.SetActive(false);
    }

    public void BotaoOpcoes()
    {
        Debug.Log("Abre o menu de opções");
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
