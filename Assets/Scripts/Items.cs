using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Items : ScriptableObject
{
    public List<Item> items;
}

[System.Serializable]
public class Item
{
    public Transform transforms;
    public Sprite sprite;
    public int value;
    public int index;
}
