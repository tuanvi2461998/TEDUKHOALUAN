Alter PROC GetBillQuantity
AS
BEGIN
				select COUNT(b.Id) as Quantity
                from Bills b
                where b.BillStatus = '0'
END

EXEC dbo.GetBillQuantity
								


				