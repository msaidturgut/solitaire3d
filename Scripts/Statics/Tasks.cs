using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Tasks
{
    public static void MoveCard(List<CardObject> source, Deck target, int index)
    {
        target.cards.Add(source[index]);
        source.Remove(source[index]);
        target.cards.Last().transform.SetParent(target.transform);
    }
}
