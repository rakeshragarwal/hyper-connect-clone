using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererGeneratoro : Singleton<LineRendererGeneratoro>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Items itemSO;
    [SerializeField] private ButtonView buttonPrefab;
    [SerializeField] private List<ButtonView> activeButtons;
    private LineRenderer lr;

    public List<Transform> transformsGO;
    public LineRenderer Lr { get => lr; set => lr = value; }


    private void Start()
    {
        canvas = Instantiate(canvas);

        Lr = GetComponent<LineRenderer>();

        Setup();
    }

    public void SetupLinesRe(int pos)
    {

        transformsGO.RemoveAt(pos);
        activeButtons.RemoveAt(pos);

    }

    public void Setup()
    {
        foreach (var item in itemSO.items)
        {
            transformsGO.Add(item.transforms);
        }

        SetupLines(transformsGO);
        UpdateLines();
        SetupGame();
    }

    public void SetupLines(List<Transform> points)
    {
        Lr.positionCount = points.Count;
    }

    public void UpdateLines()
    {
        if (transformsGO.Count != 0)
            UpdateIndex();

        if (transformsGO.Count != 0)
        {
            for (int i = 0; i < transformsGO.Count; i++)
            {
                Lr.SetPosition(i, transformsGO[i].position);
            }
        }

        if (transformsGO.Count != 0)
            Lr.positionCount = transformsGO.Count;


        if (transformsGO.Count == 0)
        {
            Lr.SetPosition(0, Vector3.zero);
            Lr.SetPosition(1, Vector3.zero);
            Lr.positionCount = 2;
        }
    }

    private void UpdateIndex()
    {
        for (int i = 0; i < activeButtons.Count; i++)
        {
            activeButtons[i].thisItem.index = i;
        }
    }

    public void SetupGame()
    {
        for (int i = 0; i < transformsGO.Count; i++)
        {
            ButtonView thisButton = Instantiate(buttonPrefab);
            thisButton.transform.SetParent(canvas.transform);

            activeButtons.Add(thisButton);

            thisButton.thisItem.index = i;

            Lr.SetPosition(i, transformsGO[i].position);

            thisButton.thisItem.transforms = transformsGO[i];

            Vector3 pos = Camera.main.WorldToScreenPoint(transformsGO[i].position);

            thisButton.transform.position = pos;
            thisButton.thisItem.sprite = itemSO.items[i].sprite;

            thisButton.GetComponent<Image>().sprite = thisButton.thisItem.sprite;

            foreach (var item in itemSO.items)
            {
                if (item.transforms == transformsGO[i])
                {
                    thisButton.thisItem.value = item.value;
                    thisButton.thisItem.sprite = item.sprite;
                }
            }
        }
    }
}