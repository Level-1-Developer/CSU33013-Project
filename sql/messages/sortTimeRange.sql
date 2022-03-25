//needs to be tested
//for the last hour
SELECT * FROM batch-forwards
WHERE DATEDIFF(HOUR, created_at , GETDATE()) <= 1;