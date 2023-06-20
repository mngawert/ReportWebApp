select 
    date_format(a.CALL_TIMESTAMP, '%Y-%m') as Id, 
    date_format(a.CALL_TIMESTAMP, '%M') as Month, 
    count(1) as TotalCount, 
    count(1) as SuccessCount, 
    0 as FailCount
    /*sum( case when message_status = 255 then 1 else 0 end) as SuccessCount, 
    sum( case when message_status = 255 then 0 else 1 end) as FailCount*/
from MCA_VMS_CC_[ID] a
where date_format(a.CALL_TIMESTAMP, '%Y') = {0}
and DESTINATING_ADDRESS = {1}
group by date_format(a.CALL_TIMESTAMP, '%Y-%m'), date_format(a.CALL_TIMESTAMP, '%M')
