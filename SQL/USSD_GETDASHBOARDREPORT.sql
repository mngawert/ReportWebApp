select destination_address as DestinationAddress, count(1) as TotalCount
from TRANS_CDR_[ID] a
where message_type = 1
and IF(Message_Status = 255, 'Success', 'Fail') = {2}
and date_format(a.delivery_time, '%Y') = {0}
and (date_format(a.delivery_time, '%m') = {1} OR {1} = '')
group by destination_address