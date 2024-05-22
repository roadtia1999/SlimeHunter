using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item : MonoBehaviour
{
    
}

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObject/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public Sprite sprite;
    public string tag;
    public string itemName;
    public GameObject subWeaponPrefab;
    public string[] levelUpDescription;
    public int[] enhancementTag;
    public float enhancementValue;
}
