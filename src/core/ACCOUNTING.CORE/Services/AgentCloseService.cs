using ACCOUNTING.CORE.Contracts;
using ACCOUNTING.CORE.Models;
using ACCOUNTING.CORE.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ACCOUNTING.CORE.Services
{
    public class AgentCloseService(IGeneralService generalService) : IAgentCloseService
    {
        private readonly IGeneralService _generalService = generalService;
     
        public async Task<List<AgentCloseDTO>> GetAgentsToClose()
        {
            List<AgentCloseDTO> res = new List<AgentCloseDTO>();
            try
            {
                var response = "";               
                 response = await _generalService.SendGetRequest("data/agentesClose.json");

                JArray arrayList = JArray.Parse(response);
                foreach (JObject? objeto in arrayList)
                {
                    /*string nombre = objeto["AgentName"].ToString();
                    uint Id = uint.Parse(objeto["IdAgent"].ToString());
                    double Balance = double.Parse(objeto["Balance"].ToString());
                    JArray arraytest = JArray.Parse(objeto["WeeksInfo"].ToString());
                    Console.WriteLine(nombre);*/
                    try
                    {
                        AgentCloseDTO obj = new AgentCloseDTO
                        {
                            IdAgent = uint.Parse(objeto["IdAgent"].ToString()),
                            AgentName = objeto["AgentName"].ToString(),
                            Balance = double.Parse(objeto["Balance"].ToString()),
                            RootDGS = objeto["RootDGS"].ToString(),
                            SubAgents = objeto["SubAgents"].ToString()
                        };

                        res.Add(obj);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: --->{ex.Message}");
                        throw;
                    }

                }
                Console.WriteLine($"Prueba: --->");
                return res;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: --->{ex.Message}");
            }
            return res;
        }
    }
}
