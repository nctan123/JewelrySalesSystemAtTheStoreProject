-- Insert ReturnBuybackPolicy -- Note(description nên lưu dạng PDF không)
INSERT INTO ReturnBuybackPolicy (description, effective_date, status) VALUES
('Policy 1 Description', '2024-01-01', 'inactive'),
('Policy 2 Description', '2024-03-15', 'inactive'),
('Policy 3 Description', '2024-05-27', 'active');

--Insert Role
INSERT INTO Role (name, description)
VALUES 
('seller', 'Responsible for selling products'),
('admin', 'Administrator with full access to the system'),
('cashier', 'Handles transactions and cash operations'),
('manager', 'Manages staff and operations');

-- Insert Account
INSERT INTO Account (role_id, username, password)
VALUES 
(2, 'admin_user', 'admin_password'),   -- Admin 
(4, 'manager_user1', 'manager_password1'), -- Manager 
(4, 'manager_user2', 'manager_password2'),
(1, 'seller_user1', 'seller_password1'), -- Seller 
(1, 'seller_user2', 'seller_password2'),
(1, 'seller_user3', 'seller_password3'),
(1, 'seller_user4', 'seller_password4'),
(1, 'seller_user5', 'seller_password5'),
(1, 'seller_user6', 'seller_password6'),
(1, 'seller_user7', 'seller_password7'),
(1, 'seller_user8', 'seller_password8'),
(1, 'seller_user9', 'seller_password9'),
(1, 'seller_user10', 'seller_password10'),
(3, 'cashier_user1', 'cashier_password1'), -- Cashier 
(3, 'cashier_user2', 'cashier_password2');

-- Insert Staff
INSERT INTO Staff (account_id, firstname, lastname, phone, email, address, gender)
VALUES
(1, 'John', 'Doe', '1234567890', 'john.doe@example.com', '123 Main St', 'male'),      -- admin
(2, 'Jane', 'Smith', '2345678901', 'jane.smith@example.com', '456 Maple Ave', 'female'),  -- manager
(3, 'Robert', 'Johnson', '3456789012', 'robert.johnson@example.com', '789 Oak St', 'male'),  
(4, 'Emily', 'Davis', '4567890123', 'emily.davis@example.com', '101 Pine Rd', 'female'),  -- seller
(5, 'Michael', 'Brown', '5678901234', 'michael.brown@example.com', '202 Cedar St', 'male'),  
(6, 'Linda', 'Jones', '6789012345', 'linda.jones@example.com', '303 Elm St', 'female'),  
(7, 'William', 'Garcia', '7890123456', 'william.garcia@example.com', '404 Birch St', 'male'),  
(8, 'Elizabeth', 'Martinez', '8901234567', 'elizabeth.martinez@example.com', '505 Spruce St', 'female'),  
(9, 'James', 'Rodriguez', '9012345678', 'james.rodriguez@example.com', '606 Walnut St', 'male'),  
(10, 'Barbara', 'Martinez', '0123456789', 'barbara.martinez@example.com', '707 Palm St', 'female'),  
(11, 'Charles', 'Anderson', '1234509876', 'charles.anderson@example.com', '808 Ash St', 'male'),  
(12, 'Susan', 'Taylor', '2345609876', 'susan.taylor@example.com', '909 Redwood St', 'female'),  
(13, 'Joseph', 'Hernandez', '3456709876', 'joseph.hernandez@example.com', '1110 Pineapple Rd', 'male'),  
(14, 'Jessica', 'Moore', '4567809876', 'jessica.moore@example.com', '1211 Coconut Ave', 'female'),  -- cashier
(15, 'Daniel', 'Thomas', '5678909876', 'daniel.thomas@example.com', '1312 Mango St', 'male');  

