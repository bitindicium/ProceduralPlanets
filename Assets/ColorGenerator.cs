using UnityEngine;

public class ColorGenerator
{
   private ColorSettings settings;
   private Texture2D texture;
   private const int textureResolution = 50;

   public void UpdateSettings(ColorSettings settings)
   {
      this.settings = settings;

      if (texture == null)
      {
         texture = new Texture2D(textureResolution, 1);
      }
   }

   public void UpdateElevation(MinMax elevation)
   {
      settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevation.Min, elevation.Max));
   }

   public void UpdateColors()
   {
      var colors = new Color[textureResolution];
      for (int i = 0; i < textureResolution; i++)
      {
         colors[i] = settings.planetColor.Evaluate(i / (textureResolution - 1f));
      }
      
      texture.SetPixels(colors);
      texture.Apply();
      settings.planetMaterial.SetTexture("_texture", texture);
   }
}
