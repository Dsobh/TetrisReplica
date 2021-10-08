﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridController
{
   private const int MAX_HEIGHT = 20; //Máximo número de filas posibles
   private static int height = 24;
   private static int width = 10;
   public static Transform[,]  blocks = new Transform[width, height];

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
       }else
       {
           return true;
       }
   }
   
   /// <summary>
   /// Método get del atributo Height
   /// </summary>
   /// <returns>Devuelve la altura del grid</returns>
   public static int getHeight()
   {
       return height;
   }

    /// <summary>
    /// Método get del atributo Width
    /// </summary>
    /// <returns>Devuelve la anchura del grid</returns>
   public static int getWidth()
   {
       return width;
   }

    
    /// <summary>
    /// Método get de la constante MAX_HEIGHT
    /// </summary>
    /// <returns>El número máximo de filas</returns>
   public static int getMaxHeight()
   {
       return MAX_HEIGHT;
   }


}
