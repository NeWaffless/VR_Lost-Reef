using UnityEngine;
using System.Collections.Generic;

public class EnvironmentUpdater : MonoBehaviour
{

    [System.Serializable]
    public struct FogInfo
    {
        public Color colour;
        public float density;
        public float distance;
        public float pollutionVal;
        public FogInfo(Color colour, float density, float distance, float pVal)
        {
            this.colour = colour;
            this.density = density;
            this.distance = distance;
            this.pollutionVal = pVal;
        }
    }


    // object references
    PollutionMeter pMeter;
    float pMax, pLevel;

    bool oilSpillEvent = false;


    // fog
    [Header("Fog")]
    // order these by pollution value threshold
    [SerializeField] FogInfo[] keyFogValues;
    int fogInd = 0;
    FogInfo currFog;
    FogInfo nextFog;
    [SerializeField] Material[] fogAffectedMats;
    [SerializeField] FogInfo oilSpillFog;
    float oilSpillCurr = 0;
    float oilSpillDuration;

    // coral
    [Header("Coral")]
    [SerializeField] GameObject referenceCoral;
    Material coralMat;
    [Rename("Initial coral saturation")]
    [SerializeField] float initSat;
    [SerializeField] int desaturationStart;

    [Header("Camera backdrop")]
    [SerializeField] Material backdrop;

    void Awake()
    {
        pMeter = GetComponent<PollutionMeter>();
        pMax = pMeter.GetPolMax();
        coralMat = referenceCoral.GetComponent<Renderer>().sharedMaterial;
        oilSpillDuration = GetComponent<GameState>().GetResetTime();
    }

    void InitialiseEnvironment()
    {
        // set coral saturation
        coralMat.SetFloat("_Saturation", initSat);

        // set starting fog values
        fogInd = 0;
        if (keyFogValues.Length <= 0) return;
        UpdateFog(keyFogValues[fogInd].colour, keyFogValues[fogInd].density, keyFogValues[fogInd].distance);
        // background colour changed to match scene fog
        if (Camera.main)
        {
            Camera.main.backgroundColor = keyFogValues[fogInd].colour;
        }
    }

    void OnEnable()
    {
        InitialiseEnvironment();
        if (keyFogValues.Length > 0) currFog = keyFogValues[0];
        if (keyFogValues.Length > 1) nextFog = keyFogValues[1];
    }

    void OnDisable()
    {
        InitialiseEnvironment();
    }

    void Update()
    {
        pLevel = pMeter.GetPolLevel();

        if(oilSpillEvent)
        {
            oilSpillCurr += Time.deltaTime;
            UpdateFog(
                Color.Lerp(currFog.colour, oilSpillFog.colour, oilSpillCurr / oilSpillDuration),
                Mathf.Lerp(currFog.density, oilSpillFog.density, oilSpillCurr / oilSpillDuration),
                Mathf.Lerp(currFog.distance, oilSpillFog.distance, oilSpillCurr / oilSpillDuration)
            );
            return;
        }
            coralMat.SetFloat("_Saturation", (1.8f * pMax - 2f * pLevel) / (1.8f*pMax));
        if (fogInd + 1 < keyFogValues.Length && pLevel > keyFogValues[fogInd + 1].pollutionVal)
        {
            fogInd++;
            currFog = keyFogValues[fogInd];
            if (fogInd + 1 < keyFogValues.Length)
            {
                nextFog = keyFogValues[fogInd + 1];
            }
        }

        if (keyFogValues.Length < 2 || fogInd + 1 >= keyFogValues.Length) return;

        float lerpVal = 0;
        if (nextFog.pollutionVal - currFog.pollutionVal != 0)
        {
            lerpVal = Mathf.Clamp((pLevel - currFog.pollutionVal) / (nextFog.pollutionVal - currFog.pollutionVal), 0f, 1f);
        }
        UpdateFog(
            Color.Lerp(currFog.colour, nextFog.colour, lerpVal),
            Mathf.Lerp(currFog.density, nextFog.density, lerpVal),
            Mathf.Lerp(currFog.distance, nextFog.distance, lerpVal)
        );
    }

    void UpdateFog(Color col, float dens, float dist)
    {
        // doesn't work
        backdrop.SetColor("_Tint", col);

        for (int i = 0; i < fogAffectedMats.Length; i++)
        {
            if (fogAffectedMats[i].HasProperty("fogColour"))
            {
                fogAffectedMats[i].SetColor("fogColour", col);
            }
            if (fogAffectedMats[i].HasProperty("fogSmoothness"))
            {
                fogAffectedMats[i].SetFloat("fogSmoothness", dens);
            }
            if (fogAffectedMats[i].HasProperty("fogDistance"))
            {
                fogAffectedMats[i].SetFloat("fogDistance", dist);
            }
        }
    }

    public void SetOilSpillTrue()
    {
        oilSpillEvent = true;
        currFog = keyFogValues[keyFogValues.Length - 1];
        oilSpillCurr = 0;
    }
}
