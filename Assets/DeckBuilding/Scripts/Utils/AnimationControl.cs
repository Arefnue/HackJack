using DeckBuilding.Managers;
using UnityEngine;

namespace DeckBuilding.Utils
{
    public class AnimationControl : MonoBehaviour
    {
        public void ChangeScene()
        {
            GameManager.instance.ChangeScene(3);
        }
    }
}
