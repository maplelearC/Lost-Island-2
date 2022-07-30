using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
   [SceneName]public string sceneForm;
   [SceneName]public string sceneTogo;

   public void TeleportToScene()
   {
      TransitionManager.Instance.Transition(sceneForm,sceneTogo);
   }
}
