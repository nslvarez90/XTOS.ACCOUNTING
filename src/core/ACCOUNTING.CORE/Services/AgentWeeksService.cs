using ACCOUNTING.CORE.Contracts;
using ACCOUNTING.CORE.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Services
{
    public class AgentWeeksService(IGeneralService generalService) : IAgentWeeksService
    {
        private readonly IGeneralService _generalService = generalService;
        public async Task<List<AgentWeeksDTO>> GetAgentsWeeks()
        {
            List<AgentWeeksDTO> res = new List<AgentWeeksDTO>();
            try
            {
                var response  = await _generalService.SendGetRequest("data/agentesWeeks.json");

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
                        AgentWeeksDTO obj = new AgentWeeksDTO
                        {
                            IdAgent= uint.Parse(objeto["IdAgent"].ToString()),
                            AgentName= objeto["AgentName"].ToString(),
                            PlayerTotal=uint.Parse(objeto["PlayerTotal"].ToString()),
                            Week1=GetWeekResumeData(JObject.Parse(objeto["Week1"].ToString())),
                            Week2=GetWeekResumeData(JObject.Parse(objeto["Week2"].ToString())),
                            Week3=GetWeekResumeData(JObject.Parse(objeto["Week3"].ToString())),
                            Week4= GetWeekResumeData(JObject.Parse(objeto["Week4"].ToString())),
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
        private WeekResumeDTO GetWeekResumeData(JObject value)
        {
            WeekResumeDTO result = new WeekResumeDTO
            {
                PlayerTotal= uint.Parse(value["PlayerTotal"].ToString()),
                PercentRepresentative= double.Parse(value["PercentRepresentative"].ToString())
            };
            return result;

        }
    }
}
