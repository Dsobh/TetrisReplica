using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallKickData
{
    private static Vector2[,] wallKickDataRest = new Vector2[8, 5];
    private static Vector2[,] wallKickDataI = new Vector2[8, 5];

    public static Vector2[,] returnRestData()
    {
        wallKickDataRest[0, 0] = new Vector2(0, 0);
        wallKickDataRest[0, 1] = new Vector2(-1, 0);
        wallKickDataRest[0, 2] = new Vector2(-1, 1);
        wallKickDataRest[0, 3] = new Vector2(0, -2);
        wallKickDataRest[0, 4] = new Vector2(-1, -2);

        wallKickDataRest[1, 0] = new Vector2(0, 0);
        wallKickDataRest[1, 1] = new Vector2(1, 0);
        wallKickDataRest[1, 2] = new Vector2(1, -1);
        wallKickDataRest[1, 3] = new Vector2(0, 2);
        wallKickDataRest[1, 4] = new Vector2(1, 2);

        wallKickDataRest[2, 0] = new Vector2(0, 0);
        wallKickDataRest[2, 1] = new Vector2(1, 0);
        wallKickDataRest[2, 2] = new Vector2(1, -1);
        wallKickDataRest[2, 3] = new Vector2(0, 2);
        wallKickDataRest[2, 4] = new Vector2(1, 2);

        wallKickDataRest[3, 0] = new Vector2(0, 0);
        wallKickDataRest[3, 1] = new Vector2(-1, 0);
        wallKickDataRest[3, 2] = new Vector2(-1, 1);
        wallKickDataRest[3, 3] = new Vector2(0, -2);
        wallKickDataRest[3, 4] = new Vector2(-1, -2);

        wallKickDataRest[4, 0] = new Vector2(0, 0);
        wallKickDataRest[4, 1] = new Vector2(1, 0);
        wallKickDataRest[4, 2] = new Vector2(1, 1);
        wallKickDataRest[4, 3] = new Vector2(0, -2);
        wallKickDataRest[4, 4] = new Vector2(1, -2);

        wallKickDataRest[5, 0] = new Vector2(0, 0);
        wallKickDataRest[5, 1] = new Vector2(-1, 0);
        wallKickDataRest[5, 2] = new Vector2(-1, -1);
        wallKickDataRest[5, 3] = new Vector2(0, 2);
        wallKickDataRest[5, 4] = new Vector2(-1, 2);

        wallKickDataRest[6, 0] = new Vector2(0, 0);
        wallKickDataRest[6, 1] = new Vector2(-1, 0);
        wallKickDataRest[6, 2] = new Vector2(-1, -1);
        wallKickDataRest[6, 3] = new Vector2(0, 2);
        wallKickDataRest[6, 4] = new Vector2(-1, 2);

        wallKickDataRest[7, 0] = new Vector2(0, 0);
        wallKickDataRest[7, 1] = new Vector2(1, 0);
        wallKickDataRest[7, 2] = new Vector2(1, 1);
        wallKickDataRest[7, 3] = new Vector2(0, -2);
        wallKickDataRest[7, 4] = new Vector2(1, -2);
        return wallKickDataRest;
    }

    public static Vector2[,] returnIData()
    {
        wallKickDataI[0, 0] = new Vector2(0, 0);
        wallKickDataI[0, 1] = new Vector2(-2, 0);
        wallKickDataI[0, 2] = new Vector2(1, 0);
        wallKickDataI[0, 3] = new Vector2(-2, -1);
        wallKickDataI[0, 4] = new Vector2(1, 2);

        wallKickDataI[1, 0] = new Vector2(0, 0);
        wallKickDataI[1, 1] = new Vector2(2, 0);
        wallKickDataI[1, 2] = new Vector2(-1, 0);
        wallKickDataI[1, 3] = new Vector2(2, 1);
        wallKickDataI[1, 4] = new Vector2(-1, -2);

        wallKickDataI[2, 0] = new Vector2(0, 0);
        wallKickDataI[2, 1] = new Vector2(-1, 0);
        wallKickDataI[2, 2] = new Vector2(2, 0);
        wallKickDataI[2, 3] = new Vector2(-1, 2);
        wallKickDataI[2, 4] = new Vector2(2, -1);

        wallKickDataI[3, 0] = new Vector2(0, 0);
        wallKickDataI[3, 1] = new Vector2(1, 0);
        wallKickDataI[3, 2] = new Vector2(-2, 0);
        wallKickDataI[3, 3] = new Vector2(1, -2);
        wallKickDataI[3, 4] = new Vector2(-2, 1);

        wallKickDataI[4, 0] = new Vector2(0, 0);
        wallKickDataI[4, 1] = new Vector2(2, 0);
        wallKickDataI[4, 2] = new Vector2(-1, 0);
        wallKickDataI[4, 3] = new Vector2(2, 1);
        wallKickDataI[4, 4] = new Vector2(-1, -2);

        wallKickDataI[5, 0] = new Vector2(0, 0);
        wallKickDataI[5, 1] = new Vector2(-2, 0);
        wallKickDataI[5, 2] = new Vector2(1, 0);
        wallKickDataI[5, 3] = new Vector2(-2, -1);
        wallKickDataI[5, 4] = new Vector2(1, 2);

        wallKickDataI[6, 0] = new Vector2(0, 0);
        wallKickDataI[6, 1] = new Vector2(1, 0);
        wallKickDataI[6, 2] = new Vector2(-2, 0);
        wallKickDataI[6, 3] = new Vector2(1, -2);
        wallKickDataI[6, 4] = new Vector2(-2, 1);

        wallKickDataI[7, 0] = new Vector2(0, 0);
        wallKickDataI[7, 1] = new Vector2(-1, 0);
        wallKickDataI[7, 2] = new Vector2(2, 0);
        wallKickDataI[7, 3] = new Vector2(-1, 2);
        wallKickDataI[7, 4] = new Vector2(2, -1);

        return wallKickDataI;
    }
}
