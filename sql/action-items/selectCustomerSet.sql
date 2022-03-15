SELECT id, status, campaign, country, when_created
FROM action_items
where customerset = @p1;