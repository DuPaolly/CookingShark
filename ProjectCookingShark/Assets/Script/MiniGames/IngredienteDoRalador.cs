using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteDoRalador : Draggable
{
    // Start is called before the first frame update
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
