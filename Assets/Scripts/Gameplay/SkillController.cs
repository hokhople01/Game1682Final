using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

[System.Serializable]
public struct Skill
{
    public KeyCode keycode;
    [Expandable]
    public SkillActionBase skillAction;

    public ReactiveProperty<bool> skillActionDown;
    public ReactiveProperty<bool> skillActionHold;
    public ReactiveProperty<bool> skillActionUp;
}

public class SkillController : MonoBehaviour
{
    public List<Skill> skillActionCollection = new List<Skill>();

    private void Start()
    {
        MovementController movement = GetComponent<MovementController>();
        foreach (var s in skillActionCollection)
        {
            s.skillAction.Init(movement);
        }
    }

    private void Update()
    {
        for (int i = 0; i < skillActionCollection.Count; i++)
        {
            var s = skillActionCollection[i];

            s.skillActionDown.Value = Input.GetKeyDown(s.keycode);
            s.skillActionHold.Value = Input.GetKey(s.keycode);
            s.skillActionUp.Value = Input.GetKeyUp(s.keycode);

            if (Input.GetKeyDown(s.keycode))
                s.skillAction.OnKeyDown();

            if (Input.GetKeyUp(s.keycode))
                s.skillAction.OnKeyUp();

            if (Input.GetKey(s.keycode))
                s.skillAction.OnKeyHold();
        }
    }
}