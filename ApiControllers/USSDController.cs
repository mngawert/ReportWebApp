﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportWebApp.Helper;
using ReportWebApp.Models;
using ReportWebApp.USSDModels;
using ReportWebApp.ViewModels;

namespace ReportWebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class USSDController : ControllerBase
    {
        private readonly TOT_USSD_CDRContext _context;

        public USSDController(TOT_USSD_CDRContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult GetReport(TransCdr01RequestViewModel model)
        {
            string sql = @"
                            SELECT  Transaction_Id as TransactionId,
                                    Delivery_Time as DeliveryTime,
                                    date_format(Delivery_Time, '%d %M %Y %T') as DeliveryTimeText,
                                    Origination_Address as OriginationAddress,
                                    Destination_Address as DestinationAddress, 
                                    IF(Message_Status=255, 1, 2) as MessageStatus 
                            from 
                            (
                                select * from TRANS_CDR_01 UNION ALL select * from TRANS_CDR_02 UNION ALL select * from TRANS_CDR_03 UNION ALL select * from TRANS_CDR_04 UNION ALL select * from TRANS_CDR_05 UNION ALL select * from TRANS_CDR_06 UNION ALL select * from TRANS_CDR_07 UNION ALL select * from TRANS_CDR_08 UNION ALL select * from TRANS_CDR_09 UNION ALL select * from TRANS_CDR_10 UNION ALL 
                                select * from TRANS_CDR_11 UNION ALL select * from TRANS_CDR_12 UNION ALL select * from TRANS_CDR_13 UNION ALL select * from TRANS_CDR_14 UNION ALL select * from TRANS_CDR_15 UNION ALL select * from TRANS_CDR_16 UNION ALL select * from TRANS_CDR_17 UNION ALL select * from TRANS_CDR_18 UNION ALL select * from TRANS_CDR_19 UNION ALL select * from TRANS_CDR_20 UNION ALL 
                                select * from TRANS_CDR_21 UNION ALL select * from TRANS_CDR_22 UNION ALL select * from TRANS_CDR_23 UNION ALL select * from TRANS_CDR_24 UNION ALL select * from TRANS_CDR_25 UNION ALL select * from TRANS_CDR_26 UNION ALL select * from TRANS_CDR_27 UNION ALL select * from TRANS_CDR_28 UNION ALL select * from TRANS_CDR_29 UNION ALL select * from TRANS_CDR_30 UNION ALL 
                                select * from TRANS_CDR_31 
                            ) a
                            where message_type = 1
                            ";
            
            var q = _context.Report1ViewModel.FromSqlRaw(sql);

            if (!string.IsNullOrEmpty(model.OriginationAddress))
            {
                q = q.Where(a => a.OriginationAddress.Contains(model.OriginationAddress));
            }
            if (!string.IsNullOrEmpty(model.DestinationAddress))
            {
                q = q.Where(a => a.DestinationAddress.Contains(model.DestinationAddress));
            }
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                var d = DateTime.Parse(model.StartDate);
                q = q.Where(a => a.DeliveryTime >= d);
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                var d = DateTime.Parse(model.EndDate);
                q = q.Where(a => a.DeliveryTime < d.AddDays(1));
            }
            if (model.MessageStatus != null)
            {
                q = q.Where(a => a.MessageStatus == model.MessageStatus);
            }

            q = q.OrderByDescending(a => a.DeliveryTime);

            var qq = PaginatedList<Report1ViewModel>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

        [HttpPost]
        public IActionResult GetMngmtReport(MngmtReportRequest model)
        {
            string sql = @"
                            select date_format(a.delivery_time, '%Y-%m') as Id, date_format(a.delivery_time, '%M') as Month, count(1) as TotalCount, sum( case when message_status = 255 then 1 else 0 end) as SuccessCount, sum( case when message_status = 255 then 0 else 1 end) as FailCount
                            from 
                            (
                                select * from TRANS_CDR_01 UNION ALL select * from TRANS_CDR_02 UNION ALL select * from TRANS_CDR_03 UNION ALL select * from TRANS_CDR_04 UNION ALL select * from TRANS_CDR_05 UNION ALL select * from TRANS_CDR_06 UNION ALL select * from TRANS_CDR_07 UNION ALL select * from TRANS_CDR_08 UNION ALL select * from TRANS_CDR_09 UNION ALL select * from TRANS_CDR_10 UNION ALL 
                                select * from TRANS_CDR_11 UNION ALL select * from TRANS_CDR_12 UNION ALL select * from TRANS_CDR_13 UNION ALL select * from TRANS_CDR_14 UNION ALL select * from TRANS_CDR_15 UNION ALL select * from TRANS_CDR_16 UNION ALL select * from TRANS_CDR_17 UNION ALL select * from TRANS_CDR_18 UNION ALL select * from TRANS_CDR_19 UNION ALL select * from TRANS_CDR_20 UNION ALL 
                                select * from TRANS_CDR_21 UNION ALL select * from TRANS_CDR_22 UNION ALL select * from TRANS_CDR_23 UNION ALL select * from TRANS_CDR_24 UNION ALL select * from TRANS_CDR_25 UNION ALL select * from TRANS_CDR_26 UNION ALL select * from TRANS_CDR_27 UNION ALL select * from TRANS_CDR_28 UNION ALL select * from TRANS_CDR_29 UNION ALL select * from TRANS_CDR_30 UNION ALL 
                                select * from TRANS_CDR_31 
                            ) a
                            where message_type = 1
                            and date_format(a.delivery_time, '%Y') = {0}
                            and destination_address = {1}
                            group by date_format(a.delivery_time, '%Y-%m'), date_format(a.delivery_time, '%M')
                            order by 1
                            ";

            var q = _context.MngmtReportViewModel.FromSqlRaw(sql, model.Year, model.DestinationAddress);

            return Ok(q);
        }

        [HttpPost]
        public IActionResult GetDashboardReport1(DashboardReport1Request model)
        {
            string sql = @"
                            select destination_address as DestinationAddress, count(1) as TotalCount
                            from 
                            (
                                select * from TRANS_CDR_01 UNION ALL select * from TRANS_CDR_02 UNION ALL select * from TRANS_CDR_03 UNION ALL select * from TRANS_CDR_04 UNION ALL select * from TRANS_CDR_05 UNION ALL select * from TRANS_CDR_06 UNION ALL select * from TRANS_CDR_07 UNION ALL select * from TRANS_CDR_08 UNION ALL select * from TRANS_CDR_09 UNION ALL select * from TRANS_CDR_10 UNION ALL 
                                select * from TRANS_CDR_11 UNION ALL select * from TRANS_CDR_12 UNION ALL select * from TRANS_CDR_13 UNION ALL select * from TRANS_CDR_14 UNION ALL select * from TRANS_CDR_15 UNION ALL select * from TRANS_CDR_16 UNION ALL select * from TRANS_CDR_17 UNION ALL select * from TRANS_CDR_18 UNION ALL select * from TRANS_CDR_19 UNION ALL select * from TRANS_CDR_20 UNION ALL 
                                select * from TRANS_CDR_21 UNION ALL select * from TRANS_CDR_22 UNION ALL select * from TRANS_CDR_23 UNION ALL select * from TRANS_CDR_24 UNION ALL select * from TRANS_CDR_25 UNION ALL select * from TRANS_CDR_26 UNION ALL select * from TRANS_CDR_27 UNION ALL select * from TRANS_CDR_28 UNION ALL select * from TRANS_CDR_29 UNION ALL select * from TRANS_CDR_30 UNION ALL 
                                select * from TRANS_CDR_31 
                            ) a
                            where message_type = 1
                            and Message_Status = 255
                            and date_format(a.delivery_time, '%Y') = {0}
                            group by destination_address
                            order by 2 desc
                            ";

            var q = _context.DashboardReport1ViewModel.FromSqlRaw(sql, model.Year).Take(10);

            return Ok(q);
        }

    }
}