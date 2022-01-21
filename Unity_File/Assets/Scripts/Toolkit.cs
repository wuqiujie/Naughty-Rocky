using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolkit
{

    public static Vector3 ProjectToXZ(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}
