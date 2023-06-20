SELECT  TRANSACTION_ID as TransactionId,
        IF(CALL_TIMESTAMP='0000-00-00 00:00:00.000',NULL,CALL_TIMESTAMP) as DeliveryTime,
        date_format(CALL_TIMESTAMP, '%d %M %Y %T') as DeliveryTimeText,
        ORIGINATING_ADDRESS as OriginationAddress,
        DESTINATING_ADDRESS as DestinationAddress, 
        /*IF(Message_Status=255, 1, 2) as MessageStatus,*/
        1 as MessageStatus,
        STATUS as InternalMessageStatus,
        Message_Type as MessageType
FROM MCA_VMS_CC_[ID] a
limit 100