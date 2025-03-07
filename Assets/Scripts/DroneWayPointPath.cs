using UnityEngine;

public class DroneWayPointPath : MonoBehaviour
{
    public Transform GetWayPoint(int index)
    {
        return transform.GetChild(index).transform;
    }

    public int GetWayPointCount()
    {
        return transform.childCount;
    }
}
