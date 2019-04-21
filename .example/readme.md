# **TypedTree-Generator** Example.

Simple example showing turret-ai implemented using a [**Behaviour tree**](https://en.wikipedia.org/wiki/Behavior_tree)

Tested to work fine on:
* Standalone MacOs
* iOS
* Android
* WebGL

View the WebGL example running [**here**](https://bastian.tech/unity/typedtree-generator-example/).

*NOTE: The behaviour-tree classes are purely as an example, the library doesn't place any restrictions on how you structure your tree.*

The json in `data/turretbrain.treescheme.json` can be loaded in the [**TypedTree Editor**](https://bastian.tech/tree)

![Example image](https://bastian.tech/media/typedtree-generator-unity.example.png)

After loading the scheme you can create a new tree in the editor or load the `data/turretbrain.tree.json` for editing.

At runtime the tree structure can be loaded with a json orm (in this case [**Newtonsoft.Json**](https://www.newtonsoft.com/json))

Loading the tree using `Newtonsoft.Json`:
```c#
public static T Deserialize<T>(string json) =>
    JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto,
        SerializationBinder = new SerializationBinder()
    });

private class SerializationBinder : ISerializationBinder
{
    public void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
        assemblyName = null;
        typeName = serializedType.FullName;
    }

    public Type BindToType(string assemblyName, string typeName) => Type.GetType(typeName);
}
```
Reason why we need the custom `ISerializationBinder` is that by default `Newtonsoft.Json` wants assembly data along with the types.
