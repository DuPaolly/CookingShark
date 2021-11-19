using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positions : MonoBehaviour
{
    // Start is called before the first frame update

    public Cliente cliente;

    public bool statusDaMesa;

    public bool mesa;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        statusDaMesa = StatusDaMesa();
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
        if (areaEmQueSaiu.CompareTag("ClienteTag"))
        {
            Cliente clienteAchado = areaEmQueSaiu.GetComponent<Cliente>();

            if (clienteAchado != null)
            {
                cliente = null;
            }
        }
    }

    bool StatusDaMesa()
    {
        if(cliente == null)
        {
            return false;
        }else
        {
            return true;
        }
    }

}
