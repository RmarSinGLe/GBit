using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<Monster> monsters; // ¥Ê¥¢π÷ŒÔ¡–±Ì
    
    public float sanDDL = 50;
    public float vanishSan = 180;
    public Player player;
    private void Start()
    {
        foreach (var monster in monsters)
        {
            monster.gameObject.SetActive(false);
        }
    }

    public void UpdateMonstersState()
    {
        foreach (var monster in monsters)
        {
            if (player.san <= sanDDL)
            {
                monster.gameObject.SetActive(true);
            }
            else if(player.san>vanishSan)
            {
                monster.gameObject.SetActive(false);
            }
        }
    }
}