--Insert Customer
INSERT INTO Customer (point_id, firstname, lastname, phone, email, gender, address)
VALUES 
(NULL, 'Alice', 'Johnson', '0912345678', 'alice.johnson@example.com', 'female', '123 Main St'),
(NULL, 'Bob', 'Williams', '0923456789', 'bob.williams@example.com', 'male', '456 Maple Ave'),
(NULL, 'Carol', 'Brown', '0934567890', 'carol.brown@example.com', 'female', '789 Oak St'),
(NULL, 'David', 'Jones', '0945678901', 'david.jones@example.com', 'male', '101 Pine Rd'),
(NULL, 'Eva', 'Martinez', '0956789012', 'eva.martinez@example.com', 'female', '202 Cedar St'),
(NULL, 'Frank', 'Garcia', '0967890123', 'frank.garcia@example.com', 'male', '303 Elm St'),
(NULL, 'Grace', 'Rodriguez', '0978901234', 'grace.rodriguez@example.com', 'female', '404 Birch St'),
(NULL, 'Henry', 'Martinez', '0989012345', 'henry.martinez@example.com', 'male', '505 Spruce St'),
(NULL, 'Ivy', 'Anderson', '0990123456', 'ivy.anderson@example.com', 'female', '606 Walnut St'),
(NULL, 'Jack', 'Taylor', '0901234567', 'jack.taylor@example.com', 'male', '707 Palm St'),
(NULL, 'Kelly', 'Hernandez', '0912345098', 'kelly.hernandez@example.com', 'female', '808 Ash St'),
(NULL, 'Leo', 'Moore', '0923456098', 'leo.moore@example.com', 'male', '909 Redwood St'),
(NULL, 'Mia', 'Thomas', '0934567098', 'mia.thomas@example.com', 'female', '1110 Pineapple Rd'),
(NULL, 'Noah', 'Wilson', '0945678098', 'noah.wilson@example.com', 'male', '1211 Coconut Ave'),
(NULL, 'Olivia', 'White', '0956789098', 'olivia.white@example.com', 'female', '1312 Mango St'),
(NULL, 'Peter', 'Walker', '0967890098', 'peter.walker@example.com', 'male', '1413 Apple St'),
(NULL, 'Quinn', 'Harris', '0978901098', 'quinn.harris@example.com', 'female', '1514 Orange St'),
(NULL, 'Ryan', 'Clark', '0989012098', 'ryan.clark@example.com', 'male', '1615 Peach St'),
(NULL, 'Samantha', 'Smith', '0990123098', 'samantha.smith@example.com', 'female', '1716 Plum St'),
(NULL, 'Thomas', 'Davis', '0901234098', 'thomas.davis@example.com', 'male', '1817 Cherry St');

-- Insert Origin
INSERT INTO Origin (name, description)
VALUES 
('natural', 'Naturally sourced'),
('labgrown', 'Artificially grown in a laboratory');

-- Insert data into the Shape table
INSERT INTO Shape (name, description)
VALUES 
('Round', 'Round-shaped diamond with excellent brilliance and sparkle'),
('Princess', 'Square-shaped diamond with sharp, pointed corners'),
('Emerald', 'Rectangular-shaped diamond with step-cut facets'),
('Asscher', 'Similar to Emerald but with a square shape'),
('Marquise', 'Oval-shaped diamond with pointed ends'),
('Oval', 'Elongated oval-shaped diamond with brilliant facets'),
('Radiant', 'Rectangular or square-shaped diamond with brilliant-cut facets'),
('PearHeart', 'Combination of pear and heart-shaped diamond'),
('Cushion', 'Square or rectangular-shaped diamond with rounded corners');

-- Insert data into the Fluorescence table
INSERT INTO Fluorescence (level, description)
VALUES 
('none', 'No fluorescence visible under UV light'),
('faint', 'Slight fluorescence visible under UV light'),
('medium', 'Moderate fluorescence visible under UV light'),
('strong', 'Strong fluorescence visible under UV light');

-- Insert data into the Symmetry table
INSERT INTO Symmetry (level, description)
VALUES 
('poor', 'Significant defects visible to the naked eye'),
('fair', 'Some defects visible under 10x magnification'),
('good', 'Minor defects visible under 10x magnification'),
('very_good', 'Few defects visible under 10x magnification'),
('excellent', 'No visible defects under 10x magnification');

-- Insert data into the Polish table
INSERT INTO Polish (level, description)
VALUES 
('poor', 'Significant polish defects visible to the naked eye'),
('fair', 'Some polish defects visible under 10x magnification'),
('good', 'Minor polish defects visible under 10x magnification'),
('very_good', 'Few polish defects visible under 10x magnification'),
('excellent', 'No visible polish defects under 10x magnification');

