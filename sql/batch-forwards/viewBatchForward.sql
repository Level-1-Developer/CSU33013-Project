SELECT started_at, completed_at, total_processed_messages, total_processed_batches
FROM batch 
WHERE id IN ()
	SELECT size, name, TIME(dispatched_at)
	FROM batch_forward_id
	ORDER BY TIME(dispatched_at) DESC);
