using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
   public ItemName requireItem;
   public bool isDone;

   public void CheckItem(ItemName itemName)
   {
      if (itemName == requireItem && !isDone)
      {
         isDone = true;
         //适用这个物品
         OnClickedAction();
         EventHandler.CallItemUsedEvent(itemName);
      }
   }
   
   /// <summary>
   /// 默认是正确物品大的情况执行
   /// </summary>
   protected virtual void OnClickedAction()
   {
      
   }

   public virtual void EmptyClicked()
   {
      Debug.Log("空点");
   }
}
