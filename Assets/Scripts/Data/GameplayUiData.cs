using Unity.Entities;
using UnityEngine.UI;

namespace Data
{
    [GenerateAuthoringComponent]
    public class UiData: IComponentData
    {
        public Text livesCountText;
        public Text scoreText;
    }
}