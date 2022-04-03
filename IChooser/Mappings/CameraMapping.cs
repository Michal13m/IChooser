using IChooser.DTO;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace IChooser.Mappings
{
    public class CameraMapping: CsvMapping<Camera>
    {
        public CameraMapping(): base()
        {
            MapProperty(0, x => x.Name);
            MapProperty(1, x => x.Latitude);
            MapProperty(2, x => x.Longitude);
        }
    }
}
