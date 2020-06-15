Create PROC GetQuantityProducts
as
begin
	select COUNT(Id) as PQ from Products
end

 EXEC dbo.GetQuantityProducts

 