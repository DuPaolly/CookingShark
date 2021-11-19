using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogoUILvl01 : MonoBehaviour
{
    [SerializeField] GameObject MenuDeIngredientes;
    [SerializeField] GameObject PopUpPedido;

    private void Start()
    {
        MenuDeIngredientes.SetActive(false);
        PopUpPedido.SetActive(false);
    }

    public void BotaoIngredientes()
    {
        MenuDeIngredientes.SetActive(true);
    }

    public void BotaoParaVisualizarOPedido()
    {
        PopUpPedido.SetActive(true);
    }

    public void FecharOVisualizadorDePedidos()
    {
        PopUpPedido.SetActive(false);
    }
}
