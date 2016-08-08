using UnityEngine;
using System.Collections;
using Poi;
using UnityEngine.UI;
using System;

public abstract class UI_Base : MonoBehaviour,iUI
{
    [SerializeField]
    private int _ID;
    [SerializeField]
    private string _Name;

    private int defaultOrder;
    private static UI_Base FrontUI { get; set; }
    [SerializeField]
    private GameObject touchMask;
    
    public int ID
    {
        get
        {
            return _ID;
        }

        set
        {
            _ID = value;
        }
    }

    public string Name
    {
        get
        {
            return _Name;
        }

        set
        {
            _Name = value;
        }
    }

    public int DefaultOrder
    {
        get
        {
            return defaultOrder;
        }

        set
        {
            defaultOrder = value;
        }
    }

    [SerializeField]
    private Only isOnly = Only.Only;
    public Only IsOnly
    {
        get
        {
            return isOnly;
        }
    }

    /// <summary>
    /// 设置UI像机
    /// </summary>
    protected void SetUICamera<T>(T _UIChild) where T:iUI
    {
        if (IsOnly == Only.Only)
        {
            foreach (var item in UIManager.UIList)
            {
                if (item.GetType() == typeof(T) && !item.Equals(this))
                {
#if UNITY_EDITOR || Development
                    Debuger.Log("销毁一个重复的UI："+ gameObject.ToString());
#endif
                    Destroy(gameObject);
                    item.Use();
                }
            }
        }

        UIManager.AddUI(_UIChild);
        SetParent();
    }

    /// <summary>
    /// 设定父子关系
    /// </summary>
    private void SetParent()
    {
        transform.SetParent(UIManager.Depth[ID]);
    }

    [Obsolete("",true)]
    private void Resize()
    {
        var _tr = GetComponent<RectTransform>();
        _tr.localScale = Vector3.one;
        _tr.anchorMin = Vector2.zero;
        _tr.anchorMax = Vector2.one;
        _tr.localPosition = Vector3.zero;
        _tr.offsetMin = Vector2.zero;
        _tr.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// false 禁用；true 启用；
    /// </summary>
    /// <param name="oi"></param>
    public void LockOrOpen(bool oi)
    {
        if (touchMask)
        {
            touchMask.SetActive(!oi);
        }
    }

    public virtual void UseDone()
    {
        gameObject.SetActive(false);
    }

    public virtual void Test()
    {

#if UNITY_EDITOR || Development 
        Debuger.Log("已响应");
#endif
    }


    public void SetFront()
    {
        if (FrontUI == this)
        {
            return;
        }

        if (FrontUI != null)
        {
            FrontUI.Redepth();
        }

        transform.SetParent(UIManager.Depth.Front);

        FrontUI = this;
    }

    private void Redepth()
    {
        SetParent();
    }

    public abstract void Help();
    public abstract void Use(Context _con);
    public virtual void Use()
    {
        gameObject.SetActive(true);
    }

    public void ReUse()
    {
        UseDone();
        Use();
    }
}
