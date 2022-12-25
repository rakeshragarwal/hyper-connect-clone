using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private LineRendererGeneratoro line;
    [SerializeField] private TapCheck tapCheckPrefab;

    void Start()
    {
        var abc = Instantiate(line.gameObject);

        line = abc.GetComponent<LineRendererGeneratoro>();

        Instantiate(tapCheckPrefab);

        TapCheck.instance.MapLineRendered(line);
    }
}
