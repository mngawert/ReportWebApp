SELECT  
	Transaction_Id as TransactionId,
	IF(Delivery_Time='0000-00-00 00:00:00.000',NULL,Delivery_Time) as DeliveryTime,
	date_format(Delivery_Time, '%d %M %Y %T') as DeliveryTimeText,
	Origination_Address as OriginationAddress,
	user_data as DestinationAddress, 
	IF(Message_Status=255, 1, 2) as MessageStatus,
	Message_Status as InternalMessageStatus,
	Message_Type as MessageType
FROM TRANS_CDR_[ID] a
