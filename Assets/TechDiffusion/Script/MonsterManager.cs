using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<Monster> monsters; // �洢�����б�
    
    public float sanDDL = 50;
    public float vanishSan = 180;
    public Player player;
    public Windows windows;
    private void Start()
    {
        foreach (var monster in monsters)
        {
            monster.gameObject.SetActive(false);
        }
    }

    public void UpdateMonstersState()
    {
        if (windows==null)
        {
            return;            
        }
        foreach (var monster in monsters)
        {
            if (player.san <= sanDDL)
            {
                monster.gameObject.SetActive(true);
            }
            if(windows.isMonsterTrriger)
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
