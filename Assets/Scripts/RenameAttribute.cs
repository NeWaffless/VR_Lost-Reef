using UnityEngine;

// script borrowed from https://answers.unity.com/questions/1487864/change-a-variable-name-only-on-the-inspector.html
public class RenameAttribute : PropertyAttribute
{
    public string NewName { get ; private set; }
    
    public RenameAttribute( string name )
    {
        NewName = name ;
    }
}
