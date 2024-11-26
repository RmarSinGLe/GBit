using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<Monster> monsters; // ¥Ê¥¢π÷ŒÔ¡–±Ì
    
    public float sanDDL = 20;
    public float vanishSan = 80;
    public Player player;
    public bool monsterWindow=false;
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
            if (monsterWindow||player.san <= sanDDL)
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
