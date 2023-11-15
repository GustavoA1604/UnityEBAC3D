using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager fsm = (GameManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("StateMachine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());
        }

        if (fsm.stateMachine.dictionaryState != null)
        {
            showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

            if (showFoldout)
            {
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var vals = fsm.stateMachine.dictionaryState.Values.ToArray();


                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }
    }
}
