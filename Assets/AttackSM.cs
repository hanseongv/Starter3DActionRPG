using Player;
using UnityEngine;
using UnityEngine.Animations;

public class AttackSM : StateMachineBehaviour
{
    // public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     var controller = animator.GetComponentInParent<PlayerController>();
    //     controller.isAttack = true;
    // }
    // public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     var controller = animator.GetComponentInParent<PlayerController>();
    //     controller.isAttack = false;
    //     Debug.Log("OnStateExit");
    // }
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        var controller = animator.GetComponentInParent<PlayerController>();
        controller.isAttack = true;
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        var controller = animator.GetComponentInParent<PlayerController>();
        controller.isAttack = false;
    }
}