using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowChanger : MonoBehaviour
{

    static ParticleSystem ps;

    [System.Obsolete]
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Change(Constants.White);
    }

    [System.Obsolete]
    public static void Change(Color color)
    {
        ps.startColor = color;
    }
}
