using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    //Setting up SerializeField so I can edit these values in the Unity app.
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        //I have a heart sprite with 10 hearts. I use .fillAmount to hide the hearts not in use my putting my current Health and dividing
        //it by 10 so It will only show the amount of hearts the player currently has
        //Reference Video where I learned this. Unity 2D Platformer for Complete Beginners - #8 IFRAMES by Pandemonium. LINK: https://www.youtube.com/watch?v=YSzmCf_L2cE&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=8
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        //I constantly update this so if the player loses/gains health the health bar will change in accordance.
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
    //


}
