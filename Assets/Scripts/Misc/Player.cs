using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField]
    private int pv;

    [SerializeField]
    private int money;

    public void SetPv(int pv)
    {
        this.pv = pv;
    }

    public void SetMoney(int money)
    {
        this.money = money;
    }

    public int GetPv()
    {
        return pv;
    }

    public int GetMoney()
    {
        return money;
    }
}
