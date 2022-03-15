SELECT *
FROM action_items
WHERE when_created between to_date('@p1','DD-MM-YYYY') and to_date('@p2','DD-MM-YYYY');