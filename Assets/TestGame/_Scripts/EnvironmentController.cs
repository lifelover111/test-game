using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public float distantCloudsSpeed = 0.5f;
    public float closestCloudsSpeed = 0.75f;
    float frontObjectsSpeed = 2f;

    Transform moon;
    Transform starfield;
    Transform distantClouds;
    Transform closestClouds;
    Transform frontObjects;
    Transform fog;

    Vector3 moonDelta;
    Vector3 starsDelta;
    Vector3 previousCamPos;
    private void Awake()
    {
        moon = transform.Find("Moon");
        starfield = transform.Find("Starfield");
        fog = transform.Find("Fog");
        Transform clouds = transform.Find("Clouds");
        frontObjects = transform.Find("FrontObjects");
        distantClouds = clouds.GetChild(0);
        closestClouds = clouds.GetChild(1);
        moonDelta = moon.position - Camera.main.transform.position;
        starsDelta = starfield.position - Camera.main.transform.position;
        previousCamPos = Camera.main.transform.position;

        Transform grid = transform.Find("Grid");
        Transform trees = transform.Find("Trees");
        Transform graves = transform.Find("Graves");
        Transform grassBushesPumpkins = transform.Find("GrassBushesPumpkins");
        StaticBatchingUtility.Combine(grid.gameObject);
        StaticBatchingUtility.Combine(trees.gameObject);
        StaticBatchingUtility.Combine(graves.gameObject);
        StaticBatchingUtility.Combine(grassBushesPumpkins.gameObject);
    }

    private void Update()
    {
        moon.position = Camera.main.transform.position + moonDelta;
        starfield.position = Camera.main.transform.position + starsDelta;
        for(int i = 0; i < distantClouds.childCount; i++)
        {
            Transform cloud = distantClouds.GetChild(i);
            cloud.position += Vector3.left * (distantCloudsSpeed - (Camera.main.transform.position - previousCamPos).magnitude/Time.deltaTime) * Time.deltaTime;
            if(cloud.position.x <= -50)
                cloud.position = new Vector3(50, cloud.position.y, cloud.position.z);
        }

        for (int i = 0; i < closestClouds.childCount; i++)
        {
            Transform cloud = closestClouds.GetChild(i);
            cloud.position += Vector3.left * (closestCloudsSpeed - (Camera.main.transform.position - previousCamPos).magnitude / Time.deltaTime) * Time.deltaTime;
            if (cloud.position.x <= -50)
                cloud.position = new Vector3(50, cloud.position.y, cloud.position.z);
        }

        for (int i = 0; i < frontObjects.childCount; i++)
        {
            Transform obj = frontObjects.GetChild(i);
            obj.position += Vector3.left * frontObjectsSpeed*(Camera.main.transform.position - previousCamPos).magnitude;
        }

        fog.GetChild(0).position += 0.3f*Vector3.right * Mathf.Sin(Time.time)*Time.deltaTime;
        fog.GetChild(1).position += 0.3f*Vector3.left * Mathf.Sin(Time.time)*Time.deltaTime;


        previousCamPos = Camera.main.transform.position;
    }
}
