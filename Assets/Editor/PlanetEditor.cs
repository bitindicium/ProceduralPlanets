using System;
using UnityEditor;
using Object = UnityEngine.Object;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
   private Planet planet;

   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();

      DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout);
      DrawSettingsEditor(planet.colorSettings, planet.OnColorSettingsUpdated, ref planet.colorSettingsFoldout);
   }

   void DrawSettingsEditor(Object settings, Action onSettingsUpdated, ref bool foldout)
   {
      foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
      using (var check = new EditorGUI.ChangeCheckScope())
      {
         if (foldout)
         {
            var editor = CreateEditor(settings);
            editor.OnInspectorGUI();

            if (check.changed)
            {
               onSettingsUpdated?.Invoke();
            }
         }
      }
   }

   private void OnEnable()
   {
      planet = target as Planet;
   }
}
