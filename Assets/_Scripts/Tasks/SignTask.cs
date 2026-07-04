using UnityEngine;

public class SignTask : Task
{
    [SerializeField] private Sign sign;
    private void Awake()
    {
        sign.turnSign += ToggleSignTurned;
    }
    public void ToggleSignTurned()
    {
        flags[0] = !flags[0];
    }
}
