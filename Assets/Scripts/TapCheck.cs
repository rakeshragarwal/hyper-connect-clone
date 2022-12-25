using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Value
{
    public int firstIndex, secondIndex;
    public int firstValue, secondValue;
    public ButtonView firstGameObject, secondGameObject;
}

public class TapCheck : MonoBehaviour
{
    public static TapCheck instance;

    [SerializeField] private LineRendererGeneratoro lineRenderer;
    [SerializeField] private int tappedCount;

    public Value value;

    // int firstValue;
    // int secondValue;

    private void Awake()
    {
        instance = this;
    }

    public void MapLineRendered(LineRendererGeneratoro lineRendererGeneratoro)
    {
        lineRenderer = lineRendererGeneratoro;
    }

    private void Start()
    {
        tappedCount = 0;
    }


    public void TapTrack(ButtonView item)
    {
        tappedCount++;

        Debug.Log(tappedCount);

        if (tappedCount == 1)
        {
            value.firstValue = item.thisItem.value;
            value.firstGameObject = item;
            value.firstIndex = item.thisItem.index;

            // CheckIfBothAreConnected(value);

            item.DisableInteraction();
        }

        if (tappedCount == 2)
        {
            value.secondValue = item.thisItem.value;
            value.secondGameObject = item;
            value.secondIndex = item.thisItem.index;

            // CheckIfBothAreConnected(value);

            if (value.firstValue == value.secondValue)
            {
                Debug.Log("both same");

                if (CheckIfBothAreConnected(value.firstGameObject, value.secondGameObject))
                {
                    Debug.Log("directly connected");

                    value.firstGameObject.gameObject.SetActive(false);
                    value.secondGameObject.gameObject.SetActive(false);

                    if (value.firstIndex > value.secondIndex)
                    {
                        lineRenderer.SetupLinesRe(value.firstIndex);
                        lineRenderer.SetupLinesRe(value.secondIndex);
                    }
                    else
                    {
                        lineRenderer.SetupLinesRe(value.secondIndex);
                        lineRenderer.SetupLinesRe(value.firstIndex);
                    }

                    lineRenderer.UpdateLines();
                    // Debug.Log()
                    ResetValues();
                }
                else
                {
                    Debug.Log("both are not directly connected");
                    ResetValues();
                }
            }
            else
            {
                Debug.Log("both not same");

                ResetValues();
            }
        }
    }

    private bool CheckIfBothAreConnected(ButtonView firstButtonTapped, ButtonView secondButtonTapped)
    {
        LineRenderer _lineRenderer = lineRenderer.Lr;

        int lrPositionCount = _lineRenderer.positionCount;
        Vector3[] lrPositions = new Vector3[lrPositionCount];
        int check = 0;

        Vector3 firstValue = firstButtonTapped.thisItem.transforms.position;
        Vector3 secondValue = secondButtonTapped.thisItem.transforms.position;

        for (int j = 0; j < lrPositionCount; j++)
        {
            int n = 0;
            n = j; n += 1;
            if (n < lrPositionCount)
            {
                if ((_lineRenderer.GetPosition(j) == firstValue) && (_lineRenderer.GetPosition(n) == secondValue))
                {
                    check++;
                }
                else if ((_lineRenderer.GetPosition(n) == firstValue) && (_lineRenderer.GetPosition(j) == secondValue))
                {
                    check++;
                }
            }
            else
            {
                n -= 1;
                if (n <= lrPositionCount)
                {
                    if ((_lineRenderer.GetPosition(j) == firstValue) && (_lineRenderer.GetPosition(n) == secondValue))
                    {
                        check++;
                    }
                    else if ((_lineRenderer.GetPosition(n) == firstValue) && (_lineRenderer.GetPosition(j) == secondValue))
                    {
                        check++;
                    }
                }

            }
        }

        Debug.Log(check);
        if (check == 1)
        { return true; }
        else
        { return false; }


    }

    private void ResetValues()
    {
        value.firstIndex = 0;
        value.secondIndex = 0;
        value.firstValue = 0;
        value.secondValue = 0;
        tappedCount = 0;
        value.firstGameObject.EnableInteraction();
        value.firstGameObject = null;
        value.secondGameObject = null;
    }
}
