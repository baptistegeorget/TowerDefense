using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField]
    private int boulepicCount;

    [SerializeField]
    private int serpentCount;

    [SerializeField]
    private int tankCount;

    [SerializeField]
    private int batCount;

    [SerializeField]
    private int healerCount;

    [SerializeField]
    private int ghostCount;

    [SerializeField]
    private int dragonCount;

    [SerializeField]
    private int rainetteCount;

    [SerializeField]
    private int invocateurCount;

    [SerializeField]
    private int ninjaCount;

    [SerializeField]
    private int centaureCount;

    [SerializeField]
    private int lapinouCount;

    [SerializeField]
    private int chamanCount;

    [SerializeField]
    private int slimeCount;

    [SerializeField]
    private int victimeCount;

    [SerializeField]
    private float rate;

    public int GetBoulepicCount()
    {
        return boulepicCount;
    }

    public int GetSerpentCount()
    {
        return serpentCount;
    }

    public int GetTankCount()
    {
        return tankCount;
    }

    public int GetBatCount()
    {
        return batCount;
    }

    public int GetHealerCount()
    {
        return healerCount;
    }

    public int GetGhostCount()
    {
        return ghostCount;
    }

    public int GetDragonCount()
    {
        return dragonCount;
    }

    public int GetRainetteCount()
    {
        return rainetteCount;
    }

    public int GetInvocateurCount()
    {
        return invocateurCount;
    }

    public int GetNinjaCount()
    {
        return ninjaCount;
    }

    public int GetCentaureCount()
    {
        return centaureCount;
    }

    public int GetLapinouCount()
    {
        return lapinouCount;
    }

    public int GetChamanCount()
    {
        return chamanCount;
    }

    public int GetSlimeCount()
    {
        return slimeCount;
    }

    public int GetVictimeCount()
    {
        return victimeCount;
    }

    public float GetRate()
    {
        return rate;
    }
}
