SELECT id, batch_id, delivery_method, status, action_item_id
FROM messages 
WHERE id = @p1
//id parameter needs to be the id of the message clicked on
	