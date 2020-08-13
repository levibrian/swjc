using KneatSC.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KneatSC.Services
{
    public class StarshipService : BaseService, IStarshipService
    {
        const string API_URL = "http://swapi.dev/api/starships/";

        public async Task<IEnumerable<StarshipDTO>> Get(int page, long distance)
        {
            try
            {
                return await base.Get<IEnumerable<StarshipDTO>>($"{API_URL}?page={page}", (data) =>
                {
                    JObject jsonObject = JObject.Parse(data);

                    return jsonObject["results"].Where(item => !string.IsNullOrEmpty((string)item["MGLT"]) &&
                                                               (string)item["MGLT"] != "unknown")
                                                .Select(item => new StarshipDTO
                                                {
                                                    Id = ReadIdFromUrl((string)item["url"]),
                                                    Name = (string)item["name"],
                                                    Model = (string)item["model"],
                                                    StarshipClass = (string)item["starship_class"],
                                                    Manufacturer = (string)item["manufacturer"],
                                                    HyperdriveRating = (string)item["hyperdrive_rating"],
                                                    MGLT = (string)item["MGLT"],
                                                    JumpCount = Math.Round(decimal.Divide(distance, (decimal)item["MGLT"]))
                                                });
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StarshipDTO>> GetAll(long distance)
        {
            try
            {
                int page = 1;
                var starshipCollection = new List<StarshipDTO>();

                while (page > 0)
                {
                    var rootObject = await base.Get<StarshipRequest>($"{API_URL}?page={page}");
                    starshipCollection.AddRange(await this.Get(page, distance));

                    page = rootObject.Next != null ?
                                Convert.ToInt32(rootObject.Next.Substring(rootObject.Next.IndexOf("=") + 1)) :
                                0;
                }

                return starshipCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int ReadIdFromUrl(string url)
        {
            var uri = new Uri(url);
            var lastSegment = uri.Segments[uri.Segments.Length - 1];
            var id = Regex.Match(lastSegment, "\\d+").Value;

            return int.Parse(id);
        }
    }
}
