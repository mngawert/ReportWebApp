SELECT  TRANSACTION_ID as TransactionId,
    IF(CALL_START_TIME='0000-00-00 00:00:00.000',NULL,CALL_START_TIME) as DeliveryTime,
    date_format(CALL_START_TIME, '%d %M %Y %T') as DeliveryTimeText,
    CALLING_PARTY as OriginationAddress,
    CALLED_PARTY as DestinationAddress, 
    1 as MessageStatus,
    STATUS_CODE as InternalMessageStatus,
    NULL as MessageType
FROM CALL_IVR_CC_[ID] a
where service_name <> 'MCA'
