using UnityEngine;

public class TerrainFace
{
   private ShapeGenerator shapeGenerator;
   private Mesh mesh;
   private int resolution;
   private Vector3 localUp;
   private Vector3 axisA, axisB;

   public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
   {
      this.shapeGenerator = shapeGenerator;
      this.mesh = mesh;
      this.resolution = resolution;
      this.localUp = localUp;
      
      axisA = new Vector3(localUp.y, localUp.z, localUp.x);
      axisB = Vector3.Cross(localUp, axisA);
   }

   public void ConstructMesh()
   {
      var vertices = new Vector3[resolution * resolution];
      var triangles = new int[(resolution - 1) * (resolution - 1) * 2 * 3];
      var triangleIndex = 0;

      for (int y = 0; y < resolution; ++y)
      {
         for (int x = 0; x < resolution; ++x)
         {
            var i = x + y * resolution;
            var percent = new Vector2(x, y) / (resolution - 1);
            var pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
            var pointOnUnitSpere = pointOnUnitCube.normalized;

            vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSpere);

            if (x != resolution - 1 && y != resolution - 1)
            {
               triangles[triangleIndex++] = i;
               triangles[triangleIndex++] = i + resolution + 1;
               triangles[triangleIndex++] = i + resolution;
               
               triangles[triangleIndex++] = i;
               triangles[triangleIndex++] = i + 1;
               triangles[triangleIndex++] = i + resolution + 1;
            }
         }
      }

      mesh.Clear();
      mesh.vertices = vertices;
      mesh.triangles = triangles;
      mesh.RecalculateNormals();
   }
}
