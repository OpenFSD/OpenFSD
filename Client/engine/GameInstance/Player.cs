using OpenTK;
using Florence.ClientAssembly.Graphics.Cameras;
using Florence.ClientAssembly.Graphics.GameObjects;
using Florence.ClientAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.game_Instance
{
    public class Player : AGameObject
    {
        private FirstPersonCamera _cameraFP;
        private ThirdPersonCamera _cameraTP;
        private bool cameraSelector;
        

        public Player(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
            _cameraFP = null;
            _cameraTP = null;
            cameraSelector = true;
        }

        public void Initialise_Player()
        {

        }
        public void Create_Cameras()
        {
            _cameraTP = new Florence.ClientAssembly.Graphics.Cameras.ThirdPersonCamera(
                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player()
            );
            while (_cameraTP == null) { }

            _cameraFP = new Florence.ClientAssembly.Graphics.Cameras.FirstPersonCamera(
                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player()
            );
            while (_cameraFP == null) { }
        }

//GET
        public FirstPersonCamera Get_Camera_FP()
        {
            return _cameraFP;
        }
        public ThirdPersonCamera Get_Camera_TP()
        {
            return _cameraTP;
        }
        public bool Get_cameraSelector()
        {
            return cameraSelector;
        }

        //SET
        public void Set_cameraSelector(bool value)
        {
            cameraSelector = value;
        }
    }
}

