--INNER JOIN PaymentOption po ON po.IdPaymentOption = co.IdPaymentOption;

select
  co.IdCustomerOrder,
  co.OrderNumber
from CustomerOrder co
where co.OrderNumber = 'fds';


-- Add an order entry
insert into CustomerOrder  
  (IdOrder, OrderNumber, DateCreated, IdCustomer, IdPaymentOption, ShippingMethod)
values 
  (2, '2000000', '2016-04-20', 1, 1, 'UPS')

-- Add products to the new order
select * from OrderProducts;
--insert into OrderProducts 

select * from ProductType where IdProductType = 1;
select * from ProductType where not IdProductType = 1;
select * from ProductType where IdProductType in (1, 2);



insert into Product
  (IdProduct, Name, [Description], Price, IdProductType)
  values
    (5, 'Office Chair', 'To save your back from strain', 129.99, 1)

select * from Customer;
insert into Customer
  (FirstName, LastName, StreetAdress, City, StateProvence,PostalCode, PhoneNumber)
values
  ('Jeremy', 'Landi', '40 Go Road', 'Murfreesboro', 'GA', 55500, '403-222-6261')



select
  co.OrderNumber,
  c.FirstName
from CustomerOrder co
right outer join Customer c on co.IdCustomer = c.IdCustomer