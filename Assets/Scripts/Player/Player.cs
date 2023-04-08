using UnityEngine;

public class Player : MonoBehaviour
{
    public static int pv;
    public static int money;

    public int startPv = 10;
    public int startMoney = 30000000;

    public void Start()
    {
        money = startMoney;
        pv = startPv;
    }
}
