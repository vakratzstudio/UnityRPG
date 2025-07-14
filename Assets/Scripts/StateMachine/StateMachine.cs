using UnityEngine;

public class StateMachine
{
   public EntityState currentState { get; private set; }

   public void Initialize(EntityState initialState)
   {
      currentState = initialState;
      currentState.Enter();
   }

   public void ChangeState(EntityState newState)
   {
      // Debug.Log($"Changing state from {currentState.GetType().Name} to {newState.GetType().Name}");
      currentState.Exit();
      currentState = newState;
      currentState.Enter();
   }

   public void UpdateActiveState()
   {
      currentState?.Update();
   }
}
