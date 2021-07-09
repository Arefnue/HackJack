using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeckBuilding.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public TextMeshProUGUI drawPileText;
        public TextMeshProUGUI discardPileText;
        public TextMeshProUGUI manaText;
        public GameObject gameCanvas;
        public GameObject winPanel;
        public GameObject losePanel;

        public GameObject randomizedDeck;
        private void Awake()
        {
            instance = this;
            winPanel.SetActive(false);
            losePanel.SetActive(false);
            randomizedDeck.SetActive(GameManager.instance.isRandomHand);
        }

        public void SetPileTexts()
        {
            drawPileText.text = $"{HandManager.instance.drawPile.Count.ToString()}";
            discardPileText.text = $"{HandManager.instance.discardPile.Count.ToString()}";
            manaText.text = $"{HandManager.instance.mana.ToString()}";
        }

        public void EndTurn()
        {
            if (LevelManager.instance.CurrentLevelState != LevelManager.LevelState.PlayerTurn)
            {
                return;
            }
            LevelManager.instance.EndTurn();
        }

        public void MainMenu()
        {
            GameManager.instance.ResetManager();
            SceneManager.LoadScene(0);
        }
    }
}
