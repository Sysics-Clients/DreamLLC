using UnityEngine;
[CreateAssetMenu(fileName ="New Enemy", menuName ="Enemies")]
public class EnemyItem : ScriptableObject
{
    public string enemyDescription;
    public string enemyName;
    public float health;
    public float shield;
    public float damage;
    public Sprite EnemySprite;
    public float runSpeed;
    public float walkSpeed;
    public float FovRadius;
    [Range(0, 360)]
    public float FovAngle;

}
