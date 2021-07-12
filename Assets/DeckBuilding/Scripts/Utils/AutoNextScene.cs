using System;
using DeckBuilding.Managers;
using UnityEngine;

namespace DeckBuilding.Utils
{
    public class AutoNextScene : MonoBehaviour
    {
        private void Start()
        {
            GameManager.instance.ChangeScene(1);
        }
    }
}
