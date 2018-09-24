using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
   private NoiseSettings.RigidNoiseSettings settings;
   private Noise noise = new Noise();

   public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
   {
      this.settings = settings;
   }

   public float Evaluate(Vector3 point)
   {
      var noiseValue = 0f;
      var frequency = settings.baseRoughness;
      var amplitude = 1f;
      var weight = 1f;

      for (int i = 0; i < settings.numLayers; i++)
      {
         var v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.center));
         v *= v;
         v *= weight;
         weight = Mathf.Clamp01(v * settings.weightMultiplier);

         noiseValue += v * amplitude;
         frequency *= settings.roughness;
         amplitude *= settings.persistence;
      }

      noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
      return noiseValue * settings.strength;
   }
}
