using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSmallOneAi : ForestHerbivoreAi
{
    // private float safeDistance; // distance that herbivore goes to before returning to path
    // private float restTime; // time to wait at safe distance before resuming path
    // private float escapeSpeedMultiplier; // speed multiplier for npc while escaping

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // THESE ARE CUSTOM
        charType = (int)PlayerProperties.CharacterType.ANIMAL;
        speed = 100f;
        safeDistance = 3f; // must be larger than detection on herbivore
        restTime = 0.5f;
        escapeSpeedMultiplier = 1.5f;
        // TODO: Health and other variables
    }
}
