DROP TABLE IF exists Users;
DROP TABLE IF exists RecipeReview;
DROP TABLE IF exists UserNotification;
DROP TABLE IF exists UserFavorite;
DROP TABLE IF exists Recipe;
DROP TABLE IF exists RecipeImage;
DROP TABLE IF exists Image;
DROP TABLE IF exists IngredientLine;
DROP TABLE IF exists Ingredient;

/* Table user */
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    Username VARCHAR (155) UNIQUE NOT NULL,
    Email VARCHAR (155) UNIQUE NOT NULL,
    Password VARCHAR (155) NOT NULL,
    IsAdmin BIT NOT NULL DEFAULT (0),
    CreatedAt DATETIME
);

/* Table Recipe */
CREATE TABLE Recipe (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    UserId INT NOT NULL,
    Title VARCHAR (155) NOT NULL,
    Slug VARCHAR (155) NOT NULL,
    Instructions NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users (Id)
);

/* Table Image */
CREATE TABLE Image (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    FileName VARCHAR (155) NOT NULL
);

/* Table Ingredient */
CREATE TABLE Ingredient (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    Name VARCHAR (155) NOT NULL
);

/* Table RecipeImage */
CREATE TABLE RecipeImage (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    RecipeId INT NOT NULL,
    ImageId INT NOT NULL,
    FOREIGN KEY (RecipeId) REFERENCES Recipe (Id),
    FOREIGN KEY (ImageId) REFERENCES Image (Id)
);

/* Table RecipeComment */
CREATE TABLE RecipeReview (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    RecipeId INT NOT NULL,
    UserId INT NOT NULL,
    Reviewer VARCHAR (155) NOT NULL,
    CreatedAt DATETIME,
    Comment NVARCHAR(255) NOT NULL,
    Rating INT NOT NULL,
    UNIQUE (RecipeId, CreatedAt),
    FOREIGN KEY (RecipeId) REFERENCES Recipe (RecipeId),
    FOREIGN KEY (UserId) REFERENCES Users (UserId)
);

/* Table UserNotification */
CREATE TABLE UserNotification (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    UserId INT NOT NULL,
    Message NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users (Id)
);

/* Table UserFavorite */
CREATE TABLE UserFavorite (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    UserId INT NOT NULL,
    RecipeId INT NOT NULL
    FOREIGN KEY (RecipeId) REFERENCES Recipe (Id),
    FOREIGN KEY (UserId) REFERENCES Users (Id)
);

/* Table IngredientLine */
CREATE TABLE IngredientLine (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    RecipeId INT NOT NULL,
    IngredientId INT NOT NULL,
    Amount DECIMAL(5,2),
    MeasureUnit INT NOT NULL,
    FOREIGN KEY (RecipeId) REFERENCES Recipe (Id),
    FOREIGN KEY (IngredientId) REFERENCES Ingredient (Id)
);
