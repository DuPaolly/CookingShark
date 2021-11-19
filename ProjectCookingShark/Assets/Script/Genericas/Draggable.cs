using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{


    private Vector3 _originalPosition;

    public bool podeVoltar;

    private Vector2 offset, _posicaoAtual;

    int smoothVelocidade = 20;
    // Start is called before the first frame update
    private void Awake()
    {
        ObjectStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectStart()
    {
        _originalPosition = transform.position;
    }

    public void MouseDragUpdate()
    {
        transform.position = GetMousePos();
    }

    public Vector3 GetMousePos()
    {
        //podeVoltar = JaChegouNoDestino();
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    public void VolteParaPosicao()
    {
        if (podeVoltar)
        {
            _posicaoAtual = transform.position;

            _posicaoAtual = Vector3.Lerp(
                transform.position,
                _originalPosition,
                smoothVelocidade * Time.deltaTime);

            transform.position = _posicaoAtual;

        }
        //else if (transform.position == _originalPosition)
        //{
        //    podeVoltar = JaChegouNoDestino();
        //}
    }

    public bool PodeVoltarAPosiçãoInicial()
    {
        return true;
    }

    public bool JaChegouNoDestino()
    {
        return false;
    }
}