-- Insert data into the Color table
INSERT INTO Color (name, description)
VALUES 
('D', 'Colorless'),
('E', 'Near Colorless'),
('F', 'Near Colorless'),
('G', 'Near Colorless'),
('H', 'Near Colorless'),
('I', 'Near Colorless'),
('J', 'Faint Yellow'),
('K', 'Faint Yellow'),
('L', 'Very Light Yellow'),
('M', 'Very Light Yellow'),
('N', 'Very Light Yellow'),
('O', 'Light Yellow'),
('P', 'Light Yellow'),
('Q', 'Light Yellow'),
('R', 'Light Yellow'),
('S', 'Light Yellow'),
('T', 'Light Yellow'),
('U', 'Light Yellow'),
('V', 'Light Yellow'),
('W', 'Light Yellow'),
('X', 'Light Yellow'),
('Y', 'Light Yellow'),
('Z', 'Light Yellow');

-- Insert data into the Cut table
INSERT INTO Cut (level, description)
VALUES 
('poor', 'Significant cut defects affecting brilliance and sparkle'),
('fair', 'Some cut defects affecting brilliance and sparkle'),
('good', 'Minor cut defects, good brilliance and sparkle'),
('very_good', 'Few cut defects, very good brilliance and sparkle'),
('excellent', 'No visible cut defects, excellent brilliance and sparkle');

-- Insert data into the Clarity table
INSERT INTO Clarity (level, description)
VALUES 
('I1', 'Included level 1 - obvious inclusions visible to the naked eye'),
('I2', 'Included level 2 - obvious inclusions visible to the naked eye'),
('I3', 'Included level 3 - obvious inclusions visible to the naked eye'),
('SI1', 'Slightly Included level 1 - inclusions visible under 10x magnification'),
('SI2', 'Slightly Included level 2 - inclusions visible under 10x magnification'),
('VS1', 'Very Slightly Included level 1 - minor inclusions visible under 10x magnification'),
('VS2', 'Very Slightly Included level 2 - minor inclusions visible under 10x magnification'),
('VVS1', 'Very Very Slightly Included level 1 - minute inclusions difficult to see under 10x magnification'),
('VVS2', 'Very Very Slightly Included level 2 - minute inclusions difficult to see under 10x magnification'),
('IF', 'Internally Flawless - no inclusions visible under 10x magnification');

-- Insert data into the Carat table
INSERT INTO Carat (weight, description)
VALUES 
(0.25, 'Quarter carat'),
(0.50, 'Half carat'),
(0.75, 'Three-quarter carat'),
(1.00, 'One carat'),
(1.25, 'One and a quarter carats'),

(1.50, 'One and a half carats'),
(1.75, 'One and three-quarter carats'),
(2.00, 'Two carats'),
(2.50, 'Two and a half carats'),
(3.00, 'Three carats');

