using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<CardObject> cards;

    public bool closedCards;

    protected void Tick()
    {
        if(cards.Count == 0 && GetType() != typeof(Stock) && !GetComponentInChildren<CardObject>()) 
        { 
            Instantiate(CardsData.s.deckGuide, transform); 
        }
    }
}
