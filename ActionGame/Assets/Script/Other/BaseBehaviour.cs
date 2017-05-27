using UnityEngine;
using System.Collections;

/// <summary>
/// MonoBehaviour継承基底クラス
/// </summary>
public class BaseBehaviour : MonoBehaviour
{
    public virtual void BaseStart() { }
    public virtual void BaseFixedUpdate() { }
    public virtual void BaseUpdate() { }
    public virtual void BaseLateUpdate() { }
}
