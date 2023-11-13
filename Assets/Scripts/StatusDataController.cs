using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDataController : MonoBehaviour
{
    //player status
    public static int currentHealth;
    public static float shootCooldown;
    public static float maxImpulse;
    public static float increaseImpulse;
    public static float decreaseImpulse;

    //score and XP status
    public static int currentScore;
    public static int currentXp;
    public static int XPLevel;
    public static int maxXp;

    //Shield Status
    public static float shieldActiveTime;
    public static float shieldRechargeTime;

    //Upgrades Status
    public static List<Upgrade> AllUpgrades;
}
