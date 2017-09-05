﻿using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.Param;
using IntegratedManageMent.Application.FinancialModule;
using ReportFormManage.Code.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Areas.Financial.Controllers
{
    public class JournalRelationMapController : Controller
    {
        IJournalRelationMapApp _journalRelationMapApp;
        public JournalRelationMapController(IJournalRelationMapApp IJournalRelationMapApp)
        {
            this._journalRelationMapApp = IJournalRelationMapApp;
        }
        // GET: Financial/JournalRalationMap
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SearchJournalRelationMapList(ViewParam param)
        {
            try
            {
                /*
                 * 获取查询条件
                 */
                //if(param.MinCreateDate)

                QueryParam queryParam = new QueryParam();
                queryParam.filter = "(CreateDate gt '20170101')";
                queryParam.orderby = "TransId";
                var rt = await _journalRelationMapApp.GetJournalRelationMapListAsync(queryParam);
                return Json(new { state = ResultType.success.ToString(), data = Newtonsoft.Json.JsonConvert.SerializeObject(rt) });
            }
            catch (Exception ex)
            {
                return Json(new { state = ResultType.error.ToString(), message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult CreateJournal(JournalRelationMapList journalRelationMapList)
        {
            if(journalRelationMapList == null)
                return Json(new { state = ResultType.error.ToString(), message = "未选择需要同步的数据" });
            foreach (var item in journalRelationMapList.JournalRelationMaps)
            {
                try
                {

                }
                catch(Exception ex)
                {

                }
            }
            return Json(new { state = ResultType.success.ToString(), message = "" });
        }
        [HttpPost]
        public async Task<ActionResult> SelectAndCreateJournal(string IDs)
        {
            if(string.IsNullOrEmpty(IDs))
                return Json(new { state = ResultType.error.ToString(), message = "选择的数据为空" });
            QueryParam queryParam = new QueryParam();
            queryParam.filter = string.Format("TransId in {0}", IDs.TrimEnd(','));
            queryParam.orderby = "TransId";
            var rt = await _journalRelationMapApp.GetJournalRelationMapListAsync(queryParam);
            foreach (var item in rt)
            {

            }

            return Json(new { state = ResultType.success.ToString(), message = "" });
        }
    }
}