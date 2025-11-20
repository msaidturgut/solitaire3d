using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CardsData : MonoBehaviour
{
    public static CardsData s;

    public List<Card> cards = new List<Card>();

    public Material defMat;

    public GameObject deckGuide;

    private void Awake()
    {
        s = this;

        deckGuide = Resources.Load<GameObject>("DeckGuide");

        string[] lines = Resources.Load<TextAsset>("CardsData").text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] splitData = lines[i].Split(',');
            Card card = new Card();
            int.TryParse(splitData[0], NumberStyles.Any, CultureInfo.InvariantCulture, out card.id);
            card.type = splitData[1];
            int.TryParse(splitData[2], NumberStyles.Any, CultureInfo.InvariantCulture, out card.value);

            card.name = $"{card.type} {card.value}";

            card.color = (card.type == "Spade" || card.type == "Club") ? "Black" : "Red";

            card.material = new Material(defMat);
            card.material.SetTexture("_MainTex", Resources.Load<Texture>($"Cards/{card.id}"));

            cards.Add(card);
        }
    }
}
