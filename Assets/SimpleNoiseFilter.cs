using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
   private NoiseSettings.SimpleNoiseSettings settings;
   private Noise noise = new Noise();

   public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings settings)
   {
      this.settings = settings;
   }

   public float Evaluate(Vector3 point)
   {
      var noiseValue = 0f;
      var frequency = settings.baseRoughness;
      var amplitude = 1f;

      for (int i = 0; i < settings.numLayers; i++)
      {
         var v = noise.Evaluate(point * frequency + settings.center);
         noiseValue += (v + 1) * .5f * amplitude;
         frequency *= settings.roughness;
         amplitude *= settings.persistence;
      }

      noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
      return noiseValue * settings.strength;
   }
}
