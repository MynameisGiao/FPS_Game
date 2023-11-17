using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ViewIndex
{
    EmptyView=1,
    HomeView=2,
    IngameView=3,
    //ShopView=4
}
public class ViewParam
{

}
public class ViewConfig
{
    public static ViewIndex[] viewIndies =
    {
        ViewIndex.EmptyView,
        ViewIndex.HomeView,
        ViewIndex.IngameView
    };
}

