﻿using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.B1
{
    internal class SAP
    {
        static object locker = new object();
        private static SAPbobsCOM.Company _SAPCompany;
        private static readonly ILog logger = LogManager.GetLogger(typeof(SAP));
        public static SAPbobsCOM.Company SAPCompany
        {
            get
            {
                try
                {
                    if (_SAPCompany == null || _SAPCompany.Connected == false)
                        lock (locker)
                        {
                            if (_SAPCompany == null || _SAPCompany.Connected == false)
                                ConnectB1Company();
                        }

                    return _SAPCompany;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return null;
                }
            }
        }
        /// <summary>
        /// 连接B1账套
        /// </summary>
        /// <author>WangPeng</author>
        private static SAPbobsCOM.Company ConnectB1Company()
        {
            if (_SAPCompany == null)
                _SAPCompany = new SAPbobsCOM.Company();
            if (_SAPCompany.Connected == true) return _SAPCompany;
            logger.Info("开始连接B1账套……");
            _SAPCompany.DbServerType = (SAPbobsCOM.BoDataServerTypes)System.Enum.Parse(typeof(SAPbobsCOM.BoDataServerTypes), ConfigurationManager.AppSettings["SAPDBServerType"]);
            _SAPCompany.Server = ConfigurationManager.AppSettings["DataSource"];
            _SAPCompany.language = SAPbobsCOM.BoSuppLangs.ln_Chinese;
            _SAPCompany.UseTrusted = Convert.ToBoolean(ConfigurationManager.AppSettings["UseTrusted"]);
            _SAPCompany.DbUserName = ConfigurationManager.AppSettings["UserID"];
            _SAPCompany.DbPassword = ConfigurationManager.AppSettings["Password"];
            _SAPCompany.CompanyDB = ConfigurationManager.AppSettings["InitialCatalog"];
            _SAPCompany.UserName = ConfigurationManager.AppSettings["SAPUser"];
            _SAPCompany.Password = ConfigurationManager.AppSettings["SAPPassword"];
            _SAPCompany.LicenseServer = ConfigurationManager.AppSettings["SAPLicenseServer"];
            int RntCode = _SAPCompany.Connect();
            if (RntCode != 0)
            {
                string errMsg = string.Format("ErrorCode:[{0}],ErrrMsg:[{1}];", SAP.SAPCompany.GetLastErrorCode(), SAP.SAPCompany.GetLastErrorDescription());
                logger.Error(errMsg);
                throw new Exception(errMsg);
            }
            logger.Info("已连接 " + _SAPCompany.CompanyName);
            return _SAPCompany;
        }
    }
}
