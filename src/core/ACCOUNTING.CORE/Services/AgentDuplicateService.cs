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
    public class AgentDuplicateService(IGeneralService generalService) : IAgentDuplicateService
    {
        private readonly IGeneralService _generalService = generalService;

        public async Task<List<DuplicateInfoDTO>> GetAgentsDuplicate()
        {
            List<DuplicateInfoDTO> res = new List<DuplicateInfoDTO>();
            try
            {
                var response = "";
                response = await _generalService.SendGetRequest("data/agentesRepeat.json");

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
                        DuplicateInfoDTO obj = new DuplicateInfoDTO
                        {
                            IdAgentMain= uint.Parse(objeto["IdAgentMain"].ToString()),
                            MainAgentName=objeto["MainAgentName"].ToString(),
                            IdAgentDuplicate= uint.Parse(objeto["IdAgentDuplicate"].ToString()),
                            DuplicateAgentName = objeto["DuplicateAgentName"].ToString(),
                            Bets= GetCoincidenceData(JObject.Parse(objeto["Bets"].ToString())),
                            Hours= GetCoincidenceData(JObject.Parse(objeto["Hours"].ToString())),
                            Ips= GetCoincidenceData(JObject.Parse(objeto["Ips"].ToString())),
                            Password= GetCoincidenceData(JObject.Parse(objeto["Password"].ToString())),
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
        private CoincidenceDTO GetCoincidenceData(JObject value) {
            CoincidenceDTO result = new CoincidenceDTO
            {
                MatchPercent = double.Parse(value["MatchPercent"].ToString()),
                RelatedInfo = value["RelatedInfo"].ToString(),
                TotalMatch = uint.Parse(value["TotalMatch"].ToString())
            };
            return result;

        }
    }
}
