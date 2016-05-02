CREATE TABLE [dbo].[Customer] (
    [IdCustomer]    INT          NOT NULL,
    [FirstName]     VARCHAR (55) NULL,
    [LastName]      VARCHAR (55) NULL,
    [StreetAddress] VARCHAR (55) NULL,
    [City]          VARCHAR (55) NULL,
    [StateProvince] VARCHAR (55) NULL,
    [PostalCode]    INT          NULL,
    [PhoneNumber]   VARCHAR (12) NULL,
    PRIMARY KEY CLUSTERED ([IdCustomer] ASC)
);

CREATE TABLE [dbo].[CustomerOrder] (
    [IdCustomerOrder] INT          NOT NULL,
    [IdProduct]       INT          NULL,
    [OrderNumber]     INT          NULL,
    [DateCreated]     DATE         NULL,
    [IdCustomer]      INT          NULL,
    [PaymentType]     VARCHAR (55) NULL,
    [Shipping]        VARCHAR (55) NULL,
    [IdPaymentOption] INT          NULL,
    PRIMARY KEY CLUSTERED ([IdCustomerOrder] ASC),
);


CREATE TABLE [dbo].[OrderProducts] (
    [IdOrderProducts] INT NOT NULL,
    [IdProduct]       INT NOT NULL,
    [IdCustomerOrder] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdOrderProducts] ASC),
);


CREATE TABLE [dbo].[PaymentOption] (
    [IdPaymentOption] INT          NOT NULL,
    [IdCustomer]      INT          NULL,
    [Name]            VARCHAR (50) NULL,
    [AccountNumber]   INT          NULL,
    PRIMARY KEY CLUSTERED ([IdPaymentOption] ASC),
);


CREATE TABLE [dbo].[Product] (
    [IdProduct]     INT           NOT NULL,
    [Name]          VARCHAR (55)  NOT NULL,
    [Description]   VARCHAR (255) NOT NULL,
    [Price]         REAL          NOT NULL,
    [IdProductType] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProduct] ASC),
);

 
CREATE TABLE [dbo].[ProductType] (
    [IdProductType] INT           NOT NULL,
    [Name]          VARCHAR (55)  NOT NULL,
    [Description]   VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProductType] ASC)
);

ALTER TABLE CustomerOrder 
 ADD CONSTRAINT [FK_CustomerOrder_IdProduct] 
 FOREIGN KEY ([IdProduct]) 
 REFERENCES [dbo].[Product] ([IdProduct]);

ALTER TABLE CustomerOrder
 ADD CONSTRAINT [FK_CustomerOrder_IdCustomer]
 FOREIGN KEY ([IdCustomer]) 
 REFERENCES [dbo].[Customer] ([IdCustomer]);

ALTER TABLE CustomerOrder
 ADD CONSTRAINT [FK_CustomerOrder_IdCustomer]
 FOREIGN KEY ([IdPaymentOption]) 
 REFERENCES [dbo].[PaymentOption] ([IdPaymentOption]);

ALTER TABLE OrderProducts
 ADD CONSTRAINT [FK_OrderProducts_Product] 
 FOREIGN KEY ([IdProduct]) 
 REFERENCES [dbo].[Product] ([IdProduct]);

ALTER TABLE OrderProducts
 ADD CONSTRAINT [FK_OrderProducts_CustomerOrder] 
 FOREIGN KEY ([IdCustomerOrder]) 
 REFERENCES [dbo].[CustomerOrder] ([IdCustomerOrder]);

ALTER TABLE PaymentOption
 ADD CONSTRAINT [FK_PaymentOption_Customer] 
 FOREIGN KEY ([IdCustomer]) 
 REFERENCES [dbo].[Customer] ([IdCustomer]);
 
ALTER TABLE Product
 ADD CONSTRAINT [FK_Product_ProductType] 
 FOREIGN KEY ([IdProductType]) 
 REFERENCES [dbo].[ProductType] ([IdProductType]);