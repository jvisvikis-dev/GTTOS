using UnityEngine;

public class LookBothWays : Task
{
    [SerializeField] private int minLeftCount;
    [SerializeField] private int minRightCount;
    [SerializeField] private PlayerController player;
    bool isLookingLeft = false;
    bool isLookingRight = false;
    int leftCount = 0;
    int rightCount = 0;
    private void Awake()
    {
        flags = new bool[2];
    }

    private void Update()
    {
        if(player.transform.rotation.eulerAngles.y < 140 && !isLookingLeft)
        {
            isLookingLeft = true;
            LookedLeft();
        }
        else if (player.transform.rotation.eulerAngles.y > 220 && !isLookingRight)
        {
            isLookingRight = true;
            LookedRight();
        }
        else if(player.transform.rotation.eulerAngles.y >= 140 && player.transform.rotation.eulerAngles.y <= 220)
        {
            isLookingLeft = false;
            isLookingRight = false;
        }
    }
    public void LookedLeft()
    {
        leftCount++;
        if (leftCount >= minLeftCount)
        {
            flags[0] = true;
        }
    }

    public void LookedRight()
    {
        rightCount++;
        if (rightCount >= minRightCount)
        {
            flags[1] = true;
        }
    }

}
