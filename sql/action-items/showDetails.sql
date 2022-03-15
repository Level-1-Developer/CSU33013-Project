SELECT id, status, campaign, country, language, customerset
FROM action_items
WHERE id = @p1;