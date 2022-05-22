using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PoolableParticles : PooledGameObject
{
   private void OnParticleSystemStopped()
   {
      Dispose();
   }

}
