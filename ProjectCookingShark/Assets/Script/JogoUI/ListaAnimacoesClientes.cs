using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaAnimacoesClientes : MonoBehaviour
{
    [SerializeField] public Animator[] animacoesPossiveis;

    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }
}
