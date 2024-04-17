#region Assembly Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Assembly-CSharp.dll
// Decompiled with ICSharpCode.Decompiler 8.1.1.7464
#endregion

using Photon.Pun;
using UnityEngine; // Ensure you're using UnityEngine for Quaternion
using Zorro.Core.CLI;

public class MonsterSpawner : MonoBehaviour
{
    public string monster;

    [ConsoleCommand]
    public static void SpawnMonster(string monster)
    {
        RaycastHit raycastHit = HelperFunctions.LineCheck(MainCamera.instance.transform.position, MainCamera.instance.transform.position + MainCamera.instance.transform.forward * 30f, HelperFunctions.LayerType.TerrainProp);
        Vector3 vector = MainCamera.instance.transform.position + MainCamera.instance.transform.forward * 60f;
        if (raycastHit.collider != null)
        {
            vector = raycastHit.point;
        }

        vector = HelperFunctions.GetGroundPos(vector + Vector3.up * 1f, HelperFunctions.LayerType.TerrainProp);
        PhotonNetwork.Instantiate(monster, vector, Quaternion.identity, 0); // Corrected quaternion.identity to Quaternion.identity
    }

    public static GameObject SpawnMonster2(string monster, Vector3 position)
    {
        Vector3 groundPos = HelperFunctions.GetGroundPos(position + Vector3.up * 1f, HelperFunctions.LayerType.TerrainProp);
        return PhotonNetwork.Instantiate(monster, groundPos, Quaternion.identity, 0); // Corrected here as well
    }




}

#if false // Decompilation log
'220' items in cache
------------------
Resolve: 'netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
WARN: Version mismatch. Expected: '2.1.0.0', Got: '2.0.0.0'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\Facades\netstandard.dll'
------------------
Resolve: 'UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.CoreModule.dll'
------------------
Resolve: 'PhotonUnityNetworking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'PhotonUnityNetworking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\PhotonUnityNetworking.dll'
------------------
Resolve: 'Zorro.Core.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Zorro.Core.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Zorro.Core.Runtime.dll'
------------------
Resolve: 'UnityEngine.AudioModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.AudioModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.AudioModule.dll'
------------------
Resolve: 'SteamAudioUnity, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'SteamAudioUnity, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'UnityEngine.UnityWebRequestModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.UnityWebRequestModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.UnityWebRequestModule.dll'
------------------
Resolve: 'Zorro.Recorder, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Zorro.Recorder, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Zorro.Recorder.dll'
------------------
Resolve: 'Zorro.PhotonUtility, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Zorro.PhotonUtility, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Zorro.PhotonUtility.dll'
------------------
Resolve: 'Unity.TextMeshPro, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.TextMeshPro, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.TextMeshPro.dll'
------------------
Resolve: 'UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.UI.dll'
------------------
Resolve: 'UnityEngine.UIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.UIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.UIModule.dll'
------------------
Resolve: 'UnityEngine.PhysicsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.PhysicsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.PhysicsModule.dll'
------------------
Resolve: 'Photon3Unity3D, Version=4.1.7.1, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Photon3Unity3D, Version=4.1.2.4, Culture=neutral, PublicKeyToken=null'
WARN: Version mismatch. Expected: '4.1.7.1', Got: '4.1.2.4'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Photon3Unity3D.dll'
------------------
Resolve: 'com.rlabrecque.steamworks.net, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'com.rlabrecque.steamworks.net, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\com.rlabrecque.steamworks.net.dll'
------------------
Resolve: 'UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.UIElementsModule.dll'
------------------
Resolve: 'PhotonRealtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'PhotonRealtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\PhotonRealtime.dll'
------------------
Resolve: 'UnityEngine.AnimationModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.AnimationModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.AnimationModule.dll'
------------------
Resolve: 'PhotonVoice, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'PhotonVoice, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\PhotonVoice.dll'
------------------
Resolve: 'PhotonVoice.PUN, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'PhotonVoice.PUN, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'Zorro.Settings.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Zorro.Settings.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Zorro.Settings.Runtime.dll'
------------------
Resolve: 'UnityEngine.TerrainModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.TerrainModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.TerrainModule.dll'
------------------
Resolve: 'Unity.Localization, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.Localization, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.Localization.dll'
------------------
Resolve: 'Unity.Mathematics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.Mathematics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.Mathematics.dll'
------------------
Resolve: 'HBAO.Universal.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'HBAO.Universal.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'Unity.RenderPipelines.Universal.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.RenderPipelines.Universal.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.RenderPipelines.Universal.Runtime.dll'
------------------
Resolve: 'sc.posteffects.runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'sc.posteffects.runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'Unity.RenderPipelines.Core.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.RenderPipelines.Core.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.RenderPipelines.Core.Runtime.dll'
------------------
Resolve: 'UnityEngine.ParticleSystemModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.ParticleSystemModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.ParticleSystemModule.dll'
------------------
Resolve: 'Zorro.UI.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Zorro.UI.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Zorro.UI.Runtime.dll'
------------------
Resolve: 'UnityEngine.VideoModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.VideoModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.VideoModule.dll'
------------------
Resolve: 'Unity.Splines, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.Splines, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.Splines.dll'
------------------
Resolve: 'UnityEngine.AIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.AIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.AIModule.dll'
------------------
Resolve: 'UnityEngine.InputLegacyModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.InputLegacyModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.InputLegacyModule.dll'
------------------
Resolve: 'Unity.Animation.Rigging, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.Animation.Rigging, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.Animation.Rigging.dll'
------------------
Resolve: 'Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Assembly-CSharp-firstpass.dll'
------------------
Resolve: 'Sirenix.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'Sirenix.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'Unity.ResourceManager, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.ResourceManager, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.ResourceManager.dll'
------------------
Resolve: 'PhotonChat, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'PhotonChat, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\PhotonChat.dll'
------------------
Resolve: 'UnityEngine.TextRenderingModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.TextRenderingModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.TextRenderingModule.dll'
------------------
Resolve: 'pworld, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'pworld, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'Sirenix.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'Sirenix.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'PhotonVoice.API, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'PhotonVoice.API, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\PhotonVoice.API.dll'
------------------
Resolve: 'Unity.VisualScripting.Core, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'Unity.VisualScripting.Core, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\Unity.VisualScripting.Core.dll'
------------------
Resolve: 'UnityEngine.JSONSerializeModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.JSONSerializeModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.JSONSerializeModule.dll'
------------------
Resolve: 'UnityEngine.IMGUIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.IMGUIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\UnityEngine.IMGUIModule.dll'
------------------
Resolve: 'PhotonUnityNetworking.Utilities, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'PhotonUnityNetworking.Utilities, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Content Warning\Mods\oldes\Content-Warning-Cheat-main\Output\PhotonUnityNetworking.Utilities.dll'
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll'
------------------
Resolve: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll'
------------------
Resolve: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll'
------------------
Resolve: 'System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Diagnostics.Tracing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Could not find by name: 'System.Diagnostics.Tracing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
------------------
Resolve: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Could not find by name: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
------------------
Resolve: 'System.IO.Compression, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.IO.Compression, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.IO.Compression.FileSystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.IO.Compression.FileSystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.ComponentModel.Composition, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.ComponentModel.Composition, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Could not find by name: 'System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
------------------
Resolve: 'System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Transactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Transactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Could not find by name: 'System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
------------------
Resolve: 'System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Runtime.InteropServices, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'System.Runtime.InteropServices, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '2.1.0.0', Got: '4.1.2.0'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\Facades\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.Unsafe, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'System.Runtime.CompilerServices.Unsafe, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null'
#endif
