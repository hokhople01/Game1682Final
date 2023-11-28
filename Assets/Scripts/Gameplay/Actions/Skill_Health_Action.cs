using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CooldownAPI;

[CreateAssetMenu(fileName = "Skill Health Action", menuName = "BomBerMan/Actions/Skills/Skill Health Action")]
public class Skill_Health_Action : SkillActionBase
{
    public override void OnKeyDown()
    {
        if (cooldown.IsActive)
            return;

        cooldown.Activate();
        Debug.Log($"{this.GetType().Name} OnKeyDown");
        // coding action for skill in here
        movement.health += 1;
    }

    public override void OnKeyHold()
    {
        Debug.Log($"{this.GetType().Name} OnKeyHold");
    }

    public override void OnKeyUp()
    {
        Debug.Log($"{this.GetType().Name} OnKeyUp");
    }
}
