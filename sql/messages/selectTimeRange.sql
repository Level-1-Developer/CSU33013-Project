SELECT *
FROM messages
WHERE sent_at BETWEEN ('@p1','YYYY-MM-DD HH:MI:SS') AND ('@p2','YYYY-MM-DD HH:MI:SS');