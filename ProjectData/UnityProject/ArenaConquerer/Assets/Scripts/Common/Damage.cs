using UnityEngine;

public class Damage : MonoBehaviour
{
    //Now only keeping Health ref here, in future keep player/LiveEntity ref here so we can use it globally
    //for Player as well as enemy
    public void DamageToPlayer(float damageAmount, Health healthRef)
    {
        /*
         * can add damage type in future which will decide if there is any special VFx or special animation to show 
         */
        healthRef.DamageHealth(damageAmount);
        Debug.Log("Player Damaged");
    }
}
