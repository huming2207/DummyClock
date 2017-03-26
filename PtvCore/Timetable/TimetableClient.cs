using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;

namespace PtvCore.Timetable
{
    public class TimetableClient
    {
        private const string BaseUrl = "http://timetableapi.ptv.vic.gov.au";
        private const string GetHealthPathAndQueryFormat = "/v2/healthcheck?timestamp={0}&";
        private const string GetNearbyPathAndQueryFormat = "/v2/nearme/latitude/{0}/longitude/{1}?";
        private const string GetPointsOfInterestPathAndQueryFormat = "/v2/poi/{0}/lat1/{1}/long1/{2}/lat2/{3}/long2/{4}/griddepth/{5}/limit/{6}?";
        private const string SearchPathAndQueryFormat = "/v2/search/{0}?";
        private const string SearchLineByModeAndQueryFormat = "/v2/lines/mode/{0}?name={1}&";
        private const string GetBroadNextDeparturesPathAndQueryFormat = "/v2/mode/{0}/stop/{1}/departures/by-destination/limit/{2}?";
        private const string GetSpecificNextDeparturesPathAndQueryFormat = "/v2/mode/{0}/line/{1}/stop/{2}/directionid/{3}/departures/all/limit/{4}?for_utc={5}&";
        private const string GetStoppingPatternPathAndQueryFormat = "/v2/mode/{0}/run/{1}/stop/{2}/stopping-pattern?for_utc={3}&";
        private const string GetLineStopsPathAndQueryFormat = "/v2/mode/{0}/line/{1}/stops-for-line?";
        private const string GetDistruptionAndQueryFormat = "/v2/disruptions/modes/{0}?";
        private const string GetStopFacilitiesAndQueryFormat = "/v2/stops/?stop_id={0}&route_type={1}&location={2}&amenity={3}&accessibility={4}&";
        private const string DeveloperIDFormat = "{0}devid={1}";
        private const string SignatureFormat = "{0}&signature={1}";
        
        public TimetableClient(string developerID, string securityKey, TimetableClientHasher hasher, CompressionType type = CompressionType.GZip)
        {
            this.DeveloperID = developerID;
            this.SecurityKey = securityKey;
            this.Hasher = hasher;
            this.CompressType = type;
        }

        public string DeveloperID { get; private set; }
        public string SecurityKey { get; private set; }
        public TimetableClientHasher Hasher { get; private set; }
        public CompressionType CompressType { get; private set; }

        private string ApplySignature(string pathAndQuery)
        {
            var pathAndQueryBytes = Encoding.UTF8.GetBytes(pathAndQuery);
            var securityKeyBytes = Encoding.UTF8.GetBytes(this.SecurityKey);

            var signatureBytes = this.Hasher(pathAndQueryBytes, securityKeyBytes);
            var signature = this.EncodeSignature(signatureBytes);

            var pathAndQueryWithSignature = string.Format(TimetableClient.SignatureFormat, pathAndQuery, signature);
            return pathAndQueryWithSignature;
        }

        private string EncodeSignature(byte[] signatureBytes)
        {
            var builder = new StringBuilder();
            foreach (var signatureByte in signatureBytes)
            {
                builder.AppendFormat("{0:X2}", signatureByte);
            }

            var signature = builder.ToString();
            return signature;
        }

        private string ApplyDeveloperID(string pathAndQuery)
        {
            var pathAndQueryWithDeveloperID = string.Format(
              TimetableClient.DeveloperIDFormat,
              pathAndQuery,
              this.DeveloperID
              );

            return pathAndQueryWithDeveloperID;
        }

        private HttpClient GetHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var client = new HttpClient(handler);
            
