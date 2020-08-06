using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HealthInterface
{
    // Start is called before the first frame update    void Initialize(); //Function without any arguments
    void InitializeHealth();
    float health { get; set; } //A variable
    float maxHealth { get; set; }
    float percentageHealth{get; set; }

    void ApplyDamage(int points); //Function with one argument
}
