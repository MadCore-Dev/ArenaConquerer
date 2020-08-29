using UnityEngine;

public class Health : MonoBehaviour
{
    private float remainingHP;
    private float maxHP;

    public float DamageHealth(float amount)
    {
        var hp = remainingHP - amount;
        remainingHP = hp < 0 ? 0f : hp;
        return GetRemainingHealth();
    }

    public float GetRemainingHealth()
    {
        return Mathf.Ceil(remainingHP);
    }

    public void SetTotalHealth(float HPValue)
    {
        maxHP = remainingHP = HPValue;
    }

    public float RestoreHealthFull() // For dummies in practice
    {
        return maxHP;
    }
}
