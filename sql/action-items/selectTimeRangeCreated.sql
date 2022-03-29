SELECT id, status, campaign, country, when_created
FROM action_items
WHERE when_created between to_date('@p1','YYYY-MM-DD') and to_date('@p2','YYYY-MM-DD');