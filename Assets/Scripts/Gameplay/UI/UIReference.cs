using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using TMPro;
using UnityEngine.UI;

namespace com.BomBerMan.Gameplay
{
    [System.Serializable]
    public struct PlayerCharacter
    {
        [Header("COMPONENTS")]
        // Ref script chua mau/mana/etc.
        public MovementController movementController;
        // Ref skill controller
        public SkillController skillController;
      

        [Header("UI REFERENCES")]
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI shieldText;
        public TextMeshProUGUI skillCooldownText;
    }

    public class UIReference : MonoBehaviour
    {
        [Header("CHARACTER REFERENCES")]
        // Ref vo nhan vat
        public List<PlayerCharacter> playerCharacterList = new List<PlayerCharacter>();

        private CompositeDisposable disposable;

        private void Start()
        {
            disposable = new CompositeDisposable();

            for (int i = 0; i < playerCharacterList.Count; i++)
            {
                var player = playerCharacterList[i];
                player.movementController
                    .ObserveEveryValueChanged(x => x.health)
                    .Subscribe(x =>
                    {
                        player.hpText.SetText($"HP: {x}");
                    }).AddTo(disposable);

                player.movementController
                    .ObserveEveryValueChanged(x => x.shield)
                    .Subscribe(x =>
                    {
                        player.shieldText.SetText($"Shield: {x}");
                    }).AddTo(disposable);

                // Sub event cho skill
                var skillCollection = player.skillController.skillActionCollection;

                for (int k = 0; k < skillCollection.Count; k++)
                {
                    var skill = skillCollection[k];

                    //Observable.EveryUpdate().Subscribe(_ => Debug.Log(skill.skillAction.currentTimeForCooldown)); test show cooldown time

                    Observable.EveryUpdate()
                        .Where(x => skill.skillAction.currentTimeForCooldown != 0)
                        .Subscribe(_ =>
                        {
                            player.skillCooldownText
                               .SetText(skill.skillAction.currentTimeForCooldown.ToString("#.##"));
                        }).AddTo(disposable);

                    Observable.EveryUpdate()
                        .Where(x => skill.skillAction.currentTimeForCooldown == 0)
                        .Subscribe(_ =>
                        {
                            player.skillCooldownText
                               .SetText("Active now");
                        }).AddTo(disposable);

                    skill.skillActionDown.ObserveEveryValueChanged(x => x.Value)
                    .Subscribe(x =>
                    {
                        if (x)
                        {
                            // Neu state down la true

                            // Chay coroutine dem gio

                        }
                        else
                        {
                            // Neu state down la false
                        }
                    }).AddTo(disposable);

                    skill.skillActionHold.ObserveEveryValueChanged(x => x.Value)
                        .Subscribe(x =>
                        {
                            if (x)
                            {
                            }
                            else
                            {
                            // Neu state hold la false
                            }
                        }).AddTo(disposable);

                    skill.skillActionUp.ObserveEveryValueChanged(x => x.Value)
                        .Subscribe(x =>
                        {
                            if (x)
                            {
                            
                                // Cancel coroutine
                            }
                            else
                            {
                                // Neu state up la false
                            }
                        }).AddTo(disposable);
                }
            }
        }

        private void CooldownText()
        {
            StartCoroutine(CooldownTextCo());
        }

        private void StopCooldownText()
        {
            StopCoroutine(CooldownTextCo());
        }

        private IEnumerator CooldownTextCo()
        {
            // Update text

            yield return null;
        }


        private void OnDestroy()
        {
            disposable?.Dispose();
        }
    }
}
