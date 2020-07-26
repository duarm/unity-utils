//Modified script made by David Robins

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Otker.Helpers
{
    public class PivotHelper : MonoBehaviour
    {
        
        [MenuItem ("Otker/Pivots/Upper-Upper")]
        static void SetPivotsUU () => RoundPivot (true, true);

        [MenuItem ("Otker/Pivots/Upper-Bottom")]
        static void SetPivotsUB () => RoundPivot (true, false);

        [MenuItem ("Otker/Pivots/Bottom-Upper")]
        static void SetPivotsBU () => RoundPivot (true, true);

        [MenuItem ("Otker/Pivots/Bottom-Bottom")]
        static void SetPivotsBB () => RoundPivot (false, false);


        [MenuItem("Otker/Pivots/Propagate First Pivot")]
        static void SetPivots()
        {
            Object[] textures = GetSelectedTextures();
            Selection.objects = new Object[0];
            foreach (Texture2D texture in textures)
            {
                string path = AssetDatabase.GetAssetPath(texture);
                TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
                ti.isReadable = true;
                List<SpriteMetaData> newData = new List<SpriteMetaData>();
                for (int i = 0; i < ti.spritesheet.Length; i++)
                {
                    SpriteMetaData d = ti.spritesheet[i];
                    d.alignment = (int)SpriteAlignment.Custom;
                    d.pivot = ti.spritesheet[0].pivot;
                    newData.Add(d);
                }
                ti.spritesheet = newData.ToArray();
                EditorUtility.SetDirty(ti);
                ti.SaveAndReimport();
            }
        }

        static void RoundPivot (bool upperX, bool upperY)
        {
            var textures = GetSelectedTextures ();
            Selection.objects = new Object[0];
            foreach (Texture2D texture in textures)
            {
                string path = AssetDatabase.GetAssetPath (texture);
                TextureImporter ti = AssetImporter.GetAtPath (path) as TextureImporter;
                ti.isReadable = true;
                List<SpriteMetaData> newData = new List<SpriteMetaData> ();
                var ppu = 1 / ti.spritePixelsPerUnit;
                for (int i = 0; i < ti.spritesheet.Length; i++)
                {
                    SpriteMetaData d = ti.spritesheet[i];
                    d.alignment = (int) SpriteAlignment.Custom;
                    //pivot from 0-1 (normalized) to 0-width (pixel)
                    var pixelPivot = new Vector2 ((d.pivot.x * d.rect.width), (d.pivot.y * d.rect.height));
                    //ceil/floor the values
                    pixelPivot = new Vector2 (upperX ? Mathf.Ceil (pixelPivot.x) : Mathf.Floor (pixelPivot.x),
                                              upperY ? Mathf.Ceil (pixelPivot.y) : Mathf.Floor (pixelPivot.y));
                    //transform back to 0-1
                    var xR = Mathf.Lerp (0, 1, Mathf.InverseLerp (0, d.rect.width, pixelPivot.x));
                    var yR = Mathf.Lerp (0, 1, Mathf.InverseLerp (0, d.rect.height, pixelPivot.y));
                    d.pivot = new Vector2 (xR, yR);
                    newData.Add (d);
                }
                //refresh the assets
                ti.spritesheet = newData.ToArray ();
                EditorUtility.SetDirty (ti);
                ti.SaveAndReimport ();
            }
        }
        
        static Object[] GetSelectedTextures()
        {
            return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        }
    }
}

/*
 using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Otker.Helpers
{
   public class MenuItems : MonoBehaviour {

       [MenuItem("Sprites/Set Pivot(s)")]
       static void SetPivots()
       {
           Object[] textures = GetSelectedTextures();
           Selection.objects = new Object[0];
           foreach (Texture2D texture in textures)
           {
               string path = AssetDatabase.GetAssetPath(texture);
               TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
               ti.isReadable = true;
               List<SpriteMetaData> newData = new List<SpriteMetaData>();
               for (int i = 0; i < ti.spritesheet.Length; i++)
               {
                   SpriteMetaData d = ti.spritesheet[i];
                   d.alignment = 9;
                   d.pivot = ti.spritesheet[0].pivot;
                   newData.Add(d);
               }
               var arrayData = newData.ToArray();
               ti.spritesheet = arrayData;
               AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
           }
       }

       static Object[] GetSelectedTextures()
       {
           return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
       }
   }
}
*/
