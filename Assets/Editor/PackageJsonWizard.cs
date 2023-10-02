using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


public class PackageJsonWizard : EditorWindow
{
    private string outputFolderPath = "Assets";
    private string jsonFileName = "package.json";

    private string packageName = "com.studio23.ss2.inventorysystem";
    private string packageVersion = "1.0.0";
    private string packageDisplayName = "Inventory System";
    private string packageDescription = "Streamline and enhance your game's inventory management with our Unity package. This powerful tool simplifies the creation and management of player inventories, making it easy to handle items, equipment, and resources, allowing you to focus on crafting the ultimate gaming experience.";
    private string unityVersion = "2022.3";
     private string unityRelease = "9f1";
    private string documentationUrl = "https://github.com/Studio-23-xyz/Inventory-System/tree/dev";
    private string changelogUrl = "https://github.com/Studio-23-xyz/Inventory-System/tree/dev";
    private string licensesUrl = "https://github.com/Studio-23-xyz/Inventory-System/tree/dev";
    private string scopedRegistryName = "com.studio23.ss2";
    private string scopedRegistryUrl = "https://studio-23.xyz";
    
    
    private List<string> scopedRegistryScopes = new List<string> { "com.studio23.ss2" };
    private Dictionary<string, string> dependencies = new Dictionary<string, string>()
    {
        {"com.unity.nuget.newtonsoft-json", "3.2.1"},
         
         
    };
    
    private List<string> keywords = new List<string> { "Inventory", "Inventory System", "System" };
    private string authorName = "Studio 23";
    private string authorEmail = "contact@studio-23.xyz";
    private string authorUrl = "https://studio-23.xyz";

    [MenuItem("Custom/Generate package.json")]
    static void CreateWizard()
    {
        PackageJsonWizard window = (PackageJsonWizard)EditorWindow.GetWindow(typeof(PackageJsonWizard), true, "Generate package.json");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Package.json Settings", EditorStyles.boldLabel);

        outputFolderPath = EditorGUILayout.TextField("Output Folder Path:", outputFolderPath);
        jsonFileName = EditorGUILayout.TextField("JSON File Name:", jsonFileName);
        
        packageName = Application.productName;
        packageName = EditorGUILayout.TextField("Name:", packageName);
        
        packageVersion = Application.version;
        packageVersion = EditorGUILayout.TextField("Version:", packageVersion);
        
        packageDisplayName = EditorGUILayout.TextField("Display Name:", packageDisplayName);
        packageDescription = EditorGUILayout.TextField("Description:", packageDescription);
        
        string[] ver = Application.unityVersion.Split('.');
        unityVersion = $"{ver[0]}.{ver[1]}";
        unityVersion = EditorGUILayout.TextField("Unity Version:", unityVersion);
        unityRelease = $"{ver[2]}";
        unityRelease = EditorGUILayout.TextField("Unity Release:", unityRelease);
        documentationUrl = EditorGUILayout.TextField("Documentation URL:", documentationUrl);
        changelogUrl = EditorGUILayout.TextField("Changelog URL:", changelogUrl);
        licensesUrl = EditorGUILayout.TextField("Licenses URL:", licensesUrl);

        GUILayout.Label("Scoped Registries", EditorStyles.boldLabel);
        scopedRegistryName = EditorGUILayout.TextField("Name:", scopedRegistryName);
        scopedRegistryUrl = EditorGUILayout.TextField("URL:", scopedRegistryUrl);

        EditorGUILayout.LabelField("Scopes:");
        for (int i = 0; i < scopedRegistryScopes.Count; i++)
        {
            scopedRegistryScopes[i] = EditorGUILayout.TextField($"Scope {i + 1}:", scopedRegistryScopes[i]);
        }

        GUILayout.Label("Dependencies", EditorStyles.boldLabel);
        
       
        EditorGUILayout.BeginVertical();

        foreach (var keyValuePair in dependencies.ToList())
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Name:");
            string updatedKey = EditorGUILayout.TextField(keyValuePair.Key);
            EditorGUILayout.LabelField("Version:");
            string updatedValue = EditorGUILayout.TextField(keyValuePair.Value);
            EditorGUILayout.EndHorizontal();

            // Update the dictionary with new values
            dependencies[keyValuePair.Key] = updatedKey;
            dependencies[updatedKey] = updatedValue;
        }

        if (GUILayout.Button("Add Dependency"))
        {
            // Adding a new dependency with empty values
            dependencies.Add(string.Empty, string.Empty);
        }

        EditorGUILayout.EndVertical();
        
        
        
       

        GUILayout.Label("Keywords", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < keywords.Count; i++)
        {
            keywords[i] = EditorGUILayout.TextField($"Keyword {i + 1}:", keywords[i]);
        }
        if (GUILayout.Button("Add Keyword"))
        {
            keywords.Add("");
        }
        EditorGUILayout.EndVertical();

        GUILayout.Label("Author Information", EditorStyles.boldLabel);
        authorName = Application.companyName;
        authorName = EditorGUILayout.TextField("Name:", authorName);
        authorEmail = EditorGUILayout.TextField("Email:", authorEmail);
        authorUrl = EditorGUILayout.TextField("URL:", authorUrl);

        if (GUILayout.Button("Generate package.json"))
        {
            GeneratePackageJson();
        }
    }

    private void GeneratePackageJson()
    {
        string outputPath = Path.Combine(Application.dataPath, outputFolderPath);
        string jsonFilePath = Path.Combine(outputPath, jsonFileName);

        // Create a JSON template
        var jsonTemplate = new
        {
            name = packageName,
            version = packageVersion,
            displayName = packageDisplayName,
            description = packageDescription,
            unity = unityVersion,
            unityRelease = unityRelease,
            documentationUrl = documentationUrl,
            changelogUrl = changelogUrl,
            licensesUrl = licensesUrl,
            scopedRegistries = new List<object>
            {
                new
                {
                    name = scopedRegistryName,
                    url = scopedRegistryUrl,
                    scopes = scopedRegistryScopes
                }
            },
            dependencies = dependencies,
            keywords = keywords,
            author = new
            {
                name = authorName,
                email = authorEmail,
                url = authorUrl
            }
        };

        // Serialize the JSON template and write it to the file
        string jsonString = JsonConvert.SerializeObject(jsonTemplate, Formatting.Indented);
        File.WriteAllText(jsonFilePath, jsonString);

        AssetDatabase.Refresh();
        Debug.Log($"package.json file generated at {jsonFilePath}");
    }
}