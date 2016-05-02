﻿SELECT
 p.IdProduct,
 p.Description,
 p.Name as ProductName,
 p.Price,
 p.IdProductType,
 pt.Name as ProdcutTypeName
FROM Product p
INNER JOIN ProductType pt
ON p.IdProductType = pt.IdProductType;