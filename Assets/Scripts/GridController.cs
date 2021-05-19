using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridController
{
   
   private static int height = 20;
   private static int width = 10;
   private static GameObject[,]  blocks = new GameObject[22, 10];

    /// <summary>
    /// Redondea un vector2
    /// </summary>
    /// <param name="vector">Vector a redondear</param>
    /// <returns>Vector redondeado</returns>
    public static Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    /// <summary>
    /// Comprueba si un vector está dentro del espacio de juego
    /// </summary>
    /// <param name="position">Vector2 a comprobar</param>
    /// <returns>True si el vector está dentro de los límites, false si no lo está</returns>
   public static bool IsInPlayfield(Vector2 position)
   {   
       if(position.x >= width || position.x < 0 || position.y < 0)
       {
           return false;
       }
       return true;
   }
   

}
