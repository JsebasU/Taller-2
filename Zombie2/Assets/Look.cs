using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    /// <summary>
    /// se toma el tranform del objeto que tiene el script
    /// </summary>
    Transform RotaTransform;

    private void Awake()
    {
        RotaTransform = transform;
    }
   
    /// <summary>
    ///  la accion de rotar la mira del personaje 
    /// </summary>
  
    public void Arround()
    {


        if (Input.GetKey(KeyCode.A))
        {
            RotaTransform.Rotate(0, -1, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            RotaTransform.Rotate(0, 1, 0);
        }

    }
}
