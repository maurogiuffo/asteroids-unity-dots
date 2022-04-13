using Unity.Entities;
using UnityEngine.UI;

namespace Data
{
    [GenerateAuthoringComponent]
    public class InputUiData: IComponentData
    {
        public Text forward;
        public Text turnLeft;
        public Text turnRight;
        public Text fire;
    }
}