using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteDoRalador : Draggable
{
    private void FixedUpdate()
    {
        VolteParaPosicao();
    }

    private void OnMouseDrag()
    {
        MouseDragUpdate();
    }

    private void OnMouseUp()
    {
        VolteParaPosicao();
    }
}
