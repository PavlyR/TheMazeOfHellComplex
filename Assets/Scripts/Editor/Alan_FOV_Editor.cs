using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Alan_Mov))]
public class FieldOfViewEditor : Editor

///I don't understand editors so using the ones from the following tutorial 
/// https://youtu.be/j1-OyLo77ss?si=SqiE6FC8-PJ0qvR5
{
    private void OnSceneGUI()
    {
        Alan_Mov fov = (Alan_Mov)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        if (fov.spotted)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerOb.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

