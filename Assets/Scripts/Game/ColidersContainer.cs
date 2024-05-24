using System.Collections.Generic;
using UnityEngine;

public class ColidersContainer : MonoBehaviour
{
   [SerializeField] private List<Collider> colliders;

   public List<Collider> Colliders
   {
      get => colliders;
      set => colliders = value;
   }
   
   public void  SetAllTriggers() => SetState(true);
   public void  SetAllColliders() => SetState(false);
   
   
   private void SetState(bool state) => colliders.ForEach(x => x.isTrigger = state);
}
