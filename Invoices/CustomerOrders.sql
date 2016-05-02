SELECT c.FirstName, c.LastName, co.OrderNumber
FROM Customer c
JOIN CustomerOrder co
ON c.IdCustomer = co.IdCustomer


insert into Customer (FirstName, LastName, StreetAdress, City, StateProvence, PostalCode, PhoneNumber)
values ('Bob', 'Smith', '1243', 'Nashville', 'TN', '35262', '235-255-5555')
