using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ColorsList")]
public class ColorsListSO : ScriptableObject
{
    public List<ColorSO> list;
    public ColorSO rainbowColor;
}
