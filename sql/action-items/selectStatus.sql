SELECT id, status, campaign, country, when_created
FROM action_items
WHERE status = @p1;