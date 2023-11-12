using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    EmptyView=1,
    HomeView=2,
    IngameView=3,
    ShopView=4,
   // DeckView=5
}
public class ViewParam
{

}
public class ViewConfig 
{
    public static ViewIndex[] viewIndices = {
        ViewIndex.EmptyView, 
        ViewIndex.HomeView,
        ViewIndex.IngameView,
        ViewIndex.ShopView,
        //ViewIndex.DeckView
    };
}
 