using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaAnimacoesClientes : MonoBehaviour
{
    [SerializeField] public RuntimeAnimatorController[] animacoesPossiveis;

    public enum Animacoes
    {
        CavaloMarinho
    }

    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    public void TocarSom(Animacoes somParaTocar)
    {
        //Animacoes.CavaloMarinho;
    }
}