            switch(CompressType)
            {
                case CompressionType.GZip:
                {
                    client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
                    break;
                }
                case CompressionType.Deflate:
                {
                    client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));
                    break;
                }
                default:
                {
                    break;
                }
            }

            client.BaseAddress = new Uri(TimetableClient.BaseUrl);
            return client;
        }

        private async Task<T> ExecuteAsync<T>(string pathAndQuery)
        {
            var pathAndQueryWithDeveloperID = this.ApplyDeveloperID(pathAndQuery);
            var pathAndQueryWithDeveloperIDAndSignature = this.ApplySignature(pathAndQueryWithDeveloperID);
            Debug.WriteLine("REQUEST HTTP ADDR: " + BaseUrl + pathAndQueryWithDeveloperIDAndSignature);
            
            using (var client = this.GetHttpClient())
            {
                var json = await client.GetStringAsync(pathAndQueryWithDeveloperIDAndSignature);
                var result = JsonConvert.DeserializeObject<T>(
                    json,
                    new DepartureConverter(),
                    new ItemConverter(),
                    new LocationConverter(),
                    new DisruptionConverter(),
                    new LineByModeConverter()
                    );

                return result;
            }
        }

        public async Task<Health> GetHealthAsync()
        {
            var timestampInIso8601 = DateTime.UtcNow.ToString("o");
            var pathAndQuery = string.Format(TimetableClient.GetHealthPathAndQueryFormat, timestampInIso8601);
            var result = await this.ExecuteAsync<Health>(pathAndQuery);
            return result;
        }

        public async Task<Stop[]> GetNearbyStopsAsync(double latitude, double longitude)
        {
            var pathAndQuery = string.Format(TimetableClient.GetNearbyPathAndQueryFormat, latitude, longitude);
            var result = await this.ExecuteAsync<Item[]>(pathAndQuery);
            var castResult = result.Cast<Stop>().ToArray();
            return castResult;
        }

        public async Task<Item[]> SearchAsync(string keyword)
        {
            var encodedKeyword = Uri.EscapeDataString(keyword);
            var pathAndQuery = string.Format(TimetableClient.SearchPathAndQueryFormat, encodedKeyword);
            var result = await this.ExecuteAsync<Item[]>(pathAndQuery);
            return result;
        }

        public async Task<PointsOfInterest> GetPointsOfInterestAsync(PointOfInterestType pointOfInterestType, double topLeftLatitude, double topLeftLongitude, double bottomRightLatitude, double bottomRightLongitude, uint gridDepth, uint limit)
        {
            var pathAndQuery = string.Format(
                TimetableClient.GetPointsOfInterestPathAndQueryFormat,
                (uint)pointOfInterestType,
                topLeftLatitude,
                topLeftLongitude,
                bottomRightLatitude,
                bottomRightLongitude,
                gridDepth,
                limit
                );
            var result = await this.ExecuteAsync<PointsOfInterest>(pathAndQuery);
            return result;
        }

        public async Task<Departure[]> GetBroadNextDeparturesAsync(TransportType mode, string stopID, uint limit)
        {
            var pathAndQuery = string.Format(
                TimetableClient.GetBroadNextDeparturesPathAndQueryFormat,
                (uint)mode,
                stopID,
                limit
                );
            var result = await this.ExecuteAsync<Departure[]>(pathAndQuery);
            return result;

        }

        public async Task<Departure[]> GetSpecificNextDeparturesAsync(TransportType mode, string lineID, string stopID, string directionID, uint limit, DateTime fromUtc)
        {
            var pathAndQuery = string.Format(
                TimetableClient.GetSpecificNextDeparturesPathAndQueryFormat,
                (uint)mode,
                lineID,
                stopID,
                directionID,
                limit,
                fromUtc.ToString("o")
                );
            var result = await this.ExecuteAsync<Departure[]>(pathAndQuery);
            return result;
        }

        public async Task<Departure[]> GetStoppingPatternAsync(TransportType mode, string runID, string stopID, DateTime fromUtc)
        {
            var pathAndQuery = string.Format(
                TimetableClient.GetStoppingPatternPathAndQueryFormat,
                (uint)mode,
                runID,
                stopID,
                fromUtc.ToString("o")
                );
            var result = await this.ExecuteAsync<Departure[]>(pathAndQuery);
            return result;
        }

        public async Task<Stop[]> GetLineStopsAsync(TransportType mode, string lineID)
        {
            var pathAndQuery = string.Format(
                TimetableClient.GetLineStopsPathAndQueryFormat,
                (uint)mode,
                lineID
                );
            var result = await this.ExecuteAsync<Stop[]>(pathAndQuery);
            return result;
        }

        public async Task<LineByMode[]> SearchLineByModeAsync(string keyword, TransportType mode)
        {
            var encodedKeyword = Uri.EscapeDataString(keyword);
            var pathAndQuery = string.Format(TimetableClient.SearchLineByModeAndQueryFormat, (uint)mode, encodedKeyword);
            var result = await this.ExecuteAsync<LineByMode[]>(pathAndQuery);
            return result;
        }

        public async Task<Disruption[]> GetDisruptionAsync(string mode)
        {
            var pathAndQuery = string.Format(TimetableClient.GetDistruptionAndQueryFormat, mode);
            var result = await this.ExecuteAsync<Disruption[]>(pathAndQuery);
            return result;
        }

        public async Task<StopFacilities> GetStopFacilitiesAsync(string stopID, RouteType routeType, bool getLocation = true, bool getAmenity = true, bool getAccessibility = true)
        {
            var pathAndQuery = string.Format(TimetableClient.GetStopFacilitiesAndQueryFormat,
                    stopID,
                    (uint)routeType,
                    Convert.ToInt32(getLocation),
                    Convert.ToInt32(getAmenity),
                    Convert.ToInt32(getAccessibility)
                    );
            var result = await this.ExecuteAsync<StopFacilities>(pathAndQuery);
            return result;
        }
    }
}
