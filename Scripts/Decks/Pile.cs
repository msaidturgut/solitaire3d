using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : Deck
{
    public int id;

    private void Awake()
    {
        id = transform.GetSiblingIndex() + 1;
    }

    private void Start()
    {
        for(int i = 0; i< id; i++)
        {
            Tasks.MoveCard(Stock.s.cards, this, Stock.s.cards.Count - 1);
        }

        closedCards = true;

        cards[id - 1].activated = true;
    }

    private void Update()
    {
        Tick();
    }
}
