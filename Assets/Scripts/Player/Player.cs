using UnityEngine;

public class Player : MonoBehaviour
{
    public static int pv;
    public static int money;
    public int startMoney = 400;

    public static void Damage(int damage)
    {
        pv -= damage;
    }

    public void Start()
    {
        money = startMoney;
    }


}
