create database TE;
go

use TE;
go

CREATE TABLE Transport (
    transportID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
    descriptionTransport VARCHAR(50) NOT NULL,
    capacityTransport DECIMAL NOT NULL,
    stateTransport INT NOT NULL
);

CREATE TABLE StorageType (
    storageTypeID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
    descriptionStorage VARCHAR(500) NOT NULL,
    stateStorageType INT NOT NULL
);

CREATE TABLE Storage (
    storageID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
    storageTypeID UNIQUEIDENTIFIER NOT NULL,
    nameStorage VARCHAR(100) NOT NULL,
    capacityStorage DECIMAL NOT NULL,
    location VARCHAR(100) NOT NULL,
    stateStorage INT NOT NULL,
    FOREIGN KEY (storageTypeID) REFERENCES StorageType (storageTypeID)
);

CREATE TABLE Product (
    productID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
    transportID UNIQUEIDENTIFIER NOT NULL,
    nameProduct VARCHAR(100) NOT NULL,
    descriptionProduct VARCHAR(500) NOT NULL,
    stateProduct INT NOT NULL,
    FOREIGN KEY (transportID) REFERENCES Transport (transportID)
);

CREATE TABLE Users (
    userID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
	uidUser VARCHAR(40) NOT NULL,
    nameUser VARCHAR(100) NOT NULL,
    identification VARCHAR(40) NOT NULL,
    phone VARCHAR(20) NOT NULL,
	email VARCHAR(50) NOT NULL,
    address VARCHAR(50) NOT NULL,
	typeUser INT NOT NULL,
    stateUser INT NOT NULL
);

CREATE TABLE Logistic (
    logisticID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
    productID UNIQUEIDENTIFIER NOT NULL,
    userID UNIQUEIDENTIFIER NOT NULL,
    storageID UNIQUEIDENTIFIER NOT NULL,
    registeredAt DATETIME NOT NULL,
    deliveredAt DATETIME,
    quantityProduct DECIMAL NOT NULL,
	shippingPrice DECIMAL NOT NULL,
    discount DECIMAL(20,2) NOT NULL,
    numberPlate VARCHAR(6),
    fleetNumber VARCHAR(8),
    guideNumber VARCHAR(10) UNIQUE NOT NULL,
    stateLogistic INT NOT NULL,
    FOREIGN KEY (productID) REFERENCES Product (productID),
    FOREIGN KEY (userID) REFERENCES Users (userID),
    FOREIGN KEY (storageID) REFERENCES Storage (storageID),
	CHECK (LEN(ISNULL(numberPlate, '')) = 0 OR numberPlate LIKE '[A-Z][A-Z][A-Z][0-9][0-9][0-9]'),
    CHECK (LEN(ISNULL(fleetNumber, '')) = 0 OR fleetNumber LIKE '[A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9][A-Z]'),
    CHECK (guideNumber LIKE '[A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9]')
);

-- Insertar datos en la tabla Transport
INSERT INTO Transport (transportID, descriptionTransport, capacityTransport, stateTransport)
VALUES ('EB08F77A-E6DD-48AB-8141-42E4A92D1816', 'Terrestre', 20, 1);
INSERT INTO Transport (transportID, descriptionTransport, capacityTransport, stateTransport)
VALUES ('28C389A5-655B-45F7-8E52-8FDDAC76440F', 'Maritimo', 50, 1);

-- Insertar datos en la tabla StorageType
INSERT INTO StorageType (storageTypeID, descriptionStorage, stateStorageType)
VALUES ('12B19F03-2FB4-48F0-98F9-4CD68F0EF4D2', 'Bodega', 1);
INSERT INTO StorageType (storageTypeID, descriptionStorage, stateStorageType)
VALUES ('0303F8EE-21ED-4170-A0EB-A09E63E4C477', 'Puerto', 1);

-- Insertar datos en la tabla Storage
INSERT INTO Storage (storageID, storageTypeID, nameStorage, capacityStorage, location, stateStorage)
VALUES ('F1C19F96-E8D3-4602-A43A-A7A05C5BB37E', '12B19F03-2FB4-48F0-98F9-4CD68F0EF4D2', 'Almacén Principal TE', 5000, 'Ubicación 123', 1);
INSERT INTO Storage (storageID, storageTypeID, nameStorage, capacityStorage, location, stateStorage)
VALUES ('027213B7-156D-4BD2-B687-45BD5A5BD91E', '0303F8EE-21ED-4170-A0EB-A09E63E4C477', 'Puerto Buenaventura TE', 10000, 'Ubicación 987', 1);

-- Insertar datos en la tabla Product
INSERT INTO Product (productID, transportID, nameProduct, descriptionProduct, stateProduct)
VALUES ('84EBBE40-0AD4-41C5-8CDD-9E34BDF8BD8C', 'EB08F77A-E6DD-48AB-8141-42E4A92D1816', 'Producto A Bodega', 'Descripción del Producto A', 1);
INSERT INTO Product (productID, transportID, nameProduct, descriptionProduct, stateProduct)
VALUES ('52418EC0-AB7C-477A-A49E-7F2EAA6E44CB', '28C389A5-655B-45F7-8E52-8FDDAC76440F', 'Producto A Puerto', 'Descripción del Producto A', 1);

-- Insertar datos en la tabla Client
INSERT INTO Users (userID, uidUser, nameUser, identification, phone, email, address, typeUser, stateUser)
VALUES ('7616B0C3-D518-47C4-B716-FD12031F0E7D', 'N7daGI7budgGdWHswJUpBmS2XJw1','Cliente A', '123456789', '5551234', 'ClienteA@test.com', 'Dirección 456', 1, 1);
INSERT INTO Users (userID, uidUser, nameUser, identification, phone, email, address, typeUser ,stateUser)
VALUES ('4592F69E-A6F1-4546-9F97-E906E58350C7', 'N7daGI7budgGdWHswJUpBmS2XJw2', 'Admin A', '098765432','9995432', 'AdminA@test.com', 'Dirección 987', 2, 1);

-- Insertar datos en la tabla Logistic
INSERT INTO Logistic (logisticID, productID, userID, storageID, registeredAt, deliveredAt, quantityProduct, shippingPrice, discount, numberPlate, fleetNumber, guideNumber, stateLogistic)
VALUES ('F75D5C79-5873-40E8-8479-B1C18485D947', '84EBBE40-0AD4-41C5-8CDD-9E34BDF8BD8C', '7616B0C3-D518-47C4-B716-FD12031F0E7D', 'F1C19F96-E8D3-4602-A43A-A7A05C5BB37E', GETDATE(), NULL, 5, 1000, 0, 'ABC123', NULL, 'AB1234567Z', 1);
INSERT INTO Logistic (logisticID, productID, userID, storageID, registeredAt, deliveredAt, quantityProduct, shippingPrice, discount, numberPlate, fleetNumber, guideNumber, stateLogistic)
VALUES ('6D1003D1-4EB1-48DF-9750-2E226998CE3F', '52418EC0-AB7C-477A-A49E-7F2EAA6E44CB', '7616b0c3-d518-47c4-b716-fd12031f0e7d', '027213B7-156D-4BD2-B687-45BD5A5BD91E', GETDATE(), NULL, 20, 2000, 60, NULL, 'XYZ5678Z', 'JA7A1234S8', 1);
