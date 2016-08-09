SELECT a.Email, a.ID, a.OrderDate, COUNT(*)
FROM dbo.Orders AS a
INNER JOIN dbo.Orders AS b ON (a.Email = b.Email AND b.ID <= a.ID)
GROUP BY a.Email, a.ID, a.OrderDate