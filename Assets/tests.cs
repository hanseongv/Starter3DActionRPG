using UnityEngine;

public class tests : StateMachineBehaviour
{

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        Debug.Log("OnStateMachineEnter");
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        // 상태 머신을 떠날 때 실행되는 코드
        Debug.Log("OnStateMachineExit");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}