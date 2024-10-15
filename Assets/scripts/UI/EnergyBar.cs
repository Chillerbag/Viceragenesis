using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    [SerializeField] private GameObject[] particles;
    [Range(0, 16)][SerializeField] public int energyLevel;
    [ColorUsageAttribute(false, true), SerializeField] private Color activeColor;
    [ColorUsageAttribute(false, true), SerializeField] private Color emptyColor;
    private int prevEnergyLevel;
    private
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject particle in particles)
        {
            particle.GetComponent<Renderer>().material = Instantiate<Material>(particle.GetComponent<Renderer>().material);
            //material = particle.GetComponent<Renderer>().material;
        }
        prevEnergyLevel = energyLevel;

    }

    // Update is called once per frame
    void Update()
    {
        if (energyLevel != prevEnergyLevel)
        {
            for (int i = 0; i < 16; i++)
            {
                Material material = particles[i].GetComponent<Renderer>().material;
                if (i >= energyLevel)
                {
                    material.SetColor("_EmissionColor", emptyColor);
                }
                else
                {
                    material.SetColor("_EmissionColor", activeColor);
                }
            }
        }
        prevEnergyLevel = energyLevel;

    }


}
