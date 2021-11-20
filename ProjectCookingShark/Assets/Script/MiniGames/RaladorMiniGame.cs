using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaladorMiniGame : MiniGameBase
{
    
    
    float quantidadeRalada;
    float quantidadeParaRalar = 100;
    float deltaDaRalacao;
    [SerializeField] float profundidadeDoMouse = 10;
    [SerializeField] Collider2D colisorIngrediente;
    [SerializeField] Collider2D colisorRalador;
    Transform trIngrediente;
    [SerializeField] LayerMask layerRalador;
    ContactFilter2D filter2D;
    public Collider2D[] colisoresEncontrados = new Collider2D [1];
    Vector3 ultimaPosicao;
    Vector3 posicaoDoMouse;
    [SerializeField] Slider sliderDoTempoDeRalacao;

    bool IngredienteEstaNoRalador()
    {
        colisoresEncontrados[0] = null;

        Physics2D.OverlapCollider(colisorIngrediente, filter2D, colisoresEncontrados);
        return colisoresEncontrados[0] != null;
        //Implicitamente retorna true ou false
    }

    private void FixedUpdate()
    {

        if (IngredienteEstaNoRalador())
        {
            //Debug.Log(colisoresEncontrados[0].name);

            sliderDoTempoDeRalacao.value = quantidadeRalada;
            Debug.Log($"delta: {trIngrediente.position - ultimaPosicao}");
            deltaDaRalacao = trIngrediente.position.y - ultimaPosicao.y;
            if (deltaDaRalacao < 0)
            {
                quantidadeRalada += -deltaDaRalacao;
                if (quantidadeRalada >= quantidadeParaRalar)
                {
                    quantidadeRalada = 0;
                    EncerraMiniGame();

                }
            }
            
        }

        ultimaPosicao = trIngrediente.position;
    }

    private void Start()
    {
        filter2D.useLayerMask = true;
        filter2D.layerMask = layerRalador;

        trIngrediente = colisorIngrediente.transform;
        ultimaPosicao = trIngrediente.position;
    }

    private void LateUpdate()
    {
        //posicaoDoMouse = Input.mousePosition;
        //posicaoDoMouse.z = profundidadeDoMouse;
        //if(Camera.current != null)
        //{
        //    trIngrediente.position = Camera.current.ScreenToWorldPoint(posicaoDoMouse);
        //}
        //Grude no mouse
    }
}
