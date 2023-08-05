using UnityEngine;
using Cinemachine;

public class RoundCameraPos : CinemachineExtension
{
    public float PPU = 32;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state,
        float deltatime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);

            state.PositionCorrection += pos2 - pos;
        }
    }
    // 9
    float Round(float x)
    {
        return Mathf.Round(x * PPU) / PPU;
    }
}
