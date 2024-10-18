using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingLasers : MonoBehaviour
{
    public GameObject[] lasers;
    // Start is called before the first frame update
    private Vector3[] laserPositions;
    private Quaternion[] laserRotations;

    void Awake()
    {
        laserPositions = new Vector3[lasers.Length];
        laserRotations = new Quaternion[lasers.Length];
        // store position of the lasers
        for (int i = 0; i < lasers.Length; i++) {
            laserPositions[i] = lasers[i].transform.position;  
            laserRotations[i] = lasers[i].transform.rotation;

        }
        
    }
    void OnEnable()
    {
        for (int i = 0; i < lasers.Length; i++) {
            lasers[i].transform.position = laserPositions[i];
            lasers[i].transform.rotation =  laserRotations[i];
        }

        for (int i = 0; i < lasers.Length; i++) {
            StartCoroutine(MoveLaser(lasers[i], i));
        }
    }

    private IEnumerator MoveLaser(GameObject laser, int index) {
        // gradually rotate the laser to 135 degrees on y
        while (laser.transform.rotation.eulerAngles.z < 90) {
            laser.transform.rotation = Quaternion.Euler(laser.transform.rotation.eulerAngles.x, laser.transform.rotation.eulerAngles.y, laser.transform.rotation.eulerAngles.z + 1);
            yield return new WaitForSeconds(0.03f);
            laser.transform.position = new Vector3(laser.transform.position.x, laser.transform.position.y - 1.5f, laser.transform.position.z);
        }
        yield return new WaitForSeconds(0.5f);
        // reset the laser position
        laser.transform.position = laserPositions[index];
        gameObject.SetActive(false);
    }
}   
