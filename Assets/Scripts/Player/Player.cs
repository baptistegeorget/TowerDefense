using UnityEngine;

public class Player : MonoBehaviour
{
    private int pv;
    private int money;
    
    public Player(int pv, int money)
    {
        this.pv = pv;
        this.money = money;
    }
}