-- Insert DiamondPriceList
INSERT INTO DiamondPriceList (color_id, cut_id,origin_id, clarity_id, carat_id, price, effective_date)
VALUES
	--Quarter carat
    (1, 5,1, 6, 1, 18095000, '2024-05-18'), -- D -- VS1
    (2, 5,1, 6, 1, 17625000, '2024-05-18'), -- E
    (3, 5,1, 6, 1, 17155000, '2024-05-18'), -- F
    (4, 5,1, 6, 1, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 1, 18095000, '2024-05-18'), -- D -- VS2
    (2, 5,1, 7, 1, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 1, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 1, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 1, 18095000, '2024-05-18'), -- D -- VVS1
    (2, 5,1, 7, 1, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 1, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 1, 16550000, '2024-05-18'), -- G

	(1, 5,1, 8, 1, 18095000, '2024-05-18'), -- D -- VVS2
    (2, 5,1, 8, 1, 17625000, '2024-05-18'), -- E
    (3, 5,1, 8, 1, 17155000, '2024-05-18'), -- F
    (4, 5,1, 8, 1, 16550000, '2024-05-18'), -- G

	(1, 5,1, 9, 1, 18095000, '2024-05-18'), -- D -- IF
    (2, 5,1, 9, 1, 17625000, '2024-05-18'), -- E
    (3, 5,1, 9, 1, 17155000, '2024-05-18'), -- F
    (4, 5,1, 9, 1, 16550000, '2024-05-18'), -- G
	-- Half carat

	(1, 5,1, 6, 2, 18095000, '2024-05-18'), -- D -- VS1
    (2, 5,1, 6, 2, 17625000, '2024-05-18'), -- E
    (3, 5,1, 6, 2, 17155000, '2024-05-18'), -- F
    (4, 5,1, 6, 2, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 2, 18095000, '2024-05-18'), -- D -- VS2
    (2, 5,1, 7, 2, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 2, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 2, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 2, 18095000, '2024-05-18'), -- D -- VVS1
    (2, 5,1, 7, 2, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 2, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 2, 16550000, '2024-05-18'), -- G

	(1, 5,1, 8, 2, 18095000, '2024-05-18'), -- D -- VVS2
    (2, 5,1, 8, 2, 17625000, '2024-05-18'), -- E
    (3, 5,1, 8, 2, 17155000, '2024-05-18'), -- F
    (4, 5,1, 8, 2, 16550000, '2024-05-18'), -- G

	(1, 5,1, 9, 2, 18095000, '2024-05-18'), -- D -- IF
    (2, 5,1, 9, 2, 17625000, '2024-05-18'), -- E
    (3, 5,1, 9, 2, 17155000, '2024-05-18'), -- F
    (4, 5,1, 9, 2, 16550000, '2024-05-18'), -- G
	--Three-quarter carat

	(1, 5,1, 6, 3, 18095000, '2024-05-18'), -- D -- VS1
    (2, 5,1, 6, 3, 17625000, '2024-05-18'), -- E
    (3, 5,1, 6, 3, 17155000, '2024-05-18'), -- F
    (4, 5,1, 6, 3, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 3, 18095000, '2024-05-18'), -- D -- VS2
    (2, 5,1, 7, 3, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 3, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 3, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 3, 18095000, '2024-05-18'), -- D -- VVS1
    (2, 5,1, 7, 3, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 3, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 3, 16550000, '2024-05-18'), -- G

	(1, 5,1, 8, 3, 18095000, '2024-05-18'), -- D -- VVS2
    (2, 5,1, 8, 3, 17625000, '2024-05-18'), -- E
    (3, 5,1, 8, 3, 17155000, '2024-05-18'), -- F
    (4, 5,1, 8, 3, 16550000, '2024-05-18'), -- G

	(1, 5,1, 9, 3, 18095000, '2024-05-18'), -- D -- IF
    (2, 5,1, 9, 3, 17625000, '2024-05-18'), -- E
    (3, 5,1, 9, 3, 17155000, '2024-05-18'), -- F
    (4, 5,1, 9, 3, 16550000, '2024-05-18'), -- G
	--One carat
	(1, 5,1, 6, 4, 18095000, '2024-05-18'), -- D -- VS1
    (2, 5,1, 6, 4, 17625000, '2024-05-18'), -- E
    (3, 5,1, 6, 4, 17155000, '2024-05-18'), -- F
    (4, 5,1, 6, 4, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 4, 18095000, '2024-05-18'), -- D -- VS2
    (2, 5,1, 7, 4, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 4, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 4, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 4, 18095000, '2024-05-18'), -- D -- VVS1
    (2, 5,1, 7, 4, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 4, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 4, 16550000, '2024-05-18'), -- G

	(1, 5,1, 8, 4, 18095000, '2024-05-18'), -- D -- VVS2
    (2, 5,1, 8, 4, 17625000, '2024-05-18'), -- E
    (3, 5,1, 8, 4, 17155000, '2024-05-18'), -- F
    (4, 5,1, 8, 4, 16550000, '2024-05-18'), -- G

	(1, 5,1, 9, 4, 18095000, '2024-05-18'), -- D -- IF
    (2, 5,1, 9, 4, 17625000, '2024-05-18'), -- E
    (3, 5,1, 9, 4, 17155000, '2024-05-18'), -- F
    (4, 5,1, 9, 4, 16550000, '2024-05-18'), -- G

	--One and a quarter carats
	(1, 5,1, 6, 5, 18095000, '2024-05-18'), -- D -- VS1
    (2, 5,1, 6, 5, 17625000, '2024-05-18'), -- E
    (3, 5,1, 6, 5, 17155000, '2024-05-18'), -- F
    (4, 5,1, 6, 5, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 5, 18095000, '2024-05-18'), -- D -- VS2
    (2, 5,1, 7, 5, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 5, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 5, 16550000, '2024-05-18'), -- G

	(1, 5,1, 7, 5, 18095000, '2024-05-18'), -- D -- VVS1
    (2, 5,1, 7, 5, 17625000, '2024-05-18'), -- E
    (3, 5,1, 7, 5, 17155000, '2024-05-18'), -- F
    (4, 5,1, 7, 5, 16550000, '2024-05-18'), -- G

	(1, 5,1, 8, 5, 18095000, '2024-05-18'), -- D -- VVS2
    (2, 5,1, 8, 5, 17625000, '2024-05-18'), -- E
    (3, 5,1, 8, 5, 17155000, '2024-05-18'), -- F
    (4, 5,1, 8, 5, 16550000, '2024-05-18'), -- G

	(1, 5,1, 9, 5, 18095000, '2024-05-18'), -- D -- IF
    (2, 5,1, 9, 5, 17625000, '2024-05-18'), -- E
    (3, 5,1, 9, 5, 17155000, '2024-05-18'), -- F
    (4, 5,1, 9, 5, 16550000, '2024-05-18'); -- G

