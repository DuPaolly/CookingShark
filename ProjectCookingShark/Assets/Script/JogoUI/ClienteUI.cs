using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClienteUI : MonoBehaviour
{
    [SerializeField] Image[] sabor01;
    [SerializeField] Image[] sabor02;
    [SerializeField] Image[] Ingredientes;

    public enum NomesSabor01
    {
        Apimentado,
        Azedo,
        Salgado
    }

    public enum NomesSabor02
    {
        Apimentado,
        Azedo,
        Salgado
    }

    public enum NomesIngredientes
    {
        Anemona,
        Areia,
        Conchas,
        Coral,
        Pimenta,
        Sal
    }
}
