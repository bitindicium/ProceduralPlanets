using System;
using UnityEngine;

[Serializable]
public class NoiseSettings
{
   public enum FilterType { Simple, Rigid };
   public FilterType filterType;

   public SimpleNoiseSettings simpleNoiseSettings;
   public RigidNoiseSettings rigidNoiseSettings;

   [Serializable]
   public class SimpleNoiseSettings
   {
      public float strength = 1;
      [Range(1, 8)] public int numLayers;
      public float baseRoughness = 1;
      public float roughness = 2;
      public float persistence = .5f;
      public Vector3 center;
      public float minValue;
   }

   [Serializable]
   public class RigidNoiseSettings : SimpleNoiseSettings
   {
      public float weightMultiplier = .8f;
   }
}
