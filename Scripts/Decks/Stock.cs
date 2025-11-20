using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Stock : Deck
{
    public static Stock s;

    public GameObject cardObj;

    private void Awake()
    {
        s = this;

        cardObj = Resources.Load<GameObject>("CardObject");

        for (int i = 1; i <= 52; i++) { CreateCard(i); }

        ShufflePile();

        for (int i = 0; i < cards.Count; i++) { cards[i].transform.SetSiblingIndex(i); }
    }

    private void Start()
    {
        closedCards = true;
    }

    private void Update()
    {
        Tick();
    }

    public CardObject CreateCard(int id)
    {
        CardObject crd = Instantiate(cardObj, transform).GetComponent<CardObject>();
        cards.Add(crd);
        crd.data = CardsData.s.cards[id];
        crd.GetComponent<MeshRenderer>().material = crd.data.material;

        return crd;
    }

    public void ShufflePile()
    {
        List<CardObject> tempStock = cards;

        cards = new List<CardObject>();

        while (tempStock.Count > 0)
        {
            int select = Random.Range(0, tempStock.Count);
            cards.Add(tempStock[select]);
            tempStock.Remove(tempStock[select]);
        }        
    }
}