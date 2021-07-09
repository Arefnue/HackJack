using DeckBuilding.Controllers;
using UnityEngine;

namespace DeckBuilding.Managers
{
    public class HandManager : MonoBehaviour
    {
        public static HandManager instance;
        
        [Header("Gameplay Settings")] 
        public int mana = 3;
        public bool canUseCards = true;
        public bool canSelectCards = true;
        
        public HandController handController;
        public CardController cardController;
        public ChoiceController choiceController;
        
        private void Awake()
        {
            instance = this;
        }
    }
}
