select CALLED_PARTY as DestinationAddress, count(1) as TotalCount
from CALL_IVR_CC_[ID] a
where date_format(a.CALL_START_TIME, '%Y') = {0}
and (date_format(a.CALL_START_TIME, '%m') = {1} OR {1} = '')
and 'Success' = {2}
group by CALLED_PARTY
