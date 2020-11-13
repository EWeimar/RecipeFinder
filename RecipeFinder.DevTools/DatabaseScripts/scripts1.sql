DROP TABLE IF exists Users;
DROP TABLE IF exists RecipeComment;
DROP TABLE IF exists UserNotification;
DROP TABLE IF exists UserFavorite;
DROP TABLE IF exists Recipe;
DROP TABLE IF exists RecipeRating;
DROP TABLE IF exists RecipeImage;
DROP TABLE IF exists Image;
DROP TABLE IF exists IngredientLine;
DROP TABLE IF exists Ingredient;

/* Table user */
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY (1, 1),
    Username VARCHAR (155) UNIQUE NOT NULL,
    Email VARCHAR (155) UNIQUE NOT NULL,
    Password VARCHAR (155) NOT NULL,
    IsAdmin BIT NOT NULL DEFAULT (0),
    CreatedAt DATETIME
);

/* Table Recipe */
CREATE TABLE Recipe (
    RecipeId INT PRIMARY KEY IDENTITY (1, 1),
    Title VARCHAR (155) NOT NULL,
    Instructions NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME
);

/* Table Image */
CREATE TABLE Image (
    ImageId INT PRIMARY KEY IDENTITY (1, 1),
    FileName VARCHAR (155) NOT NULL,
);

/* Table Ingredient */
CREATE TABLE Ingredient (
    IngredientId INT PRIMARY KEY IDENTITY (1, 1),
    Name VARCHAR (155) NOT NULL
);


/* Table RecipeRating */
CREATE TABLE RecipeRating (
    UserId INT NOT NULL,
    RecipeId INT NOT NULL,
    Value INT NOT NULL DEFAULT (0)
    FOREIGN KEY (RecipeId) REFERENCES Recipe (RecipeId),
    FOREIGN KEY (UserId) REFERENCES Users (UserId)
);

/* Table RecipeImage */
CREATE TABLE RecipeImage (
    RecipeId INT NOT NULL,
    ImageId INT NOT NULL,
    FOREIGN KEY (RecipeId) REFERENCES Recipe (RecipeId),
    FOREIGN KEY (ImageId) REFERENCES Image (ImageId)
);

/* Table RecipeComment */
CREATE TABLE RecipeComment (
    RecipeId INT NOT NULL,
    UserId INT NOT NULL,
    CreatedAt DATETIME,
    Text NVARCHAR(255) NOT NULL,
    UNIQUE (RecipeId, UserId, CreatedAt),
    FOREIGN KEY (RecipeId) REFERENCES Recipe (RecipeId),
    FOREIGN KEY (UserId) REFERENCES Users (UserId)
);

/* Table UserNotification */
CREATE TABLE UserNotification (
    NotificationId INT PRIMARY KEY IDENTITY (1, 1),
    UserId INT NOT NULL,
    Message NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users (UserId)
);

/* Table UserFavorite */
CREATE TABLE UserFavorite (
    UserId INT NOT NULL,
    RecipeId INT NOT NULL
    FOREIGN KEY (RecipeId) REFERENCES Recipe (RecipeId),
    FOREIGN KEY (UserId) REFERENCES Users (UserId)
);

/* Table IngredientLine */
CREATE TABLE IngredientLine (
    RecipeId INT NOT NULL,
    IngredientId INT NOT NULL,
    Amount DECIMAL(5,2),
    MeasureUnit INT NOT NULL,
    FOREIGN KEY (RecipeId) REFERENCES Recipe (RecipeId),
    FOREIGN KEY (IngredientId) REFERENCES Ingredient (IngredientId)
);
