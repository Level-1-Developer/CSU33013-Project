SELECT size, name, TIME(completed_at) 
FROM BATCH_FORWARDS 
WHERE completed_at BETWEEN (@p1) AND (@p2);
