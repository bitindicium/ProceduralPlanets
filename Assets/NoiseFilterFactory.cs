﻿using System;

public static class NoiseFilterFactory
{
   public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
   {
      switch (settings.filterType)
      {
         case NoiseSettings.FilterType.Simple:
            return new SimpleNoiseFilter(settings.simpleNoiseSettings);
         case NoiseSettings.FilterType.Rigid:
            return new RigidNoiseFilter(settings.rigidNoiseSettings);
         default:
            throw new ArgumentOutOfRangeException();
      }
   }
}