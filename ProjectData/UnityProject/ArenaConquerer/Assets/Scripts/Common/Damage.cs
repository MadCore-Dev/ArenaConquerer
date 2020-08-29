using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    //Now only keeping Health ref here, in future keep player/LiveEntity ref here so we can use it globally
    //for Player as well as enemy

    public void DamageUnit()
    {
        //This method will take parameter which will have info related to damage and unit which is getting damage
        //Depending on dmg type we can call Other logic here
    }

    /// <summary>
    /// This method will be used for applying damage only once
    /// </summary>
    /// <param name="damageAmount"></param>
    /// <param name="healthRef"></param>
    public void SingleDamageInstance(float damageAmount, Health healthRef)
    {
        /*
         * can add damage type in future which will decide if there is any special VFx or special animation to show 
         */
        healthRef.DamageHealth(damageAmount);
        Debug.Log("Player Damaged");
    }

    /// <summary>
    /// For damages like Posion or bleeding effects
    /// </summary>
    /// <returns></returns>
    public IEnumerator ApplyDamageOverTime()
    {
        yield return null;
    }
}
