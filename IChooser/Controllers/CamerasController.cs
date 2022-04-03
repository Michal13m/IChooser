using IChooser.Abstractions;
using IChooser.DTO;
using Microsoft.AspNetCore.Mvc;

namespace IChooser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamerasController : ControllerBase
    {
        private readonly ICamerasProvider _camerasProvider;

        public CamerasController(ICamerasProvider camerasProvider)
        {
            _camerasProvider = camerasProvider;
        }

        [HttpGet, Route(nameof(GetCameras))]
        public IEnumerable<Camera> GetCameras(string? name = "")
        {
            return _camerasProvider.GetCameras(name);
        }
    }
}
