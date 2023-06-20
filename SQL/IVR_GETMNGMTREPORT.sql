select 
    date_format(a.CALL_START_TIME, '%Y-%m') as Id, 
    date_format(a.CALL_START_TIME, '%M') as Month, 
    count(1) as TotalCount, 
    count(1) as SuccessCount, 
    0 as FailCount
    /*sum( case when message_status = 255 then 1 else 0 end) as SuccessCount, 
    sum( case when message_status = 255 then 0 else 1 end) as FailCount*/
from CALL_IVR_CC_[ID] a
where date_format(a.CALL_START_TIME, '%Y') = {0}
and CALLED_PARTY = {1}
and service_name <> 'MCA'
group by date_format(a.CALL_START_TIME, '%Y-%m'), date_format(a.CALL_START_TIME, '%M')
