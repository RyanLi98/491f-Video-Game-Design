using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructWall : MonoBehaviour
{
    public int HP = 3;
 
   void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Projectile")
        {
            HP--;
            if (HP <= 0)
            { 
                Destroy(gameObject);
       
            }
        }
    }
}
