-- Required
------------------------------------------------
INSERT INTO AspNetRoles (Id, [Name])
VALUES ('1', 'Admin'), ('2', 'Bidder');
------------------------------------------------
--Optional

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES
('822f401c-49a3-4296-97e6-decc97284d37', '1'),
('4eaab345-e1d4-4c56-9ecb-405959708c8e', '2');

INSERT INTO ProductCategory (CategoryName)
VALUES ('Electronics'), ('Clothes'), ('Shoes'), ('Appliances'), ('Furniture'), ('Toys'), ('Sports');