-- Insert Diamond 
INSERT INTO Diamond (code, name, origin_id, shape_id, fluorescence_id, color_id, symmetry_id, polish_id, cut_id, clarity_id, carat_id)
VALUES 
('DIA001', 'Natural-D-Round-VS1', 1, 1, 1, 1, 5, 5, 5, 6, 3),
('DIA002', 'Natural-E-Princess-VS2', 1, 2, 2, 2, 5, 5, 5, 7, 4),
('DIA003', 'Natural-F-Emerald-VS1', 1, 3, 3, 3, 5, 5, 5, 6, 5),
('DIA004', 'Natural-G-Asscher-VS2', 1, 4, 4, 4, 5, 5, 5, 7, 1),
('DIA005', 'Natural-H-Marquise-VVS1', 1, 5, 1, 5, 5, 5, 5, 8, 2),
('DIA006', 'Natural-I-Oval-VS2', 1, 6, 2, 6, 5, 5, 5, 7, 3),
('DIA007', 'Natural-J-Radiant-VVS1', 1, 7, 3, 7, 5, 5, 5, 8, 4),
('DIA008', 'Natural-K-PearHeart-VS2', 1, 8, 4, 8, 5, 5, 5, 7, 5),
('DIA009', 'Natural-L-Cushion-IF', 1, 9, 1, 9, 5, 5, 5, 9, 1),
('DIA010', 'Labgrown-D-Round-VS1', 2, 1, 2, 1, 5, 5, 5, 6, 2),
('DIA011', 'Labgrown-E-Princess-VS2', 2, 2, 3, 2, 5, 5, 5, 7, 3),
('DIA012', 'Labgrown-F-Emerald-VVS1', 2, 3, 4, 3, 5, 5, 5, 8, 4),
('DIA013', 'Labgrown-G-Asscher-VS2', 2, 4, 1, 4, 5, 5, 5, 7, 5),
('DIA014', 'Labgrown-H-Marquise-VVS1', 2, 5, 2, 5, 5, 5, 5, 8, 1),
('DIA015', 'Labgrown-I-Oval-VS2', 2, 6, 3, 6, 5, 5, 5, 7, 2),
('DIA016', 'Labgrown-J-Radiant-VVS1', 2, 7, 4, 7, 5, 5, 5, 8, 3),
('DIA017', 'Labgrown-K-PearHeart-VS2', 2, 8, 1, 8, 5, 5, 5, 7, 4),
('DIA018', 'Labgrown-L-Cushion-IF', 2, 9, 2, 9, 5, 5, 5, 9, 5),
('DIA019', 'Natural-M-Princess-SI1', 1, 2, 3, 10, 5, 5, 5, 5, 2),
('DIA020', 'Labgrown-N-Princess-SI2', 2, 2, 4, 11, 5, 5, 5, 6, 3);


