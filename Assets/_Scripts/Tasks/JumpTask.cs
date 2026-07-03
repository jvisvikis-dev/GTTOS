using UnityEngine;

public class JumpTask : Task
{
    [SerializeField] private int minJumpCount;
    [SerializeField] private PlayerController player;
    int currentJumpCount;
    private void OnEnable()
    {
        player.jump += countJump; 
    }

    public void countJump()
    {
        if(++currentJumpCount >= minJumpCount)
        {
            flags[0] = true;
        }
    }

}
