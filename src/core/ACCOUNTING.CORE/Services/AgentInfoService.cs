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
    public class AgentInfoService(IGeneralService generalService) : IAgentInfoService
    {
        private readonly IGeneralService _generalService = generalService;
        public async Task<List<AgentInfoDTO>> GetActiveAgents(int numberWeeks)
        {
            List<AgentInfoDTO> res = new List<AgentInfoDTO>();
            try
            {
                var response = "";
                if (numberWeeks == 0)
                    response = await _generalService.SendGetRequest("data/agentes.json");
                else if (numberWeeks == 1)
                    response = await _generalService.SendGetRequest("data/agentes8.json");

                JArray arrayList = JArray.Parse(response);
                foreach (JObject objeto in arrayList)
                {
                    /*string nombre = objeto["AgentName"].ToString();
                    uint Id = uint.Parse(objeto["IdAgent"].ToString());
                    double Balance = double.Parse(objeto["Balance"].ToString());
                    JArray arraytest = JArray.Parse(objeto["WeeksInfo"].ToString());
                    Console.WriteLine(nombre);*/
                    try
                    {
                        AgentInfoDTO obj = new AgentInfoDTO
                        {
                            IdAgent = uint.Parse(objeto["IdAgent"].ToString()),
                            AgentName = objeto["AgentName"].ToString(),
                            Fee = double.Parse(objeto["Fee"].ToString()),
                            Balance = double.Parse(objeto["Balance"].ToString()),
                            WeeksInfo = getList(JArray.Parse(objeto["WeeksInfo"].ToString())),
                            BalancePeriod = double.Parse(objeto["BalancePeriod"].ToString()),
                            TotalPayment = double.Parse(objeto["TotalPayment"].ToString())
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

        private List<WeekDTO> getList(JArray values)
        {
            List<WeekDTO> result = new List<WeekDTO>();
            foreach (JObject objeto in values)
            {
                WeekDTO obj = new WeekDTO
                {
                    ActivePlayer = double.Parse(objeto["ActivePlayer"].ToString()),
                    Charge = double.Parse(objeto["Charge"].ToString()),
                    Payment = double.Parse(objeto["Payment"].ToString()),
                };
                result.Add(obj);
            }
            return result;
        }

        public async Task<string> GetInactiveAgents()
        {
            return await this._generalService.SendGetRequest(Constants.INACTIVE_AGENTS);
        }
    }
}
