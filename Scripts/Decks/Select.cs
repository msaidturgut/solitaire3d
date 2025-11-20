using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Select : Deck
{
    public static Select s;

    private Camera cam;

    private Deck prevDeck;

    private RaycastHit hit;

    private const float addition = 0.5f;
    private const float divide = 8000;

    public List<CardObject> tester;

    private void Awake() { s = this; }

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        closedCards = false;
    }

    private void Update()
    {
        FollowMouse();

        DetectSelection();
    }

    private void FollowMouse()
    {
        float multiplier = addition + (Input.mousePosition.y / divide);

        Vector2 screenPosition = Input.mousePosition;
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3
                (screenPosition.x, screenPosition.y, multiplier));

        transform.localPosition = touchPosition;
    }

    private void DetectSelection()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 10))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.GetComponent<Stock>() && cards.Count == 0) { Waste.s.DrawCard(); return; }

                if (!hit.transform.GetComponent<CardObject>()) { return; }

                CardObject selection = hit.transform.GetComponent<CardObject>();

                if (cards.Count == 0)
                {
                    if(selection.data.value == 0) { return; }

                    TakeCard(selection.deck, selection.index);
                }
                else
                {
                    PutCard(selection);
                }
            }
        }
    }

    private void TakeCard(Deck selected, int index)
    {
        prevDeck = selected;

        int remover = 0;

        for (int i = index; i < selected.cards.Count; i++)
        {
            cards.Add(selected.cards[i]);
            cards.Last().transform.SetParent(transform);

            remover++;
        }

        while(remover > 0)
        {
            selected.cards.Remove(selected.cards.Last());
            remover--;
        }
    }

    private void PutCard(CardObject selection)
    {
        if (selection.deckType == typeof(Foundation))
        {
            PutOnFoundation(selection);
            return;
        }

        if ((selection.deckType == typeof(Pile) && IsSuitable(selection)) 
            || selection.deck == prevDeck)
        {
            PutOnPile(selection);
            return;
        }

        Debug.Log("Invalid Placement");
    }

    private void PutOnPile (CardObject selection)
    {
        if (selection.deck.cards.Count == 0)
        {
            if (cards[0].data.value != 13 && selection.deck != prevDeck)
            {
                Debug.Log("Only king can be placed on empty pile");
                return;
            }
            Destroy(selection.deck.transform.GetChild(0).gameObject);
        }

        while (cards.Count > 0)
        {
            Tasks.MoveCard(cards, selection.deck, 0);
        }

        Protocol();
    }

    private void PutOnFoundation(CardObject selection)
    {
        if (cards.Count == 1)
        {
            Foundation foundation = selection.deck.GetComponent<Foundation>();

            CardObject cardOnTop = foundation.transform.GetChild(foundation.transform.childCount - 1).GetComponent<CardObject>();

            if (foundation.type == cards[0].data.type &&
                cardOnTop.data.value == cards[0].data.value - 1)
            {
                if (selection.deck.cards.Count == 0) { Destroy(selection.deck.transform.GetChild(0).gameObject); }

                Tasks.MoveCard(cards, foundation, 0);

                Protocol();
            }
        }
        else
        {
            Debug.Log("You can only place cards one by one");
        }
    }

    private void Protocol()
    {
        if (prevDeck.cards.Count != 0
            && (prevDeck.GetType() == typeof(Pile) 
            || prevDeck.GetType() == typeof(Foundation) 
            || prevDeck.GetType() == typeof(Waste)))
        {
            prevDeck.cards.Last().activated = true;
        }

        prevDeck = null;
    }

    private bool IsSuitable(CardObject selection)
    {
        bool check1 = cards[0].data.value + 1 == selection.data.value && cards[0].data.color != selection.data.color;

        bool check2 = selection.deck.cards.Count == 0 && cards[0].data.value == 13;

        return check1 || check2;
    }
}