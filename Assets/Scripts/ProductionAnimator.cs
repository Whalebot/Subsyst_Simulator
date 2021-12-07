using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionAnimator : MonoBehaviour
{
    Animator anim;
    public List<ProductionSO> production;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        FoodManager.Instance.productionEvent += CheckProduction;
    }

    // Update is called once per frame
    void CheckProduction(ProductionSO p)
    {
        if (production.Contains(p))
            anim.SetTrigger("Production");
    }
}
