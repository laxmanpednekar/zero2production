/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--tblCompanies
INSERT INTO tblCompanies 
            (Id,Name,Address,Country) 
VALUES      ('IWq8RFxFWUOeYOfi2bZgwg','IT_Solutions Ltd','583 Wall Dr. Gwynn Oak, MD 21207','USA'),
            ('flaI5ThsXk2VfVKLyfacSA','Admin_Solutions Ltd','312 Forest Avenue, BF 923','USA'),
            ('-6c7C5G0nU6Gr5ljbYZpTg','Software Development Limited','New Delhi','India')

--tblEmployees
INSERT INTO tblEmployees 
            (Id,Name,Age,Position,CompanyId,Salary) 
VALUES      ('urZAjXzGXUC3YMZbxtOhiw','Sam Raiden',26,'Software developer','IWq8RFxFWUOeYOfi2bZgwg',10000.00),
            ('TukZuZUp80axU3b804H81g','Kane Miller',35,'Administrator','flaI5ThsXk2VfVKLyfacSA',5000.00),
            ('-z81Yh_rvrUGaig1ij2_WX','Jana McLeaf',30,'Software developer','IWq8RFxFWUOeYOfi2bZgwg',22000.00)

--tblRoles
INSERT INTO tblRoles
            (Id,Name)
VALUES      ('3YMZbxtOhiwTukZuZUp80a','Admin'),
            ('Gaig1ij2_WX-z81Yh_rvrU','User'),
            ('ij2_WX-z81YhGaig1_rvrU','Visitor')

--tblPermissions
INSERT INTO tblPermissions
            (Id,Name)
VALUES      ('YOfi2bZgwgIWq8RFxFWUOe','Read'),
            ('flaI5ThsXk2VfVKLyfacSA','List'),
            ('-6c7C5G0nU6Gr5ljbYZpTg','Write'),
            ('bZgwgIWq8RFxYOfi2FWUOe','Modify'),
            ('k2JQEVUgtxAwwaTAhd623d','Delete')

--tblRolePermissions
INSERT INTO tblRolePermissions
            (RoleId,PermissionId)
VALUES      ('3YMZbxtOhiwTukZuZUp80a','YOfi2bZgwgIWq8RFxFWUOe'),('3YMZbxtOhiwTukZuZUp80a','flaI5ThsXk2VfVKLyfacSA'),
            ('3YMZbxtOhiwTukZuZUp80a','-6c7C5G0nU6Gr5ljbYZpTg'),('3YMZbxtOhiwTukZuZUp80a','bZgwgIWq8RFxYOfi2FWUOe'),('3YMZbxtOhiwTukZuZUp80a','k2JQEVUgtxAwwaTAhd623d'),
            ('Gaig1ij2_WX-z81Yh_rvrU','YOfi2bZgwgIWq8RFxFWUOe'),('Gaig1ij2_WX-z81Yh_rvrU','flaI5ThsXk2VfVKLyfacSA'),('Gaig1ij2_WX-z81Yh_rvrU','bZgwgIWq8RFxYOfi2FWUOe'),
            ('ij2_WX-z81YhGaig1_rvrU','YOfi2bZgwgIWq8RFxFWUOe')

--tblUserRoles
--INSERT INTO tblUserRoles
--            (UserId,RoleId)
--VALUES      ('lEpc0CrNlUGs_fHkeajO_g','ij2_WX-z81YhGaig1_rvrU')