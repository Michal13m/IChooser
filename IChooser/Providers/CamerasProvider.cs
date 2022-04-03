using System.Text;
using IChooser.Abstractions;
using IChooser.DTO;
using IChooser.Mappings;
using TinyCsvParser;

namespace IChooser.Providers
{
    public class CamerasProvider: ICamerasProvider
    {

        public IEnumerable<Camera> GetCameras(string filterName)
        {
            var csvParserOptions = new CsvParserOptions(true, ';');
            CameraMapping mapper = new ();
            var csvParser = new CsvParser<Camera>(csvParserOptions, mapper);

            var csvResult = csvParser
                .ReadFromFile(@"cameras-defb.csv", Encoding.ASCII)
                .ToList();

            var rawCameras = csvResult.Select(x => x.Result);

            var camerasWithNumber = rawCameras
                .Where(x => x != null && ExpectedName(x.Name, filterName))
                .Select(x =>
            {
                int.TryParse(x.Name?.Substring(7, 3), out var number);

                return new Camera()
                {
                    Number = number,
                    Name = x?.Name,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,

                };
            });

            return camerasWithNumber;

        }

        private static bool ExpectedName(string name, string filterName)
        {
            if (string.IsNullOrEmpty(filterName)) return true;

            return name.Contains(filterName);
        }
    }
}
