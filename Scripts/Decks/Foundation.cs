using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : Deck
{
    public int id;

    public string type;

    private void Awake()
    {
        id = transform.GetSiblingIndex() - 9;
        type = Enums.types[id - 1];
    }

    private void Start()
    {
        closedCards = false;
    }

    private void Update()
    {
        Tick();
    }
}
