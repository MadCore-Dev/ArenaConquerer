using UnityEngine;

public class Health : MonoBehaviour
{
    private float remainingHP;
    private float maxHP;

    public float DamageHealth(float damageAmount)
    {
        var hp = remainingHP - damageAmount;
        remainingHP = hp < 0 ? 0f : hp;
        return GetRemainingHealth();
    }

    public float GetRemainingHealth()
    {
        return Mathf.Ceil(remainingHP);
    }

    public float SetTotalHealth(float HPValue)
    {
        return maxHP = remainingHP = HPValue;
    }

    public float RestoreHealthFull() // For dummies in practice
    {
        return maxHP;
    }
}
