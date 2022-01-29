using UnityEngine;

public class PlayerEvents : ScriptableObject
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;

    public static void RaisePlayerDeath()
    {
        OnPlayerDeath();
    }
}
