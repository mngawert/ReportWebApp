select date_format(a.delivery_time, '%Y-%m') as Id, date_format(a.delivery_time, '%M') as Month, count(1) as TotalCount, sum( case when message_status = 255 then 1 else 0 end) as SuccessCount, sum( case when message_status = 255 then 0 else 1 end) as FailCount
from TRANS_CDR_[ID] a
where message_type in (1, 4)
and date_format(a.delivery_time, '%Y') = {0}
and user_data = {1}
group by date_format(a.delivery_time, '%Y-%m'), date_format(a.delivery_time, '%M')