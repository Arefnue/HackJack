using System;
using DeckBuilding.Controllers;
using DeckBuilding.Managers;

namespace DeckBuilding.Card
{
    public static class CardActions
    {
        public static void PlayCardAction(EnemyBase targetEnemy, PlayerAction playerAction)
        {
            switch (playerAction.myPlayerActionType)
            {
                case PlayerAction.PlayerActionType.Attack:
                    AttackTargetEnemy(targetEnemy, playerAction);
                    break;
                case PlayerAction.PlayerActionType.Heal:
                    HealPlayer(playerAction);
                    break;
                case PlayerAction.PlayerActionType.Block:
                    GainBlock(playerAction);
                    break;
                case PlayerAction.PlayerActionType.IncreaseStr:
                    GainStrength(playerAction);
                    break;
                case PlayerAction.PlayerActionType.IncreaseMaxHealth:
                    GainMaxHealth(playerAction);
                    break;
                case PlayerAction.PlayerActionType.Draw:
                    DrawCards(playerAction);
                    break;
                case PlayerAction.PlayerActionType.ReversePoisonDamage:
                    ReversePoisonToDamage(targetEnemy, playerAction);
                    break;
                case PlayerAction.PlayerActionType.ReversePoisonHeal:
                    ReversePoisonToHeal(playerAction);
                    break;
                case PlayerAction.PlayerActionType.IncreaseMana:
                    GainMana(playerAction);
                    break;
                case PlayerAction.PlayerActionType.StealMaxHealth:
                    StealMaxHealthFromTarget(targetEnemy, playerAction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region Methods

         private static void GainMana(PlayerAction playerAction)
        {
            LevelManager.instance.IncreaseMana((int) playerAction.value);
        }

        private static void ReversePoisonToHeal(PlayerAction playerAction)
        {
            var poisonCount = LevelManager.instance.playerController.myHealth.poisonStack;
            LevelManager.instance.playerController.myHealth.Heal(playerAction.value * poisonCount);
            LevelManager.instance.playerController.myHealth.ClearPoison();
        }

        private static void DrawCards(PlayerAction playerAction)
        {
            LevelManager.instance.DrawCards((int) playerAction.value);
        }

        private static void GainMaxHealth(PlayerAction playerAction)
        {
            GameManager.instance.ChangePlayerMaxHealth(playerAction.value);
        }

        private static void GainStrength(PlayerAction playerAction)
        {
            LevelManager.instance.playerController.IncreaseStr((int) playerAction.value);
        }

        private static void GainBlock(PlayerAction playerAction)
        {
            LevelManager.instance.playerController.myHealth.ApplyBlock(playerAction.value);
        }


        private static void StealMaxHealthFromTarget(EnemyBase targetEnemy, PlayerAction playerAction)
        {
            targetEnemy.myHealth.DecreaseMaxHealth(playerAction.value);
            GameManager.instance.ChangePlayerMaxHealth(playerAction.value);
        }

        private static void ReversePoisonToDamage(EnemyBase targetEnemy, PlayerAction playerAction)
        {
            var poisonCount = LevelManager.instance.playerController.myHealth.poisonStack;
            targetEnemy.myHealth.TakeDamage(playerAction.value * poisonCount);
            LevelManager.instance.playerController.myHealth.ClearPoison();
        }

        private static void HealPlayer(PlayerAction playerAction)
        {
            LevelManager.instance.playerController.myHealth.Heal(playerAction.value);
        }

        private static void AttackTargetEnemy(EnemyBase targetEnemy, PlayerAction playerAction)
        {
            targetEnemy.myHealth.TakeDamage(playerAction.value + LevelManager.instance.playerController.bonusStr);
        }

        #endregion
    }
}