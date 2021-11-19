using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaDePedidos : MonoBehaviour
{

    [SerializeField] public Pedido[] pedidosPossiveis;

    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
   
}
