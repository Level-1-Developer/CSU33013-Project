SELECT id, status, campaign, country, when_created
FROM action_items
where language = @p1;