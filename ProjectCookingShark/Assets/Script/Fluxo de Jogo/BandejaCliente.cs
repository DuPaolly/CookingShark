using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandejaCliente : Draggable
{
    public Receita pratoServido;

    private Cliente cliente;

    public Frigideira.IngredientePremium premiumIngrediente;

    [SerializeField] public SpriteRenderer spriteDaReceitaFinal;

    void FixedUpdate()
    {
        VolteParaPosicao();
        MudarSpriteDaReceitaNaBandeja();
    }

    private void OnTriggerEnter2D(Collider2D areaEmQueEncostou)
    {
        if (areaEmQueEncostou.CompareTag("ClienteTag"))
        {
            Cliente clienteAchado = areaEmQueEncostou.GetComponent<Cliente>();

            if (clienteAchado != null)
            {
                cliente = clienteAchado;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D areaEmQueSaiu)
    {
        if(areaEmQueSaiu.CompareTag("ClienteTag"))
        {
            Cliente clienteAchado = areaEmQueSaiu.GetComponent<Cliente>();

            if (clienteAchado != null)
            {
                cliente = null;
            }
        }
    }


    private void OnMouseDrag()
    {
        MouseDragUpdate();
    }
    private void OnMouseUp()
    {
        VolteParaPosicao();
        MouseDropObject();
    }

    private void MouseDropObject()
    {
        if (cliente != null)
        {
            cliente.ingredientePremium = premiumIngrediente;
            cliente.pratoRecebido = pratoServido;
            DescartaPrato();
        }
        podeVoltar = PodeVoltarAPosiçãoInicial();
    }

    public void DescartaPrato()
    {
        premiumIngrediente = Frigideira.IngredientePremium.SemIngredientePremium;
        pratoServido = null;
        //desligar o sprite
    }

    void MudarSpriteDaReceitaNaBandeja()
    {
        if (pratoServido != null)
        {
            AtualizaSpriteDoPratoNaBandeja(pratoServido.spriteDaReceitaNaBandeja);
            //mudar o sprite
        }
        else
        {
            spriteDaReceitaFinal.gameObject.SetActive(false);
            //desativar o sprite
        }
    }
    public void AtualizaSpriteDoPratoNaBandeja(Sprite spriteFinal)
    {
        spriteDaReceitaFinal.gameObject.SetActive(true);
        spriteDaReceitaFinal.sprite = spriteFinal;
    }
}
