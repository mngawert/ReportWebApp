﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportWebApp.Helper;
using ReportWebApp.Models;
using ReportWebApp.ViewModels;
using ReportWebApp.VOICEModels;

namespace ReportWebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IVRController : ControllerBase
    {
        private readonly TOT_VOICE_CDRContext _context;

        public IVRController(TOT_VOICE_CDRContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult GetReport(TransCdr01RequestViewModel model)
        {
            string sql = @"
                            SELECT  Transaction_Id as TransactionId,
                                    IF(Delivery_Time='0000-00-00 00:00:00.000',NULL,Delivery_Time) as DeliveryTime,
                                    date_format(Delivery_Time, '%d %M %Y %T') as DeliveryTimeText,
                                    Origination_Address as OriginationAddress,
                                    Destination_Address as DestinationAddress, 
                                    1 as MessageStatus,
                                    /*IF(Message_Status=255, 1, 2) as MessageStatus,*/
                                    Message_Status as InternalMessageStatus,
                                    Message_Type as MessageType
                            FROM 
                            (
                                SELECT  CALL_START_TIME as Delivery_Time,
                                        TRANSACTION_ID as Transaction_Id,
                                        CALLING_PARTY as Origination_Address,
                                        CALLED_PARTY as Destination_Address, 
                                        STATUS_CODE as Message_Status,
                                        NULL as Message_Type
                                FROM 
                                (
                                    select * from CALL_IVR_CC_01 UNION ALL select * from CALL_IVR_CC_02 UNION ALL select * from CALL_IVR_CC_03 UNION ALL select * from CALL_IVR_CC_04 UNION ALL select * from CALL_IVR_CC_05 UNION ALL select * from CALL_IVR_CC_06 UNION ALL select * from CALL_IVR_CC_07 UNION ALL select * from CALL_IVR_CC_08 UNION ALL select * from CALL_IVR_CC_09 UNION ALL select * from CALL_IVR_CC_10 UNION ALL
                                    select * from CALL_IVR_CC_11 UNION ALL select * from CALL_IVR_CC_12
                                ) a
                                WHERE CALLED_PARTY != '893770053'
                            ) a
                            ";

            var q = _context.Report1ViewModel.FromSqlRaw(sql);

            if (model.MessageType != null)
            {
                q = q.Where(a => a.MessageType == model.MessageType);
            }
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
                            select 
                                date_format(a.delivery_time, '%Y-%m') as Id, 
                                date_format(a.delivery_time, '%M') as Month, 
                                count(1) as TotalCount, 
                                count(1) as SuccessCount, 
                                0 as FailCount
                                /*sum( case when message_status = 255 then 1 else 0 end) as SuccessCount, 
                                sum( case when message_status = 255 then 0 else 1 end) as FailCount*/
                            from 
                            (
                                SELECT  CALL_START_TIME as Delivery_Time,
                                        TRANSACTION_ID as Transaction_Id,
                                        CALLING_PARTY as Origination_Address,
                                        CALLED_PARTY as Destination_Address, 
                                        STATUS_CODE as Message_Status
                                FROM 
                                (
                                    select * from CALL_IVR_CC_01 UNION ALL select * from CALL_IVR_CC_02 UNION ALL select * from CALL_IVR_CC_03 UNION ALL select * from CALL_IVR_CC_04 UNION ALL select * from CALL_IVR_CC_05 UNION ALL select * from CALL_IVR_CC_06 UNION ALL select * from CALL_IVR_CC_07 UNION ALL select * from CALL_IVR_CC_08 UNION ALL select * from CALL_IVR_CC_09 UNION ALL select * from CALL_IVR_CC_10 UNION ALL
                                    select * from CALL_IVR_CC_11 UNION ALL select * from CALL_IVR_CC_12
                                ) a
                                WHERE CALLED_PARTY != '893770053'
                            ) a
                            where date_format(a.delivery_time, '%Y') = {0}
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
                                SELECT  CALL_START_TIME as Delivery_Time,
                                        TRANSACTION_ID as Transaction_Id,
                                        CALLING_PARTY as Origination_Address,
                                        CALLED_PARTY as Destination_Address, 
                                        STATUS_CODE as Message_Status
                                FROM 
                                (
                                    select * from CALL_IVR_CC_01 UNION ALL select * from CALL_IVR_CC_02 UNION ALL select * from CALL_IVR_CC_03 UNION ALL select * from CALL_IVR_CC_04 UNION ALL select * from CALL_IVR_CC_05 UNION ALL select * from CALL_IVR_CC_06 UNION ALL select * from CALL_IVR_CC_07 UNION ALL select * from CALL_IVR_CC_08 UNION ALL select * from CALL_IVR_CC_09 UNION ALL select * from CALL_IVR_CC_10 UNION ALL
                                    select * from CALL_IVR_CC_11 UNION ALL select * from CALL_IVR_CC_12
                                ) a
                                WHERE CALLED_PARTY != '893770053'
                            ) a
                            where date_format(a.delivery_time, '%Y') = {0}
                            and (date_format(a.delivery_time, '%m') = {1} OR {1} = '')
                            and 'Success' = {2}
                            /*and IF(Message_Status = 255, 'Success', 'Fail') = {2}*/
                            group by destination_address
                            order by 2 desc
                            ";

            var q = _context.DashboardReport1ViewModel.FromSqlRaw(sql, model.Year, model.Month, model.MessageStatus).Take(10);

            return Ok(q);
        }

    }
}