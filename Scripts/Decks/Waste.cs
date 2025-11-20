using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waste : Deck
{
    public static Waste s;

    private void Awake() { s = this; }

    private void Start()
    {
        closedCards = false;
    }

    private void Update()
    {
        Tick();
    }

    public void DrawCard()
    {
        if (Stock.s.cards.Count == 0)
        {
            while (cards.Count > 0)
            {
                cards.Last().activated = false;

                Tasks.MoveCard(cards, Stock.s, cards.Count - 1);
            }

            return;
        }

        if (cards.Count == 0) { Destroy(transform.GetChild(0).gameObject); }

        if (cards.Count > 0) { cards.Last().activated = false; }

        Tasks.MoveCard(Stock.s.cards, this, Stock.s.cards.Count - 1);

        cards.Last().activated = true;
    }
}
