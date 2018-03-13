using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLimit : MonoBehaviour
{

    public List<Vector3> Path = new List<Vector3>();

    protected virtual void OnDrawGizmos()
    {

#if UNITY_EDITOR


        for (int i = 0; i < Path.Count; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Path[i], 0.2f);

        }
#endif
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(MoveLimit))]
[InitializeOnLoad]
public class PathMovementEditor : Editor
{
    public void OnSceneGUI()
    {
        MoveLimit t = (target as MoveLimit);
        for (int i = 0; i < t.Path.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newPoint = UnityEditor.Handles.FreeMoveHandle(t.Path[i], Quaternion.identity, .5f, new Vector3(.25f, .25f, .25f), Handles.CircleHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                t.Path[i] = newPoint;
            }
        }
    }
}
#endif