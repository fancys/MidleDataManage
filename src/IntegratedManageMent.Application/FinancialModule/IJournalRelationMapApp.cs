﻿using IntegratedManagement.Entity.FinancialModule.JournalRalationMap;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:47:51
	===============================================================================================================================*/
    public interface IJournalRelationMapApp
    {
        Task<List<JournalRelationMap>> GetSalesOrderAsync(QueryParam QueryParam);
    }
}