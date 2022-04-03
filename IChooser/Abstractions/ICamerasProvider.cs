using IChooser.DTO;

namespace IChooser.Abstractions
{
    public interface ICamerasProvider
    {
        IEnumerable<Camera> GetCameras(string name);
    }
}
