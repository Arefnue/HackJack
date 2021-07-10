using System;
using System.Collections.Generic;
using System.Linq;
using DeckBuilding.Managers;
using NueExtentions;
using UnityEngine;

namespace DeckBuilding.Controllers
{
    public class MalfunctionController : MonoBehaviour
    {
        public List<MalfunctionBase> allMalfunctions;

        [HideInInspector] public MalfunctionBase currentMalfunction;
        public int maxMalfunctionTurn = 5;
        [HideInInspector] public int malfunctionTurnCounter;

        private void Awake()
        {
            //currentMalfunction = allMalfunctions[0];
        }

        public void CountMalfunction()
        {
            malfunctionTurnCounter--;
            if (malfunctionTurnCounter<=0)
            {
                malfunctionTurnCounter = maxMalfunctionTurn;
                GetRandomMalfunction();
            }
            UIManager.instance.UpdateMalfunctionCounter();
        }
        
        public void GetRandomMalfunction()
        {
            var randomMalfunction = allMalfunctions.RandomItem();
            if (currentMalfunction != null)
            {
                if (currentMalfunction == randomMalfunction)
                {
                    randomMalfunction = allMalfunctions.FirstOrDefault(x => x != currentMalfunction);
                }
            }

            if (randomMalfunction != null)
            {
                ApplyStatus(randomMalfunction);
                UIManager.instance.UpdateMalfunctionName(currentMalfunction);
            }
            else
            {
                ApplyStatus(allMalfunctions.RandomItem());
            }
            
           
        }
        
        
        public void ApplyStatus(MalfunctionBase targetMalfunction)
        {
            if (currentMalfunction != null)
            {
                ReleaseStatus(currentMalfunction);
            }
            
            currentMalfunction = targetMalfunction;

            switch (targetMalfunction.myMalfunctionType)
            {
                case MalfunctionBase.MalfunctionType.VisualDisorder:
                    break;
                case MalfunctionBase.MalfunctionType.ReverseDraw:
                    break;
                case MalfunctionBase.MalfunctionType.SuicidalViolence:
                    break;
                case MalfunctionBase.MalfunctionType.CentralConcentration:
                    LevelManager.instance.CompressEnemies();
                    break;
                case MalfunctionBase.MalfunctionType.LackOfEmpathy:
                    UIManager.instance.UpdateHealthText();
                    foreach (var currentEnemy in LevelManager.instance.currentEnemies)
                    {
                        currentEnemy.myHealth.ChangeHealthText();
                    }
                    LevelManager.instance.playerController.myHealth.ChangeHealthText();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            UpdateStatus(targetMalfunction);
        }

        public void ReleaseStatus(MalfunctionBase targetMalfunction)
        {
            
            //currentMalfunction = null;
            switch (targetMalfunction.myMalfunctionType)
            {
                case MalfunctionBase.MalfunctionType.VisualDisorder:
                    break;
                case MalfunctionBase.MalfunctionType.ReverseDraw:
                    break;
                case MalfunctionBase.MalfunctionType.SuicidalViolence:
                    break;
                case MalfunctionBase.MalfunctionType.CentralConcentration:
                    LevelManager.instance.DecompressEnemies();
                    break;
                case MalfunctionBase.MalfunctionType.LackOfEmpathy:
                    // UIManager.instance.UpdateHealthText();
                    // foreach (var currentEnemy in LevelManager.instance.currentEnemies)
                    // {
                    //     currentEnemy.myHealth.ChangeHealthText();
                    // }
                    // LevelManager.instance.playerController.myHealth.ChangeHealthText();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateStatus(MalfunctionBase targetMalfunction)
        {
            UIManager.instance.UpdateHealthText();
            foreach (var currentEnemy in LevelManager.instance.currentEnemies)
            {
                currentEnemy.myHealth.ChangeHealthText();
            }
            LevelManager.instance.playerController.myHealth.ChangeHealthText();
        }
    }
    
    [Serializable]
    public class MalfunctionBase
    {
        public enum MalfunctionType
        {
            VisualDisorder,
            ReverseDraw,
            SuicidalViolence,
            CentralConcentration,
            LackOfEmpathy
        }

        public string malfunctionName;
        public MalfunctionType myMalfunctionType;
    }
    
}