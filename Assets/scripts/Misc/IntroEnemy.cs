using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject introEnemy;
     void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void OnEnable()
    {
        introEnemy.SetActive(true);
    }

    void OnDisable() {
        introEnemy.SetActive(false);
    }

}
