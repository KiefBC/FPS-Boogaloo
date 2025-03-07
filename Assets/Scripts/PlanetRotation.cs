using UnityEngine;

[System.Serializable]
public class PlanetData
{
    public GameObject planet;
    public float rotationSpeed = 10f;
}

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] private PlanetData[] planets;

    // Update is called once per frame
    void Update()
    {
        if (planets == null || planets.Length == 0)
            return;

        foreach (PlanetData planetData in planets)
        {
            if (planetData.planet != null)
            {
                planetData.planet.transform.Rotate(0, 0, planetData.rotationSpeed * Time.deltaTime);
            }
        }
    }
}