using System.Collections.Generic;
using UnityEngine;

namespace DeckBuilding.Card
{
    [CreateAssetMenu(fileName = "Deck", menuName = "Data/Deck", order = 0)]
    public class DeckSO : ScriptableObject
    {
        public List<CardSO> cards;
    }
}