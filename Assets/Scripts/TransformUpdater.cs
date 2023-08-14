using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUpdater : MonoBehaviour
{
    public Transform obje;

    void Updateter( TransformProperty.Transforms newTransfoms)
    {
        
        obje.position = new Vector3(newTransfoms.pozitions.x, newTransfoms.pozitions.y, newTransfoms.pozitions.z);
        obje.rotation = Quaternion.Euler(newTransfoms.rotations.x, newTransfoms.rotations.y, newTransfoms.rotations.z);
    }


}

public class TransformProperty
{


    public class Pozitions
    {
        public float x, y, z;
    }

    public class Rotations
    {
        public float x, y, z;
    }

    public class Transforms
    {
        public Pozitions pozitions;
        public Rotations rotations;
    }

    


}

