using System;
using System.Collections;
using DeckBuilding.Controllers;
using DeckBuilding.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeckBuilding
{
    public class Health : MonoBehaviour
    {
        public float maxHealth;
        private float _currentHealth = 100;
        public bool isPlayer;
        private bool _isDead=false;
        public TextMeshProUGUI currentHealthText;
        public Image blockImage;
        public TextMeshProUGUI blockText;
        public Image poisonImage;
        public TextMeshProUGUI poisonText;
        
        public Action deathAction;

        [HideInInspector] public float poisonStack;
        [HideInInspector] public float blockStack;
        
        [HideInInspector] public float bonusStr=0;

        public Image strImage;
        public TextMeshProUGUI strText;

        private void Start()
        {
            if (isPlayer)
            {
                _currentHealth = GameManager.instance.playerCurrentHealth;
                maxHealth = GameManager.instance.playerMaxHealth;
            }
            else
            {
                _currentHealth = maxHealth;
            }
            
            ClearStr();
            ClearBlock();
            ClearPoison();
            ChangeHealthText();
           
        }

        private void ClearStr()
        {
            bonusStr = 0;
            strImage.gameObject.SetActive(false);
            strText.gameObject.SetActive(false);
        }

        public void ApplyStr(int bonus)
        {
            bonusStr += bonus;
            strImage.gameObject.SetActive(true);
            strText.gameObject.SetActive(true);
            strText.text = bonusStr.ToString();
            GiveFeedback(strImage);
        }
        
        public void SavePlayerStats()
        {
            GameManager.instance.playerCurrentHealth = _currentHealth;
        }
        

        public void ApplyPoisonDamage(float stack)
        {
            poisonStack += stack;
            poisonImage.gameObject.SetActive(true);
            poisonText.gameObject.SetActive(true);
            poisonText.text =  $"{poisonStack}";
            GiveFeedback(poisonImage);
        }

        public void ApplyBlock(float stack)
        {
            blockStack += stack;
            ChangeHealthText();
            GiveFeedback(blockImage);
        }
        
        public void DecreaseMaxHealth(float value)
        {
            maxHealth -= value;
            if (_currentHealth>maxHealth)
            {
                _currentHealth = maxHealth;
            }
            ChangeHealthText();
        }

        public void ClearPoison()
        {
            poisonStack = 0;
            poisonImage.gameObject.SetActive(false);
            poisonText.gameObject.SetActive(false);
        }
        public void ClearBlock()
        {
            blockStack = 0;
            blockImage.gameObject.SetActive(false);
            blockText.gameObject.SetActive(false);
        }

        public void TakeDamage(float damage,bool isPoison = false)
        {
            if (_isDead)
            {
                return;
            }
            
            if (!isPoison)
            {
                TakeNormalDamage(damage);
            }
            else
            {
                TakePoisonDamage(damage);
            }


            if (_currentHealth<=0)
            {
                _currentHealth = 0;
                _isDead = true;
                deathAction?.Invoke();
            }
            ChangeHealthText();
        }

        private void TakeNormalDamage(float damage)
        {
            if (blockStack > 0)
            {
                blockStack -= damage;
                if (blockStack < 0)
                {
                    _currentHealth -= Mathf.Abs(blockStack);
                    GiveFeedback(blockImage);
                }
            }
            else
            {
                _currentHealth -= damage;
            }
        }

        private void TakePoisonDamage(float damage)
        {
            _currentHealth -= damage;
            GiveFeedback(poisonImage);
        }

        public void Heal(float healValue)
        {
            if (_isDead)
            {
                return;
            }
            _currentHealth += healValue;
            if (_currentHealth>maxHealth)
            {
                _currentHealth = maxHealth;
            }
            ChangeHealthText();
        }

        private void GiveFeedback(Image targetImage)
        {
            StartCoroutine(ImageFeedbackRoutine(targetImage));
        }

        private IEnumerator ImageFeedbackRoutine(Image targetImage)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var initalScale = targetImage.transform.localScale;
            var targetScale = initalScale * 1.15f;
            
            while (true)
            {
                timer += Time.deltaTime*10;

                targetImage.transform.localScale = Vector3.Lerp(initalScale, targetScale, timer);
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }

            timer = 0f;
            while (true)
            {
                timer += Time.deltaTime*10;

                targetImage.transform.localScale = Vector3.Lerp(targetScale, initalScale, timer);
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
            
        }

        public void ChangeHealthText()
        {
            currentHealthText.text = $"{_currentHealth}/{maxHealth}";
            if (isPlayer)
            {
                UIManager.instance.UpdateHealthText();
            }

            if (LevelManager.instance.malfunctionController.currentMalfunction.myMalfunctionType == MalfunctionBase.MalfunctionType.LackOfEmpathy)
            {
                currentHealthText.text = $"???/???";
            }
            
            if (blockStack>0)
            {
                blockImage.gameObject.SetActive(true);
                blockText.gameObject.SetActive(true);
                blockText.text = $"{blockStack}";
            }
            else
            {
                blockImage.gameObject.SetActive(false);
                blockText.gameObject.SetActive(false);
            }
        }

    }
}