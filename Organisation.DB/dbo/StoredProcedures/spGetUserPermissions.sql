CREATE PROCEDURE [dbo].[spGetUserPermissions]
	@userId VARCHAR(22)
AS
BEGIN
	SELECT DISTINCT p.* FROM tblPermissions as p
	INNER JOIN tblRolePermissions as rp
	ON p.Id= rp.PermissionId
	INNER JOIN tblRoles as r
	ON r.Id = rp.RoleId
	INNER JOIN tblUserRoles as ur
	ON r.Id=ur.RoleId
	INNER JOIN tblUserDetails as u
	ON ur.UserId=u.Id
	WHERE u.Id=@userId and r.IsDeleted=0 and u.IsDeleted=0 and p.IsDeleted=0
END
