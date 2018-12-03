using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if(collidedWith.tag == "Wall" || collidedWith.tag == "Slab")
        {
            MissionDemolition.CollisionUp();

        }
    }
}
