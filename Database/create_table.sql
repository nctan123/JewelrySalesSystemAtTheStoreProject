IF DB_ID('JSSATS') IS NULL
BEGIN
    CREATE DATABASE JSSATS
END
GO
USE JSSATS


-- Table for ProductCategory types
CREATE TABLE ProductCategoryType (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(50) UNIQUE NOT NULL,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive'))
);

-- Table for Stall types
CREATE TABLE StallType (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(50) UNIQUE NOT NULL,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive'))
);

CREATE TABLE ProductCategory (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  type_id INT NOT NULL,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive')),
  FOREIGN KEY (type_id) REFERENCES ProductCategoryType(id)
);

CREATE TABLE Stall (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  type_id INT NOT NULL,
  description TEXT,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive')),
  FOREIGN KEY (type_id) REFERENCES StallType(id)
);
    --***End of new table***

CREATE TABLE Material (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100) NOT NULL
);

CREATE TABLE Role (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  description TEXT,
  CONSTRAINT unique_role_name UNIQUE (name)
);

CREATE TABLE Point (
  id INT IDENTITY(1,1) PRIMARY KEY,
  available_point INT DEFAULT 0,
  totalpoint INT DEFAULT 0
);

CREATE TABLE PaymentMethod (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive'))
);




CREATE TABLE Product (
  id INT IDENTITY(1,1) PRIMARY KEY,
  category_id INT NOT NULL,
  stalls_id INT,
  code VARCHAR(50) UNIQUE,
  name VARCHAR(100) NOT NULL,
  material_cost DECIMAL(10,2),
  production_cost DECIMAL(10,2),
  gem_cost DECIMAL(10,2),
  img VARCHAR(255),
  price_rate DECIMAL(10,2) NOT NULL,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive')),
  FOREIGN KEY (category_id) REFERENCES ProductCategory(id),
  FOREIGN KEY (stalls_id) REFERENCES Stall(id)
);

CREATE TABLE Customer (
  id INT IDENTITY(1,1) PRIMARY KEY,
  point_id INT,
  firstname VARCHAR(50) NOT NULL,
  lastname VARCHAR(50) NOT NULL,
  phone VARCHAR(20) NOT NULL UNIQUE,
  email VARCHAR(100) NOT NULL UNIQUE,
  gender VARCHAR(10) CHECK (gender IN ('male', 'female', 'other')),
  address TEXT,
  FOREIGN KEY (point_id) REFERENCES Point(id)
);

CREATE TABLE Account (
  id INT IDENTITY(1,1) PRIMARY KEY,
  role_id INT NOT NULL,
  username VARCHAR(100) NOT NULL UNIQUE,
  password VARCHAR(255) NOT NULL,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive', 'suspended')),
  FOREIGN KEY (role_id) REFERENCES Role(id)
);

CREATE TABLE Staff (
  id INT IDENTITY(1,1) PRIMARY KEY,
  account_id INT NOT NULL UNIQUE,
  firstname VARCHAR(50) NOT NULL,
  lastname VARCHAR(50) NOT NULL,
  phone VARCHAR(20) NOT NULL UNIQUE,
  email VARCHAR(100) NOT NULL UNIQUE,
  address TEXT,
  gender VARCHAR(10) CHECK (gender IN ('male', 'female', 'other')),
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive')),
  FOREIGN KEY (account_id) REFERENCES Account(id)
);

CREATE TABLE ProductMaterial (
  material_id INT NOT NULL,
  product_id INT NOT NULL,
  weight DECIMAL(10,2),
  PRIMARY KEY (material_id, product_id),
  FOREIGN KEY (material_id) REFERENCES Material(id),
  FOREIGN KEY (product_id) REFERENCES Product(id)
);

CREATE TABLE MaterialPriceList (
  id INT IDENTITY(1,1) PRIMARY KEY,
  material_id INT NOT NULL,
  buy_price DECIMAL(10,2) NOT NULL,
  sell_price DECIMAL(10,2) NOT NULL,
  effective_date DATETIME NOT NULL,
  FOREIGN KEY (material_id) REFERENCES Material(id)
);


CREATE TABLE Origin (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  description TEXT
);

CREATE TABLE Shape (
  id INT PRIMARY KEY IDENTITY(1,1),
  name VARCHAR(100) NOT NULL,
  description TEXT
);

CREATE TABLE Fluorescence (
  id INT PRIMARY KEY IDENTITY(1,1),
  level VARCHAR(50) NOT NULL,
  description TEXT
);

CREATE TABLE Symmetry (
  id INT PRIMARY KEY IDENTITY(1,1),
  level VARCHAR(50) NOT NULL,
  description TEXT
);

CREATE TABLE Polish (
  id INT PRIMARY KEY IDENTITY(1,1),
  level VARCHAR(50) NOT NULL,
  description TEXT
);

CREATE TABLE Color (
  id INT PRIMARY KEY IDENTITY(1,1),
  name VARCHAR(50) NOT NULL,
  description TEXT
);

CREATE TABLE Cut (
  id INT PRIMARY KEY IDENTITY(1,1),
  level VARCHAR(50) NOT NULL,
  description TEXT
);

CREATE TABLE Clarity (
  id INT PRIMARY KEY IDENTITY(1,1),
  level VARCHAR(50) NOT NULL,
  description TEXT
);

CREATE TABLE Carat (
  id INT PRIMARY KEY IDENTITY(1,1),
  weight DECIMAL(5,2) NOT NULL,
  description TEXT
);

