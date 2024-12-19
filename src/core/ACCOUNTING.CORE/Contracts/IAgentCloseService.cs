﻿using ACCOUNTING.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Contracts
{
    public interface IAgentCloseService
    {
        Task<List<AgentCloseDTO>>GetAgentsToClose(); 
    }
}
