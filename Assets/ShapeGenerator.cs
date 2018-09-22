﻿using UnityEngine;

public class ShapeGenerator
{
   private ShapeSettings settings;
   private NoiseFilter[] noiseFilters;

   public ShapeGenerator(ShapeSettings settings)
   {
      this.settings = settings;
      this.noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
      for (int i = 0; i < noiseFilters.Length; i++)
      {
         noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
      }
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

      return pointOnUnitSphere * settings.planetRadius * (1 + elevation);
   }
}
