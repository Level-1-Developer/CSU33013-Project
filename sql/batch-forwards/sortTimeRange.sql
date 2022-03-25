//needs to be tested
//suppsoed to show batch-forwards from the past hour. should do for 6 hours, 12 hours and 24 hours also
SELECT * FROM batch-forwards
WHERE DATEDIFF(HOUR, completed_at , GETDATE()) <= 1;
