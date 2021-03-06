using DeckBuilding.Controllers;
using DeckBuilding.Managers;
using UnityEngine;

namespace DeckBuilding.Card
{
    public class Choice : MonoBehaviour
    {
        public Transform cardTransform;
        private CardSO _myChoiceProfile;
        
        public void DetermineChoice()
        {
            do
            {
                _myChoiceProfile = GameManager.instance.choiceCardList[Random.Range(0, GameManager.instance.choiceCardList.Count)];
                if (HandManager.instance.sameChoiceContainerList.Count>= GameManager.instance.choiceCardList.Count)
                {
                    break;
                }
            } while (HandManager.instance.sameChoiceContainerList.Contains(_myChoiceProfile.myID));
            
            HandManager.instance.sameChoiceContainerList.Add(_myChoiceProfile.myID);
            var clone =GameManager.instance.BuildAndGetCard(_myChoiceProfile.myID,cardTransform);
            GameManager.instance.choiceContainer.Add(clone);
            if (LevelManager.instance.malfunctionController.currentMalfunction.myMalfunctionType == MalfunctionBase.MalfunctionType.VisualDisorder)
            {
                
            }

        }

        public void OnChoice()
        {
            GameManager.instance.myDeckIDList.Add(_myChoiceProfile.myID);
            HandManager.instance.sameChoiceContainerList.Clear();
            foreach (var cardBase in GameManager.instance.choiceContainer)
            {
                Destroy(cardBase);
            }
            GameManager.instance.NextLevel();
        }
    }
}
