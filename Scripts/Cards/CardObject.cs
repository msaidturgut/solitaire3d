using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public Card data;
    public Deck deck;

    public int index;
    public Type deckType;

    public bool closed;
    public bool activated;

    private BoxCollider col;

    private void Start()
    {
        gameObject.name = data.name;

        col = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        index = transform.GetSiblingIndex();

        deck = GetComponentInParent<Deck>();

        deckType = deck.GetType();

        closed = deck.closedCards && !activated;

        bool validType = deckType != typeof(Stock) && deckType != typeof(Select);

        col.enabled = (validType  && (index == deck.cards.Count - 1 || activated)) || deck.cards.Count == 0;

        transform.localPosition = deckType != typeof(Pile) && deckType != typeof(Select) ? 
            new Vector3(0, index * 0.001f, 0) : 
            new Vector3(0, index == 0 ? 0 : 0.003f, index * -0.045f);

        transform.localEulerAngles = new Vector3(
            deckType != typeof(Pile) || index == 0 ? 0 : -2,
            0,
            closed ? -180 : 0);
    }
}
