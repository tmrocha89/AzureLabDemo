using ISEP_AzureLabDemo.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEP_AzureLabDemo.Helpers
{

    public class HttpRequestClient
    {
        private const string _uri = "http://br.premierleague.com/pa-services/api/";
        private const string _endpoint = "football/lang_pt/i18n/leaguetable/dynamic/tableAndStats.json";
        RestClient client;

        public HttpRequestClient()
        {
            client = new RestClient(_uri);
        }

        public IList<Team> GetFixtureTable()
        {
            var request = new RestRequest(_endpoint, Method.GET);
            var response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<Rootobject>(response.Content);
            var table = obj.abstractCacheWebRootElement.leagueTable.entries;

            return table.Select(t =>
                new Team
                {
                    Position = t.rank,
                    Name = t.displayTeamName,
                    Played = t.total.played,
                    Points = t.total.points
                }).ToList();
        }
    }
}
