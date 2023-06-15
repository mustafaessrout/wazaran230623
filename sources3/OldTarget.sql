alter table tmst_PriorityProductMails add PriorityFileName nvarchar(200) null;
go
alter table tmst_PriorityProductMails add ProductName nvarchar(200) null;
go
alter table tmst_PriorityProductMails add MethodName nvarchar(200) null;
go

alter table tmst_TargetPeriorityItems add isPriority bit null;
go
update tmst_TargetPeriorityItems set isPriority = 1
go
update tmst_TargetPeriorityItems set isPriority = 0 where prod_cds='11500'

ALTER procedure [dbo].[sp_PriorityProductMails_inst]
(
@emaildate date = null,
@email nvarchar(200) = null,
@salesmanType varchar(50) = null,
@PriorityFileName nvarchar(200) = null,
@ProductName nvarchar(200) = null,
@MethodName nvarchar(200)
)
as
begin
 insert into tmst_PriorityProductMails values (@emaildate  , @email,@salesmanType, GETDATE(), @PriorityFileName, @ProductName, @MethodName );
end 
go
drop procedure sp_TargetPeriorityItems_get
go

create procedure [dbo].[sp_TargetNonPeriorityItems_get]
as
begin
	select *,
			CASE WHEN EXISTS ((SELECT  prod_nm FROM tmst_product O WHERE O.prod_cd = tmst_TargetPeriorityItems.prod_cds)) 
			then (SELECT  prod_nm FROM tmst_product O WHERE O.prod_cd =tmst_TargetPeriorityItems.prod_cds) 
			ELSE ''
       END AS prod_nm
	 from tmst_TargetPeriorityItems where isPriority = 0
end

go

create procedure [dbo].[sp_TargetPeriorityItems_get]
as
begin
	select *,
			CASE WHEN EXISTS ((SELECT  prod_nm FROM tmst_product O WHERE O.prod_cd = tmst_TargetPeriorityItems.prod_cds)) 
			then (SELECT  prod_nm FROM tmst_product O WHERE O.prod_cd =tmst_TargetPeriorityItems.prod_cds) 
			ELSE ''
       END AS prod_nm
	 from tmst_TargetPeriorityItems where isPriority = 1
end