CREATE TABLE Diamond (
  id INT PRIMARY KEY IDENTITY(1,1),
  code VARCHAR(50) UNIQUE NOT NULL,
  name VARCHAR(100) NOT NULL,
  origin_id INT NOT NULL,
  shape_id INT NOT NULL,
  fluorescence_id INT NOT NULL,
  color_id INT NOT NULL,
  symmetry_id INT NOT NULL,
  polish_id INT NOT NULL,
  cut_id INT NOT NULL,
  clarity_id INT NOT NULL,
  carat_id INT NOT NULL,
  status VARCHAR(10) DEFAULT 'active',
  FOREIGN KEY (origin_id) REFERENCES Origin(id),
  FOREIGN KEY (shape_id) REFERENCES Shape(id),
  FOREIGN KEY (fluorescence_id) REFERENCES Fluorescence(id),
  FOREIGN KEY (color_id) REFERENCES Color(id),
  FOREIGN KEY (symmetry_id) REFERENCES Symmetry(id),
  FOREIGN KEY (polish_id) REFERENCES Polish(id),
  FOREIGN KEY (cut_id) REFERENCES Cut(id),
  FOREIGN KEY (clarity_id) REFERENCES Clarity(id),
  FOREIGN KEY (carat_id) REFERENCES Carat(id)
);

CREATE TABLE DiamondPriceList (
  id INT PRIMARY KEY IDENTITY(1,1),
  color_id INT NOT NULL,
  cut_id INT NOT NULL,
  origin_id INT NOT NULL,
  clarity_id INT NOT NULL,
  carat_id INT NOT NULL,
  price DECIMAL(12,2) NOT NULL,
  effective_date DATETIME NOT NULL,
  FOREIGN KEY (color_id) REFERENCES Color(id),
  FOREIGN KEY (cut_id) REFERENCES Cut(id),
  FOREIGN KEY (origin_id) REFERENCES Origin(id),
  FOREIGN KEY (clarity_id) REFERENCES Clarity(id),
  FOREIGN KEY (carat_id) REFERENCES Carat(id)
);

CREATE TABLE [Order] (
  id INT PRIMARY KEY IDENTITY(1,1),
  customer_id INT NOT NULL,
  staff_id INT NOT NULL,
  total_amount DECIMAL(10,2) NOT NULL,
  create_date DATETIME NOT NULL,
  is_draft BIT NOT NULL DEFAULT 1,
  type VARCHAR(50) DEFAULT 'purchase',
  description TEXT,
  FOREIGN KEY (customer_id) REFERENCES Customer(id),
  FOREIGN KEY (staff_id) REFERENCES Staff(id)
);



CREATE TABLE Payment (
  id INT PRIMARY KEY IDENTITY(1,1),
  order_id INT NOT NULL,
  customer_id INT NOT NULL,
  create_date DATETIME NOT NULL,
  amount DECIMAL(10,2) NOT NULL,
  status VARCHAR(50) DEFAULT 'processing',
  FOREIGN KEY (order_id) REFERENCES [Order](id),
  FOREIGN KEY (customer_id) REFERENCES Customer(id)
);

CREATE TABLE PaymentDetails (
  id INT PRIMARY KEY IDENTITY(1,1),
  payment_id INT NOT NULL,
  payment_method_id INT NOT NULL,
  amount DECIMAL(10,2) NOT NULL,
  ExternalTransactionCode VARCHAR(100),
  status VARCHAR(50) NOT NULL,
  FOREIGN KEY (payment_id) REFERENCES Payment(id),
  FOREIGN KEY (payment_method_id) REFERENCES PaymentMethod(id)
);



CREATE TABLE OrderDetails (
  id INT PRIMARY KEY IDENTITY(1,1),
  order_id INT NOT NULL,
  product_id INT NOT NULL,
  size VARCHAR(50),
  unit_price DECIMAL(10,2) NOT NULL,
  quantity INT NOT NULL,
  size_price DECIMAL(10,2) NOT NULL,
  status VARCHAR(50) DEFAULT 'delivered',
  FOREIGN KEY (order_id) REFERENCES [Order](id),
  FOREIGN KEY (product_id) REFERENCES Product(id)
);

CREATE TABLE Promotion (
  id INT PRIMARY KEY IDENTITY(1,1),
  name VARCHAR(100) NOT NULL,
  discount_rate INT NOT NULL,
  description TEXT,
  start_date DATETIME NOT NULL,
  end_date DATETIME NOT NULL,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive'))
);

CREATE TABLE Guarantee (
  id INT PRIMARY KEY IDENTITY(1,1),
  product_id INT NOT NULL,
  description TEXT NOT NULL,
  effective_date DATETIME NOT NULL,
  expired_date DATETIME NOT NULL,
  FOREIGN KEY (product_id) REFERENCES Product(id)
);

CREATE TABLE PromotionCategory (
  promotion_id INT NOT NULL,
  category_id INT NOT NULL,
  PRIMARY KEY (promotion_id, category_id),
  FOREIGN KEY (promotion_id) REFERENCES Promotion(id),
  FOREIGN KEY (category_id) REFERENCES ProductCategory(id)
);

CREATE TABLE ProductDiamond (
  product_id INT NOT NULL,
  diamond_id INT NOT NULL,
  PRIMARY KEY (product_id, diamond_id),
  FOREIGN KEY (product_id) REFERENCES Product(id),
  FOREIGN KEY (diamond_id) REFERENCES Diamond(id)
);

CREATE TABLE ReturnBuyBackPolicy (
  id INT PRIMARY KEY IDENTITY(1,1),
  description TEXT NOT NULL,
  effective_date DATETIME NOT NULL,
  status VARCHAR(10) DEFAULT 'active' CHECK (status IN ('active', 'inactive'))
);
