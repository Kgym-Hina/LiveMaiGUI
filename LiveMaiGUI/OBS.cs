using OBSWebsocketDotNet;

namespace LiveMai;

public static class Obs
{
    public static OBSWebsocketDotNet.OBSWebsocket Client;

    static Obs()
    {
        Client = new OBSWebsocket();
    }
    
}

public enum VideoMixType
{
    OBS_WEBSOCKET_VIDEO_MIX_TYPE_PREVIEW,
    OBS_WEBSOCKET_VIDEO_MIX_TYPE_PROGRAM,
    OBS_WEBSOCKET_VIDEO_MIX_TYPE_MULTIVIEW
}
public class ProjectorGeometry
{
    public int x { get; set; }
    public int y { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}