SELECT size, name, DATETIME(completed_at) 
FROM BATCH_FORWARDS 
WHERE completed_at BETWEEN ('@p1','YYYY-MM-DD HH:MI:SS') AND ('@p2','YYYY-MM-DD HH:MI:SS');
