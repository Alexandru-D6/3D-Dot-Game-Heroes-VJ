using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TexturesObject", order = 1)]
public class TexturesObject : ScriptableObject {

    public Material material;
    public Mesh mesh;

}
