using DeckBuilding.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeckBuilding.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public Health myHealth;
       
        public GameObject playerHighlight;
        public Transform fxParent;
        
        private void Awake()
        {
            myHealth = GetComponent<Health>();
            myHealth.deathAction += OnDeath;
           
            playerHighlight.SetActive(false);
        }

       

        private void OnDeath()
        {
            LevelManager.instance.OnPlayerDeath();
        }
    }
}