select DESTINATING_ADDRESS as DestinationAddress, count(1) as TotalCount
from MCA_VMS_CC_[ID] a
where date_format(a.CALL_TIMESTAMP, '%Y') = {0}
and (date_format(a.CALL_TIMESTAMP, '%m') = {1} OR {1} = '')
and 'Success' = {2}
group by destination_address
