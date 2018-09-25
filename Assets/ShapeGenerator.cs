using UnityEngine;

public class ShapeGenerator
{
   private ShapeSettings settings;
   private INoiseFilter[] noiseFilters;
   public MinMax elevationMinMax;

   public void UpdateSettings(ShapeSettings settings)
   {
      this.settings = settings;
      this.noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
      for (int i = 0; i < noiseFilters.Length; i++)
      {
         noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
      }
      
      elevationMinMax = new MinMax();
   }

   public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
   {
      var firstLayerValue = 0f;
      var elevation = 0f;

      if (noiseFilters.Length > 0)
      {
         firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
         if (settings.noiseLayers[0].enabled)
         {
            elevation = firstLayerValue;
         }
      }
      
      for (int i = 1; i < noiseFilters.Length; i++)
      {
         if (!settings.noiseLayers[i].enabled)
            continue;

         var mask = (settings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1f);
         elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
      }

      elevation = settings.planetRadius * (1 + elevation);
      
      elevationMinMax.AddValue(elevation);

      return pointOnUnitSphere * elevation;
   }
}
