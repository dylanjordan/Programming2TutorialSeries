using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public bool _enableDebug;

    private void Update()
    {
        Utility._debug = _enableDebug;
    }
}
