using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TravelApp.Model;

namespace TravelApp.Logic
{
    public class VenueLogic
    {
        public async static Task<List<Venue>> GetVenues(double latitude, double longtitude)
        {
            List<Venue> venues = new List<Venue>();

            var url = VenueRoot.GenerateUrl(latitude, longtitude);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venureRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                venues = venureRoot.response.venues as List<Venue>;
            }

            return venues;
        }
    }
}
