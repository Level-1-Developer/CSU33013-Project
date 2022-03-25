//needs to be tested
SELECT * FROM batch-forwards
WHERE DATEDIFF(HOUR, completed_at , GETDATE()) <= 1;
