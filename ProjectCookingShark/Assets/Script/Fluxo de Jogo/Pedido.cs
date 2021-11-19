using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedido : Sabores
{

    [SerializeField] SaboresExistentes saborPedido01;
    public SaboresExistentes SaborPedido01 => saborPedido01;

    [SerializeField] SaboresExistentes saborPedido02;
    public SaboresExistentes SaborPedido02 => saborPedido02;

    [SerializeField] Ingrediente ingredienteProibidoPedido;
    public Ingrediente IngredienteProibidoPedido => ingredienteProibidoPedido;

    [SerializeField] IngredientePremiumDoClente premiumIngredientesDoCliente;
    public IngredientePremiumDoClente PremiumIngredienteDoCliente => premiumIngredientesDoCliente;

    public SaboresExistentes saborPedido03 = Sabores.SaboresExistentes.nenhum;

    private void Update()
    {
    }

    public enum IngredientePremiumDoClente
    {
        SemIngredientePremium,
        PrimeiroIngredientePremium,
        SegundoIngredientePremium
    }
}