-- Insert ProductCategoryType
INSERT INTO ProductCategoryType (name)
VALUES 
('diamonds'), 
('jewelry'), 
('retail gold'), 
('wholesale gold');


INSERT INTO dbo.Promotion
(
    name,
    discount_rate,
    description,
    start_date,
    end_date,
    status
)
VALUES
(   
    'Glamorous Jewelry Sale',  -- name - varchar(100)
    25,         -- discount_rate - int
    'Empty',      -- description - text
    '2024-01-01', -- start_date - date
    DATEADD(year, 1, '2024-01-01'), -- end_date - date
    DEFAULT    -- status - varchar(10)
),
(   
    'Golden Harvest Sale',  -- name - varchar(100)
    10,         -- discount_rate - int
    'Empty',      -- description - text
    '2023-08-01', -- start_date - date
    DATEADD(year, 1, '2024-01-01'), -- end_date - date
    DEFAULT    -- status - varchar(10)
),
(   
    'Diamond Delight Sale',  -- name - varchar(100)
    15,         -- discount_rate - int
    'Empty',      -- description - text
    '2023-08-01', -- start_date - date
    DATEADD(year, 1, '2024-01-01'), -- end_date - date
    DEFAULT    -- status - varchar(10)
)
;


INSERT INTO ProductCategory (name, type_id, status) VALUES 
('Ring', 2, 'active'),
('Earrings', 2, 'active'),
('Bracelet', 2, 'active'),
('Necklace', 2, 'active'),
('Retail gold', 3, 'active'),
('Whole gold', 4, 'active'),
('Diamonds', 1, 'active')

INSERT INTO PromotionCategory (promotion_id, category_id)
VALUES 
    (1, 1),  -- Glamorous Jewelry Sale - Ring
    (1, 2),  -- Glamorous Jewelry Sale - Earrings
    (1, 3),  -- Glamorous Jewelry Sale - Bracelet
    (1, 4),  -- Glamorous Jewelry Sale - Necklace
    (2, 5),  -- Golden Harvest Sale - Retail gold
    (2, 6),  -- Golden Harvest Sale - Whole gold
    (3, 7);  -- Diamond Delight Sale - Earrings

--Insert StallType
INSERT INTO StallType (name)
VALUES 
('diamonds'),
('jewelry'),
('retail gold'),
('wholesale gold'),
('counter');

--Insert Stall
INSERT INTO Stall (name, type_id, description, status) VALUES
('Stall 1', 1, 'Description of Stall 1', 'active'),
('Stall 2', 1, 'Description of Stall 2', 'active'),
('Stall 3', 2, 'Description of Stall 3', 'active'),
('Stall 4', 2, 'Description of Stall 4', 'active'),
('Stall 5', 3, 'Description of Stall 5', 'active'),
('Stall 6', 4, 'Description of Stall 6', 'active'),
('Stall 7', 5, 'Description of Stall 7', 'active'),
('Stall 8', 5, 'Description of Stall 8', 'active'),
('Stall 9', 1, 'Description of Stall 9', 'active'),
('Stall 10', 1, 'Description of Stall 10', 'active');

--Insert Material
INSERT INTO Material (name)
VALUES 
('24K Gold'),
('18K Gold'),
('14K Gold'),
('10K Gold'),
('White Gold');

-- Insert MaterialPriceList
INSERT INTO MateriaPriceList(material_id, buy_price, sell_price, effective_date)
VALUES 
(1, 5500000.00, 5600000.00, '2024-05-27'),  -- 24K Gold
(2, 4200000.00, 4300000.00, '2024-05-27'),  -- 18K Gold
(3, 3200000.00, 3300000.00, '2024-05-27'),  -- 14K Gold
(4, 2700000.00, 2800000.00, '2024-05-27'),  -- 10K Gold
(5, 4800000.00, 4900000.00, '2024-05-27');  -- White Gold

-- Insert MaterialDetail
INSERT INTO MaterialDetail (material_id, quantity_in_stock)
VALUES 
(1, 100),  -- 24K Gold
(2, 200),  -- 18K Gold
(3, 150),  -- 14K Gold
(4, 80),   -- 10K Gold
(5, 120);  -- White Gold
