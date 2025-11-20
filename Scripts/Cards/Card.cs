using System;
using UnityEngine;

[Serializable]
public class Card
{
    public string name;
    public int id;
    public string type;
    public string color;
    public int value;
    public Material material;

    public Card() { }
}